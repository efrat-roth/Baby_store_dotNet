using BlApi;
using BO;
using System.Collections.Generic;

namespace BlTest;

internal class BlTest
{
   
    static void Main()
    {

        IBl bl = new BL();
        mainActions();
        
        void manageProduct() //manages all the methods in product
        {
            Enums.ProductEnum choice = Enums.ProductEnum.getlp;
            while (choice != Enums.ProductEnum.exit)  //while the user wants to use the methods of product.
            {
                Console.WriteLine(
                                       $@"Enter
getlp to get the list of products.
getpm to manager to get the details of product.
getpc to customer to get details of product.
add to add product to store.
up to update product in the store.
del to delete product from the store.");
                choice = (Enums.ProductEnum)Console.Read();
                switch (choice)
                {
                    case Enums.ProductEnum.getlp:   //asks for list of products
                        {
                            List<ProductForList> lists = new List<ProductForList>();
                            lists = bl.Product.GetListOfProduct();
                            Console.WriteLine(lists);
                            break;
                        }
                    case Enums.ProductEnum.getpm:   //returns details of product
                        {
                            Console.WriteLine("Enter id of product");
                            int id=Console.Read();
                            Product product = bl.Product.GetProductManager(id);
                            Console.WriteLine(product);
                            break;
                        }
                    case Enums.ProductEnum.getpc:   //returns details of product
                        {
                            Console.WriteLine("Enter id of product");
                            int id = Console.Read();
                            Product product = bl.Product.GetProductManager(id);
                            Console.WriteLine(product);
                            break;
                        }
                    case Enums.ProductEnum.up:  //Updates product in the store.
                        {
                            DO.Product product = new DO.Product();
                            Console.WriteLine("Enter the id of product to update");
                            int id = Console.Read();
                            if (id <= 0)
                                throw new InvalidVariableException();
                            product.ID = id;
                            Console.WriteLine("Enter the name of product to update");
                            string name1 = Console.ReadLine();
                            if (name1 == null)
                                throw new InvalidVariableException();
                            product.Name = name1;
                            Console.WriteLine("Enter the price of product to update");
                            double price1 = Console.Read();
                            if(price1 <0)
                                throw new InvalidVariableException();
                            product.Price = price1;
                            Console.WriteLine("Enter the category of product to update");
                            DO.Enums.Category category1 = (DO.Enums.Category)Console.Read();                           
                            product.Category = category1;
                            Console.WriteLine("Enter amount of products in stock");
                            int inStock1 = Console.Read();
                            if(inStock1<0)
                                throw new InvalidVariableException();
                            product.InStock = inStock1;
                            bl.Product.UpdatingProductDetails(product);
                            Console.WriteLine(product);

                            break;
                        }
                    case Enums.ProductEnum.add:   //Adding a product
                        {
                            DO.Product p = new DO.Product();
                            Console.WriteLine("Enter the id of product to add");
                            int id1 = Console.Read();
                            p.ID = id1;
                            Console.WriteLine("Enter the name of product to add");
                            string name1 = Console.ReadLine();
                            p.Name = name1;
                            Console.WriteLine("Enter the category of product to add");
                            DO.Enums.Category category1 = (DO.Enums.Category)Console.Read();
                            p.Category = category1;
                            Console.WriteLine("Enter the price of product to add");
                            double price1 = Console.Read();
                            p.Price = price1;
                            Console.WriteLine("Enter amount of products in stock");
                            int inStock1 = Console.Read();
                            p.InStock = inStock1;
                            bl.Product.AddProduct(p);
                            Console.WriteLine(p);
                            break;
                        }
                    case Enums.ProductEnum.del:  // deletes product from the store.
                        {
                            Console.WriteLine("Enter id of product you want to delete");
                            int id = Console.Read();
                            bl.Product.DeleteProduct(id);
                            break;
                        }
                    case Enums.ProductEnum.exit:   // exit the product functions area.
                        {
                            mainActions();
                            break;
                        }

                }
            }

        }
        void manageOrder()
        {
            Enums.OrderEnum option = Enums.OrderEnum.GetList;

            while(option!=Enums.OrderEnum.Exit)
            {
                Console.WriteLine($@"Enter
GetList to get the all orders
Details to get the details of order
Delivered to update the delivery of order
Arrived to update that order arrived
Tracking to track after order
Update to update order");
                option = (Enums.OrderEnum)Console.Read();
                switch(option)
                {
                    case Enums.OrderEnum.GetList://return the all orders in the store
                        {
                            List<OrderForList> list = new List<OrderForList>();
                            list = bl.Order.GetListOfOrders();
                            break;
                        }
                    case Enums.OrderEnum.Details:
                        {
                            break;
                        }
                    case Enums.OrderEnum.Delivered:
                        { 
                            break;
                        }
                    case Enums.OrderEnum.Arrived:
                        {
                            break;
                        }
                    case Enums.OrderEnum.Tracking:
                        {
                            break;
                        }
                    case Enums.OrderEnum.Update:
                        {
                            break;
                        }
                    case Enums.OrderEnum.Exit:
                        {
                            mainActions();
                            break;
                        }
                }
            }
    
            
        }
        
       

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
                    case "cart":
                        {
                            manageCart();
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
cart to manage the items in the cart
exit to exit the store");
                choice = Console.ReadLine();

            } while (choice != "exit");
        }

       








    }








}