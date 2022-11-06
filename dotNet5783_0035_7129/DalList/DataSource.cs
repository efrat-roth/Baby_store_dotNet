using DO;

namespace Dal;
/// <summary>
/// Class for the source of the data
/// </summary>
static internal class DataSource
{
    /// <summary>
    /// Constractor of the DataSource class
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// Variable to read random numbers
    /// </summary>
    public static readonly Random random = new Random();//check resets
    /// <summary>
    /// List of the products
    /// </summary>
    static internal List<Product> products;
    /// <summary>
    /// List of the orders
    /// </summary>
    static internal List<Order> orders;
    /// <summary>
    /// List of the items in order
    /// </summary>
    static internal List<OrderItem> orderItems;
    /// <summary>
    /// Adding new product to the products
    /// </summary>
    /// <param name="p"></param>
    static void addProduct(Product p)
    {
        products.Add(p);
    }
    /// <summary>
    /// Adding new order to the orders
    /// </summary>
    /// <param name="o"></param>
    static void addOrder(Order o)
    {
        orders.Add(o);
    }
    /// <summary>
    /// Adding new item to the orderItem list
    /// </summary>
    /// <param name="oi"></param>
    static void addOrderItem(OrderItem oi)
    {
        orderItems.Add(oi);
    }
    /// <summary>
    /// Function that initialize the data values 
    /// </summary>
    private static void s_Initialize()
    {
        Product p = new Product();
        Order o = new Order();
        OrderItem oi = new OrderItem();
        addProduct(p);
        addOrderItem(oi);
        addOrder(o);
        Order order.ShipDate = DateTime.Minvalue;

    }

    internal const int s_startOrderNumber = 1000;
    private static int s_nextOrderNumber = s_startOrderNumber;
    internal static int s_NextOrderNumber { get => ++s_nextOrderNumber; }


}
////**************************************//kk
//using DO;
//using System.IO;

//namespace Dal
//{
//    internal static class DataSource
//    {
//        readonly static Random rnd = new Random();
//        internal static Product[] products = new Product[25];
//        internal static Order[] orders = new Order[120];
//        internal static OrderItem[] orderItems = new OrderItem[250];

//        static DataSource() { Initialize(); }

//        internal class Config
//        {
//            private static int countProductID = 100000;
//            private static int countOrderID = 1;
//            private static int countOrderItemsID = 1;
//            internal static int nextEmptyProduct = 0;
//            internal static int nextEmptyOrder = 0;
//            internal static int nextEmptyOrderItem = 0;

//            internal static int nextCountProductID()
//            {
//                return countProductID++;
//            }

//            internal static int nextCountOrderID()
//            {
//                return countOrderID++;
//            }
//            internal static int nextCountOrderItemsID()
//            {
//                return countOrderItemsID++;
//            }
//        }

//        internal static void Initialize()
//        {

//            for (int i = 0; i < 20; i++)
//            {
//                int index = 0;
//                Product product = new Product();
//                product.Category = (Category)rnd.Next(1, 7);

//                if (rnd.Next(0, 100) > 5)
//                    product.InStock = rnd.Next(100, 250);
//                else
//                    product.InStock = 0;

//                switch (product.Category)
//                {
//                    case Category.Phone:
//                        product.Name = "" + (PhoneType)rnd.Next(0, 5);
//                        index = ProductIndex(product.Name);
//                        if (index == -1)
//                        {
//                            product.ID = Config.nextCountProductID();
//                            product.Price = 4500 - rnd.Next(300, 800);
//                        }
//                        else
//                            i--;
//                        break;

//                    case Category.Computer:
//                        product.Name = "" + (ComputerType)rnd.Next(0, 5);
//                        index = ProductIndex(product.Name);
//                        if (index == -1)
//                        {
//                            product.ID = Config.nextCountProductID();
//                            product.Price = 4000 - rnd.Next(100, 2000);
//                        }
//                        else
//                            i--;
//                        break;

//                    case Category.Tablet:
//                        product.Name = "" + (TabletType)rnd.Next(0, 5);
//                        index = ProductIndex(product.Name);
//                        if (index == -1)
//                        {
//                            product.ID = Config.nextCountProductID();
//                            product.Price = 2500 - rnd.Next(300, 1000);
//                        }
//                        else
//                            i--;
//                        break;

//                    case Category.Watch:
//                        product.Name = "" + (WatchType)rnd.Next(0, 5);
//                        index = ProductIndex(product.Name);
//                        if (index == -1)
//                        {
//                            product.ID = Config.nextCountProductID();
//                            product.Price = 1500 - rnd.Next(100, 500);
//                        }
//                        else
//                            i--;
//                        break;

//                    case Category.TV:
//                        product.Name = "" + (TVTipe)rnd.Next(0, 5);
//                        index = ProductIndex(product.Name);
//                        if (index == -1)
//                        {
//                            product.ID = Config.nextCountProductID();
//                            product.Price = 15000 - rnd.Next(1000, 5000);
//                        }
//                        else
//                            i--;
//                        break;

//                    case Category.EarPhones:
//                        product.Name = "" + (EarPhonesTipe)rnd.Next(0, 5);
//                        index = ProductIndex(product.Name);
//                        if (index == -1)
//                        {
//                            product.ID = Config.nextCountProductID();
//                            product.Price = 1000 - rnd.Next(300, 700);
//                        }
//                        else
//                            i--;
//                        break;

//                    default:
//                        break;
//                }
//                if (index == -1)
//                    products[Config.nextEmptyProduct++] = product;
//            }

//            string[] firstNames = new string[10] { "Yonatan", "Doron", "Arie", "Yosef", "Talya", "Hila", "Dvora", "Efrat", "Avraham", "Dina" };
//            string[] lastNames = new string[10] { "Levy", "Cohen", "Zilberstein", "Tzanani", "Sharabi", "Drori", "Ben tov", "Buhnic", "Shlomo", "Sinay" };
//            string[] City = new string[10] { "Jerusalem", "Tel Aviv", "Ashdod", "Haifa", "Beer Sheva", "Eilat", "Petah Tikva", "Bney Brak", "Ramat Gan", "Tveria" };
//            string[] St = new string[10] { "Hafet Haim", "Herzel", "Hashlosha", "Tveria", "Hagiborim", "Ezra", "Havradim", "Hahagana", "Tzfat", "Tora Vaavoda" };

//            for (int i = 0; i < 100; i++)
//            {
//                Order order = new Order();
//                order.ID = Config.nextCountOrderID();
//                order.CustomerName = firstNames[rnd.Next(0, 10)] + " " + lastNames[rnd.Next(0, 10)];
//                order.CustomerEmail = order.CustomerName.Replace(" ", String.Empty) + "@gmail.com";
//                order.CustomerAdress = City[rnd.Next(0, 10)] + " " + St[rnd.Next(0, 10)] + " " + rnd.Next(1, 150);
//                order.OrderDate = DateTime.Now.AddMinutes(rnd.Next(-100, -10));
//                if (rnd.Next(0, 100) > 20)
//                {
//                    order.shipDate = order.OrderDate.AddMinutes(rnd.Next(10, 100));
//                    if (rnd.Next(0, 100) > 40)
//                        order.deliveryDate = order.shipDate.AddDays(rnd.Next(1, 4));
//                    else
//                        order.deliveryDate = DateTime.MinValue;
//                }

//                else
//                {
//                    order.shipDate = DateTime.MinValue;
//                    order.deliveryDate = DateTime.MinValue;
//                }
//                orders[Config.nextEmptyOrder++] = order;
//            }

//            for (int i = 0; i < 200; i++)
//            {
//                Product product = new Product();
//                OrderItem orderItem = new OrderItem();
//                product = products[rnd.Next(0, Config.nextEmptyProduct)];
//                orderItem.ID = Config.nextCountOrderItemsID();
//                orderItem.ProductID = product.ID;
//                orderItem.Amount = rnd.Next(1, 11);
//                orderItem.OrderID = orders[rnd.Next(0, Config.nextEmptyOrder)].ID;
//                orderItem.Price = orderItem.Amount * product.Price;
//                orderItems[Config.nextEmptyOrderItem++] = orderItem;
//            }
//        }

//        internal static int ProductIndex(string name)
//        {
//            for (int i = 0; i <= Config.nextEmptyProduct; i++)
//            {
//                if (products[i].Name == name)
//                    return i;
//            }
//            return -1;
//        }
//    }
//}

