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
            try
            {
                IDal dalList1=new DalList();
                Order orders = new Order();
                Console.WriteLine(
                                  $@"Enter
product to manage the products
order to manage the orders
orderItem to manage the items in the order");
                
                string choice = Console.ReadLine();
                if (choice == "product")//manages the products
                {
                    manageProduct();
                }
                if (choice == "order")//manages the orders
                {
                    manageOrder();
                }
                if (choice == "orderItem")//manages the orderItems
                {
                    manageOrderItem();
                }
                void manageProduct()
                {
                    
                    Product inputProduct()//return product
                    {
                        Product p=new Product();
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
Exit to exit the program");
                    Enums.ProductEnum optionsProduct;
                    optionsProduct = (Enums.ProductEnum) Console.Read();
                    while (optionsProduct != Enums.ProductEnum.Exit)
                    {
                        switch (optionsProduct)
                        {
                            case Enums.ProductEnum.Adding:
                                {
                                    Product p = inputProduct();
                                    int i = dalList1.IProduct.Add(p);
                                    break;
                                }
                            case Enums.ProductEnum.PrintById:
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    Product p = dalList1.IProduct.PrintByID(id);
                                    Console.WriteLine( p);
                                    break;
                                }

                            case Enums.ProductEnum.PrintAll:
                                {
                                    IEnumerable<Product> productPrint;
                                    productPrint = dalList1.IProduct.PrintAll();
                                    foreach (Product p in productPrint)
                                    {
                                        Console.WriteLine( p);
                                    }
                                    break;
                                }
                            case Enums.ProductEnum.Delete:
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IProduct.Delete(id);
                                    break;
                                }
                            case Enums.ProductEnum.Update:
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    Product p = dalList1.IProduct.PrintByID(id);
                                    bool result = dalList1.IProduct.Update(ref p);
                                    break;
                                }
                            case Enums.ProductEnum.Exit:
                                break;
                        }


                        optionsProduct = (Enums.ProductEnum)Console.Read();
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
                            throw new Exception("The date is invalid");
                        }
                        if (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) < 1 | (int.Parse(orderDate) - (int.Parse(orderDate) / 10000 * 10000) / 100) > 12)
                        {
                            throw new Exception("The date is invalid");
                        }
                        if (int.Parse(orderDate) - (int.Parse(orderDate) / 100 * 100) < 1)
                        {
                            throw new Exception("The date is invalid");
                        }
                        o.OrderDate = DateTime.Parse(orderDate);
                        Console.WriteLine("Enter ship date in ######:for day,month,year format");
                        string shipDate = Console.ReadLine();
                        if (int.Parse(shipDate) / 10000 < 1 | int.Parse(shipDate) / 1000 > 30)
                        {
                            throw new Exception("The date is invalid");
                        }
                        if (int.Parse(shipDate) - (int.Parse(shipDate) / 10000 * 10000) < 1 | (int.Parse(shipDate) - (int.Parse(shipDate) / 10000 * 10000) / 100) > 12)
                        {
                            throw new Exception("The date is invalid");
                        }
                        if (int.Parse(shipDate) - (int.Parse(shipDate) / 100 * 100) < 1)
                        {
                            throw new Exception("The date is invalid");
                        }
                        o.ShipDate = DateTime.Parse(shipDate);
                        Console.WriteLine("Enter delivery date in ######:for day,month,year format");
                        string deliveryDate = Console.ReadLine();
                        if (int.Parse(deliveryDate) / 10000 < 1 | int.Parse(deliveryDate) / 1000 > 30)
                        {
                            throw new Exception("The date is invalid");
                        }
                        if (int.Parse(deliveryDate) - (int.Parse(deliveryDate) / 10000 * 10000) < 1 | (int.Parse(deliveryDate) - (int.Parse(deliveryDate) / 10000 * 10000) / 100) > 12)
                        {
                            throw new Exception("The date is invalid");
                        }
                        if (int.Parse(deliveryDate) - (int.Parse(deliveryDate) / 100 * 100) < 1)
                        {
                            throw new Exception("The date is invalid");
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
                    while (optionsOrder != Enums.OrderEnum.Exit)
                    {
                        switch (optionsOrder)
                        {
                            case Enums.OrderEnum.Adding:
                                {
                                    Order order1 = inputOrder();
                                    int i = dalList1.IOrder.Add(order1);
                                    break;
                                }

                            case Enums.OrderEnum.PrintById:
                                {
                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    Order p = dalList1.IOrder.PrintByID(id);
                                    Console.WriteLine(value: p);
                                    break;
                                }
                            case Enums.OrderEnum.PrintAll:
                                {
                                    IEnumerable<Order> orderPrint;
                                    orderPrint = dalList1.IOrder.PrintAll();
                                    foreach (Order p in orderPrint)
                                    {
                                        Console.WriteLine(value: p);
                                    
                                    }
                                    break;
                                }
                            case Enums.OrderEnum.Delete:
                                {
                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IOrder.Delete(id);
                                    break;
                                }
                            case Enums.OrderEnum.Update:
                                {

                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    Order p = new Order();
                                    p = dalList1.IOrder.PrintByID(id);
                                    bool answer = dalList1.IOrder.Update(ref p);
                                    break;
                                }
                            case Enums.OrderEnum.Exit:

                                break;
                        }
                        optionsOrder = (Enums.OrderEnum)Console.Read();
                    }
                }//manages the orders
                void manageOrderItem()
                {
                    OrderItem inputOrderItem()//return product
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
                    while (optionsOrderItem != Enums.OrderItemEnum.Exit)
                    {
                        switch (optionsOrderItem)
                        {
                            case Enums.OrderItemEnum.Adding:
                                {
                                    OrderItem p = inputOrderItem();
                                    int i = dalList1.IOrderItem.Add(p);
                                    break;
                                }
                            case Enums.OrderItemEnum.PrintById:
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    OrderItem p = dalList1.IOrderItem.PrintByID(id);
                                    Console.WriteLine(value: p);
                                    break;
                                }
                            case Enums.OrderItemEnum.PrintAll:
                                {
                                    IEnumerable<OrderItem> orderItemPrint;
                                    orderItemPrint = dalList1.IOrderItem.PrintAll();
                                    foreach (OrderItem p in orderItemPrint)
                                    {
                                        Console.WriteLine(value: p);
                                    }
                                    break;
                                }
                            case Enums.OrderItemEnum.Delete:
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    bool answer = dalList1.IOrderItem.Delete(id);
                                    break;
                                }
                            case Enums.OrderItemEnum.Update:

                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    OrderItem p = new OrderItem();
                                    p = dalList1.IOrderItem.PrintByID(id);
                                    bool answer = dalList1.IOrderItem.Update(ref p);
                                    break;
                                }
                            case Enums.OrderItemEnum.Exit:
                                break;
                            case Enums.OrderItemEnum.PrintByTwoId:
                                {
                                    Console.WriteLine("Enter order ID");
                                    int id1 = Console.Read();
                                    Console.WriteLine("Enter product ID");
                                    int id2 = Console.Read();
                                    OrderItem oi = dalList1.IOrderItem.PrintByTwoId(id2, id1);
                                    Console.WriteLine( oi);
                                    break;
                                }

                            case Enums.OrderItemEnum.PrintAllByOrder:
                                {
                                    Console.WriteLine("Enter order ID");
                                    int id1 = Console.Read();
                                    IEnumerable<OrderItem> orderItemByOrderId;
                                    orderItemByOrderId = dalList1.IOrderItem.PrintAllByOrder(id1);
                                    foreach (OrderItem p in orderItemByOrderId)
                                    {
                                        Console.WriteLine(oi);
                                    }
                                    break;
                                }
                        }

                        optionsOrderItem = (Enums.OrderItemEnum)Console.Read();
                    }
                }//manages the orderItems
            }
            catch(Exception s)
            {
                Console.WriteLine(s);
                break;
            }


        }

    }
}


