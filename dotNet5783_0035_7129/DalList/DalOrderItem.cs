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
    public bool Update(ref OrderItem oi)
    {
        Console.WriteLine("Do you want to change the id of order?, enter y for yes and n for no");
        string answer = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter ID order");
            int orderID1 = Console.Read();
            oi.OrderID = orderID1;
        }
        Console.WriteLine("Do you want to change the id of product?, enter y for yes and n for no");
        string answer1 = Console.ReadLine();
        if (answer1 == "y")
        {
            Console.WriteLine("Enter ID product");
            int productID1 = Console.Read();
            oi.ProductID = productID1;
        }
        Console.WriteLine("Do you want to change the price?, enter y for yes and n for no");
        string answer2 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter price");
            double price1 = Console.Read();
            oi.Price = price1;
        }
        Console.WriteLine("Do you want to change the amount?, enter y for yes and n for no");
        string answer3 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter amount");
            int amount = Console.Read();
            oi.Amount = amount;
        }
        return true;
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
