using DO;
using static Dal.DataSource;

using System.Runtime.CompilerServices;
using DO;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// Adding new OrderItem to the database
    /// </summary>
    /// <param name="oi"></param>OrderItem variable
    /// <returns></returns>ID of the new orderItem
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem oi)
    {
        for(int i=0;i<Config.nextEmptyOrderItem;i++)
        {
            if(oi.ID==orderItems[i].ID)
            {
                throw new Exception("The ID is in the database already");
            }
        }
        orderItems[orderItems.Length-1] = oi;
        return orderItems[orderItems.Length-1].ID;
    }
    /// <summary>
    /// Returns OrderItem by its ID
    /// </summary>
    /// <param name="id"></param>integer-ID of the OrderItem
    /// <returns></returns>OrderItem
    public OrderItem PrintByID(int id)
    {
        for (int i = 0; i < Config.nextEmptyOrderItem; i++)
        {
            if(orderItems[i].ID==id)
            {
                 return orderItems[i];
            }
        }
        throw new Exception("The ID is not in the database");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>Returns the all database of OrderItem
    public IEnumerable<OrderItem> PrintAll()
    {
        if(Config.nextEmptyOrderItem==0)
        {
            throw new Exception("The database is empty");
        }
        return orderItems;
    }
    /// <summary>
    /// Delete an orderItem by its ID
    /// </summary>
    /// <param name="id"></param>Integer-ID of the orderItem
    /// <returns></returns>True if the orderItem in the database, else returns false
    public bool Delete(int id)
    {
        for(int i=0;i<orderItems.Length-1;i++)
        {
            if (orderItems[i].ID==id)
            {
                orderItems[i] = orderItems[Config.nextEmptyOrderItem - 1];
                --Config.nextEmptyOrderItem;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Update the details of an orderItem
    /// </summary>
    /// <param name="oi"></param>OrderItem variable
    /// <returns></returns>True if the id is in the database, else returns false
    public bool Update(OrderItem oi)
    {
        for(int i=0;i<orderItems.Length-1;i++)
        {
            if (orderItems[i].ID==oi.ID)
            {
                orderItems[i] = oi;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Return OrderItem by the id of the order and the ID of the Product
    /// </summary>
    /// <param name="productID"></param>Integer variable
    /// <param name="orderID"></param>Integer variable
    /// <returns></returns>OrderItem
    public OrderItem PrintByTwoId(int productID, int orderID)
    {
        for(int i=0;i<Config.nextEmptyOrderItem;i++)
        {
            if (orderItems[i].OrderID==orderID)
            {
                if (orderItems[i].ProductID == productID)
                    return orderItems[i];
            }
        }
        throw new Exception("The OrderItem is not in the database");
    }
    /// <summary>
    /// Return array of orderItem that include the ID
    /// </summary>
    /// <returns></returns>array of oderItem
    public IEnumerable<OrderItem> PrintAllByOrder(int idOrder)
    {
       OrderItem[] orderItemsByOrder = new OrderItem[orderItems.Length];
        for(int i=0;i<orderItems.Length-1;i++)
        {
            if(orderItems[i].OrderID==idOrder)
            {
                orderItemsByOrder[orderItemsByOrder.Length-1] = orderItems[i]; 
            }
        }
        return orderItemsByOrder;
    }

}
