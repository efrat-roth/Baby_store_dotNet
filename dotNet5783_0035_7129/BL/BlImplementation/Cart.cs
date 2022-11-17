using BlApi;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Cart:ICart
{
    IDal dalList1 = new DalList();
    public Cart AddProductToCart(Cart cart1, int id)
    {
        DO.Product productIsExist= dalList1.IProduct.PrintByID(id);
        BO.OrderForList t = new BO.OrderForList();
        if (productIsExist.ID==id)
        {
            if(productIsExist.InStock>0)
            {
                t.AmountOfItems++;
                t.TotalPrice += productIsExist.Price;
            }
            else()
            {

            }
        }


        DO.Order productInCart = dalList1.IOrder.PrintByID(id);

        
    }

}
