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
    public int Add(Order? o)
    {

        bool exist = orders.Exists(order => order?.ID == o?.ID);
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = o?.ID ?? throw new InvalidVariableException();
        orders.Add(o);

        return y; 
    }
    /// <summary>
    /// Return order by its ID
    /// </summary>
    /// <param name="id"></param>integer
    /// <returns></returns>IOrder
    public Order GetByID(int id)
    {
        if(id<0)
            throw new InvalidVariableException();
        Order? o = orders.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }
    /// <summary>
    /// Return a specific order that matches the condition.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>Order?
    public Order? GetByCondition(Func<Order?, bool>? func)
    {
        func = func ?? throw new InvalidVariableException();
        Order? o = orders.FirstOrDefault(i => func(i))??throw new IdDoesNotExistException();
        return o;
    }

    /// <summary>
    /// Print The all orders
    /// </summary>
    /// <returns></returns>The database of the all orders
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
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
        Order? o = orders.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        return true;
    }
    /// <summary>
    /// Update  details of order
    /// </summary>
    /// <param name="p"></param>IOrder
    /// <returns></returns> True if the ID in the database, else return false
    public bool Update(Order? o)
    {
        Order? order = orders.FirstOrDefault(order => order?.ID == o?.ID) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(order);
        orders.Add(o);
        return true;
    }
}
