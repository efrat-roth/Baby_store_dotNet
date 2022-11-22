using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

using Dal;
using DO;

namespace DalTest
{
    internal class DalTest
    {
        static void Main(string[] args)
        {
            IDal dalList1 = new DalList();
            Order orders = new Order();
            mainActions();
            void manageProduct()
            {

                Product inputProduct()//return product
                {
                    Product p = new Product();
                    Console.WriteLine("Enter the id of product to add");
                    int id1 = Console.Read();
                    p.ID = id1;
                    Console.WriteLine("Enter the name of product to add");
                    string name1 = Console.ReadLine();
                    p.Name = name1;
                    Console.WriteLine("Enter the category of product to add");
                    Enums.Category category1 = (Enums.Category)Console.Read();
                    p.Category = category1;
                    Console.WriteLine("Enter the price of product to add");
                    double price1 = Console.Read();
                    p.Price = price1;
                    Console.WriteLine("Enter amount of products in stock");
                    int inStock1 = Console.Read();
                    p.InStock = inStock1;
                    return p;
                }//input details of new product
                int optionsProduct = 0;


                while (optionsProduct != 6)
                {
                    Console.WriteLine(@"Enter
1 to add product
2 to print product by input id of product
3 to print the all list of the products
4 to update details of product
5 to delete a product
6 to exit the product manager");
                    int.TryParse(Console.ReadLine(), out optionsProduct);
                    while (optionsProduct < 1 | optionsProduct > 6)
                    {
                        Console.WriteLine("Enter valid number");
                        int.TryParse(Console.ReadLine(),out optionsProduct);
                    }
                    switch (optionsProduct)
                    {
                        case 1://Add a product
                            {
                                try
                                {
                                    Product p = inputProduct();
                                    int i = dalList1.IProduct.Add(p);
                                }
                                catch (InvalidVariableException m)
                                {
                                    Console.WriteLine(m);
                                    Console.WriteLine("Enter the variable again");
                                }
                                catch (IdAlreadyExistException m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 2://Print a product
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    Product p = dalList1.IProduct.PrintByID(id);
                                    Console.WriteLine(p);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                            }

                        case 3://Print the all products
                            {
                                try
                                {
                                    IEnumerable<Product> productPrint;
                                    productPrint = dalList1.IProduct.PrintAll();
                                    foreach (Product p in productPrint)
                                    {
                                        Console.WriteLine(p);
                                    }
                                }
                                catch (ListIsEmptyException m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 4://Delete product
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IProduct.Delete(id);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                            }
                        case 5://Update Product
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    Product p = dalList1.IProduct.PrintByID(id);
                                    Console.WriteLine("Do you want to change the name?, enter y for yes and n for no");
                                    string answer = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter name");
                                        string name1 = Console.ReadLine();
                                        p.Name = name1;
                                    }

                                    Console.WriteLine("Do you want to change the category?, enter y for yes and n for no");
                                    string answer1 = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter category");
                                        Enums.Category category1 = (Enums.Category)Console.Read();
                                        p.Category = category1;
                                    }
                                    Console.WriteLine("Do you want to change the price?, enter y for yes and n for no");
                                    string answer2 = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter price");
                                        double price1 = Console.Read();
                                        p.Price = price1;
                                    }
                                    Console.WriteLine("Do you want to change the in stock?, enter y for yes and n for no");
                                    string answer3 = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter in stock?");
                                        int inStock1 = Console.Read();
                                        p.InStock = inStock1;
                                    }
                                    bool result = dalList1.IProduct.Update(p);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (InvalidVariableException m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;

                                }
                                break;
                            }
                        case 6:
                            {
                                mainActions();
                                break;
                            }
                    }


                }
            }//manages the products

            void manageOrder()
            {
                Order inputOrder()//return order
                {
                    Order o = new Order();
                    Console.WriteLine("Enter the id of order to add");
                    int id1 = Console.Read();
                    o.ID = id1;
                    Console.WriteLine("Enter the name of costumer to add");
                    string name1 = Console.ReadLine();
                    o.CustomerName = name1;
                    Console.WriteLine("Enter the email of costumer to add");
                    string email1 = Console.ReadLine();
                    o.CustomerEmail = email1;
                    Console.WriteLine("Enter the adress of costumer to add");
                    string adress1 = Console.ReadLine();
                    o.CustomerAdress = adress1;
                    Console.WriteLine("Enter order date in ######:for day,month,year format");
                    string orderDate = Console.ReadLine();
                    if (int.Parse(orderDate) / 10000 < 1 | int.Parse(orderDate) / 1000 > 30)
                    {
                        throw new InvalidVariableException();
                    }
                    if (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) < 1 | (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) / 100) > 12)
                    {
                        throw new InvalidVariableException();
                    }
                    if (int.Parse(orderDate) - (int.Parse(orderDate) / 100 * 100) < 1)
                    {
                        throw new InvalidVariableException();
                    }
                    o.OrderDate = DateTime.Parse(orderDate);
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
                    o.ShipDate = DateTime.Parse(shipDate);
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
                    o.DeliveryDate = DateTime.Parse(deliveryDate);
                    return o;

                }//input details of new order
                int optionsOrder = 0;



                while (optionsOrder != 6)
                {
                    Console.WriteLine(@"Enter
1 to add an order
2 to print order by input id of order
3 to print the all list of the orders
4 to update details of order
5 to delete an order
6 to exit the program");
                    int.TryParse(Console.ReadLine(),out optionsOrder);
                    while (optionsOrder < 1 | optionsOrder > 6)
                    {
                        Console.WriteLine("Enter valid number");
                        int.TryParse(Console.ReadLine(), out optionsOrder);
                    }
                    switch (optionsOrder)
                    {
                        case 1://Add order
                            {
                                try
                                {
                                    Order order1 = inputOrder();
                                    int i = dalList1.IOrder.Add(order1);
                                    break;
                                }
                                catch (InvalidVariableException message)
                                {
                                    Console.WriteLine(message);
                                }
                                catch (IdAlreadyExistException m)
                                {
                                    Console.WriteLine(m);

                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }

                        case 2://Print order
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    Order p = dalList1.IOrder.PrintByID(id);
                                    Console.WriteLine(value: p);
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 3://Print the all orders
                            {
                                try
                                {
                                    IEnumerable<Order> orderPrint;
                                    orderPrint = dalList1.IOrder.PrintAll();
                                    foreach (Order p in orderPrint)
                                    {
                                        Console.WriteLine(value: p);

                                    }
                                }
                                catch (ListIsEmptyException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 4://Delete the order
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IOrder.Delete(id);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 5://Updates the order
                            {

                                try
                                {
                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    Order p = new Order();
                                    p = dalList1.IOrder.PrintByID(id);
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
                                        if (int.Parse(orderDate) / 10000 < 1 | int.Parse(orderDate) / 1000 > 30)
                                        {
                                            throw new InvalidVariableException();
                                        }
                                        if (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) < 1 | (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) / 100) > 12)
                                        {
                                            throw new InvalidVariableException();
                                        }
                                        if (int.Parse(orderDate) - (int.Parse(orderDate) / 100 * 100) < 1)
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
                                    bool answer12 = dalList1.IOrder.Update(p);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (InvalidVariableException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 6:
                            {
                                mainActions();
                                break;

                            }
                    }

                }
            }//manages the orders

            void manageOrderItem()
            {
                OrderItem inputOrderItem()//return new orderItem
                {
                    OrderItem oi = new OrderItem();
                    Console.WriteLine("Enter the id of orderItem to add");
                    int id1 = Console.Read();
                    oi.ID = id1;
                    Console.WriteLine("Enter the order id to add");
                    int orderId = Console.Read();
                    oi.OrderID = orderId;
                    Console.WriteLine("Enter the product id of product to add");
                    int productId = Console.Read();
                    oi.ProductID = productId;
                    Console.WriteLine("Enter the price of product to add");
                    double price1 = Console.Read();
                    oi.Price = price1;
                    Console.WriteLine("Enter amount of products in order");
                    int amount = Console.Read();
                    oi.Amount = amount;
                    return oi;
                }//input details of new product
                int optionsOrderItem = 0;
                Console.WriteLine(@"Enter
1 to add an orderItem
2 to print orderItem by input id of its
3 to print the all list of the orderItem
4 to update details of orderItem
5 to delete an orderItem
6 to print order by two ID
7 to print all orderItem of order
8 to exit the program");
                int.TryParse(Console.ReadLine(), out optionsOrderItem);
                while (optionsOrderItem < 1 | optionsOrderItem > 6)
                {
                    Console.WriteLine(@"Enter valid number");
                    Console.WriteLine("Enter valid number");
                    int.TryParse(Console.ReadLine(), out optionsOrderItem);
                    optionsOrderItem = Console.Read();

                }
                while (optionsOrderItem != 6)
                {
                    switch (optionsOrderItem)
                    {
                        case 1://Add OrderItem
                            {
                                try
                                {
                                    OrderItem p = inputOrderItem();
                                    int i = dalList1.IOrderItem.Add(p);
                                    break;
                                }
                                catch (InvalidVariableException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (IdAlreadyExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 2://Print orderItem
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    OrderItem p = dalList1.IOrderItem.PrintByID(id);
                                    Console.WriteLine(p);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 3://Prints the all orderItems
                            {
                                try
                                {
                                    IEnumerable<OrderItem> orderItemPrint;
                                    orderItemPrint = dalList1.IOrderItem.PrintAll();
                                    foreach (OrderItem p in orderItemPrint)
                                    {
                                        Console.WriteLine(value: p);
                                    }
                                    break;
                                }
                                catch (ListIsEmptyException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 5://Deletes orderItem
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IOrderItem.Delete(id);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 4://Update the field of orderItem
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    OrderItem p = new OrderItem();
                                    p = dalList1.IOrderItem.PrintByID(id);
                                    Console.WriteLine("Do you want to change the id of order?, enter y for yes and n for no");
                                    string answer = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter ID order");
                                        int orderID1 = Console.Read();
                                        p.OrderID = orderID1;
                                    }
                                    Console.WriteLine("Do you want to change the id of product?, enter y for yes and n for no");
                                    string answer1 = Console.ReadLine();
                                    if (answer1 == "y")
                                    {
                                        Console.WriteLine("Enter ID product");
                                        int productID1 = Console.Read();
                                        p.ProductID = productID1;
                                    }
                                    Console.WriteLine("Do you want to change the price?, enter y for yes and n for no");
                                    string answer2 = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter price");
                                        double price1 = Console.Read();
                                        p.Price = price1;
                                    }
                                    Console.WriteLine("Do you want to change the amount?, enter y for yes and n for no");
                                    string answer3 = Console.ReadLine();
                                    if (answer == "y")
                                    {
                                        Console.WriteLine("Enter amount");
                                        int amount = Console.Read();
                                        p.Amount = amount;
                                    }
                                    bool answer11 = dalList1.IOrderItem.Update(p);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (InvalidVariableException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 6://Prints orderItem by two identifiers
                            {
                                try
                                {
                                    Console.WriteLine("Enter order ID");
                                    int id1 = Console.Read();
                                    Console.WriteLine("Enter product ID");
                                    int id2 = Console.Read();
                                    OrderItem oi = dalList1.IOrderItem.PrintByTwoId(id2, id1);
                                    Console.WriteLine(oi);
                                    break;
                                }
                                catch (IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 7://Print the all orderItem of order
                            {
                                try
                                {
                                    Console.WriteLine("Enter order ID");
                                    int id1 = Console.Read();
                                    IEnumerable<OrderItem> orderItemByOrderId;
                                    orderItemByOrderId = dalList1.IOrderItem.PrintAllByOrder(id1);
                                    foreach (OrderItem p in orderItemByOrderId)
                                    {
                                        Console.WriteLine(p);
                                    }
                                    break;
                                }
                                catch (ListIsEmptyException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch (Exception m)
                                {
                                    Console.WriteLine(m);
                                    break;
                                }
                                break;
                            }
                        case 8://Exit
                            {
                                mainActions();
                                break;
                            }
                    }
        }


            }//manages the orderItems

            void mainActions()//The method ask the user to coose the main item by enim
                {
                    string choice = "start";
                    do
                    {
                        switch (choice)
                        {
                            case "product":
                                {
                                    manageProduct();
                                    break;
                                }
                            case "order":
                                {
                                    manageOrder();
                                    break;
                                }
                            case "orderItem":
                                {
                                    manageOrderItem();
                                    break;
                                }
                            case "start":
                                break;
                            case "exit":
                                {
                                    break;
                                }

                        }
                        Console.WriteLine(
                                               $@"Enter
product to manage the products
order to manage the orders
orderItem to manage the items in the order
exit to exit the store");
                        choice = Console.ReadLine();

                    } while (choice != "exit");
                }
            

        }
    }
}



