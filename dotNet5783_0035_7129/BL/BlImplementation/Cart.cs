using BlApi;
using BO;

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
    DalApi.IDal _dal = new Dal.DalList();
    /// <summary>
    /// A static variable for the each id product in the order.
    /// </summary>
    static int _randomIdForOrderItem = 0;
    /// <summary>
    /// The mothod adds new or existing product to the cart.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to add
    /// <returns>finalCart</returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart AddProductToCart(BO.Cart finalCart, int id)
    {
        DO.Product ProductInStore;
        try
        {
            ProductInStore = _dal.Product.PrintByID(id);//variable for the product.
        }   
        catch(Exception inner)
        {
            throw new FailedGet(inner);
        }
                     
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
                        throw new InvalidVariableException();
                    }
                
                
                }                           
            }
            if (ProductInStore.InStock > 0)   //If the product is not on order and is in                                              //stock then it will be added to the cart.
            {
                BO.OrderItem newProductInOrder = new BO.OrderItem
                { 
                    ID = _randomIdForOrderItem++,
                    Price = ProductInStore.Price,
                    TotalPrice = ProductInStore.Price,
                    ProductID = id,
                    Name = ProductInStore.Name,
                    Amount = 1,
                };
                
                finalCart.Items.Add(newProductInOrder);
                finalCart.TotalPrice += newProductInOrder.Price;
                return finalCart;
            }
            return finalCart;
        
        

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
        DO.Product ProductInStore;
        try { ProductInStore = _dal.Product.PrintByID(id); } //variable for the product.
        catch(Exception inner) { throw new FailedGet(inner); }                                                               //
        foreach (OrderItem o in finalCart.Items)   //Goes through all products order in the cart.
        {
            if (o.ProductID == id)   //If the product is on order.
            {
                if (o.Amount < newAmount)   //if the new amount is bigger.
                {
                    if (ProductInStore.InStock >= (newAmount-o.Amount))  //if there are enough products in stock                                                                       //it will change the amount of products                                                                        //in the cart.
                    {
                        o.TotalPrice += o.Price * (newAmount - o.Amount);
                        finalCart.TotalPrice += o.Price * (newAmount - o.Amount);
                        o.Amount = newAmount;
                        return finalCart;
                    }
                    throw new CanNotDOActionException();
                }
                else 
                if (o.Amount > newAmount)//it will change the amount of products                                               //in the cart.
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
        throw new CanNotDOActionException();
    }
    /// <summary>
    /// The method makes the order.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="adress11"></param>customer adress
    /// <param name="name11"></param>customer name
    /// <param name="emailAdress"></param>customer email adress
    /// <exception cref="Exception"></exception>

    public DO.Order MakeOrder(BO.Cart finalCart, string adress11, string name11, string emailAdress)
    {
        if(finalCart.Items.Count()==0)
        {
            throw new ListIsEmptyException();
        }   
        IEnumerable<DO.Product> ProductInStore = _dal.Product.PrintAll();  //variable for the product.
        if (adress11 == null || name11 == null || emailAdress == null //checks if all the strings fields is correct.
                || emailAdress[0] == '@' || emailAdress[emailAdress.Length - 1] == '@')
            throw new BO.InvalidVariableException();
        bool isRight = false;
        foreach (char c in emailAdress)//checks if email is correct and has the @ in their.
        {
            if (c == '@')
                isRight = true;
            if (c == ' ')
            {
                isRight = false;
                break;
            }
        }
        if (!isRight)
            throw new BO.InvalidVariableException();

        bool ifExist = false;
        foreach (OrderItem o in finalCart.Items)   //Goes through all products order in the cart.
        {
            if (o.Amount < 0)  //checks if the amount is positive.
                throw new BO.InvalidVariableException();

            foreach (DO.Product temporaryProduct in ProductInStore)  //goes througe all products in store.
            {
                if (o.ProductID == temporaryProduct.ID)  //checks if the product is exist.
                {
                    ifExist = true;
                    if (o.Amount > temporaryProduct.InStock) //checks if there are products in stock.
                        throw new Exception(" ");
                }
            }
        }
        if (!ifExist)
            throw new Exception(" ");

        // if everything is correct ***        
        DO.Order finalOrder = new DO.Order
        {
            CustomerAdress = adress11,   //creates new order.
            CustomerName = name11,
            CustomerEmail = emailAdress,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null,
        };
        int id = 0;
        try { id = _dal.Order.Add(finalOrder); } //adds the new order  
        catch (Exception inner) { throw new FailedAdd(inner); }
        foreach (BO.OrderItem o in finalCart.Items)  //insert the order items details to the order items list.
        {
            DO.OrderItem orderItem111 = new DO.OrderItem
            {
                ID = o.ID,
                Amount = o.Amount,
                OrderID = id,
                Price = o.Price,
                ProductID = o.ProductID,
            };
            try { _dal.OrderItem.Add(orderItem111); }
            catch (Exception inner) { throw new FailedAdd(inner); }
            DO.Product p;
            try { p = _dal.Product.PrintByID(o.ProductID); }
            catch (Exception inner) { throw new FailedGet(inner); }
            p.InStock -= o.Amount;  //reduces the amount of product in stock.
            try { _dal.Product.Update(p); }
            catch (Exception inner) { throw new FailedUpdate(inner); }

        }
        return finalOrder;
    }
        
        

    
}
