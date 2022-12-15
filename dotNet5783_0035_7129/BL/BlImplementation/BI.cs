﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    public ICart Cart => new Cart();

    public IProduct Product => new Product();
    public IOrder Order => new Order();
}
