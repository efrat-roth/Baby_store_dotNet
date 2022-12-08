using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Order:BlApi.IOrder
{
    DalApi.IDal _dal = new Dal.DalList();
    /// <summary>
    /// The method returns details of orders
    /// </summary>
    /// <returns></returns>List of OrderForList
    public List<OrderForList?> GetListOfOrders()
    {
        IEnumerable<DO.Order?> orders = _dal.Order.PrintAll()??throw new ListIsEmptyException();
        List<OrderForList?> listOrders = new List<OrderForList?>();
        foreach (DO.Order o in orders)
        {
            IEnumerable<DO.OrderItem?> orderItems1=new List<DO.OrderItem?>();
            try { orderItems1 = _dal.OrderItem.PrintAll(oi=>oi?.ID==o.ID); }
            catch(Exception inner)
            {
                throw new FailedGet(inner);
            }
            OrderForList OrderList = new OrderForList
            {
                ID = o.ID,
                CustomerName = o.CustomerName,
                AmountOfItems = 0,
                TotalPrice = 0
            };

            if (o.ArrivedDate < DateTime.Now)
            {
                OrderList.Status = OrderStatus.ArrivedOrder;
            }
            if (o.DeliveredDate < DateTime.Now)
            {
                OrderList.Status = OrderStatus.DeliveredOrder;
            }
            if (o.OrderDate < DateTime.Now)
            {
                OrderList.Status = OrderStatus.ConfirmedOrder;
            }
            foreach (DO.OrderItem OI in orderItems1)
            {
                OrderList.AmountOfItems+=1;
                OrderList.TotalPrice += OI.Price;
            }
            listOrders.Add(OrderList);
        }
        return listOrders;      
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
        try { order1 = _dal.Order.PrintByID(ID); }//asked order
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
            foreach (DO.OrderItem OI in orderItems)//resets the orderItems by the orderItems of the order in data layer
            {
            try
            {
                OrderItem OItem = new OrderItem
                {
                    ID = OI.ID,
                    Name = _dal.Product.PrintByID(OI.ProductID).Name,
                    ProductID = OI.ProductID,
                    Price = OI.Price,
                    Amount = OI.Amount,
                    TotalPrice = OI.Price * OI.Amount
                };
                logicOrder.Items.Add(OItem);
                logicOrder.TotalPrice += OItem.TotalPrice;
            }
            catch(Exception inner) { throw new FailedGet(inner); }
            }
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
        try { CheckOrder = _dal.Order.PrintByID(IDOrder); }
        catch(Exception inner) { throw new FailedGet(inner); }
            if (CheckOrder.DeliveredDate <= DateTime.Now)
            {
                throw new CanNotDOActionException();
            }
            CheckOrder.DeliveredDate=DateTime.Now;
        try { _dal.Order.Update(t: CheckOrder); }
        catch (Exception inner) { throw new FailedUpdate(inner); }
        IEnumerable<DO.OrderItem?> items1=new List<DO.OrderItem?>();
        try { items1 = _dal.OrderItem.PrintAll(oi=>oi?.ID==IDOrder); }
        catch(Exception inner) { throw new FailedGet(inner); }
        BO.Order ReturnOrder = new BO.Order
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
            foreach (DO.OrderItem item in items1)//Adding the relevant OrderItem to the items field of order
            {
                if (item.OrderID == IDOrder)
                {
                try
                {
                    BO.OrderItem orderItem = new BO.OrderItem
                    {
                        ID = item.ID,
                        Name = _dal.Product.PrintByID(item.ProductID).Name,
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = item.Amount,
                        TotalPrice = item.Price * item.Amount,
                    };
                    ReturnOrder.Items.Add(orderItem);

                }
                catch (Exception inner) { throw new FailedGet(inner); }
                }

            }
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
        try { CheckOrder = _dal.Order.PrintByID(IDOrder); }
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
            foreach (DO.OrderItem item in items1)//Adding the relevant OrderItem to the items field of order
            {
                if (item.ID == IDOrder)
                {
                try
                {
                    BO.OrderItem orderItem = new BO.OrderItem
                    {
                        ID = item.ID,
                        Name = _dal.Product.PrintByID(item.ProductID).Name,
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = item.Amount,
                        TotalPrice = item.Price * item.Amount,
                    };
                    ReturnOrder.Items.Add(orderItem);
                }
                catch (Exception inner){throw new FailedGet(inner); }
                }

            }
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
        try { CheckOrder = _dal.Order.PrintByID(IDOrder); }
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
        try { _dal.Order.PrintByID(IDOrder); }
        catch(Exception inner)
        {
            throw new FailedGet(inner);
        }
        if (_dal.Order.PrintByID(IDOrder).DeliveredDate <= DateTime.Now)
            throw new CanNotDOActionException();
        BO.Order wantedOrder = GetDetailsOrderManager(IDOrder);
            foreach (OrderItem? orderItem in wantedOrder.Items!)
            {
                if (orderItem?.ProductID == IDProduct)
                {
                    wantedOrder.TotalPrice -= orderItem.TotalPrice;//for calculate the new total price of the order
                    orderItem.Amount = newAmount;
                    orderItem.TotalPrice = newAmount * orderItem.Price;
                    wantedOrder.TotalPrice += orderItem.TotalPrice;//for calculate the new total price of the order
                }
            }
            return wantedOrder;      
    }
}
