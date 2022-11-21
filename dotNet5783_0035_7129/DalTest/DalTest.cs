﻿using System;
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
            IDal dalList1=new DalList();
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
                Console.WriteLine(@"Enter
Adding to add product
printById to print product by input id of product
PrintAll to print the all list of the products
Update to update details of product
Delete to delete a product
Exit to exit the product manager");
                Enums.ProductEnum optionsProduct;
                optionsProduct = (Enums.ProductEnum)Console.Read();
                do
                {
                    switch (optionsProduct)
                    {
                        case Enums.ProductEnum.Adding://Add a product
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
                        case Enums.ProductEnum.PrintById://Print a product
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

                        case Enums.ProductEnum.PrintAll://Print the all products
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
                        case Enums.ProductEnum.Delete://Delete product
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
                        case Enums.ProductEnum.Update://Update Product
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
                                    bool result = dalList1.IProduct.Update( p);
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
                        case Enums.ProductEnum.Exit:
                            {
                                mainActions();
                                break;
                            }
                    }

                    Console.WriteLine(@"Enter
Adding to add product
printById to print product by input id of product
PrintAll to print the all list of the products
Update to update details of product
Delete to delete a product
Exit to exit the program");
                    optionsProduct = (Enums.ProductEnum)Console.Read();
                } while (optionsProduct != Enums.ProductEnum.Exit);
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
                    Console.WriteLine(@"Enter
Adding to add an order
printById to print order by input id of order
PrintAll to print the all list of the orders
Update to update details of order
Delete to delete an order
Exit to exit the program");
                    Enums.OrderEnum optionsOrder;
                    optionsOrder = (Enums.OrderEnum)Console.Read();
                do
                {
                    switch (optionsOrder)
                    {
                        case Enums.OrderEnum.Adding://Add order
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

                        case Enums.OrderEnum.PrintById://Print order
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
                        case Enums.OrderEnum.PrintAll://Print the all orders
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
                        case Enums.OrderEnum.Delete://Delete the order
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
                        case Enums.OrderEnum.Update://Updates the order
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
                        case Enums.OrderEnum.Exit:
                            {
                                mainActions();
                                break;

                            }
                    }
                    Console.WriteLine(@"Enter
Adding to add an order
printById to print order by input id of order
PrintAll to print the all list of the orders
Update to update details of order
Delete to delete an order
Exit to exit the program");
                    optionsOrder = (Enums.OrderEnum)Console.Read();
                } while (optionsOrder != Enums.OrderEnum.Exit);
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
                    Console.WriteLine(@"Enter
Adding to add an orderItem
printById to print orderItem by input id of its
PrintAll to print the all list of the orderItem
Update to update details of orderItem
Delete to delete an orderItem
Exit to exit the program");
                    Enums.OrderItemEnum optionsOrderItem;
                    optionsOrderItem = (Enums.OrderItemEnum)Console.Read();
                    do 
                    {
                        switch (optionsOrderItem)
                        {
                            case Enums.OrderItemEnum.Adding://Add OrderItem
                                {
                                try
                                {
                                    OrderItem p = inputOrderItem();
                                    int i = dalList1.IOrderItem.Add(p);
                                    break;
                                }
                                catch(InvalidVariableException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch(IdAlreadyExistException m)
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
                            case Enums.OrderItemEnum.PrintById://Print orderItem
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
                                catch(IdDoesNotExistException m)
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
                            case Enums.OrderItemEnum.PrintAll://Prints the all orderItems
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
                                catch(ListIsEmptyException m)
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
                            case Enums.OrderItemEnum.Delete://Deletes orderItem
                                {
                                try
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IOrderItem.Delete(id);
                                    break;
                                }
                                catch(IdDoesNotExistException m)
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
                            case Enums.OrderItemEnum.Update://Update the field of orderItem

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
                                    bool answer11 = dalList1.IOrderItem.Update( p);
                                    break;
                                }
                                catch(IdDoesNotExistException m)
                                {
                                    Console.WriteLine(m);
                                }
                                catch(InvalidVariableException m)
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
                            case Enums.OrderItemEnum.Exit:
                            {
                                mainActions();
                                break;

                            }
                        case Enums.OrderItemEnum.PrintByTwoId://Prints orderItem by two identifiers
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
                                catch(IdDoesNotExistException m)
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
                            case Enums.OrderItemEnum.PrintAllByOrder://Print the all orderItem of order
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
                                catch(ListIsEmptyException m)
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
                        }
                    Console.WriteLine(@"Enter
Adding to add an orderItem
printById to print orderItem by input id of its
PrintAll to print the all list of the orderItem
Update to update details of orderItem
Delete to delete an orderItem
Exit to exit the program");
                    optionsOrderItem = (Enums.OrderItemEnum)Console.Read();
                    } while (optionsOrderItem != Enums.OrderItemEnum.Exit);
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


