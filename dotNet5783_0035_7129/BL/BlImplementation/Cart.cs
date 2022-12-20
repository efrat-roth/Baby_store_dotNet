using BlApi;
using BO;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools;

namespace BlImplementation;

internal class Cart:ICart
{
    DalApi.IDal? _dal = DalApi.Factory.Get();
    /// <summary>
    /// The mothod adds new or existing product to the cart.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to add
    /// <returns>finalCart</returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart AddProductToCart(BO.Cart finalCart, int id)
    {
        DO.Product ProductInStore=new DO.Product();
        try
        {
            ProductInStore = _dal?.Product.PrintByID(id) ?? throw new ObgectNullableException();//variable for the product.
        }   
        catch(Exception inner)
        {
            throw new FailedGet(inner);
        }
        BO.OrderItem? orderItem = finalCart?.Items?.FirstOrDefault(o => o?.ProductID == id)??new OrderItem();
        if (orderItem == null)
        {
            DO.OrderItem oi1 = _dal.OrderItem.PrintAll().Last() ?? throw new InvalidVariableException();
            if (ProductInStore.InStock > 0)   //If the product is not on order and is in the store.
            {
                BO.OrderItem newProductInOrder = new BO.OrderItem
                {
                    ID = oi1.ID,
                    Price = ProductInStore.Price,
                    TotalPrice = ProductInStore.Price,
                    ProductID = id,
                    Name = ProductInStore.Name,
                    Amount = 1,
                };

                finalCart?.Items?.Add(newProductInOrder);
                finalCart.TotalPrice += newProductInOrder.Price;
                return finalCart;
            }
        }
        finalCart?.Items?.Remove(orderItem);
        if (ProductInStore.InStock > 0)
        {
            orderItem.Amount++;
            orderItem.TotalPrice += orderItem.TotalPrice;
            finalCart.TotalPrice+=orderItem.Price;
            finalCart.Items?.Add(orderItem);
            return finalCart;
        }
        else
        {
            throw new InvalidVariableException();
        }
           
        throw new CanNotDOActionException();
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
        try { ProductInStore = _dal?.Product.PrintByID(id)?? throw new ObgectNullableException(); } //variable for the product.
        catch(Exception inner) { throw new FailedGet(inner); }
        OrderItem? orderItemOfProduct = finalCart?.Items?.FirstOrDefault(o => o?.ProductID == id);
        if (orderItemOfProduct?.Amount < newAmount)   //if the new amount is bigger.
        {
            if (ProductInStore.InStock >= (newAmount - orderItemOfProduct.Amount))  //if there are enough products in stock                                                                       //it will change the amount of products                                                                        //in the cart.
            {
                orderItemOfProduct.TotalPrice += orderItemOfProduct.Price * (newAmount - orderItemOfProduct.Amount);
                finalCart!.TotalPrice += orderItemOfProduct.Price * (newAmount - orderItemOfProduct.Amount);
                orderItemOfProduct.Amount = newAmount;
                return finalCart;
            }
            throw new CanNotDOActionException();
        }
        else
        if (orderItemOfProduct?.Amount > newAmount)//it will change the amount of products                                               //in the cart.
        {
            orderItemOfProduct.TotalPrice -= orderItemOfProduct.Price * (orderItemOfProduct.Amount - newAmount);
            finalCart!.TotalPrice -= orderItemOfProduct.Price * (orderItemOfProduct.Amount - newAmount);
            orderItemOfProduct.Amount = newAmount;
            return finalCart;
        }
        else if (newAmount == 0)//it will change the amount of products 
                                //in the cart.
        {
            finalCart!.TotalPrice -= orderItemOfProduct!.TotalPrice;
            finalCart?.Items!.Remove(orderItemOfProduct);
            return finalCart!;
        }
        else if (orderItemOfProduct?.Amount == newAmount)
            return finalCart!;
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
        if(finalCart.Items?.Count()==0)
        {
            throw new ListIsEmptyException();
        }   
        IEnumerable<DO.Product?> ProductInStore = _dal?.Product.PrintAll() ?? throw new ObgectNullableException();  //variable for the product.
        if (adress11 == null || name11 == null || emailAdress == null //checks if all the strings fields is correct.
                || emailAdress[0] == '@' || emailAdress[emailAdress.Length - 1] == '@')
            throw new BO.InvalidVariableException();

        bool isExistTab = emailAdress.Contains(' ');//checks if email is correct and hasn't a tab there.
        if (isExistTab)//if the email has tab-throw exception
            throw new BO.InvalidVariableException();

        bool isExistShtrudel = emailAdress.Contains('@');//checks if email is correct and has the @ in their.
        if(!isExistShtrudel)//if the email hasn't @-throw exception
            throw new BO.InvalidVariableException();

        bool ifExist = false;
        OrderItem? WrongAmount = finalCart?.Items?.Find(o => o?.Amount < 0);//checks if the amount is positive
        if (WrongAmount != null)
            throw new InvalidVariableException();

        IEnumerable<OrderItem?>? checkExistProduct = finalCart?.Items?.Where
            (oi => ProductInStore.Any(p=>p?.ID == oi?.ProductID)); //checks if all products is exist in store
        if(!checkExistProduct?.Any()??throw new InvalidVariableException()) //if there is product that is not in the store
            throw new InvalidVariableException();

          if(checkExistProduct!.Any(oi => ProductInStore.Any(p => p?.ID == oi?.ProductID)))//checks if there are products in stock.
            throw new InvalidVariableException();

        // if everything is correct ***
        DO.Order op = _dal.Order.PrintAll().Last() ?? throw new InvalidVariableException();
        DO.Order finalOrder = new DO.Order
        {
            ID= op.ID + 1,
            CustomerAdress = adress11,   //creates new order.
            CustomerName = name11,
            CustomerEmail = emailAdress,
            OrderDate = DateTime.Now,
            DeliveredDate = null,
            ArrivedDate = null,
        };
        try { _dal.Order.Add(finalOrder); } //adds the new order  
        catch (Exception inner) { throw new FailedAdd(inner); }
        IEnumerable<DO.OrderItem> orderitems = from o in finalCart?.Items//converts the all orderItems to DO 
                                               let orderItem111 = new DO.OrderItem()
                                               {
                                                   ID = o!.ID,
                                                   Amount = o.Amount,
                                                   OrderID = finalOrder.ID,
                                                   Price = o.Price,
                                                   ProductID = o.ProductID,
                                               }
                                               select orderItem111;
        try { orderitems.ToList().ForEach(o => _dal.OrderItem.Add(o)); }//insert the order items details to the order items list
        catch (Exception inner) { throw new FailedAdd(inner); }
        IEnumerable<DO.Product> productsInCart=from o in orderitems
                                               let productInCart= _dal.Product.PrintByID(o.ProductID)
                                               select productInCart;
        foreach (BO.OrderItem? o in finalCart.Items)  //insert the order items details to the order items list.

        {
            DO.OrderItem orderItem111 = new DO.OrderItem
            {
                ID = o!.ID,
                Amount = o.Amount,
                OrderID = finalOrder.ID,
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
