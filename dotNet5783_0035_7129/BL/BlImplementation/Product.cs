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

internal class Product:BlApi.IProduct
{
    IDal _dal = new DalList();

    /// <summary>
    /// The method asking for list of products
    /// </summary>
    /// <returns></returns>List<ProductForList>
    public List<ProductForList> GetListOfProduct()
    {
        try
        {
            IEnumerable<DO.Product> list = _dal.Product.PrintAll();
            List<ProductForList> productList = new List<ProductForList>();
            foreach (DO.Product p in list)
            {
                ProductForList listProducts = new ProductForList
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    Category = (Enums.Category)p.Category
                };
                productList.Add(listProducts);
            }
            return productList;
        }
        catch(BO.ListIsEmptyException m)
        {
            throw m;
        }
        catch(Exception m)
        {
            throw m;
        }
    }
    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>id of product
    /// <returns></returns>Product
    public BO.Product GetProductManager(int ID)
    {
        if (ID < 0)
            throw new BO.InvalidVariableException();
        else
        {           
            try
            {
                DO.Product p = _dal.Product.PrintByID(ID);
                BO.Product product = new BO.Product
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    Category = (Enums.Category)p.Category,
                    InStock = p.InStock
                };
                return product;
            }
            catch(BO.IdDoesNotExistException m)
            {
                throw m;
            }
            catch (Exception message)
            {
                throw message;
            }
        }
    }
    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>ID of product
    /// <returns></returns>ProductItem
    public ProductItem GetProductCustomer(int ID,BO.Cart cart)
    {
        if (ID < 0)
            throw new BO.InvalidVariableException();
        else
        {          
            try
            {
                DO.Product p = _dal.Product.PrintByID(ID);
                bool inStock1 = false;
                if(p.InStock>0)
                    inStock1 = true;
                ProductItem product = new ProductItem
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    Category = (Enums.Category)p.Category,
                    InStock = inStock1
                };       
                return product;
            }
            catch(BO.IdDoesNotExistException m)
            {
                throw m;
            }
            catch (Exception message)
            {
                throw message;
            }
        }

    }
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="product"></param>Product to add
    public void AddProduct(DO.Product product)
    {
        try
        {
            if (product.ID > 0 && product.Name != null && product.Price > 0 && product.InStock >= 0)
            {
                int id = _dal.Product.Add(product);
                return;
            }
            throw new BO.InvalidVariableException();
        }
        catch(BO.InvalidVariableException m)
        {
            throw m;
        }
        catch(BO.IdAlreadyExistException m)
        {
            throw m;
        }
        catch (Exception message)
        {
            throw message;
        }
    }
    /// <summary>
    /// Updates product in the store.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>

    public void UpdatingProductDetails(DO.Product product)
    {
        try
        {
            bool update = false;
            if (product.ID > 0 && product.Name != null && product.Price > 0 && product.InStock >= 0)
            {
                update = _dal.Product.Update( product);
            }
            if (update)
                throw new BO.InvalidVariableException();
            return;
        }
        catch(BO.InvalidVariableException m)
        {
            throw m;
        }
        catch(Exception message)
        {
            throw message;
        }
    }
    /// <summary>
    /// The method delete product from the store
    /// </summary>
    /// <param name="ID"></param>Integer
    /// <exception cref="Exception"></exception>
    public void DeleteProduct(int ID)
    {
        if (ID < 0)
            throw new BO.InvalidVariableException();
        try
        {
            IEnumerable<DO.Order> orders = _dal.Order.PrintAll();
            foreach (DO.Order o in orders)
            {
                IEnumerable<DO.OrderItem> orderItems = _dal.OrderItem.PrintAllByOrder(o.ID);
                foreach (DO.OrderItem item in orderItems)
                {
                    if (item.ProductID == ID)
                        throw new BO.CanNotDOActionException();
                }
            }
            if (!_dal.Product.Delete(ID))
                throw new BO.IdDoesNotExistException();
        }
        catch(BO.ListIsEmptyException m)
        {
            throw m;
        }
        catch (BO.CanNotDOActionException m)
        {
            throw m;
        }
        catch (BO.IdDoesNotExistException m)
        {
            throw m;
        }
        catch (Exception m)
        {
            throw m;
        }
    }
}
