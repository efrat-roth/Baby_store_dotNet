using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed public class DalList : IDal
    {
        public IOrder IOrder => new DalOrder();

        public IProduct IProduct => new DalProduct();
        public IOrderItem IOrderItem => new DalOrderItem();

    }
}
