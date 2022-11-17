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
    public Product GetProduct(int ID)
    {
        IDal dalList1 = new DalList();
        DO.Product p=dalList1.IProduct.PrintByID(ID);
        Product product = new Product();
        
    }
}
