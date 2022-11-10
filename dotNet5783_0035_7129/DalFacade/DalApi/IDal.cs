using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface IDal
    {
        IProduct product { get; }
        IOrder order { get; }
        IOrderItem orderitem { get; }


    }
  
}
