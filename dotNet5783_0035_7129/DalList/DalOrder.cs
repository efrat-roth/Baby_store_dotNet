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
    public int Add(Order p)
    {
        foreach (Order o in orders)
        {
            if (p.ID == o.ID)
            {
                throw new DalApi.IdAlreadyExistException();
            }
        };

        orders.Add(p);
        return p.ID;
    }
    /// <summary>
    /// Return order by its ID
    /// </summary>
    /// <param name="id"></param>integer
    /// <returns></returns>IOrder
    public Order PrintByID(int id)
    {
        foreach (Order o in orders) 
        { 
            if (id == o.ID)
            {
                return o;
            }
        };   
        throw new IdDoesNotExistException();
    }
    /// <summary>
    /// Print The all orders
    /// </summary>
    /// <returns></returns>The database of the all orders
    public IEnumerable<Order> PrintAll()
    {
        if (orders.Count() == 0)
        {
            throw new ListIsEmptyException();
        }
        return orders;
    }
    /// <summary>
    /// Delete a order from the database by its ID
    /// </summary>
    /// <param name="id"></param>ID of the order to delete
    public bool Delete(int id)
    {
        foreach (Order o in orders)
        {
            if(id == o.ID)
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
    public bool Update(Order  p)
    {

        
        return true;


    }
}
