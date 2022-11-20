using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using Dal;
using DalApi;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// The method returns details of orders
    /// </summary>
    /// <returns></returns>List of OrderForList
    public List<OrderForList> GetListOfOrders();
    /// <summary>
    /// The method gets details of order for manager
    /// </summary>
    /// <param name="ID"></param>ID of order
    /// <returns></returns>Order
    /// <exception cref="Exception"></exception>ID not exist
    public BO.Order GetDetailsOrderManager(int ID);
    /// <summary>
    /// The method gets details of order for customer
    /// </summary>
    /// <param name="ID"></param>ID of order
    /// <returns></returns>Order
    /// <exception cref="Exception"></exception>ID not exist
    public BO.Order GetDetailsOrderCustomer(int ID);
    /// <summary>
    /// The method updates the order as delivered order
    /// </summary>
    /// <param name="IDOrder"></param>ID order
    /// <returns></returns>BO.Order
    public BO.Order DeliveredOrder(int IDOrder);
    /// <summary>
    /// The method updates the order as arrived order
    /// </summary>
    /// <param name="IDOrder"></param>ID order
    /// <returns></returns>BO.Order
    public BO.Order ArrivedOrder(int IDOrder);
    /// <summary>
    /// The method track after an order
    /// </summary>
    /// <param name="IDOrder"></param>ID of Order
    /// <returns></returns>OrderTracking
    /// <exception cref="Exception"></exception>The Order was shiped already
    public OrderTracking OrderTracking(int IDOrder);
}
