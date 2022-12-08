using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    /// <summary>
    /// The id of the order.
    /// </summary>
    public int ID { get; set; }
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
    /// The date of the order.
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// The status of the order.
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// The ship date.
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// The delivery date.
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    ///  The list of the order details.
    /// </summary>
    public List<OrderItem?>? Items { get; set; }
    /// <summary>
    ///  The total price of the order.
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// Prints all the details of the order.
    /// </summary>
    /// <returns></returns>details of the order
    public override string ToString() => $@"
       Id: {ID}
       Customer Name={CustomerName}, 
       Customer Email = {CustomerEmail},
       Customer Adress= {CustomerAdress},
       Order Date: {OrderDate},
       Order Status: {Status},
       Ship Date:{ShipDate},
       Delivery Date:{DeliveryDate},
       Order details:{string.Join('\n', Items)},
       Total Price:{TotalPrice}";
}
