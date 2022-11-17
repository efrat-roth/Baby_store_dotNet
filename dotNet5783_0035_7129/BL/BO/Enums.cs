﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Enums
{
    /// <summary>
    /// Enum for the categories of the products
    /// </summary>
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages }
    /// <summary>
    /// Enum for the status of the order.
    /// </summary>
    public enum OrderStatus {ConfirmedOrder, DeliveredOrder, ArrivedOrder }
}