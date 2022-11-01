
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// stucture for the products of the store.
/// </summary>

public struct Product
{
    /// <summary>
    /// uniqe ID for each product.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// uniqe name for each product.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the category of the product.
    /// </summary>
    public Enum? Category { get; set; }
    /// <summary>
    /// uniqe price for each product.
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// the amount of each product in stock.
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
