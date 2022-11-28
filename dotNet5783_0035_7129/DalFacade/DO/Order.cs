using static DO.Enums;
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Struct for orders. 
/// </summary>

public struct Order
{
    /// <summary>
    /// Uniqe ID for each product.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of the ordering customer.
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// The email adress of the customer.
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// The adress of the customer.
    /// </summary>
    public string CustomerAdress { get; set; }
    /// <summary>
    /// The  date the order was created.
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// The date of the ship day.
    /// </summary>
    public DateTime? DeliveredDate { get; set; }
    /// <summary>
    /// The date of the delivery day
    /// </summary>
    public DateTime? ArrivedDate { get; set; }
    /// <summary>
    /// The order information.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
       Order ID: {ID}, 
       Customer name: {CustomerName}
       Customer email: {CustomerEmail}
       Date of order: {OrderDate}
       Date of ship: {DeliveredDate}
       Date of delivery: {ArrivedDate}";

}
