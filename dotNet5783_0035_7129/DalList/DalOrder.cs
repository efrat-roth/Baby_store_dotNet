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
    public bool Update(ref Order  p)
    {

        Console.WriteLine("Do you want to change the customer name?, enter y for yes and n for no");
        string answer = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter customer name");
            string costumerName = Console.ReadLine();
            p.CustomerName = costumerName;
        }
        Console.WriteLine("Do you want to change the email of the costumer ?, enter y for yes and n for no");
        string answer1 = Console.ReadLine();
        if (answer1 == "y")
        {
            Console.WriteLine("Enter email customer");
            string customerEmail = Console.ReadLine();
            p.CustomerEmail = customerEmail;
        }
        Console.WriteLine("Do you want to change the customer adress?, enter y for yes and n for no");
        string answer2 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter customer adress");
            string adress1 = Console.ReadLine();
            p.CustomerAdress = adress1;
        }
        Console.WriteLine("Do you want to change the order date?, enter y for yes and n for no");
        string answer3 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter order date in ######:for day,month,year format");
            string orderDate = Console.ReadLine();
            if (int.Parse(orderDate)/ 10000<1| int.Parse(orderDate) / 1000>30)
            {
                throw new InvalidVariableException();
            }
            if(int.Parse(orderDate) - (int.Parse(orderDate) / 10000*10000)<1| (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) / 100) >12)
            {
                throw new InvalidVariableException();
            }
            if (int.Parse(orderDate) - (int.Parse(orderDate) / 100 * 100)  < 1 )
            {
                throw new InvalidVariableException();
            }
            p.OrderDate = DateTime.Parse(orderDate);
        }
        Console.WriteLine("Do you want to change the ship date?, enter y for yes and n for no");
        string answer4 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter ship date in ######:for day,month,year format");
            string shipDate = Console.ReadLine();
            if (int.Parse(shipDate) / 10000 < 1 | int.Parse(shipDate) / 1000 > 30)
            {
                throw new InvalidVariableException();
            }
            if (int.Parse(shipDate) - (int.Parse(shipDate) / 10000 * 10000) < 1 | (int.Parse(shipDate) - (int.Parse(shipDate) / 10000 * 10000) / 100) > 12)
            {
                throw new InvalidVariableException();
            }
            if (int.Parse(shipDate) - (int.Parse(shipDate) / 100 * 100) < 1)
            {
                throw new InvalidVariableException();
            }
            p.ShipDate = DateTime.Parse(shipDate);
        }
        Console.WriteLine("Do you want to change the delivery date?, enter y for yes and n for no");
        string answer5 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter delivery date in ######:for day,month,year format");
            string deliveryDate = Console.ReadLine();
            if (int.Parse(deliveryDate) / 10000 < 1 | int.Parse(deliveryDate) / 1000 > 30)
            {
                throw new InvalidVariableException();
            }
            if (int.Parse(deliveryDate) - (int.Parse(deliveryDate) / 10000 * 10000) < 1 | (int.Parse(deliveryDate) - (int.Parse(deliveryDate) / 10000 * 10000) / 100) > 12)
            {
                throw new InvalidVariableException();
            }
            if (int.Parse(deliveryDate) - (int.Parse(deliveryDate) / 100 * 100) < 1)
            {
                throw new InvalidVariableException();
            }
            p.DeliveryDate = DateTime.Parse(deliveryDate);
        }
        return true;


    }
}
