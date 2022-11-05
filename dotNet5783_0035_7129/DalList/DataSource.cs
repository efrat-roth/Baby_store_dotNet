using DO;

namespace Dal
{
    internal static class DataSource
    {
        public static readonly Random random = new Random();
        /// <summary>
        /// Random number only for read.
        /// </summary>
        public static int num;
        /// <summary>
        /// List of products.
        /// </summary>
        internal static List<Product> products;
        /// <summary>
        /// List of orders
        ///</summary>
        internal static List<Order> orders;
        /// <summary>
        /// List of items of order.
        /// </summary>
        internal static List<OrderItem> orederitems;
        /// <summary>
        /// List of enums.
        /// </summary>
        internal static List<Enums> enums;
    }
}
