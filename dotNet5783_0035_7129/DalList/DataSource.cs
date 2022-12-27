using DO;
using System.IO;

namespace Dal
{
    internal static class DataSource
    {
        readonly static Random rnd = new Random();
        internal static  List<Product?> products=new List<Product?>();
        internal static List<Order?> orders=new List<Order?>();
        internal static List<OrderItem?> orderItems=new List<OrderItem?>();
        /// <summary>
        /// Constracto
        /// r that initialize the item
        /// </summary>
        static DataSource() { Initialize(); }
        private static int countProductID = 100000;
        private static int countOrderID = 1;
        private static int countOrderItemsID = 1;

        /// <summary>
        /// Intializes the lists of the items
        /// </summary>

        internal static void Initialize()
        {

            for (int i = 0; i < 20; i++)
            {
                int index = 0;
                Product product = new Product();
                product.Category = (Category)rnd.Next(0, 6);

                if (rnd.Next(0, 100) > 5)
                    product.InStock = rnd.Next(100, 250);
                else
                    product.InStock = 0;

                switch (product.Category)
                {
                    case Category.Clothes:
                        product.Name = "" + (ClothesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID =nextCountProductID();
                            product.Price = 4500 - rnd.Next(300, 800);
                        }
                        else
                            i--;
                        break;

                    case Category.Bottles:
                        product.Name = "" + (BottlesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = nextCountProductID();
                            product.Price = 4000 - rnd.Next(100, 2000);
                        }
                        else
                            i--;
                        break;

                    case Category.Toys:
                        product.Name = "" + (ToysType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = nextCountProductID();
                            product.Price = 2500 - rnd.Next(300, 1000);
                        }
                        else
                            i--;
                        break;

                    case Category.Socks:
                        product.Name = "" + (SocksType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = nextCountProductID();
                            product.Price = 1500 - rnd.Next(100, 500);
                        }
                        else
                            i--;
                        break;

                    case Category.Accessories:
                        product.Name = "" + (AccessoriesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID =nextCountProductID();
                            product.Price = 15000 - rnd.Next(1000, 5000);
                        }
                        else
                            i--;
                        break;

                    case Category.BabyCarriages:
                        product.Name = "" + (BabyCarriagesType)rnd.Next(0, 5);
                        index = ProductIndex(product.Name);
                        if (index == -1)
                        {
                            product.ID = nextCountProductID();
                            product.Price = 1000 - rnd.Next(300, 700);
                        }
                        else
                            i--;
                        break;

                    default:
                        break;
                }
                if (index == -1)
                    products.Add(product);
            }

            string[] firstNames = new string[10] { "Yael", "Rachel", "Shilat", "Natan", "Dan", "Hila", "Daniel", "Efrat", "Yair", "Ayala" };
            string[] lastNames = new string[10] { "Roth", "Kahana", "Vays", "Cohen", "Rubin", "Levi", "Mansur", "Perelman", "Mangel", "Sharon" };
            string[] City = new string[10] { "Karmiel", "Bnei Brak", "Netivot", "Tiberias", "Jerusalem", "Beit Shemesh", "Tel Aviv", "Netanya", "Hadera", "Kiryat Shmona" };
            string[] St = new string[10] { "Hshoshanim", "Hertzog", "Najara", "Beit Hadfus", "Zait", "Hertzel", "Tze'elon", "Ktav Sofer", "Yanai", "Ben Gurion" };

            for (int i = 0; i < 100; i++)
            {
                Order order = new Order();
                order.ID = nextCountOrderID();
                order.CustomerName = firstNames[rnd.Next(0, 10)] + " " + lastNames[rnd.Next(0, 10)];
                order.CustomerEmail = order.CustomerName.Replace(" ", String.Empty) + "@gmail.com";
                order.CustomerAdress = City[rnd.Next(0, 10)] + " " + St[rnd.Next(0, 10)] + " " + rnd.Next(1, 150);
                order.OrderDate = DateTime.Now.AddMinutes(rnd.Next(-100, -10));
                if (rnd.Next(0, 100) > 20)
                {
                    order.DeliveredDate = order.OrderDate?.AddMinutes(rnd.Next(10, 100));
                    if (rnd.Next(0, 100) > 40)
                        order.ArrivedDate = order.DeliveredDate?.AddDays(rnd.Next(1, 4));
                    else
                        order.ArrivedDate = null;
                }

                else
                {
                    order.DeliveredDate = null;
                    order.ArrivedDate=null;
                }
                orders.Add(order);
            }
            int y=countOrderID-100;
            for (int i = 0; i < 180; i++)
            {
                Product product = new Product();
                OrderItem orderItem = new OrderItem();
                product = (Product)products[rnd.Next(0, products.Count())]!;
                orderItem.ID = nextCountOrderItemsID();
                orderItem.ProductID = product.ID;
                orderItem.Amount = rnd.Next(1, 11);
                orderItem.OrderID = (++y)%100;
                orderItem.Price = orderItem.Amount * product.Price;
                orderItems.Add(orderItem);
            }
        }
        /// <summary>
        /// Check if the id is in the system
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static int ProductIndex(string name)
        {
            for (int i = 0; i < products.Count(); i++)
            {
                if (products.ElementAt(i)?.Name == name)
                    return i;
            }
            return -1;
        }
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
}


