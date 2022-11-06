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
                Enums.productEnum optionsProduct;
                optionsProduct = (Enums.productEnum)Console.Read();
                while (optionsProduct != Enums.productEnum.Exit)
                {
                    switch (optionsProduct)
                    {
                        case Enums.productEnum.Adding:

                            break;
                        case Enums.productEnum.PrintById:
                            break;
                        case Enums.productEnum.printAll:
                            break;
                        case Enums.productEnum.Delete:
                            break;
                        case Enums.productEnum.Update:
                            break;
                        case Enums.productEnum.Exit:
                            break;
                    }

                    
                    optionsProduct = (Enums.productEnum)Console.Read();
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
                Enums.orderEnum optionsOrder;
                optionsOrder = (Enums.orderEnum)Console.Read();
                while (optionsOrder != Enums.orderEnum.Exit)
                {
                    switch (optionsOrder)
                    {
                        case Enums.orderEnum.Adding:
                            break;
                        case Enums.orderEnum.PrintById:
                            break;
                        case Enums.orderEnum.printAll:
                            break;
                        case Enums.orderEnum.Delete:
                            break;
                        case Enums.orderEnum.Update:
                            break;
                        case Enums.orderEnum.Exit:
                            break;
                    }
                    optionsOrder = (Enums.orderEnum)Console.Read();
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
                Enums.orderItemEnum optionsOrderItem;
                optionsOrderItem = (Enums.orderItemEnum)Console.Read();
                while (optionsOrderItem != Enums.orderItemEnum.Exit)
                {
                    switch(optionsOrderItem)
                    {
                        case Enums.productEnum.Adding:
                            break;
                        case Enums.productEnum.PrintById:
                            break;
                        case Enums.productEnum.printAll:
                            break;
                        case Enums.productEnum.Delete:
                            break;
                        case Enums.productEnum.Update:
                            break;
                        case Enums.productEnum.Exit:
                            break;
                    }

                    optionsOrderItem = (Enums.orderItemEnum)Console.Read();
                }
            }//manages the orderItems


        }

    }
}


