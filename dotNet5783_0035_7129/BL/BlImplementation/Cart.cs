﻿using BlApi;
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

namespace BlImplementation;

internal class Cart:ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// The mothod adds new or existing product to the cart.
    /// </summary>
    /// <param name="finalCart"></param>Cart
    /// <param name="id"></param>id of product to add
    /// <returns>finalCart</returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart AddProductToCart(BO.Cart finalCart, int id)
    {
        DO.Product? ProductInStore=new DO.Product();
        try
        {
            ProductInStore = dal?.Product.GetByID(id);//variable for the product.
        }   
        catch(Exception inner)
        {
            throw new FailedGet(inner);
        }
        BO.OrderItem? orderItem=new BO.OrderItem();
        try
        {
            orderItem = finalCart?.Items?.FirstOrDefault(o => o?.ProductID == id);
        }
        catch (Exception? ex) { throw ex; }
            if (orderItem == null)
            {
                int idOI = dal?.OrderItem.GetAll().Last()?.ID ?? throw new InvalidVariableException();
                bool flag = true;
                while (flag)//While the new id is already exist
                {
                    try { dal.OrderItem.GetByID(idOI); ++idOI; }
                    catch { flag = false; };
                }

                if (ProductInStore?.InStock > 0)   //If the product is not on order and is in the store.
                {
                    BO.OrderItem newProductInOrder = new BO.OrderItem
                    {
                        ID = idOI,
                        Price = ProductInStore?.Price ?? throw new ObgectNullableException(),
                        TotalPrice = ProductInStore?.Price ?? throw new ObgectNullableException(),
                        ProductID = id,
                        Name = ProductInStore?.Name,
                        Amount = 1,

                    };
               
                    finalCart?.Items?.Add(newProductInOrder);
                    finalCart!.TotalPrice += newProductInOrder.Price;
                    return finalCart;
                }
            }

            finalCart?.Items?.Remove(orderItem);
            if (ProductInStore?.InStock > 0)
            {
                orderItem!.Amount++;
                orderItem.TotalPrice += orderItem.TotalPrice;
                finalCart!.TotalPrice += orderItem.Price;
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
        if (id < 0 || newAmount < 0)
            throw new InvalidVariableException();
        DO.Product? ProductInStore=new DO.Product();
        try { ProductInStore = dal?.Product.GetByID(id); } //Variable for the product.
        catch (Exception inner) { throw new FailedGet(inner); }

        int p = ProductInStore?.InStock ?? throw new ObgectNullableException();

        OrderItem? orderItemOfProduct = finalCart?.Items?.FirstOrDefault(o => o?.ProductID == id);
        if (orderItemOfProduct?.Amount < newAmount)   //If the new amount is bigger.
        {
            if (ProductInStore?.InStock >= (newAmount - orderItemOfProduct.Amount))  //If there are enough products in stock                                                                       //it will change the amount of products                                                                        //in the cart.
            {
                orderItemOfProduct.TotalPrice += orderItemOfProduct.Price * (newAmount - orderItemOfProduct.Amount);
                finalCart!.TotalPrice += orderItemOfProduct.Price * (newAmount - orderItemOfProduct.Amount);
                p = p + orderItemOfProduct.Amount;//to update in dal layer
                orderItemOfProduct.Amount = newAmount;
                p = p - newAmount;

                DO.Product ProductUpdate = new DO.Product()
                {
                    Price = orderItemOfProduct.Price,
                    Category = ProductInStore?.Category,
                    ID = ProductInStore?.ID??throw new ObgectNullableException(),
                    Name = ProductInStore?.Name,
                    InStock = p
                };
                dal?.Product.Update(ProductUpdate);
                return finalCart;
            }
            throw new CanNotDOActionException();
        }
        else
        if (orderItemOfProduct?.Amount > newAmount)//It will change the amount of products in the cart.
        {
            orderItemOfProduct.TotalPrice -= orderItemOfProduct.Price * (orderItemOfProduct.Amount - newAmount);
            finalCart!.TotalPrice -= orderItemOfProduct.Price * (orderItemOfProduct.Amount - newAmount);
            p += orderItemOfProduct.Amount;
            orderItemOfProduct.Amount = newAmount;
            p -= newAmount;
            
            DO.Product ProductUpdate = new DO.Product()//update the product
            {
                Price = orderItemOfProduct.Price,
                Category = ProductInStore?.Category,
                ID = ProductInStore?.ID ?? throw new ObgectNullableException(),
                Name = ProductInStore?.Name,
                InStock = p
            };
            dal?.Product.Update(ProductUpdate);
            return finalCart;
        }
        else if (newAmount == 0)//It will change the amount of products                                 
        {
            finalCart!.TotalPrice -= orderItemOfProduct!.TotalPrice;
            finalCart?.Items!.Remove(orderItemOfProduct);

            DO.Product ProductUpdate = new DO.Product()
            {
                Price = orderItemOfProduct.Price,
                Category = ProductInStore?.Category,
                ID = ProductInStore?.ID ?? throw new ObgectNullableException(),
                Name = ProductInStore?.Name,
                InStock = ProductInStore?.InStock+ orderItemOfProduct.Amount??throw new ObgectNullableException(),
            };
            dal?.Product.Update(ProductUpdate);
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
    public BO.Order MakeOrder(BO.Cart finalCart, string adress11, string name11, string emailAdress)
    {
        if (finalCart.Items?.Count() == 0)
        {
            throw new ListIsEmptyException();
        }
        IEnumerable<DO.Product?> ProductInStore = dal?.Product.GetAll() ?? throw new ObgectNullableException();  //variable for the product.
        if (adress11 == null || name11 == null || emailAdress == null //checks if all the strings fields is correct.
                || emailAdress[0] == '@' || emailAdress[emailAdress.Length - 1] == '@')
            throw new BO.InvalidVariableException();

        bool isExistTab = emailAdress.Contains(' ');//checks if email is correct and hasn't a tab there.
        if (isExistTab)//if the email has tab-throw exception
            throw new BO.InvalidVariableException();

        bool isExistShtrudel = emailAdress.Contains('@');//checks if email is correct and has the @ in their.
        if (!isExistShtrudel)//if the email hasn't @-throw exception
            throw new BO.InvalidVariableException();

        OrderItem? WrongAmount = finalCart?.Items?.Find(o => o?.Amount <= 0);//checks if the amount is positive
        if (WrongAmount != null)
            throw new InvalidVariableException();

        IEnumerable<OrderItem?>? checkExistProduct = finalCart?.Items?.Where
            (oi => ProductInStore.Any(p => p?.ID == oi?.ProductID)); //checks if all products is exist in store
        if (!checkExistProduct?.Any() ?? throw new InvalidVariableException()) //if there is product that is not in the store
            throw new InvalidVariableException();

        if (!checkExistProduct!.Any(oi => ProductInStore.Any(p => p?.ID == oi?.ProductID)))//checks if there are products in stock.
            throw new InvalidVariableException();

        // if everything is correct ***
        bool flag1 = false;
        int id1 = dal.Order.GetAll().Last()?.ID + 1 ?? 0; ;
        while (!flag1)//while he id is in he store
        {
            try { DO.Order? o = dal.Order.GetByID(id1); }
            catch (DO.IdDoesNotExistException ) { flag1 = true; break; }
            ++id1;
        }
        DO.Order finalOrder = new DO.Order
        {
            ID =id1,
            CustomerAdress = adress11,   //creates new order.
            CustomerName = name11,
            CustomerEmail = emailAdress,
            OrderDate = DateTime.Today,
            DeliveredDate = null,
            ArrivedDate = null,
        };
        try { dal.Order.Add(finalOrder); } //adds the new order  
        catch (Exception inner) { throw new FailedAdd(inner); }
        IEnumerable<DO.OrderItem> orderitems;        
        bool flag = false;
        int id = dal.OrderItem.GetAll().Last()?.ID + 1 ?? 0; ;
        while (!flag)//while he id is in he store
        {            
            try { DO.OrderItem? o = dal.OrderItem.GetByID(id); }
            catch (DO.IdDoesNotExistException ) { flag = true; break; }
            ++id;
        }
        try{
            orderitems = from o in finalCart?.Items//converts the all orderItems to DO 
                         let orderItem111 = new DO.OrderItem()
                         {
                             ID = id++,
                             Amount = o.Amount,
                             OrderID = finalOrder.ID,
                             Price = o.Price,
                             ProductID = o.ProductID,
                         }
                         let t = dal.OrderItem.Add(orderItem111)
                         select orderItem111;
        
        }
        //Insert the order items details to the order items list.
        catch (Exception inner) { throw new FailedAdd(inner); }
        IEnumerable<DO.Product> productsInCart = new List<DO.Product>();
        try
        {
            productsInCart = from o in orderitems
                             let productInCart = dal.Product.GetByID(o.ProductID)
                             let productToSelect = new DO.Product()//Update the new amount.
                             {
                                 ID = productInCart.ID,
                                 InStock = productInCart.InStock - o.Amount,
                                 Category = productInCart.Category,
                                 Name = productInCart.Name,
                                 Price = productInCart.Price
                             }
                             select productToSelect;
        }
        catch (Exception inner) { throw new FailedGet(inner); }
        try{productsInCart.ToList().ForEach((p => dal.Product.Update(p))); }//Update the new amount of each product in the cart, in the database. 
        catch (Exception inner) { throw new FailedUpdate(inner); }
        BO.Order oToReturn = new BO.Order()
        {
            ID = finalOrder.ID,
            CustomerAdress = finalOrder.CustomerAdress,
            DeliveryDate = finalOrder.ArrivedDate,
            CustomerEmail = finalOrder.CustomerEmail,
            CustomerName = finalOrder.CustomerName,
            Items = finalCart?.Items,
            OrderDate = finalOrder.OrderDate,
            ShipDate = finalOrder.DeliveredDate,
            Status = BO.OrderStatus.ConfirmedOrder,
            TotalPrice = finalCart.TotalPrice
        };
        return oToReturn;
    }
        
        

    
}
