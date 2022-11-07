

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int Add(Product p)
    {
        products[Config.nextEmptyProduct] = p;
        Config.nextCountProductID();
        return 1;
    }
    
}
