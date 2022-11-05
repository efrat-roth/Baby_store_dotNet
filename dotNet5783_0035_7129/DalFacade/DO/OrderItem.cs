namespace DO;
/// <summary>
/// Struct for the order items from the store.
/// </summary>

public struct OrderItem
{
    /// <summary>
    /// Uniqe ID for each product.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Uniqe ID for each order product.
    /// </summary>
    public int? ProductID { get; set; }
    /// <summary>
    /// Uniqe ID for each order. 
    /// </summary>
    public int? OrderID { get; set; }
    /// <summary>
    /// Price of one product.
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// Amount of product.
    /// </summary>
    public int? amount { get; set; }
}

