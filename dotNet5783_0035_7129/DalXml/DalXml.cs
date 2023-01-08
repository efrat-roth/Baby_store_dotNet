﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
    public IProduct Product { get; } = new Dal.Product();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    public IOrder Order { get; } = new Dal.Order();
}