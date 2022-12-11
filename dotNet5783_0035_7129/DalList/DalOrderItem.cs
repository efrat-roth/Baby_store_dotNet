using System.Reflection.Metadata.Ecma335;
using DalApi;
using DO;
using static Dal.DataSource;


namespace Dal;

internal class DalOrderItem:IOrderItem
{
    /// <summary>
    /// Adding new IOrderItem to the database
    /// </summary>
    /// <param name="oi"></param>IOrderItem variable
    /// <returns></returns>ID of the new orderItem
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem? oi)
    {
        foreach (OrderItem? oI in orderItems)
        {
            if (oi?.ID == oI?.ID)
            {
                throw new IdAlreadyExistException();
            };
        }
        int y = oi?.ID ?? throw new InvalidVariableException();
        orderItems.Add(oi);

        return y;
    }
    /// <summary>
    /// Returns IOrderItem by its ID
    /// </summary>
    /// <param name="id"></param>integer-ID of the IOrderItem
    /// <returns></returns>IOrderItem
    public OrderItem PrintByID(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        foreach (OrderItem? oI in orderItems)
        {
            if (id == oI?.ID)
            {
                return oI??throw new InvalidVariableException();
            }
        };
        throw new IdDoesNotExistException();
    }
    /// <summary>
    /// Return a specific orderItem that matches the condition.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>OrderItem?
    public OrderItem? PrintByCondition(Func<OrderItem?, bool>? func)
    {
        func =func??throw new InvalidVariableException();
        OrderItem? o = orderItems.First<OrderItem?>(i => func(i ));
        return o;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>Returns the all database of IOrderItem
    public IEnumerable<OrderItem?> PrintAll(Func<OrderItem?, bool>? func = null)
    {
        if (func == null)
        {
            return orderItems;
        }

        IEnumerable<OrderItem?> o = orderItems.Where(i => func(i)).ToList<OrderItem?>();
        return o;
    }
    /// <summary>
    /// Delete an orderItem by its ID
    /// </summary>
    /// <param name="id"></param>Integer-ID of the orderItem
    /// <returns></returns>True if the orderItem in the database, else returns false
    public bool Delete(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        foreach (OrderItem? oI in orderItems )
        {
            if (oI?.ID==id)
            {
                orderItems.Remove(oI);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Update the details of an orderItem
    /// </summary>
    /// <param name="oi"></param>IOrderItem variable
    /// <returns></returns>True if the id is in the database, else returns false
    public bool Update(OrderItem? oi)
    {
        foreach (OrderItem? o in orderItems)
        {
            if (o?.ID == oi?.ID)
            {
                orderItems.Remove(o);
                orderItems.Add(oi);
                return true;
            }
        };
        return false;
    }
    

}
