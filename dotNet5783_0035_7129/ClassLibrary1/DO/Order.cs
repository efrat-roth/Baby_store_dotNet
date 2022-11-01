

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
    /// The date the order was created.
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// The date of the ship day.
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// The date of the delivery day
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

}
