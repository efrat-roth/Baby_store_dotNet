using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    /// <summary>
    /// the mothod adds new or existing product to the cart.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to add
    /// <returns>finalCart</returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart AddProductToCart(BO.Cart finalCart, int id);
    /// <summary>
    /// The method uptades the amount of the given product in cart to the amount that was given.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to change amount
    /// <param name="newAmount"></param>new amount of product in cart.
    /// <returns></returns>the updated cart
    /// <exception cref="Exception"></exception>
    public BO.Cart UpdateProductAmount(BO.Cart finalCart, int id, int newAmount);
    /// <summary>
    /// The method makes the order.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="adress11"></param>customer adress
    /// <param name="name11"></param>customer name
    /// <param name="emailAdress"></param>customer email adress
    /// <exception cref="Exception"></exception>
    public DO.Order MakeOrder(BO.Cart finalCart, string adress11, string name11, string emailAdress);
}
