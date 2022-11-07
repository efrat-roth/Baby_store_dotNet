using System.Data;
using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// Adding an Order
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>The ID of the new order
    public int Add(Order p)
    {
        for (int i = 0; i < Config.nextEmptyOrder; i++)
        {
            if (p.ID == orders[i].ID)
            {
                throw new Exception("The ID is in the database already");
            }
        }
        orders[Config.nextEmptyOrder] = p;
        ++Config.nextEmptyOrder;
        return orders[Config.nextEmptyOrder].ID;
    }
    /// <summary>
    /// Return order by its ID
    /// </summary>
    /// <param name="id"></param>integer
    /// <returns></returns>Order
    public Order PrintById(int id)
    {
        for (int i = 0; i < Config.nextEmptyOrder; i++)
        {
            if (id == orders[i].ID)
            {
                return orders[i];
            }
        }
        throw new Exception("The order is not on the database");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>The database of the all orders
    public IEnumerable<Order> PrintAll()
    {
        if (Config.nextEmptyOrder == 0)
        {
            throw new Exception("There are no orders in the database");
        }
        return orders;
    }
    /// <summary>
    /// Delete a order from the database by its ID
    /// </summary>
    /// <param name="id"></param>ID of the order to delete
    public bool Delete(int id)
    {
        for (int i = 0; i < Config.nextEmptyOrder; i++)
        {
            if (orders[i].ID == id)
            {
                orders[i] = orders[Config.nextEmptyOrder];
                Config.nextEmptyOrder--;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Update details of order
    /// </summary>
    /// <param name="p"></param>Order
    /// <returns></returns> True if the ID in the database, else return false
    public bool Update(Order p)
    {
        for (int i = 0; i < Config.nextEmptyOrder; i++)
        {
            if (p.ID == orders[i].ID)
            {
                orders[i] = p;
                return true;
            }
        }
        return false;
    }
}
