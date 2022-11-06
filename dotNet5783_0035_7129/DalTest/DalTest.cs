using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DO;

namespace DalTest
{
    internal class DalTest
    {
        static void Main(string[] args)
        { 
            DalOrder order = new DalOrder();
            DalOrderItem orderItem = new DalOrderItem();
            DalProduct product = new DalProduct();
            Console.WriteLine(@"Enter
                             product to manage the products
                             order to manage the orders
                             orderItem to manage the items in the order");
            string choice=Console.ReadLine();
            if (choice=="product")//manages the products
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
                optionsProduct = (Enums.ProductEnum)Console.Read();
                while (optionsProduct != Enums.ProductEnum.Exit)
                {
                    switch (optionsProduct)
                    {
                        case Enums.ProductEnum.Adding:

                            break;
                        case Enums.ProductEnum.PrintById:
                            break;
                        case Enums.ProductEnum.printAll:
                            break;
                        case Enums.ProductEnum.Delete:
                            break;
                        case Enums.ProductEnum.Update:
                            break;
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
                            break;
                        case Enums.OrderEnum.PrintById:
                            break;
                        case Enums.OrderEnum.printAll:
                            break;
                        case Enums.OrderEnum.Delete:
                            break;
                        case Enums.OrderEnum.Update:
                            break;
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
                    switch(optionsOrderItem)
                    {
                        case Enums.OrderItemEnum.Adding:
                            break;
                        case Enums.OrderItemEnum.PrintById:
                            break;
                        case Enums.OrderItemEnum.printAll:
                            break;
                        case Enums.OrderItemEnum.Delete:
                            break;
                        case Enums.OrderItemEnum.Update:
                            break;
                        case Enums.OrderItemEnum.Exit:
                            break;
                    }

                    optionsOrderItem = (Enums.OrderItemEnum)Console.Read();
                }
            }//manages the orderItems


        }

    }
}


