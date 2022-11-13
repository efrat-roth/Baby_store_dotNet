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
                throw new Exception();
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
        foreach (Product p in products)
        {
            if (id == p.ID)
            {
                return p;
            }
        };
        throw new Exception("The product is not on the database");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>The database of the all products
    public IEnumerable<Product> PrintAll()
    {
        if(products.Count==0)
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
    public bool Update(ref Product p)
    {
        Console.WriteLine("Do you want to change the name?, enter y for yes and n for no");
        string answer = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter name");
            string name1 = Console.ReadLine();
            p.Name = name1;
        }

        Console.WriteLine("Do you want to change the category?, enter y for yes and n for no");
        string answer1 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter category");
            Enums.Category category1 = (Enums.Category)Console.Read();
            p.Category = category1;
        }
        Console.WriteLine("Do you want to change the price?, enter y for yes and n for no");
        string answer2 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter price");
            double price1 = Console.Read();
            p.Price = price1;
        }
        Console.WriteLine("Do you want to change the in stock?, enter y for yes and n for no");
        string answer3 = Console.ReadLine();
        if (answer == "y")
        {
            Console.WriteLine("Enter in stock?");
            int inStock1 = Console.Read();
            p.InStock = inStock1;
        }
        return true;
    }

         
          
        
    
}
