using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class OrderItem : IOrderItem
{
    //static XElement? OrderItemRoot;
    static string OrderItemPath = @"OrderItem.xml";


    /// <summary>
    /// Return the all orderItems in the store
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
   
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
    {
        List<DO.OrderItem?> OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath) ?? throw new ListIsEmptyException();
        if (func == null)
        {
            return OrderItems;
        }
        IEnumerable<DO.OrderItem?>? o = OrderItems.Where(i => func(i)).OrderBy(o=>o?.ID);
        return o.ToList();

    }

    /// <summary>
    /// Adding an orderItem to the store
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="IdAlreadyExistException"></exception>
    /// <exception cref="InvalidVariableException"></exception>
    public int Add(DO.OrderItem? orderItem)
    {
        if (orderItem?.ID <= 0|| orderItem?.OrderID <= 0|| orderItem?.ProductID <= 0|| orderItem?.Amount < 0|| orderItem?.Price <= 0)
            throw new InvalidVariableException();
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML( OrderItemPath)??throw new ListIsEmptyException();
        bool exist = OrderItems.Exists(o => o?.ID == orderItem?.ID);
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = orderItem?.ID ?? throw new InvalidVariableException();
        OrderItems.Add(orderItem);
        Tools<DO.OrderItem?>.saveListToXML(OrderItems, OrderItemPath);
        return y;
    }

    /// <summary>
    /// Update details of orderItem
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="IdDoesNotExistException"></exception>
    public bool Update(DO.OrderItem? order)
    {
        if (order?.ID <= 0 || order?.OrderID <= 0 || order?.ProductID <= 0 || order?.Amount < 0 || order?.Price <= 0)
            throw new InvalidVariableException();
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath)??throw new ListIsEmptyException();
        DO.OrderItem? o = OrderItems.FirstOrDefault(order1 => order1?.ID == order?.ID) ?? throw new IdDoesNotExistException(); ;
        OrderItems.Remove(o);
        OrderItems.Add(order);
        Tools<DO.OrderItem?>.saveListToXML(OrderItems, OrderItemPath); return true;
    }

    /// <summary>
    /// Delete an orderItem from the store
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath)??throw new ListIsEmptyException();      
        DO.OrderItem? o = OrderItems.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        OrderItems.Remove(o);
        Tools<DO.OrderItem?>.saveListToXML(OrderItems, OrderItemPath); return true;
    }

    /// <summary>
    /// Return an orderItem by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public DO.OrderItem GetByID(int id)
    {
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath)??throw new ListIsEmptyException();
        if (id < 0)
            throw new InvalidVariableException();
        DO.OrderItem? o = OrderItems.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }

    /// <summary>
    /// Return an orderItem by a condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public DO.OrderItem? GetByCondition(Func<DO.OrderItem?, bool>? func)
    {
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath)??throw new ListIsEmptyException();
        func = func ?? throw new InvalidVariableException();
        DO.OrderItem? o = OrderItems.FirstOrDefault(i => func(i)) ?? throw new IdDoesNotExistException();
        return o;
    }

}
