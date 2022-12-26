using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Order:BlApi.IOrder
{
    DalApi.IDal? _dal = DalApi.Factory.Get();
    /// <summary>
    /// The method returns details of orders
    /// </summary>
    /// <returns></returns>List of OrderForList
    public List<OrderForList?> GetListOfOrders()
    {
        IEnumerable<DO.Order?> orders = _dal?.Order.PrintAll()??throw new ObgectNullableException();
        IEnumerable<OrderForList?> ordersOrderedReturn = from o in orders
                                                             /*create a new order to return of ordered orders for each group of orderItems */
                                                         where o?.OrderDate < DateTime.Now
                                                         let orderReturn = new OrderForList()
                                                         {
                                                             ID = o?.ID ?? throw new ObgectNullableException(),
                                                             CustomerName = o?.CustomerName ?? throw new ObgectNullableException(),
                                                             Status=OrderStatus.ConfirmedOrder,
                                                             AmountOfItems = (from oi in _dal?.OrderItem.PrintAll() ?? throw new ObgectNullableException()
                                                                              where oi?.OrderID == o?.ID
                                                                              select oi).Count(),//sum the orderItems of order
                                                             TotalPrice= (from oi in _dal?.OrderItem.PrintAll() ?? throw new ObgectNullableException()
                                                                          where oi?.OrderID == o?.ID
                                                                          select oi).Sum(oi=>oi?.Price)??throw new ObgectNullableException()//sum the prices of all orderitems in the order

                                                         }
                                                         select orderReturn;
        IEnumerable<OrderForList?> ordersDeliveredReturn = from o in orders
                                                               /*create a new order to return of ordered orders for each group of orderItems */
                                                           where o?.DeliveredDate < DateTime.Now
                                                           let orderReturn = new OrderForList()
                                                           {
                                                               ID = o?.ID ?? throw new ObgectNullableException(),
                                                               CustomerName = o?.CustomerName ?? throw new ObgectNullableException(),
                                                               Status=OrderStatus.DeliveredOrder,
                                                               AmountOfItems = (from oi in _dal?.OrderItem.PrintAll() ?? throw new ObgectNullableException()
                                                                                where oi?.OrderID == o?.ID
                                                                                select oi).Count(),//sum the orderItems of order
                                                               TotalPrice = (from oi in _dal?.OrderItem.PrintAll() ?? throw new ObgectNullableException()
                                                                             where oi?.OrderID == o?.ID
                                                                             select oi).Sum(oi => oi?.Price) ?? throw new ObgectNullableException()//sum the prices of all orderitems in the order

                                                           }
                                                           select orderReturn;
        IEnumerable<OrderForList?> ordersArrivedReturn = from o in orders
                                                             /*create a new order to return of ordered orders for each group of orderItems */
                                                         where o?.ArrivedDate < DateTime.Now
                                                         let orderReturn = new OrderForList()
                                                         {
                                                             ID = o?.ID ?? throw new ObgectNullableException(),
                                                             CustomerName = o?.CustomerName ?? throw new ObgectNullableException(),
                                                             Status = OrderStatus.ArrivedOrder,
                                                             AmountOfItems = (from oi in _dal?.OrderItem.PrintAll() ?? throw new ObgectNullableException()
                                                                              where oi?.OrderID == o?.ID
                                                                              select oi).Count(),//sum the orderItems of order
                                                             TotalPrice = (from oi in _dal?.OrderItem.PrintAll() ?? throw new ObgectNullableException()
                                                                           where oi?.OrderID == o?.ID
                                                                           select oi).Sum(oi => oi?.Price) ?? throw new ObgectNullableException()//sum the prices of all orderitems in the order

                                                         }
                                                         select orderReturn;

        return ordersOrderedReturn.Union(ordersArrivedReturn.Union(ordersDeliveredReturn)).ToList();    
    //return the union of the all groups
    }

    /// <summary>
    /// The method gets details of order for manager
    /// </summary>
    /// <param name="ID"></param>ID of order
    /// <returns></returns>Order
    /// <exception cref="Exception"></exception>ID not exist
    public BO.Order GetDetailsOrderManager(int ID)
    {
        DO.Order order1=new DO.Order();
        try { order1 = _dal?.Order.PrintByID(ID) ?? throw new ObgectNullableException(); }//asked order
        catch(Exception inner) { throw new FailedGet(inner); }
        IEnumerable<DO.OrderItem?> orderItems=new List<DO.OrderItem?>();
        try { orderItems = _dal.OrderItem.PrintAll(oi=>oi?.ID==ID); }//List of orderItems of the order
        catch(Exception inner) { throw new FailedGet(inner); }
        BO.Order logicOrder = new BO.Order
        {
            ID = order1.ID,
            CustomerName = order1.CustomerName,
            CustomerEmail = order1.CustomerEmail,
            CustomerAdress = order1.CustomerAdress,
            OrderDate = order1.OrderDate,
            ShipDate = order1.DeliveredDate,
            DeliveryDate = order1.ArrivedDate,
            Items=new List<OrderItem?>()
        };//Resets the field of item to return
        if (order1.ArrivedDate < DateTime.Today)
        {
            logicOrder.Status = OrderStatus.ArrivedOrder;
        }
        if (order1.DeliveredDate < DateTime.Today)
        {
            logicOrder.Status = OrderStatus.DeliveredOrder;
        }
        if (order1.OrderDate < DateTime.Today)
        {
            logicOrder.Status = OrderStatus.ConfirmedOrder;
        }
        IEnumerable<OrderItem?> orderItems1;
        try
        {
            orderItems1 = from OI in orderItems//For each orderItem convert to BO from DO
                          let OItem = new OrderItem()
                          {
                              ID = OI?.ID ?? throw new ObgectNullableException(),
                              Name = _dal.Product.PrintByID(OI?.ProductID ?? throw new ObgectNullableException()).Name,
                              ProductID = OI?.ProductID ?? throw new ObgectNullableException(),
                              Price = OI?.Price ?? throw new ObgectNullableException(),
                              Amount = OI?.Amount ?? throw new ObgectNullableException(),
                              TotalPrice = OI?.Price * OI?.Amount ?? throw new ObgectNullableException()
                          }
                          select OItem;
        }
        catch (Exception inner) { throw new FailedGet(inner); }
        logicOrder.Items = orderItems1.ToList();//put the all orderItems in the item field of logicOrder
        logicOrder.TotalPrice=logicOrder.Items.Sum(o => o?.TotalPrice??throw new ObgectNullableException());
        //Sum the all price of all orderItems
        return logicOrder;     
    }


    /// <summary>
    /// The method gets details of order for customer
    /// </summary>
    /// <param name="ID"></param>ID of order
    /// <returns></returns>Order
    /// <exception cref="Exception"></exception>ID not exist
    public BO.Order GetDetailsOrderCustomer(int ID)
    {
        return GetDetailsOrderManager(ID);
    }


    /// <summary>
    /// The method updates the order as delivered order
    /// </summary>
    /// <param name="IDOrder"></param>ID order
    /// <returns></returns>BO.Order
    public BO.Order DeliveredOrder(int IDOrder)
    {

        DO.Order CheckOrder;
        try { CheckOrder = _dal?.Order.PrintByID(IDOrder) ?? throw new ObgectNullableException(); }
        catch(Exception inner) { throw new FailedGet(inner); }
        if (CheckOrder.DeliveredDate <= DateTime.Now)//If the order delivered already
        {
            throw new CanNotDOActionException();
        }
        CheckOrder.DeliveredDate=DateTime.Now;//resets the field to now
        try { _dal.Order.Update(t: CheckOrder); }//update the order in the database
        catch (Exception inner) { throw new FailedUpdate(inner); }
        IEnumerable<DO.OrderItem?> items1=new List<DO.OrderItem?>();
        try { items1 = _dal.OrderItem.PrintAll(oi=>oi?.ID==IDOrder); }//gets the all orderItems of the order
        catch(Exception inner) { throw new FailedGet(inner); }
        BO.Order ReturnOrder = new BO.Order//create BO order
        {
            ID = IDOrder,
            CustomerName = CheckOrder.CustomerName,
            CustomerEmail = CheckOrder.CustomerEmail,
            CustomerAdress = CheckOrder.CustomerAdress,
            OrderDate = CheckOrder.OrderDate,
            DeliveryDate = CheckOrder.ArrivedDate,
            ShipDate = CheckOrder.DeliveredDate,
            Status = OrderStatus.DeliveredOrder,
            Items = new List<OrderItem?>(),
        };
        IEnumerable<OrderItem?> convertedOrderItems;
        try
        {
            convertedOrderItems = from item in items1//convert the all orderItems to BO
                                  let orderItem = new BO.OrderItem
                                  {
                                      ID = item?.ID ?? throw new ObgectNullableException(),
                                      Name = _dal.Product.PrintByID(item?.ProductID ?? throw new ObgectNullableException()).Name,
                                      ProductID = item?.ProductID ?? throw new ObgectNullableException(),
                                      Price = item?.Price ?? throw new ObgectNullableException(),
                                      Amount = item?.Amount ?? throw new ObgectNullableException(),
                                      TotalPrice = item?.Price * item?.Amount ?? throw new ObgectNullableException(),
                                  }
                                  select orderItem;
        }
        catch (Exception inner) { throw new FailedGet(inner); }
        ReturnOrder.Items = convertedOrderItems.ToList();
        return ReturnOrder;          
    }

    /// <summary>
    /// The method updates the order as arrived order
    /// </summary>
    /// <param name="IDOrder"></param>ID order
    /// <returns></returns>BO.Order
    public BO.Order ArrivedOrder(int IDOrder)
    {

        DO.Order CheckOrder=new DO.Order();
        try { CheckOrder = _dal?.Order.PrintByID(IDOrder) ?? throw new ObgectNullableException(); }
        catch (Exception inner) { throw new FailedGet(inner); }
            if (CheckOrder.ArrivedDate <= DateTime.Now)
            {
                 throw new CanNotDOActionException();
            }
            CheckOrder.ArrivedDate= DateTime.Now;

                if (!(_dal.Order.Update(CheckOrder)))
                    throw new CanNotDOActionException();

        IEnumerable<DO.OrderItem?> items1 = _dal.OrderItem.PrintAll(items1 => items1?.ID == IDOrder);
            BO.Order ReturnOrder = new BO.Order
            {
                ID = IDOrder,
                CustomerName = CheckOrder.CustomerName,
                CustomerEmail = CheckOrder.CustomerEmail,
                CustomerAdress = CheckOrder.CustomerAdress,
                OrderDate = CheckOrder.OrderDate,
                ShipDate = CheckOrder.DeliveredDate,
                DeliveryDate = CheckOrder.ArrivedDate,
                Status = OrderStatus.ArrivedOrder,
                Items = new List<OrderItem?>(),
            };
        IEnumerable<OrderItem?> convertedOrderItems;
        try
        {
            convertedOrderItems = from item in items1//convert the all orderItems to BO
                                  let orderItem = new BO.OrderItem
                                  {
                                      ID = item?.ID ?? throw new ObgectNullableException(),
                                      Name = _dal.Product.PrintByID(item?.ProductID ?? throw new ObgectNullableException()).Name,
                                      ProductID = item?.ProductID ?? throw new ObgectNullableException(),
                                      Price = item?.Price ?? throw new ObgectNullableException(),
                                      Amount = item?.Amount ?? throw new ObgectNullableException(),
                                      TotalPrice = item?.Price * item?.Amount ?? throw new ObgectNullableException(),
                                  }
                                  select orderItem;
        }
        catch (Exception inner) { throw new FailedGet(inner); }
        ReturnOrder.Items = convertedOrderItems.ToList();
        return ReturnOrder;        
    }

    /// <summary>
    /// The method track after an order
    /// </summary>
    /// <param name="IDOrder"></param>ID of Order
    /// <returns></returns>OrderTracking
    /// <exception cref="Exception"></exception>The Order was shiped already
    public OrderTracking OrderTracking(int IDOrder)
    {

        DO.Order CheckOrder=new DO.Order();
        try { CheckOrder = _dal?.Order.PrintByID(IDOrder) ?? throw new ObgectNullableException(); }
        catch (Exception inner){throw new FailedGet(inner); }
            List<NodeDateStatus> ListDateStatus1 = new List<NodeDateStatus>();
            OrderStatus status1 = new OrderStatus();
            if (CheckOrder.OrderDate <= DateTime.Now)
            {
                NodeDateStatus newNode = new NodeDateStatus
                {
                    Date = CheckOrder.OrderDate,
                    status = "The order was created"
                };
                ListDateStatus1.Add(newNode);
                status1 = OrderStatus.ConfirmedOrder;
            }
            if (CheckOrder.DeliveredDate <= DateTime.Now)
            {
                NodeDateStatus newNode1 = new NodeDateStatus
                {
                    Date = CheckOrder.OrderDate,
                    status = "The order was delivered"
                };
                ListDateStatus1.Add(newNode1);
                status1 = OrderStatus.DeliveredOrder;
            }
            if (CheckOrder.ArrivedDate <= DateTime.Now)
            {
                NodeDateStatus newNode2 = new NodeDateStatus
                {
                    Date = CheckOrder.OrderDate,
                    status = "The order was arrived"
                };
                ListDateStatus1.Add(newNode2);
                status1 = OrderStatus.ArrivedOrder;
            }
            OrderTracking NewOrderTracking = new OrderTracking
            {
                ID = IDOrder,
                Status = status1,
                ListDateStatus = ListDateStatus1
            };
            return NewOrderTracking;
    }

    
    /// <summary>
    /// The method update the amount of product in exist order
    /// </summary>
    /// <param name="IDOrder"></param>ID of Order
    /// <param name="IDProduct"></param>ID of product
    /// <param name="newAmount"></param>New amont of product
    /// <returns></returns>BO.Order with the new amount
    /// <exception cref="Exception"></exception>The ID is less than zero / The order was shiped already
    public BO.Order UpdateOrder(int IDOrder,int IDProduct, int  newAmount)
    {
        if (IDOrder < 0)
            throw  new BO.InvalidVariableException();
        if (IDProduct < 0)
            throw new BO.InvalidVariableException();
        if (newAmount < 0)
            throw  new BO.InvalidVariableException();
        try { _dal?.Order.PrintByID(IDOrder); }
        catch(Exception inner)
        {
            throw new FailedGet(inner);
        }
        if (_dal?.Order.PrintByID(IDOrder).DeliveredDate <= DateTime.Now)
            throw new CanNotDOActionException();
        BO.Order? wantedOrder = GetDetailsOrderManager(IDOrder);
        BO.OrderItem? oi = wantedOrder?.Items?.FirstOrDefault(oi => oi?.ProductID == IDProduct);
        wantedOrder!.TotalPrice -= oi!.TotalPrice;//for calculate the new total price of the order
        oi.Amount = newAmount;
        oi.TotalPrice = newAmount * oi.Price;
        wantedOrder.TotalPrice += oi.TotalPrice;//for calculate the new total price of the order
        return wantedOrder;    
    }
}
