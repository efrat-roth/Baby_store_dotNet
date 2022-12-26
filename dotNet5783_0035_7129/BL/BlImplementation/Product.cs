using AutoMapper;
using BlApi;
using BO;
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal? _dal = DalApi.Factory.Get();
    /// <summary>
    /// The method asking for list of products
    /// </summary>
    /// <returns></returns>List<ProductForList>
    public List<ProductForList?> GetListOfProduct()
    {

        IEnumerable<DO.Product?> list= new List<DO.Product?>();
        try { list = _dal?.Product.PrintAll() ?? new List<DO.Product?>(); }//gets the all product
        catch(Exception inner) { throw new FailedGet(inner); }
        IEnumerable<ProductForList?> productList;
        productList = from product in list//for each product, convert ot to the wanted type
                      orderby product?.ID
                      select new ProductForList()
                      {
                          ID = product?.ID ?? throw new BO.ObgectNullableException(),
                          Name = product?.Name,
                          Category = (BO.Category?)product?.Category,
                          Price = product?.Price ?? throw new BO.ObgectNullableException()
                      };
        return productList.ToList();

    }

    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>id of product
    /// <returns></returns>Product
    public BO.Product GetProductManager(int ID)
    {
        try
        {
            DO.Product p = _dal?.Product.PrintByID(ID) ?? throw new BO.ObgectNullableException();//get the wanted product
            BO.Product product = new BO.Product()//create the product to return
            {
                ID = p.ID,
                InStock = p.InStock,
                category = (BO.Category?)p.Category,
                Name = p.Name,
                Price = p.Price
            };
            return product;
        }
        catch (Exception inner)
        {
            throw new FailedGet(inner);
        }

    }

    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>ID of product
    /// <param name="cart"></param>cart of the customer
    /// <returns></returns>ProductItem
    public ProductItem GetProductCustomer(int ID, BO.Cart cart)
    {
        try
        {
            DO.Product p = _dal?.Product.PrintByID(ID) ?? throw new BO.ObgectNullableException();//gets the wanted product
            bool inStock1 = false;
            if (p.InStock > 0)
                inStock1 = true;

            ProductItem? product = new ProductItem()//create the product to return
            {
                ID = p.ID,
                Category = (BO.Category?)p.Category,
                Name = p.Name,
                Price = p.Price
            };
            BO.OrderItem? oi = cart?.Items?.FirstOrDefault(p => p?.ProductID == product.ID);//get the item in the cart that represent the wanted product
            if(oi != null)//ewswt the amount of the product in the cart
            {
                product.AmountInCart = oi.Amount;
            }
            product.InStock = inStock1;   
            return product;
        }
        catch (Exception inner)
        {
            throw new FailedGet(inner);
        }
    }

    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="product"></param>Product to add
    public void AddProduct(BO.Product product)
    {

        if (product.ID >= 100000 && product.Name != null && product.Price > 0 && product.InStock >= 0)//if the details of the product are OK
        {
            try {

                DO.Product p = new DO.Product {//convert the product to DO
                ID=product.ID,
                Name=product.Name,
                Category=(DO.Category)product.category!,
                Price=product.Price,
                InStock=product.InStock
                };
                int id = _dal?.Product.Add(p) ?? throw new BO.ObgectNullableException();//Adding the product
            }
            catch (Exception inner)
            {
                throw new FailedAdd(inner);
            }
            return;
        }
        throw new BO.InvalidVariableException();
    }

    /// <summary>
    /// Updates product in the store.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>

    public void UpdatingProductDetails(BO.Product product)
    {
        try
        {
            bool update = false;
            if (product.ID >= 100000 && product.Price > 0 && product.InStock >= 0)//if the details are OK.
            {
                DO.Product p = new DO.Product//convet to DO type
                {
                    ID = product.ID,
                    Name = product.Name,
                    Category = (DO.Category)product.category!,
                    Price = product.Price,
                    InStock = product.InStock
                };
                update = _dal?.Product.Update(p) ?? throw new BO.ObgectNullableException();//update the product
            }
            if (!update)
                throw new BO.InvalidVariableException();
        }
        catch(Exception inner)
        {
            throw new FailedUpdate(inner);
        }
        return;
    }

    /// <summary>
    /// The method delete product from the store
    /// </summary>
    /// <param name="ID"></param>Integer
    /// <exception cref="Exception"></exception>
    public void DeleteProduct(int ID)
    {
        IEnumerable<DO.OrderItem?> orderI;
        try { orderI = _dal?.OrderItem.PrintAll() ?? new List<DO.OrderItem?>(); }//gets the al orderItems
        catch (Exception inner)
        {
            throw new FailedGet(inner);
        }
        DO.OrderItem? exist = orderI.FirstOrDefault(oi => oi?.ProductID == ID) ?? new DO.OrderItem();//check if the product to delete is exist
        if(exist==null)// if the product is not exist
            throw new BO.CanNotDOActionException();
        if (!_dal?.Product.Delete(ID) ?? throw new BO.ObgectNullableException())//delete the product
            throw new BO.IdDoesNotExistException();
    }

    /// <summary>
    /// return a list of products by filtering them
    /// </summary>
    /// <param name="c"></param>Category of the product
    /// <returns></returns>list of the products
    public List<BO.ProductForList?>? GetProductByCondition(Func<BO.ProductForList?,bool>f)
    {
        IEnumerable<DO.Product?> Allproduct = _dal?.Product.PrintAll() ?? new List<DO.Product?>();//gets the all products
        IEnumerable<ProductForList?>? newProducts = from p in Allproduct     //convert the product to ProductForList
                                                    select new ProductForList()
                                                    {
                                                        ID = p?.ID ?? throw new BO.ObgectNullableException(),
                                                        Name = p?.Name,
                                                        Price = p?.Price ?? throw new BO.ObgectNullableException(),
                                                        Category = (BO.Category?)p?.Category,
                                                    };
                                                    

        newProducts = newProducts.Where(p =>f(p));//filters the products by the condition
        return newProducts.ToList();
    }
    
}