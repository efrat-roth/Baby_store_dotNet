using DO;
using System.IO;

namespace Dal
{
    internal static class DataSource
    {
        readonly static Random rnd = new Random();
        internal static Product[] products = new Product[30];
        internal static Order[] orders = new Order[125];
        internal static OrderItem[] orderItems = new OrderItem[200];
        /// <summary>
        /// Constractor that initialize the item
        /// </summary>
        static DataSource() { Initialize(); }

        internal class Config
        {
            private static int countProductID = 100000;
            private static int countOrderID = 1;
            private static int countOrderItemsID = 1;
            internal static int nextEmptyProduct = 0;
            internal static int nextEmptyOrder = 0;
            internal static int nextEmptyOrderItem = 0;
            /// <summary>
            /// New id for the products
            /// </summary>
            /// <returns></returns>
            internal static int nextCountProductID()
            {
                return countProductID++;
            }
            /// <summary>
            /// New id for the orders
            /// </summary>
            /// <returns></returns>
            internal static int nextCountOrderID()
            {
                return countOrderID++;
            }
            /// <summary>
            /// New id for the orderitems
            /// </summary>
            /// <returns></returns>
            internal static int nextCountOrderItemsID()
            {
                return countOrderItemsID++;
            }
        }
        /// <summary>
        /// Intializes the arrays of the items
        /// </summary>

        internal static void Initialize()
        {

            for (int i = 0; i < 20; i++)
            {
                int index = 0;
                Product product = new Product();
                product.Category = (Enums.Category)rnd.Next(0, 6);

                if (rnd.Next(0, 100) > 5)
                    product.InStock = rnd.Next(100, 250);
                else
                    product.InStock = 0;

                switch (product.Category)
                {
                    case Enums.Category.Clothes:
                        product.Name = "" + (Enums.ClothesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = Config.nextCountProductID();
                            product.Price = 4500 - rnd.Next(300, 800);
                        }
                        else
                            i--;
                        break;

                    case Enums.Category.Bottles:
                        product.Name = "" + (Enums.BottlesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = Config.nextCountProductID();
                            product.Price = 4000 - rnd.Next(100, 2000);
                        }
                        else
                            i--;
                        break;

                    case Enums.Category.Toys:
                        product.Name = "" + (Enums.ToysType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = Config.nextCountProductID();
                            product.Price = 2500 - rnd.Next(300, 1000);
                        }
                        else
                            i--;
                        break;

                    case Enums.Category.Socks:
                        product.Name = "" + (Enums.SocksType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = Config.nextCountProductID();
                            product.Price = 1500 - rnd.Next(100, 500);
                        }
                        else
                            i--;
                        break;

                    case Enums.Category.Accessories:
                        product.Name = "" + (Enums.AccessoriesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = Config.nextCountProductID();
                            product.Price = 15000 - rnd.Next(1000, 5000);
                        }
                        else
                            i--;
                        break;

                    case Enums.Category.BabyCarriages:
                        product.Name = "" + (Enums.BabyCarriagesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = Config.nextCountProductID();
                            product.Price = 1000 - rnd.Next(300, 700);
                        }
                        else
                            i--;
                        break;

                    default:
                        break;
                }
                if (index == -1)
                    products[Config.nextEmptyProduct++] = product;
            }

            string[] firstNames = new string[10] { "Yael", "Rachel", "Shilat", "Natan", "Dan", "Hila", "Daniel", "Efrat", "Yair", "Ayala" };
            string[] lastNames = new string[10] { "Roth", "Kahana", "Vays", "Cohen", "Rubin", "Levi", "Mansur", "Perelman", "Mangel", "Sharon" };
            string[] City = new string[10] { "Karmiel", "Bnei Brak", "Netivot", "Tiberias", "Jerusalem", "Beit Shemesh", "Tel Aviv", "Netanya", "Hadera", "Kiryat Shmona" };
            string[] St = new string[10] { "Hshoshanim", "Hertzog", "Najara", "Beit Hadfus", "Zait", "Hertzel", "Tze'elon", "Ktav Sofer", "Yanai", "Ben Gurion" };

            for (int i = 0; i < 100; i++)
            {
                Order order = new Order();
                order.ID = Config.nextCountOrderID();
                order.CustomerName = firstNames[rnd.Next(0, 10)] + " " + lastNames[rnd.Next(0, 10)];
                order.CustomerEmail = order.CustomerName.Replace(" ", String.Empty) + "@gmail.com";
                order.CustomerAdress = City[rnd.Next(0, 10)] + " " + St[rnd.Next(0, 10)] + " " + rnd.Next(1, 150);
                order.OrderDate = DateTime.Now.AddMinutes(rnd.Next(-100, -10));
                if (rnd.Next(0, 100) > 20)
                {
                    order.ShipDate = order.OrderDate.AddMinutes(rnd.Next(10, 100));
                    if (rnd.Next(0, 100) > 40)
                        order.DeliveryDate = order.ShipDate.AddDays(rnd.Next(1, 4));
                    else
                        order.DeliveryDate = DateTime.MinValue;
                }

                else
                {
                    order.ShipDate = DateTime.MinValue;
                    order.DeliveryDate = DateTime.MinValue;
                }
                orders[Config.nextEmptyOrder++] = order;
            }

            for (int i = 0; i < 180; i++)
            {
                Product product = new Product();
                OrderItem orderItem = new OrderItem();
                product = products[rnd.Next(0, Config.nextEmptyProduct)];
                orderItem.ID = Config.nextCountOrderItemsID();
                orderItem.ProductID = product.ID;
                orderItem.Amount = rnd.Next(1, 11);
                orderItem.OrderID = orders[rnd.Next(0, Config.nextEmptyOrder)].ID;
                orderItem.Price = orderItem.Amount * product.Price;
                orderItems[Config.nextEmptyOrderItem++] = orderItem;
            }
        }
        /// <summary>
        /// Check if the id is in the system
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static int ProductIndex(string name)
        {
            for (int i = 0; i <= Config.nextEmptyProduct; i++)
            {
                if (products[i].Name == name)
                    return i;
            }
            return -1;
        }
    }
}


