using BlApi;

namespace BlTest;

internal class BlTest
{
    static void Main()
    {
        IBl bl = new BL();
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