using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
{
    /// <summary>
    /// ID of product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Name of product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Price of product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// category of product
    /// </summary>
    public Category? category { get; set; }
    /// <summary>
    /// Check if the product in stock
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// The product information.
    /// </summary>
    /// <returns></returns> string
    public override string ToString() => $@"
       Product ID={ID}: {Name}, 
       category - {category},
       Price: {Price},
       Amount in stock: {InStock}";
}
