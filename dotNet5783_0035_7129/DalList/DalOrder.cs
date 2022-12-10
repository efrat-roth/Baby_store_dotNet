using System.Data;
using DO;
using static Dal.DataSource;
using DalApi;


namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// Adding an IOrder
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>The ID of the new order
    public int Add(Order? p)
    {
        
        foreach (Order? o in orders)
        {
            if (p?.ID == o?.ID)
            {
                throw new DO.IdAlreadyExistException();
            }
        };
        int y = p?.ID ?? throw new InvalidVariableException();
        orders.Add(p);
        return y;
    }
    /// <summary>
    /// Return order by its ID
    /// </summary>
    /// <param name="id"></param>integer
    /// <returns></returns>IOrder
    public Order PrintByID(int id)
    {
        if(id<0)
            throw new InvalidVariableException();
        foreach (Order? o in orders) 
        { 
            if (id == o?.ID)
            {
                return o??throw new InvalidVariableException();
            }
        };   
        throw new IdDoesNotExistException();
    }
    /// <summary>
    /// Return a specific order that matches the condition.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>Order?
    public Order? PrintByCondition(Func<Order?, bool>? func)
    {
        func = func ?? throw new InvalidVariableException();
        Order? o = orders.First<Order?>(i => func(i));
        return o;
    }

    /// <summary>
    /// Print The all orders
    /// </summary>
    /// <returns></returns>The database of the all orders
    public IEnumerable<Order?> PrintAll(Func<Order?, bool>? func = null)
    {
        if(func==null)
        {
            return orders;
        }
       
        IEnumerable<Order?> o = orders.Where(i => func( i)).ToList<Order?>();
        return o;
    }
    /// <summary>
    /// Delete a order from the database by its ID
    /// </summary>
    /// <param name="id"></param>ID of the order to delete
    public bool Delete(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        foreach (Order? o in orders)
        {
            if(id == o?.ID)
            {
                orders.Remove(o);
                return true;
            }
        };
        return false;
    }
    /// <summary>
    /// Update details of order
    /// </summary>
    /// <param name="p"></param>IOrder
    /// <returns></returns> True if the ID in the database, else return false
    public bool Update(Order? o)
    {
        foreach (Order? order in orders)
        {
            if (order?.ID == o?.ID)
            {
                orders.Remove(order);
                orders.Add(o);
                return true;
            }
        };
        return false;
    }
}
