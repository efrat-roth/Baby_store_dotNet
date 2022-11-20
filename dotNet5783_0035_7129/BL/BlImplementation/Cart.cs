using BlApi;
using BO;
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
    /// <summary>
    /// A static variable for the each id product in the order.
    /// </summary>
    static int randomIdForOrderItem = 0;
    /// <summary>
    /// the mothod adds new or existing product to the cart.
    /// </summary>
    /// <param name="finalCart"></param>
    /// <param name="id"></param>
    /// <returns>finalCart</returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart AddProductToCart(BO.Cart finalCart, int id)
    {
        try
        {           
            DO.Product ProductInStore = dalList1.IProduct.PrintByID(id);  //variable for the product.            
            foreach (OrderItem o  in finalCart.Items )   //Goes through all products order in the cart.
            {
                if(o.ProductID == id)   //If the product is on order
                {
                    if(ProductInStore.InStock> 0)   //If the product is in stock then it will add to the cart.
                    {
                        o.Amount++;
                        o.TotalPrice += o.Price;
                        finalCart.TotalPrice += o.Price;
                        return finalCart;
                    }
                    else
                    {
                        throw new Exception("");
                    }                     
                }                            
            }
            if (ProductInStore.InStock > 0)   //If the product is not on order and is in
                                              //stock then it will be added to the cart.
            {
                BO.OrderItem j = new BO.OrderItem();
                j.ID = randomIdForOrderItem++;
                j.Price = ProductInStore.Price;
                j.TotalPrice = ProductInStore.Price;
                j.ProductID = id;
                j.Name = ProductInStore.Name;
                j.Amount++;
                finalCart.Items.Add(j);
                finalCart.TotalPrice += j.Price;
            }
            return finalCart;                            
        }
        catch (Exception) { throw new Exception(" "); }       
    }


    public BO.Cart UpdateProductAmount(BO.Cart finalCart, int id, int newAmount)
    {
        //DO.Product ProductInStore = dalList1.IProduct.PrintByID(id);  //variable for the product.            
        foreach (OrderItem o in finalCart.Items)   //Goes through all products order in the cart.
        {
            if (o.ProductID == id)   //If the product is on order
            {
                if(o.Amount<newAmount)
                {
                    o.Amount=newAmount;
                    o.TotalPrice += o.Price*(newAmount-o.Amount);
                    finalCart.TotalPrice += o.Price*(newAmount - o.Amount);
                    return finalCart;
                }
                else if(o.Amount > newAmount)
                {

                }
            }
        }
    }
}
