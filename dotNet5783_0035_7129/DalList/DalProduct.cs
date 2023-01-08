//using System.Reflection.Metadata.Ecma335;
//using System.Security.Cryptography;
//using DalApi;
//using DO;
//using static Dal.DataSource;

//namespace Dal;

//internal class DalProduct:IProduct
//{
//    /// <summary>
//    /// Adding a product
//    /// </summary>
//    /// <param name="p"></param>
//    /// <returns></returns>The ID of the new product
//    public int Add(Product? p)
//    {
//        bool exist = products.Exists(product=>p?.ID==product?.ID);
//        if (exist)
//        {
//            throw new IdAlreadyExistException();
//        }
//        int y = p?.ID ?? throw new InvalidVariableException();
//        if (y < 100000)
//            throw new InvalidVariableException();
//        products.Add(p);
//        return y;
//    }
//    /// <summary>
//    /// Return IProduct by its ID
//    /// </summary>
//    /// <param name="id"></param>
//    /// <returns></returns>IProduct
//    public Product GetByID(int id)
//    {
//        if (id < 0)
//            throw new InvalidVariableException();
//        Product? p = products.FirstOrDefault(p => p?.ID == id);
//        return p?? throw new IdDoesNotExistException();
//    }
//    /// <summary>
//    /// Return a specific product that matches the condition.
//    /// </summary>
//    /// <param name="func"></param>
//    /// <returns></returns>Product?
//    public Product? GetByCondition(Func<Product?, bool>? func)
//    {
//        func = func ?? throw new InvalidVariableException();
//        Product? o = products.FirstOrDefault(i => func(i));
//        return o??throw new IdDoesNotExistException();
//    }
//    /// <summary>
//    /// Return the all products
//    /// </summary>
//    /// <returns></returns>The database of the all products
//    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
//    {
//        if (func == null)
//        {
//            return products;
//        }
//        IEnumerable<Product?> o = products.Where(i => func(i)).ToList();
//        return o;
//    }
//    /// <summary>
//    /// Delete a product from the database by its ID
//    /// </summary>
//    /// <param name="id"></param>ID of the product to delete
//    public bool Delete(int id)
//    {
//        if (id < 100000)
//            throw new InvalidVariableException();
//        Product? p = products.FirstOrDefault(p => p?.ID == id)?? throw new IdDoesNotExistException(); ;
//        products.Remove(p);
//        return true;
//    }
//    /// <summary>
//    /// Update details of product
//    /// </summary>
//    /// <param name="p"></param>IProduct
//    /// <returns></returns> True if the ID in the database, else return false
//    public bool Update( Product? p)
//    {
//        Product? product = products.FirstOrDefault(product => product?.ID == p?.ID) ?? throw new IdDoesNotExistException(); ;
//        products.Remove(product);
//        products.Add(p);
//        return true;
//    }   
    
//}
