

namespace DO;
/// <summary>
/// struct for the order items from the store.
/// </summary>

public struct OrderItem
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int amount { get; set; }
}
