﻿using DO;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>The ID of the new product
    public int Add(Product p)
    {
        for(int i=0;i< Config.nextEmptyProduct;i++)
        {
            if (p.ID == products[i].ID)
            {
                throw new Exception("The ID is in the database already");
            }
        }
        products[Config.nextEmptyProduct] = p;
        ++Config.nextEmptyProduct;
        return products[Config.nextEmptyProduct].ID;
    }
    /// <summary>
    /// Return Product by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>Product
    public Product PrintById(int id)
    {
        for(int i=0;i<Config.nextEmptyProduct;i++)
        {
            if(id== products[i].ID)
            {
                return products[i];
            }
        }
        throw new Exception("The product is not on the database");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>The database of the all products
    public IEnumerable<Product> PrintAll()
    {
        if(Config.nextEmptyProduct==0)
        {
            throw new Exception("There are no products in the database");
        }
        return products;
    }
    /// <summary>
    /// Delete a product from the database by its ID
    /// </summary>
    /// <param name="id"></param>ID of the product to delete
    public bool Delete(int id)
    {
        for(int i=0;i<Config.nextEmptyProduct;i++)
        {
            if(products[i].ID == id)
            {
                products[i] = products[Config.nextEmptyProduct];
                --Config.nextEmptyProduct;
            }
        }
        return false;
    }
    /// <summary>
    /// Update details of product
    /// </summary>
    /// <param name="p"></param>Product
    /// <returns></returns> True if the ID in the database, else return false
    public bool Update(Product p)
    {
        for(int i=0;i<Config.nextEmptyProduct;i++)
        {
            if(p.ID == products[i].ID)
            {
                products[i] = p;
                return true;
            }
        }
        return false;
    }
}
