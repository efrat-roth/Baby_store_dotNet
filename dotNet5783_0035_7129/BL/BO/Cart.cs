using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class Cart
{
    /// <summary>
    /// The name of the customer.
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// The email adress of the customer.
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// The adress of the customer.
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    ///The list of the order details.
    /// </summary>
    public List<OrderItem?>? Items { get; set; }
    /// <summary>
    /// The total price of the order.
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// Prints all the details of the Cart.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Tools.ToStringProperty(this);
    }

}
