using DO;

namespace Dal;
/// <summary>
/// Class for the source of the data
/// </summary>
static internal class DataSource
{
    /// <summary>
    /// Variable to read random numbers
    /// </summary>
    static readonly int readNumber ;//check resets
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
    static void s_Initialize()
    {

    }



}
