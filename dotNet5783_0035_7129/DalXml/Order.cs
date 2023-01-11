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

internal class Order : IOrder
{
    static string OrderPath = @"Order.xml";
   
    
    /// <summary>
    /// Gets the all orders
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        //LoadData();
        List<DO.Order?> orders=Tools<DO.Order?>.loadListFromXML(OrderPath);
        if (func == null)
        {
            return orders;
        }
        IEnumerable<DO.Order?> o = orders.Where(i => func(i));
        return o.ToList();
    }
   
    /// <summary>
    /// Adding order to lit
    /// </summary>
    /// <param name="order"></param>DO.Order
    /// <returns></returns>
    /// <exception cref="IdAlreadyExistException"></exception>
    /// <exception cref="InvalidVariableException"></exception>
    public int Add(DO.Order? order)
    { 
        List<DO.Order?> orders = Tools<DO.Order?>.loadListFromXML(OrderPath);
        bool exist = orders.Exists(o => o?.ID == order?.ID);
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = order?.ID ?? throw new InvalidVariableException();
        orders.Add(order);
        Tools<DO.Order?>.saveListToXML(orders, OrderPath);
        return y;
    }
    
    /// <summary>
    /// Update details of order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="IdDoesNotExistException"></exception>
    public bool Update(DO.Order? order)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath);
        DO.Order? o = orders.FirstOrDefault(order1 => order1?.ID == order?.ID) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        orders.Add(order);
        Tools<DO.Order?>.saveListToXML(orders, OrderPath);
        return true;
    }
    
    /// <summary>
    /// Delete order from the store
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath);
        if (id < 0)
            throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        Tools<DO.Order?>.saveListToXML(orders,OrderPath);
        return true;
    }

    /// <summary>
    /// Return an order by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public DO.Order GetByID(int id)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath);
        if (id < 0)
            throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }

    /// <summary>
    /// Retuen an order by a condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public DO.Order? GetByCondition(Func<DO.Order?, bool>? func)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath);
        func = func ?? throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(i => func(i)) ?? throw new IdDoesNotExistException();
        return o;
    }

}
