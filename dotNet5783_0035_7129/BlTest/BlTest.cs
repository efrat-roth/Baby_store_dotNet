using System.Net.Mail;
using BlApi;
using BO;
using System.Collections.Generic;

namespace BlTest;

internal class BlTest
{

    static void Main(string[] args)
    {

        IBl bl = new BL();

        mainActions();
        Cart createCart()//create new cart by accept the values from the user
        {
            Console.WriteLine("Enter Customer Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Customer Email");
            string email = Console.ReadLine();
            bool isRight = false;
            foreach (char d in email)//checks if email is correct and has the @ in their.
                if (d == '@')
                    isRight = true;
            if (!isRight)
                throw new BO.InvalidVariableException();
            Console.WriteLine("Enter Customer Adress");
            String adress = Console.ReadLine();
            Cart c = new Cart
            {
                CustomerName = name,
                CustomerEmail = email,
                CustomerAdress = adress,
                TotalPrice = 0,
                Items = new List<BO.OrderItem>()
            };
            return c;
        }

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
                string s= Console.ReadLine();
                Enums.ProductEnum.TryParse(s,out choice);
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
                            int id = Console.Read();
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
                            if (price1 < 0)
                                throw new InvalidVariableException();
                            product.Price = price1;
                            Console.WriteLine("Enter the category of product to update");
                            DO.Enums.Category category1 = (DO.Enums.Category)Console.Read();
                            product.Category = category1;
                            Console.WriteLine("Enter amount of products in stock");
                            int inStock1 = Console.Read();
                            if (inStock1 < 0)
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
            int option = 1;

            while (option != 0)
            {
                while (option > 0 & option < 7)
                {
                    Console.WriteLine($@"Enter
1 to get the all orders
2 to get the details of order
3 to update the delivery of order
4 to update that order arrived
5 to track after order
6 to update order
0 to exit");
                    option = Console.Read();
                }
               
                switch (option)
                {
                    case Enums.OrderEnum.GetList://return the all orders in the store
                        {
                            try
                            {
                                List<OrderForList> list = new List<OrderForList>();
                                list = bl.Order.GetListOfOrders();
                                foreach (OrderForList order in list)//print the all orderItem
                                {
                                    Console.WriteLine(order);
                                }
                            }
                            catch (Exception e)
                            {

                            }
                            break;
                        }
                    case Enums.OrderEnum.Details://return details of order
                        {
                            Console.WriteLine("Enter m if yoy are mannager, and c for cstumer");
                            char identity = char.Parse(Console.ReadLine());
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter the ID of Order");
                                int id = Console.Read();
                                if (identity == 'm')//in case of maneger
                                {
                                    order = bl.Order.GetDetailsOrderManager(id);
                                }
                                if (identity == 'c')//in case of customer
                                {
                                    order = bl.Order.GetDetailsOrderCustomer(id);
                                }
                                else//in case that the input is invalid(not m or c
                                {
                                    throw new InvalidVariableException();
                                }
                                Console.WriteLine(order);//prints the order
                            }
                            catch (Exception N)
                            {

                            }
                            break;
                        }
                    case Enums.OrderEnum.Delivered://update order as delivered
                        {
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter ID of order to update");
                                int id = Console.Read();
                                order = bl.Order.DeliveredOrder(id);
                                Console.WriteLine(order);
                            }
                            catch (Exception n)
                            {

                            }
                            break;
                        }
                    case Enums.OrderEnum.Arrived://update order as an arrived
                        {
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter ID of order to update");
                                int id = Console.Read();
                                order = bl.Order.ArrivedOrder(id);
                                Console.WriteLine(order);
                            }
                            catch (Exception n)
                            {

                            }
                            break;
                        }
                    case Enums.OrderEnum.Tracking://track afetr an order
                        {
                            try
                            {
                                BO.OrderTracking orderTrack = new BO.OrderTracking();
                                Console.WriteLine("Enter ID of order to track");
                                int id = Console.Read();
                                orderTrack = bl.Order.OrderTracking(id);
                                Console.WriteLine(orderTrack);
                            }
                            catch (InvalidVariableException m)
                            {
                                Console.WriteLine(m);
                            }
                            break;
                        }
                    case Enums.OrderEnum.Update://update amount of product in order
                        {
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter the ID of order");
                                int idO = Console.Read();
                                Console.WriteLine("Enter the ID of product");
                                int idP = Console.Read();
                                Console.WriteLine("Enter the new amount");
                                int amount = Console.Read();
                                order = bl.Order.UpdateOrder(idO, idP, amount);
                                Console.WriteLine(order);
                            }
                            catch (Exception n)
                            {

                            }

                            break;
                        }
                    case Enums.OrderEnum.Exit:
                        {
                            break;
                        }
                }
            }


        }
        void manageCart()
        {
            Enums.CartEnum option = Enums.CartEnum.Update;//resets the variable

            while (option != Enums.CartEnum.Exit)
            {
                Cart c = new Cart();//create new cart
                try
                {
                    c = createCart();
                }
                catch (Exception m)
                {

                }
                Console.WriteLine($@"Enter
Add to add product to cart
Update to update product amount
Make to make an order");
                option = (Enums.CartEnum)Console.Read();
                switch (option)
                {
                    case Enums.CartEnum.Add://add product to the cart
                        {
                            try
                            {
                                Console.WriteLine("Enter the ID of product");
                                int id = Console.Read();
                                c = bl.Cart.AddProductToCart(c, id);
                                Console.WriteLine(c);
                            }
                            catch (Exception m)
                            { }
                            break;

                        }
                    case Enums.CartEnum.Update://update amount of product in cart
                        {
                            try
                            {
                                Console.WriteLine("Enter product ID");
                                int id = Console.Read();
                                Console.WriteLine("Enter the new Amount");
                                int amount = Console.Read();
                                c = bl.Cart.UpdateProductAmount(c, id, amount);
                                Console.WriteLine(c);
                            }
                            catch(Exception n)
                            {

                            }
                            break;
                        }
                    case Enums.CartEnum.Make://make an order by the cart
                        {
                            try
                            {
                                DO.Order order = bl.Cart.MakeOrder(c, c.CustomerAdress, c.CustomerName, c.CustomerEmail);
                                Console.WriteLine(order);
                            }
                            catch(Exception m)
                            {
                                
                            }
                            break;
                        }
                    case Enums.CartEnum.Exit://exit the actions on cart
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








