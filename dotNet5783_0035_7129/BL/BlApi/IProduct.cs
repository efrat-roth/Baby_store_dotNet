using BO;
using Dal;
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
    public List<ProductForList> GetListOfProduct()
    {
        IDal dalList1 = new DalList();
        IEnumerable<DO.Product> list = dalList1.IProduct.PrintAll();
        List<ProductForList> productList = new List<ProductForList>();
        foreach(DO.Product p in list)
        {
            ProductForList listProducts=new ProductForList();
            listProducts.ID = p.ID;
            listProducts.Name=p.Name;
            listProducts.Price=p.Price;
            listProducts.Category = (Enums.Category)p.Category;
            productList.Add(listProducts);
        }
        return productList;
    }
    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>id of product
    /// <returns></returns>Product
    public Product GetProductManager(int ID)
    {
        if (ID < 0)
            throw new Exception("The id is invalid");
        else
        {
            IDal dalList1 = new DalList();
            try
            {
                DO.Product p = dalList1.IProduct.PrintByID(ID);
                Product product = new Product();

                product.ID = p.ID;
                product.Name = p.Name;
                product.Price = p.Price;
                product.Category = (Enums.Category)p.Category;
                product.InStock = p.InStock;
                return product;
            }
            catch (Exception message)
            {
                Console.WriteLine(message);
                return null;
            }
        }
    }
    /// <summary>
    /// The method return details of product
    /// </summary>
    /// <param name="ID"></param>ID of product
    /// <returns></returns>ProductItem
    public ProductItem GetProductCustomer(int ID)
    {
        if (ID < 0)
            throw new Exception("The id is invalid");
        else
        {
            IDal dalList1 = new DalList();
            try
            {
                DO.Product p = dalList1.IProduct.PrintByID(ID);
                ProductItem product = new ProductItem();
                product.ID = p.ID;
                product.Name = p.Name;
                product.Price = p.Price;
                product.Category = (Enums.Category)p.Category;
                product.InStock = p.InStock;
                return product;
            }
            catch (Exception message)
            {
                Console.WriteLine(message);
                return null;
            }
        }
    }
    public 
}
