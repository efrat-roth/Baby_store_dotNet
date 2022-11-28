using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;

internal class DalProduct:IProduct
{
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>The ID of the new product
    public int Add(Product p)
    {
        foreach(Product product in products)
        {
            if (p.ID == product.ID)
            {
                throw new IdAlreadyExistException();
            }
        };
        products.Add(p);
        return p.ID;
    }
    /// <summary>
    /// Return IProduct by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>IProduct
    public Product PrintByID(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        foreach (Product p in products)
        {
            if (id == p.ID)
            {
                return p;
            }
        };
        throw new IdDoesNotExistException();
    }
    /// <summary>
    /// Return the all products
    /// </summary>
    /// <returns></returns>The database of the all products
    public IEnumerable<Product> PrintAll()
    {
       
        return products;
    }
    /// <summary>
    /// Delete a product from the database by its ID
    /// </summary>
    /// <param name="id"></param>ID of the product to delete
    public bool Delete(int id)
    {if (id < 0)
            throw new InvalidVariableException();
        foreach (Product p in products)
        {
            if (id == p.ID)
            {
                products.Remove(p);
                return true;
            }
        };
        return false;
    }
    /// <summary>
    /// Update details of product
    /// </summary>
    /// <param name="p"></param>IProduct
    /// <returns></returns> True if the ID in the database, else return false
    public bool Update( Product p)
    {
       
        foreach (Product product in products)
        {
            if (product.ID == p.ID)
            {
                products.Remove(product);
                products.Add(p);
                return true;
            }
        };
        return false;
    }   
    
}
