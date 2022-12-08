using BlApi;
using BO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Product:IProduct
{
    DalApi.IDal _dal = new Dal.DalList();

    /// <summary>
    /// The method asking for list of products
    /// </summary>
    /// <returns></returns>List<ProductForList>
    public List<ProductForList?> GetListOfProduct()
    { 
        IEnumerable<DO.Product?> list = _dal.Product.PrintAll()??new List<DO.Product?>();
        List<ProductForList?> productList = new List<ProductForList?>();
            foreach (DO.Product p in list)
        {
            ProductForList listProducts = new ProductForList
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,

                Category = (Enums.Category?)p.Category
            };
            productList.Add(listProducts);
        }
        return productList;    
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
                DO.Product p = _dal.Product.PrintByID(ID);
                BO.Product product = new BO.Product
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    Category = (Enums.Category?)p.Category,
                    InStock = p.InStock
                };
                return product;
            }
            catch(Exception inner)
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
    public ProductItem GetProductCustomer(int ID,BO.Cart cart)
    {

        try
        {
            DO.Product p = _dal.Product.PrintByID(ID);
            bool inStock1 = false;
            if (p.InStock > 0)
                inStock1 = true;
            ProductItem product = new ProductItem
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (Enums.Category?)p.Category,
                InStock = inStock1
            };
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
    public void AddProduct(DO.Product product)
    {
       
            if (product.ID > 100000 && product.Name != null && product.Price > 0 && product.InStock >= 0)
            {
               try { int id = _dal.Product.Add(product); }
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

    public void UpdatingProductDetails(DO.Product product)
    {

            bool update = false;
            if (product.ID > 100000  && product.Price > 0 && product.InStock >= 0)
            {
                update = _dal.Product.Update( product);
            }
            if (!update)
                throw new BO.InvalidVariableException();
            return;
    }
    /// <summary>
    /// The method delete product from the store
    /// </summary>
    /// <param name="ID"></param>Integer
    /// <exception cref="Exception"></exception>
    public void DeleteProduct(int ID)
    {
        IEnumerable<DO.Order?> orders = _dal.Order.PrintAll()??new List<DO.Order?>();
        foreach (DO.Order o in orders)
        {
            IEnumerable<DO.OrderItem?> orderItems=new List<DO.OrderItem?>();
            try { orderItems = _dal.OrderItem.PrintAll(o=>o?.ID==ID); }
            catch (Exception inner)
            {
                throw new FailedGet(inner);
            }
            foreach (DO.OrderItem? item in orderItems)
            {
                    if (item?.ProductID == ID)
                    throw new BO.CanNotDOActionException();
            }           
        
        }
        if (!_dal.Product.Delete(ID))
            throw new BO.IdDoesNotExistException();       
    }
}
