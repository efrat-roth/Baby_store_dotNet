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
    public int Add(OrderItem oi)
    {
        foreach (OrderItem oI in orderItems)
        {
            if (oi.ID == oI.ID)
            {
                throw new Exception("T");
            };
        }
        orderItems.Add(oi);
        return oi.ID;
    }
    /// <summary>
    /// Returns IOrderItem by its ID
    /// </summary>
    /// <param name="id"></param>integer-ID of the IOrderItem
    /// <returns></returns>IOrderItem
    public OrderItem PrintByID(int id)
    {
        foreach (OrderItem oI in orderItems)
        {
            if (id == oI.ID)
            {
                return oI;
            }
        };
        throw new Exception("The ID is not in the database");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>Returns the all database of IOrderItem
    public IEnumerable<OrderItem> PrintAll()
    {
        if(orders.Count()==0)
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
        foreach(OrderItem oI in orderItems )
        {
            if (oI.ID==id)
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
    /// Return IOrderItem by the id of the order and the ID of the IProduct
    /// </summary>
    /// <param name="productID"></param>Integer variable
    /// <param name="orderID"></param>Integer variable
    /// <returns></returns>IOrderItem
    public OrderItem PrintByTwoId(int productID, int orderID)
    {
        foreach(OrderItem oI in orderItems)
        {
            if (oI.OrderID==orderID)
            {
                if (oI.ProductID == productID)
                    return oI;
            }
        }
        throw new Exception("The IOrderItem is not in the database");
    }
    /// <summary>
    /// Return array of orderItem that include the ID
    /// </summary>
    /// <returns></returns>array of oderItem
    public IEnumerable<OrderItem> PrintAllByOrder(int idOrder)
    {
       List<OrderItem> orderItemsByOrder=null;
        for(int i=0;i<orderItems.Count();i++)
        {
            if(orderItems.ElementAt(i).OrderID==idOrder)
            {
                orderItemsByOrder.Add(orderItems.ElementAt(i)); 
            }
        }
        if(orderItemsByOrder.Count<=0)
        {
            throw new Exception("The list is empty");
        }
        return orderItemsByOrder;
    }

}
