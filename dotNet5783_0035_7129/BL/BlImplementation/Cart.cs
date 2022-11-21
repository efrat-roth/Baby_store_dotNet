using BlApi;
using BO;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to add
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
                    if(ProductInStore.InStock > 0)   //If the product is in stock then it will add to the cart.
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
                BO.OrderItem newProductInOrder = new BO.OrderItem();
                newProductInOrder.ID = randomIdForOrderItem++;
                newProductInOrder.Price = ProductInStore.Price;
                newProductInOrder.TotalPrice = ProductInStore.Price;
                newProductInOrder.ProductID = id;
                newProductInOrder.Name = ProductInStore.Name;
                newProductInOrder.Amount++;
                finalCart.Items.Add(newProductInOrder);
                finalCart.TotalPrice += newProductInOrder.Price;
                return finalCart;
            }
            throw new Exception("");
        }
        catch (Exception) { throw new Exception(" "); }       
    }

    /// <summary>
    /// The method uptades the amount of the given product in cart to the amount that was given.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to change amount
    /// <param name="newAmount"></param>new amount of product in cart.
    /// <returns></returns>the updated cart
    /// <exception cref="Exception"></exception>
    public BO.Cart UpdateProductAmount(BO.Cart finalCart, int id, int newAmount)
    {
        DO.Product ProductInStore = dalList1.IProduct.PrintByID(id);  //variable for the product.            
        foreach (OrderItem o in finalCart.Items)   //Goes through all products order in the cart.
        {
            if (o.ProductID == id)   //If the product is on order.
            {
                if (o.Amount < newAmount)   //if the new amount is bigger.
                {
                    if (ProductInStore.InStock >= (newAmount-o.Amount))  //if there are enough products in stock
                                                                        //it will change the amount of products 
                                                                        //in the cart.
                    {
                        o.TotalPrice += o.Price * (newAmount - o.Amount);
                        finalCart.TotalPrice += o.Price * (newAmount - o.Amount);
                        o.Amount = newAmount;
                        return finalCart;
                    }
                    throw new Exception(" ");
                }
                else if (o.Amount > newAmount)//it will change the amount of products 
                                              //in the cart.
                {
                    o.TotalPrice -= o.Price * (o.Amount - newAmount);
                    finalCart.TotalPrice -= o.Price * (o.Amount - newAmount);
                    o.Amount = newAmount;
                    return finalCart;
                }
                else if (newAmount == 0)//it will change the amount of products 
                                        //in the cart.
                {
                    finalCart.TotalPrice -= o.TotalPrice;
                    finalCart.Items.Remove(o);
                    return finalCart;
                }
                else if (o.Amount == newAmount)
                    return finalCart;
            }
        }
        throw new Exception(" ");
    }

    public void MakeOrder(BO.Cart finalCart, string adress11, string name11, string emailAdress)
    {
        try
        {
            IEnumerable<DO.Product> ProductInStore = dalList1.IProduct.PrintAll();  //variable for the product.

            if (adress11 == null || name11 == null || emailAdress == null //checks if all the strings fields is correct.
                || emailAdress[0] == '@' || emailAdress[emailAdress.Length - 1] == '@')
                throw new Exception(" ");

            bool isRight = false;
            foreach (char c in emailAdress)//checks if email is correct and has the @ in their.
                if (c == '@')
                    isRight = true;
            if (!isRight)
                throw new Exception(" ");

            bool ifExist = false;
            foreach (OrderItem o in finalCart.Items)   //Goes through all products order in the cart.
            {
                if (o.Amount < 0)  //checks if the amount is positive.
                    throw new Exception(" ");

                foreach (DO.Product temporaryProduct in ProductInStore)  //goes througe all products in store.
                {
                    if (o.ProductID == temporaryProduct.ID)  //checks if the product is exist.
                    {
                        ifExist = true;
                        if (o.Amount > temporaryProduct.InStock)   //checks if there are products in stock.
                            throw new Exception(" ");
                    }
                }
            }
            if (!ifExist)
                throw new Exception(" ");

            // if everything is correct ***        
            DO.Order finalOrder = new DO.Order();
            finalOrder.CustomerAdress = adress11;   //creates new order.
            finalOrder.CustomerName = name11;
            finalOrder.CustomerEmail = emailAdress;
            finalOrder.OrderDate = DateTime.Now;
            finalOrder.ShipDate = 0;
            finalOrder.DeliveryDate = 0;
            int id = dalList1.IOrder.Add(finalOrder);   //adds the new order  
            DO.OrderItem orderItem111 = new DO.OrderItem();
            foreach (BO.OrderItem o in finalCart.Items)  //insert the order items details to the order items list.
            {
                orderItem111.ID = o.ID;
                orderItem111.Amount = o.Amount;
                orderItem111.OrderID = id;
                orderItem111.Price = o.Price;
                orderItem111.ProductID = o.ProductID;
                dalList1.IOrderItem.Add(orderItem111);
                DO.Product p = dalList1.IProduct.PrintByID(o.ProductID);
                p.InStock -= o.Amount;  //reduces the amount of product in stock.
            }
        }
        catch (Exception)
        {
            throw new Exception(" ");
        }

    }
}
