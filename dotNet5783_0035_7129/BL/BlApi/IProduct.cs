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
    public void AddProduct(DO.Product product)
    {
        if(product.ID>0 && product.Name!=null && product.Price>0 && product.InStock>=0)
        {
            IDal dalList1 = new DalList();
            int id=dalList1.IProduct.Add(product);
            return;
        }
        throw new Exception("Cannot add the details are wrong"); 
    }
    /// <summary>
    /// Updates sproduct in the store.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>

    public void UpdatingProductDetails(DO.Product product)
    {
        bool update = false;
        if (product.ID > 0 && product.Name != null && product.Price > 0 && product.InStock >= 0)
        {
            IDal dalList1 = new DalList();
            update = dalList1.IProduct.Update(ref product);       
        }
        if(update)
            throw new Exception("Cannot update because the details are wrong");
        return;
    }

   public void DeleteProduct(int ID)
    {
        if (ID < 0)
            throw new Exception("The ID is invalid");
        IDal dalList1 = new DalList();
        IEnumerable<DO.Order> orders= dalList1.IOrder.PrintAll();
        foreach(DO.Order o in orders)
        {
            IEnumerable<DO.OrderItem> orderItems= dalList1.IOrderItem.PrintAllByOrder(o.ID); 
            foreach(DO.OrderItem item in orderItems)
            {
                if (item.ProductID == ID)
                    throw new Exception("The product is in order");
            }
        }
        if (!dalList1.IProduct.Delete(ID))
            throw new Exception("The product is not in the store");
    }
    
    

}
