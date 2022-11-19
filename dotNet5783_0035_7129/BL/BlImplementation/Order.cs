﻿using BlApi;
using BO;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Order:BlApi.IOrder
{
    IDal dalList1 = new DalList();
    /// <summary>
    /// The method returns details of orders
    /// </summary>
    /// <returns></returns>List of OrderForList
    public List<OrderForList> GetListOfOrders()
    {
        IEnumerable<DO.Order> orders = dalList1.IOrder.PrintAll();
        List<OrderForList> listOrders=new List<OrderForList>;
        foreach (DO.Order o in orders)
        {
            IEnumerable<DO.OrderItem>  orderItems = dalList1.IOrderItem.PrintAllByOrder(o.ID);
            OrderForList OrderList = new OrderForList
            {
                ID = o.ID,
                CustomerName = o.CustomerName,
                AmountOfItems = 0,
                TotalPrice = 0
            };
           
            if(o.DeliveryDate<DateTime.Today)
            {
                OrderList.Status = Enums.OrderStatus.ArrivedOrder;
            }
            if (o.ShipDate < DateTime.Today)
            {
                OrderList.Status = Enums.OrderStatus.DeliveredOrder;
            }
            if (o.OrderDate < DateTime.Today)
            {
                OrderList.Status = Enums.OrderStatus.ConfirmedOrder;
            }
            foreach (DO.OrderItem OI in orderItems)
            {
                ++OrderList.AmountOfItems;
                OrderList.TotalPrice += OI.Price;
            }
            listOrders.Add(OrderList);
        }
        return listOrders;       
    }
    /// <summary>
    /// The method gets details of order
    /// </summary>
    /// <param name="ID"></param>ID of order
    /// <returns></returns>Order
    /// <exception cref="Exception"></exception>ID not exist
    public BO.Order GetDetailsOrder(int ID)
    {
        if (ID < 0)
            throw new Exception("The ID is invalid");
        try
        {
            DO.Order order1 = dalList1.IOrder.PrintByID(ID);//asked order
            IEnumerable<DO.OrderItem> orderItems = dalList1.IOrderItem.PrintAllByOrder(ID);//List of orderItems of the order
            BO.Order logicOrder = new BO.Order
            {
                ID = ID,
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
                OrderItem OItem = new OrderItem
                {
                    ID = OI.ID,
                    Name = dalList1.IProduct.PrintByID(OI.ProductID).Name,
                    ProductID = OI.ProductID,
                    Price = OI.Price,
                    Amount = OI.Amount,
                    TotalPrice = OI.Price * OI.Amount
                };
                logicOrder.Items.Add(OItem);
                logicOrder.TotalPrice += OItem.TotalPrice;
            }
            return logicOrder;
        }
        catch(Exception message)
        {
            throw message;
        }
    }
    /// <summary>
    /// The method updates the order as delivered order
    /// </summary>
    /// <param name="IDOrder"></param>ID order
    /// <returns></returns>BO.Order
    public BO.Order DeliveredOrder(int IDOrder)
    {
        try
        {
            DO.Order CheckOrder = dalList1.IOrder.PrintByID(IDOrder);
            if (CheckOrder.ShipDate <= DateTime.Now)
            {
                throw new Exception("The order was shiped already");
            }
            //CheckOrder.ShipDate;////לזכור לעדכן שדה
            IEnumerable<DO.OrderItem> items1 = dalList1.IOrderItem.PrintAllByOrder(IDOrder);
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
                    BO.OrderItem orderItem = new BO.OrderItem
                    {
                        ID = item.ID,
                        Name = dalList1.IProduct.PrintByID(item.ProductID).Name,
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = item.Amount,
                        TotalPrice = item.Price * item.Amount,
                    };
                    ReturnOrder.Items.Add(orderItem);
                }

            }
            return ReturnOrder;
        }
        catch(Exception message)
        {
            throw message;
        }
    }
    /// <summary>
    /// The method updates the order as arrived order
    /// </summary>
    /// <param name="IDOrder"></param>ID order
    /// <returns></returns>BO.Order
    public BO.Order ArrivedOrder(int IDOrder)
    {
        try
        {
            DO.Order CheckOrder = dalList1.IOrder.PrintByID(IDOrder);
            if (CheckOrder.DeliveryDate <= DateTime.Now)
            {
                throw new Exception("The order was shiped already");
            }
            //CheckOrder.ShipDate;////לזכור לעדכן שדה
            IEnumerable<DO.OrderItem> items1 = dalList1.IOrderItem.PrintAllByOrder(IDOrder);
            BO.Order ReturnOrder = new BO.Order
            {
                ID = IDOrder,
                CustomerName = CheckOrder.CustomerName,
                CustomerEmail = CheckOrder.CustomerEmail,
                CustomerAdress = CheckOrder.CustomerAdress,
                OrderDate = CheckOrder.OrderDate,
                DeliveryDate = CheckOrder.DeliveryDate,
                ShipDate = CheckOrder.ShipDate,
                Status = Enums.OrderStatus.ArrivedOrder
            };
            foreach (DO.OrderItem item in items1)//Adding the relevant OrderItem to the items field of order
            {
                if (item.ID == IDOrder)
                {
                    BO.OrderItem orderItem = new BO.OrderItem
                    {
                        ID = item.ID,
                        Name = dalList1.IProduct.PrintByID(item.ProductID).Name,
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = item.Amount,
                        TotalPrice = item.Price * item.Amount,
                    };
                    ReturnOrder.Items.Add(orderItem);
                }

            }
            return ReturnOrder;
        }
        catch (Exception message)
        {
            throw message;
        }
    }
    public OrderTracking OrderR(int IDOrder)
    {
        DO.Order CheckOrder = dalList1.IOrder.PrintByID(IDOrder);
        if (CheckOrder.DeliveryDate <= DateTime.Now)
        {
            throw new Exception("The order was shiped already");
        }
        IEnumerable<NodeDateStatus> ListDateStatus1 = new List<NodeDateStatus>();
        Enums.OrderStatus status1 = new Enums.OrderStatus();
        if (CheckOrder.OrderDate != null)
        {
            NodeDateStatus newNode = new NodeDateStatus
            {
                Date = CheckOrder.OrderDate,
                status = "The order was created"
            };
            ListDateStatus1.Append(newNode);
            status1 = Enums.OrderStatus.ConfirmedOrder;
        }
        if (CheckOrder.ShipDate != null)
        {
            NodeDateStatus newNode1 = new NodeDateStatus
            {
                Date = CheckOrder.OrderDate,
                status = "The order was delivered"
            };
            ListDateStatus1.Append(newNode1);
            status1 = Enums.OrderStatus.DeliveredOrder;
        }
        if (CheckOrder.DeliveryDate != null)
        {
            NodeDateStatus newNode2 = new NodeDateStatus
            {
                Date = CheckOrder.OrderDate,
                status = "The order was arrived"
            };
            ListDateStatus1.Append(newNode2);
            status1 = Enums.OrderStatus.ArrivedOrder;
        }
        OrderTracking orderTracking = new OrderTracking
        {
            ID = IDOrder,
            Status=status1,
            ListDateStatus=ListDateStatus1
        };
    }
    

}