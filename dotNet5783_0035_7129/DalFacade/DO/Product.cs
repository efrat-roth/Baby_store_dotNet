using System.Diagnostics;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Stucture for the products of the store.
/// </summary>

public struct Product
{
    /// <summary>
    /// Uniqe ID for each product.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Uniqe name for each product.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The category of the product.
    /// </summary>
    public Enum? Category { get; set; }
    /// <summary>
    /// Uniqe price for each product.
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// The amount of each product in stock.
    /// </summary>
    public int? InStock { get; set; }
    /// <summary>
    /// The product information.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
       Product ID={ID}: {Name}, 
       category - {Category}
       Price: {Price}
       Amount in stock: {InStock}";

}
