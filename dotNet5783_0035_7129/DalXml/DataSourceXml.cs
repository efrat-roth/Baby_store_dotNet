using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dal;
using DO;
using System.Collections;

using static Dal.DataSourceXml;
using System.Data;

namespace Dal
{
    internal static class DataSourceXml
    {
        /// <summary>
        /// Constractor that initialize the item
        /// </summary>
        static DataSourceXml() { 
            Initialize();
           //SaveProductListLinq(products);
           // SaveOrdertListLinq(orders);
           // SaveOrderItemtListLinq(orderItems);
        }
        readonly static Random rnd = new Random();
        private static XElement intialize;

        internal static string ProductPath = @"Product.xml";
        internal static string OrderPath = @"Order.xml";
        internal static string OrderItemPath = @"OrderItem.xml";
        internal static List<DO.Product?> products = new List<DO.Product?>();
        internal static List<DO.Order?> orders = new List<DO.Order?>();
        internal static List<DO.OrderItem?> orderItems = new List<DO.OrderItem?>();


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
                DO.Product product = new DO.Product();
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
                            product.ID = nextCountProductID();
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
                            product.ID = nextCountProductID();
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
                DO.Order order = new DO.Order();
                order.ID = nextCountOrderID();
                order.CustomerName = firstNames[rnd.Next(0, 10)] + " " + lastNames[rnd.Next(0, 10)];
                order.CustomerEmail = order.CustomerName.Replace(" ", String.Empty) + "@gmail.com";
                order.CustomerAdress = City[rnd.Next(0, 10)] + " " + St[rnd.Next(0, 10)] + " " + rnd.Next(1, 150);
                order.OrderDate = DateTime.Today.AddMinutes(rnd.Next(-100, -10));
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
                    order.ArrivedDate = null;
                }
                orders.Add(order);
            }
            int y = countOrderID - 100;
            for (int i = 0; i < 180; i++)
            {
                DO.Product product = new DO.Product();
                DO.OrderItem orderItem = new DO.OrderItem();
                product = (DO.Product)products[rnd.Next(0, products.Count())]!;
                orderItem.ID = nextCountOrderItemsID();
                orderItem.ProductID = product.ID;
                orderItem.Amount = rnd.Next(1, 11);
                orderItem.OrderID = (++y) % 100;
                orderItem.Price = orderItem.Amount * product.Price;
                orderItems.Add(orderItem);
            }
        }

        private static void SaveProductListLinq(List<DO.Product?> listPruducts)
        {
            var v = from p in listPruducts
                    select new XElement("Product",
                        new XElement("Id", p?.ID),
                        new XElement("Name", p?.Name),
                        new XElement("InStock", p?.InStock),
                        new XElement("Price", p?.Price)
                        );

            intialize = new XElement("Product", v);
            intialize.Save(ProductPath);
        }

        private static void SaveOrdertListLinq(List<DO.Order?> listOrders)
        {
            var v = from p in listOrders
                    select new XElement("Order",
                        new XElement("id", p?.ID),
                        new XElement("Name", p?.CustomerName),
                        new XElement("Email", p?.CustomerEmail),
                        new XElement("Adress", p?.CustomerAdress),
                        new XElement("OrderTime", p?.OrderDate),
                        new XElement("ShipDate", p?.DeliveredDate),
                        new XElement("DeliveryrDate", p?.ArrivedDate)
                        );

            intialize = new XElement("Order", v);
            intialize.Save(OrderPath);
        }

        private static void SaveOrderItemtListLinq(List<DO.OrderItem?> listOrderItems)
        {
            var v = from p in listOrderItems
                    select new XElement("OrderItem",
                        new XElement("id", p?.ID),
                        new XElement("name", p?.ProductID),
                        new XElement("firstName", p?.OrderID),
                        new XElement("lastName", p?.Price),
                        new XElement("name", p?.Amount)
                        );

            intialize = new XElement("OrderItem", v);
            intialize.Save(OrderItemPath);
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