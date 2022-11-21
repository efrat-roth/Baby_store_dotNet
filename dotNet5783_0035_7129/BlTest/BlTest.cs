using BlApi;
using BO;

namespace BlTest;

internal class BlTest
{
    static void Main()
    {
        IBl bl = new BL();
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

        void manageProduct()
        {
            string choice = "start";

        }








    }








}