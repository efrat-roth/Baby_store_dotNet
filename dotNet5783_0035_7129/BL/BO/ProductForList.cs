using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
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
    /// Category of product
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// The information about the product for the list of the products
    /// </summary>
    /// <returns></returns>string
    public override string ToString() 
    {
        return Tools.ToStringProperty(this);
    }

}
