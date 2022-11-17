﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// ID of product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///Name of product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Price of product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Category of product
    /// </summary>
    public Enum Category { get; set; }
    /// <summary>
    /// check if the product in stock
    /// </summary>
    public bool InStock { get; set; }
    /// <summary>
    /// The amount of the product in the cart
    /// </summary>
    public int AmountInCart { get; set; }
    /// <summary>
    /// The information about the product for the list of the products
    /// </summary>
    /// <returns></returns>string
    public override string ToString() => $@"
       Product ID={ID}: {Name}, 
       category - {Category},
       Price: {Price},
       Stock availability: {InStock},
       Amount in the cart: {AmountInCart}";
}