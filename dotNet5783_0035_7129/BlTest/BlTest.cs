using BlApi;
using BO;

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
            Enums.ProductEnum choice = Enums.ProductEnum.getlp;
            while(choice != Enums.ProductEnum.exit)
            {
                Console.WriteLine(
                                       $@"Enter
getlp to get the list of products.
getpm to manager to get the details of product.
getpc to customer to get details of product.
add to add product to store.
up to update product in the store.
del to delete product from the store.");
                choice =  (Enums.ProductEnum)Console.Read();
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