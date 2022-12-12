using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// The id of the orderItem.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of the product.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The id of the product in the order
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// The price of the product.
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The amount of each product in the order.
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    ///  The total price of the product in the order.
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// Prints all the details of the items in order.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Tools.ToStringProperty(this);
    }
}
