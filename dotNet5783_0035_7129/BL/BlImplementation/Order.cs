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
    public List<OrderForList> GetListOfOrders()
    {
            IEnumerable<DO.Order> orders = _dal.Order.PrintAll();
            List<OrderForList> listOrders = new List<OrderForList>();
            foreach (DO.Order o in orders)
            {
            IEnumerable<DO.OrderItem> orderItems1;
            try { orderItems1 = _dal.OrderItem.PrintAllByOrder(o.ID); }
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

                if (o.DeliveryDate < DateTime.Now)
                {
                    OrderList.Status = Enums.OrderStatus.ArrivedOrder;
                }
                if (o.ShipDate < DateTime.Now)
                {
                    OrderList.Status = Enums.OrderStatus.DeliveredOrder;
                }
                if (o.OrderDate < DateTime.Now)
                {
                    OrderList.Status = Enums.OrderStatus.ConfirmedOrder;
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

    DO.Order order1;
        try { order1 = _dal.Order.PrintByID(ID); }//asked order
        catch(Exception inner) { throw new FailedGet(inner); }
        IEnumerable<DO.OrderItem> orderItems;
        try { orderItems = _dal.OrderItem.PrintAllByOrder(ID); }//List of orderItems of the order
        catch(Exception inner) { throw new FailedGet(inner); }
            BO.Order logicOrder = new BO.Order
            {
                ID = order1.ID,
                CustomerName = order1.CustomerName,
                CustomerEmail = order1.CustomerEmail,
                CustomerAdress = order1.CustomerAdress,
                OrderDate = order1.OrderDate,
                ShipDate = order1.ShipDate,
                DeliveryDate = order1.DeliveryDate,
            };//Resets the field of item to return
            if (order1.DeliveryDate < DateTime.Today)
            {
                logicOrder.Status = Enums.OrderStatus.ArrivedOrder;
            }
            if (order1.ShipDate < DateTime.Today)
            {
                logicOrder.Status = Enums.OrderStatus.DeliveredOrder;
            }
            if (order1.OrderDate < DateTime.Today)
            {
                logicOrder.Status = Enums.OrderStatus.ConfirmedOrder;
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
            if (CheckOrder.ShipDate <= DateTime.Now)
            {
                throw new Exception("The order was shiped already");
            }
            CheckOrder.ShipDate=DateTime.Now;
        try { _dal.Order.Update(t: CheckOrder); }
        catch (Exception inner) { throw new FailedUpdate(inner); }
        IEnumerable<DO.OrderItem> items1;
        try { items1 = _dal.OrderItem.PrintAllByOrder(IDOrder); }
        catch(Exception inner) { throw new FailedGet(inner); }
            BO.Order ReturnOrder = new BO.Order
            {
                ID = IDOrder,
                CustomerName = CheckOrder.CustomerName,
                CustomerEmail = CheckOrder.CustomerEmail,
                CustomerAdress = CheckOrder.CustomerAdress,
                OrderDate = CheckOrder.OrderDate,
                DeliveryDate = CheckOrder.DeliveryDate,
                ShipDate = CheckOrder.ShipDate,
                Status = Enums.OrderStatus.DeliveredOrder,
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

        DO.Order CheckOrder;
        try { CheckOrder = _dal.Order.PrintByID(IDOrder); }
        catch (Exception inner) { throw new FailedGet(inner); }
            if (CheckOrder.DeliveryDate <= DateTime.Now)
            {
                throw new Exception("The order was arrived already");
            }
            CheckOrder.DeliveryDate= DateTime.Now;
            try
            {
                if (!(_dal.Order.Update(CheckOrder)))
                    throw new CanNotDOActionException();
            }
            catch (Exception inner){throw new FailedUpdate(inner); }
            IEnumerable<DO.OrderItem> items1 = _dal.OrderItem.PrintAllByOrder(IDOrder);
            BO.Order ReturnOrder = new BO.Order
            {
                ID = IDOrder,
                CustomerName = CheckOrder.CustomerName,
                CustomerEmail = CheckOrder.CustomerEmail,
                CustomerAdress = CheckOrder.CustomerAdress,
                OrderDate = CheckOrder.OrderDate,
                ShipDate = CheckOrder.ShipDate,
                DeliveryDate = CheckOrder.DeliveryDate,
                Status = Enums.OrderStatus.ArrivedOrder
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

        DO.Order CheckOrder;
        try { CheckOrder = _dal.Order.PrintByID(IDOrder); }
        catch (Exception inner){throw new FailedGet(inner); }
            IEnumerable<NodeDateStatus> ListDateStatus1 = new List<NodeDateStatus>();
            Enums.OrderStatus status1 = new Enums.OrderStatus();
            if (CheckOrder.OrderDate <= DateTime.Now)
            {
                NodeDateStatus newNode = new NodeDateStatus
                {
                    Date = CheckOrder.OrderDate,
                    status = "The order was created"
                };
                ListDateStatus1.Append(newNode);
                status1 = Enums.OrderStatus.ConfirmedOrder;
            }
            if (CheckOrder.ShipDate <= DateTime.Now)
            {
                NodeDateStatus newNode1 = new NodeDateStatus
                {
                    Date = CheckOrder.OrderDate,
                    status = "The order was delivered"
                };
                ListDateStatus1.Append(newNode1);
                status1 = Enums.OrderStatus.DeliveredOrder;
            }
            if (CheckOrder.DeliveryDate <= DateTime.Now)
            {
                NodeDateStatus newNode2 = new NodeDateStatus
                {
                    Date = CheckOrder.OrderDate,
                    status = "The order was arrived"
                };
                ListDateStatus1.Append(newNode2);
                status1 = Enums.OrderStatus.ArrivedOrder;
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
            if (_dal.Order.PrintByID(IDOrder).ShipDate <= DateTime.Now)
                throw new Exception("The Order was shiped already");
            BO.Order wantedOrder = GetDetailsOrderManager(IDOrder);
            foreach (OrderItem orderItem in wantedOrder.Items)
            {
                if (orderItem.ProductID == IDProduct)
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
