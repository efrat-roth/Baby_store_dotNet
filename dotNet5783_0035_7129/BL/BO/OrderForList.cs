using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// The id of the order.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of the customer
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// The status of the order.
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// The amount of items in the order.
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    ///  The total price of the order.
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// Prints all the details of the list of the order.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
       Id: {ID}
       Customer Name={CustomerName}, 
       Order Status: {Status},
       Amount Of Items:{AmountOfItems},
       Total Price:{TotalPrice}";

}
