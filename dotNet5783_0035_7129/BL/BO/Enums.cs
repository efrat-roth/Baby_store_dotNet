﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO

{
    /// <summary>
    /// Enum for the categories of the products
    /// </summary>
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages }
    /// <summary>
    /// Enum for the status of the order.
    /// </summary>
    public enum OrderStatus { ConfirmedOrder, DeliveredOrder, ArrivedOrder }
    public enum OrderEnum { GetList = 1, Details, Delivered, Arrived, Tracking, Update, Exit }
    public enum ProductEnum { getlp = 1, getpm, getp, add, up, del, exit }
    public enum CartEnum { Add = 1, Update, Make, Exit }
}

