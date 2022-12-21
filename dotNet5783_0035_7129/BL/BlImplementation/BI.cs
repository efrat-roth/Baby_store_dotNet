using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    public ICart Cart { get; } = new Cart();
    public IProduct Product { get; } = new Product();
    public IOrder Order { get; } = new Order();

}
