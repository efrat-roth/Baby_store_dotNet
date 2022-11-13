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
                                    Product p = new Product();
                                    int i = dalList1.IProduct.Add(p);
                                    break;
                                }
                            case Enums.ProductEnum.PrintById:
                                {
                                    Console.WriteLine("Enter ID of product");
                                    int id;
                                    id = Console.Read();
                                    Product p = dalList1.IProduct.PrintByID(id);
                                    p.ToString();
                                    break;
                                }

                            case Enums.ProductEnum.PrintAll:
                                {
                                    IEnumerable<Product> productPrint;
                                    productPrint = dalList1.IProduct.PrintAll();
                                    foreach (Product p in productPrint) { p.ToString(); }
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
                                    Order p = new Order();
                                    int i = dalList1.IOrder.Add(p);
                                    break;
                                }

                            case Enums.OrderEnum.PrintById:
                                {
                                    Console.WriteLine("Enter ID of order");
                                    int id;
                                    id = Console.Read();
                                    Order p = dalList1.IOrder.PrintByID(id);
                                    p.ToString();
                                    break;
                                }
                            case Enums.OrderEnum.PrintAll:
                                {
                                    IEnumerable<Order> orderPrint;
                                    orderPrint = dalList1.IOrder.PrintAll();
                                    foreach (Order p in orderPrint) { p.ToString(); }
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
                                    OrderItem p = new OrderItem();
                                    int i = dalList1.IOrderItem.Add(p);
                                    break;
                                }
                            case Enums.OrderItemEnum.PrintById:
                                {
                                    Console.WriteLine("Enter ID of orderItem");
                                    int id;
                                    id = Console.Read();
                                    OrderItem p = dalList1.IOrderItem.PrintByID(id);
                                    p.ToString();
                                    break;
                                }
                            case Enums.OrderItemEnum.PrintAll:
                                {
                                    IEnumerable<OrderItem> orderItemPrint;
                                    orderItemPrint = dalList1.IOrderItem.PrintAll();
                                    foreach (OrderItem p in orderItemPrint) { p.ToString(); }
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
                                    oi.ToString();
                                    break;
                                }

                            case Enums.OrderItemEnum.PrintAllByOrder:
                                {
                                    Console.WriteLine("Enter order ID");
                                    int id1 = Console.Read();
                                    IEnumerable<OrderItem> orderItemByOrderId;
                                    orderItemByOrderId = dalList1.IOrderItem.PrintAllByOrder(id1);
                                    foreach (OrderItem p in orderItemByOrderId) { p.ToString(); }
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
            }


        }

    }
}


