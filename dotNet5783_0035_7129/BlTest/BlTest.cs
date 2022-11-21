using System.Net.Mail;
using BlApi;
using BO;

namespace BlTest;

internal class BlTest
{
    static void Main(string[] args)
    {
        IBl bl = new BL();
        Cart createCart()
        {
            Console.WriteLine("Enter Customer Name");
            String name = Console.ReadLine();
            Console.WriteLine("Enter Customer Email");
            String email = Console.ReadLine();
            bool isRight = false;
            foreach (char c in email)//checks if email is correct and has the @ in their.
                if (c == '@')
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
        mainActions();
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
                            try
                            {
                                List<OrderForList> list = new List<OrderForList>();
                                list = bl.Order.GetListOfOrders();
                                foreach (OrderForList order in list)//print the all orderItem
                                {
                                    Console.WriteLine(order);
                                }
                            }
                            catch(Exception e)
                            {

                            }
                            break;
                        }
                    case Enums.OrderEnum.Details://return details of order
                        {
                            Console.WriteLine("Enter m if yoy are mannager, and c for cstumer");
                            char identity=char.Parse(Console.ReadLine());
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
                            catch(Exception N)
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
                            catch(Exception n)
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
                            catch(Exception n)
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
                            catch(InvalidVariableException m)
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
                            catch(Exception n)
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
            Enums.CartEnum option = Enums.CartEnum.Update;
  
            while (option != Enums.CartEnum.Exit)
            {
                Cart c=new Cart();
                try
                {
                    c = createCart();
                }
                catch(Exception m)
                {
                    
                }
                Console.WriteLine($@"Enter
Add to add product to cart
Update to update product amount
Make to make an order");
                option = (Enums.CartEnum)Console.Read();
                switch (option)
                {
                    case Enums.CartEnum.Add:
                        {
                            try
                            {
                                Console.WriteLine("Enter the ID of product");
                                int id = Console.Read();
                                c = bl.Cart.AddProductToCart(c, id);
                                Console.WriteLine(c);
                            }
                            catch(Exception m)
                            { }
                            break;

                        }
                    case Enums.CartEnum.Update:
                        {
                            
                            break;
                        }
                    case Enums.CartEnum.Make:
                        {
                            break;
                        }
                    case Enums.CartEnum.Exit:
                        {
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




            void manageProduct()
            {
                Enums.ProductEnum choice = Enums.ProductEnum.getlp;
                while (choice != Enums.ProductEnum.exit)
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
                        case Enums.ProductEnum.getlp:
                            {

                                break;
                            }


                    }
                }

            }






        }








}