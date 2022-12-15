using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// The method asking for list of products
    /// </summary>
    /// <returns></returns>List<ProductForList>
    public List<ProductForList?> GetListOfProduct();
    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>id of product
    /// <returns></returns>Product
    public BO.Product GetProductManager(int ID);
    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>ID of product
    /// <param name="cart"></param>cart of the customer
    /// <returns></returns>ProductItem
    public ProductItem GetProductCustomer(int ID, BO.Cart cart);
    public void AddProduct(BO.Product product);
    /// <summary>
    /// Updates sproduct in the store.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>

    public void UpdatingProductDetails(BO.Product product);
    /// <summary>
    /// The method delete product from the store
    /// </summary>
    /// <param name="ID"></param>Integer
    /// <exception cref="Exception"></exception>
    public void DeleteProduct(int ID);
    /// <summary>
    /// return a list of products by filtering them
    /// </summary>
    /// <param name="c"></param>category of the product
    /// <returns></returns>list of the products
    public List<BO.ProductForList?>? GetProductByCondition(Func<BO.ProductForList?, bool> f);

}


