using System.Net.Mail;
using BlApi;
using BO;
using System.Collections.Generic;
using System;
using BlImplementation;
using System.Data;

namespace BlTest;

internal class BlTest
{

    static void Main(string[] args)
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        Cart c = new Cart();
        mainActions();
        Cart createCart()//create new cart by accept the values from the user
        {
            c ??= new Cart();
            Console.WriteLine("Enter Customer Name");
            string? name = Console.ReadLine()?? throw new BO.InvalidVariableException();
            Console.WriteLine("Enter Customer Email");
            string? email = Console.ReadLine()??throw new BO.InvalidVariableException();
            bool isRight = false;
            foreach (char d in email)//checks if email is correct and has the @ in their.
                if (d == '@')
                    isRight = true;
            if (!isRight)
                throw new BO.InvalidVariableException();
            Console.WriteLine("Enter Customer Adress");
            string? adress = Console.ReadLine()?? throw new BO.InvalidVariableException();
            Cart cart = new Cart
            {
                CustomerName = name,
                CustomerEmail = email,
                CustomerAdress = adress,
                TotalPrice = 0,
                Items = new List<BO.OrderItem?>()
            };
            return cart;
        }
        void manageProduct() //manages all the methods in product
        {
            int choice1 =0;
            while (choice1 != 7)  //while the user wants to use the methods of product.
            {
                Console.WriteLine(
                   $@"Enter
1 to get the list of products.
2 to manager to get the details of product.
3 to customer to get details of product.
4 to update product to store.
5 to add product in the store.
6 to delete product from the store.
7 to exit product");

                int.TryParse(Console.ReadLine(),out choice1);
                while (choice1 < 1 | choice1 > 7)
                {
                    Console.WriteLine($@"Enter valid number");
                    int.TryParse(Console.ReadLine(),out choice1);
                }
                
                switch (choice1)
                {
                    case 1:   //asks for list of products
                        {
                            try
                            {
                                List<ProductForList?> lists = new List<ProductForList?>();
                                lists = bl?.Product.GetListOfProduct() ?? throw new ObgectNullableException();
                                foreach(ProductForList? p in lists)
                                {
                                    Console.WriteLine(p);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 2:   //returns details of product
                        {
                            try
                            {
                                Console.WriteLine("Enter id of product");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                BO.Product product = bl?.Product.GetProductManager(id) ?? throw new ObgectNullableException();
                                Console.WriteLine(product);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 3:   //returns details of product
                        {
                            try
                            {
                                Console.WriteLine("Enter id of product");
                                int id;
                                int.TryParse( Console.ReadLine(),out id);
                                ProductItem product = bl?.Product.GetProductCustomer(id, c) ?? throw new ObgectNullableException();
                                Console.WriteLine(product);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 4:  //Updates product in the store.
                        {
                            try
                            {
                                BO.Product product = new BO.Product();
                                Console.WriteLine("Enter the id of product to update");
                                int id;                              
                                int.TryParse(Console.ReadLine(), out id);
                                if (id <= 0)
                                    throw new BO.InvalidVariableException();
                                product.ID = id;
                                Console.WriteLine("Enter the Category of product to update");
                                DO.Category category1;
                                DO.Category.TryParse(Console.ReadLine(), out category1);
                                product.category = (BO.Category)category1;
                                Console.WriteLine("Enter the name of product to update");
                                string? name1 = Console.ReadLine() ?? throw new BO.InvalidVariableException();
                                product.Name = name1;
                                Console.WriteLine("Enter the price of product to update");
                                double price1;
                                double.TryParse(Console.ReadLine(), out price1); 
                                if (price1 < 0)
                                    throw new BO.InvalidVariableException();
                                product.Price = price1;
                             
                                Console.WriteLine("Enter amount of products in stock");
                                int inStock1;
                                int.TryParse(Console.ReadLine(), out inStock1);
                                if (inStock1 < 0)
                                    throw new BO.InvalidVariableException();
                                product.InStock = inStock1;
                                bl?.Product.UpdatingProductDetails(product);
                                Console.WriteLine(product);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 5:   //Adding a product
                        {
                            try
                            {
                                BO.Product p = new BO.Product();
                                Console.WriteLine("Enter the id of product to add");
                                int id1;
                                int.TryParse(Console.ReadLine(), out id1);
                                if(id1<100000)
                                    throw new BO.InvalidVariableException();
                                p.ID = id1;
                                Console.WriteLine("Enter the name of product to add");
                                string? name1 = Console.ReadLine()??throw new BO.InvalidVariableException();
                                p.Name = name1;
                                Console.WriteLine("Enter the Category of product to add");
                                BO.Category category1 ;
                                BO.Category.TryParse(Console.ReadLine(), out category1);
                                p.category = (BO.Category)category1;
                                p.category = category1;
                                Console.WriteLine("Enter the price of product to add");
                                double price1;
                                double.TryParse(Console.ReadLine(), out price1);
                                p.Price = price1;
                                Console.WriteLine("Enter amount of products in stock");
                                int inStock1;
                                int.TryParse(Console.ReadLine(), out inStock1); 
                                p.InStock = inStock1;
                                bl?.Product.AddProduct(p);
                                Console.WriteLine(p);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 6:  // deletes product from the store.
                        {
                            try
                            {
                                Console.WriteLine("Enter id of product you want to delete");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                bl?.Product.DeleteProduct(id);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 7:   // exit the product functions area.
                        {
                            mainActions();
                            break;
                        }

                }
            }

        }
        void manageOrder()
        {
            int option = 0;

            while (option != 7)
            {
                Console.WriteLine($@"Enter
1 to get the all orders
2 to get the details of order
3 to update the delivery of order
4 to update that order arrived
5 to track after order
6 to update order
7 to exit");
                int.TryParse(Console.ReadLine(), out option);
                while (option < 1 & option > 7)
                {
                    Console.WriteLine($@"Enter valid number");
                    int.TryParse(Console.ReadLine(), out option);
                }

               
                switch (option)
                {
                    case 1://return the all orders in the store
                        {
                            try
                            {
                                List<OrderForList?> list = new List<OrderForList?>();
                                list = bl?.Order.GetListOfOrders() ?? throw new ObgectNullableException();
                                foreach (OrderForList? order in list)//print the all orderItem
                                {
                                    Console.WriteLine(order);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 2://return details of order
                        {
                            Console.WriteLine("Enter m if yoy are mannager, and c for cstumer");
                            char identity = char.Parse(Console.ReadLine() ?? throw new BO.InvalidVariableException());
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter the ID of Order");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                if (identity == 'm')//in case of maneger
                                {
                                    order = bl?.Order.GetDetailsOrderManager(id) ?? throw new ObgectNullableException();
                                }
                                else if (identity == 'c')//in case of customer
                                {
                                    order = bl?.Order.GetDetailsOrderCustomer(id) ?? throw new ObgectNullableException();
                                }
                                else//in case that the input is invalid(not m or c
                                {
                                    throw new BO.InvalidVariableException();
                                }
                                Console.WriteLine(order);//prints the order
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 3://update order as delivered
                        {
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter ID of order to update");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                order = bl?.Order.DeliveredOrder(id) ?? throw new ObgectNullableException();
                                Console.WriteLine(order);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 4://update order as an arrived
                        {
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter ID of order to update");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                order = bl?.Order.ArrivedOrder(id) ?? throw new ObgectNullableException();
                                Console.WriteLine(order);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 5://track afetr an order
                        {
                            try
                            {
                                BO.OrderTracking orderTrack = new BO.OrderTracking();
                                Console.WriteLine("Enter ID of order to track");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                orderTrack = bl?.Order.OrderTracking(id) ?? throw new ObgectNullableException();
                                Console.WriteLine(orderTrack);
                            }
                            catch (Exception m)
                            {
                                Console.WriteLine(m);
                            }
                            break;
                        }
                    case 6://update amount of product in order
                        {
                            try
                            {
                                BO.Order order = new BO.Order();
                                Console.WriteLine("Enter the ID of order");
                                int idO;
                                int.TryParse(Console.ReadLine(), out idO);
                                Console.WriteLine("Enter the ID of product");
                                int idP;
                                int.TryParse(Console.ReadLine(), out idP);
                                Console.WriteLine("Enter the new amount");
                                int amount;
                                int.TryParse(Console.ReadLine(), out amount);
                                order = bl?.Order.UpdateOrder(idO, idP, amount) ?? throw new ObgectNullableException();
                                Console.WriteLine(order);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            break;
                        }
                    case 7:
                        {
                            mainActions();
                            break;
                        }
                }
            }


        }
        void manageCart()
        {
            int option = 0;//resets the variable
            try
            {
                c = new Cart();
                c = createCart(); //create new cart
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Enter the details again");
                manageCart();
            }
            while (option != 4)
            {
                
                Console.WriteLine($@"Enter
1 to add product to cart
2 to update product amount
3 to make an order
4 to exit");
                int.TryParse(Console.ReadLine(), out option);
                while (option < 1 | option > 4)
                {
                    Console.WriteLine("Enter valid number");
                    int.TryParse(Console.ReadLine(), out option);
                }
                switch (option)
                {
                    case 1://add product to the cart
                        {
                            try
                            {
                                Console.WriteLine("Enter the ID of product");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                c = bl?.Cart.AddProductToCart(c, id) ?? throw new ObgectNullableException();
                                Console.WriteLine(c);
                            }
                            catch (Exception m)
                            {
                                Console.WriteLine(m);
                            }
                            break;

                        }
                    case 2://update amount of product in cart
                        {
                            try
                            {
                                Console.WriteLine("Enter product ID");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine("Enter the new Amount");
                                int amount;
                                int.TryParse(Console.ReadLine(), out amount);
                                c = bl?.Cart.UpdateProductAmount(c, id, amount) ?? throw new ObgectNullableException();
                                Console.WriteLine(c);
                            }
                            catch (Exception m)
                            {
                                Console.WriteLine(m);
                            }
                            break;
                        }
                    case 3://make an order by the cart
                        {
                            try
                            {
                                BO.Order order = bl?.Cart.MakeOrder(c, c.CustomerAdress!, c.CustomerName!, c.CustomerEmail!) ?? throw new ObgectNullableException();
                                Console.WriteLine(order);
                            }
                            catch (Exception m)
                            {
                                Console.WriteLine(m);
                            }
                            break;
                        }
                    case 4://exit the actions on cart
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
                choice = Console.ReadLine()??throw new BO.InvalidVariableException();

            } while (choice != "exit");
        }

    }
}








