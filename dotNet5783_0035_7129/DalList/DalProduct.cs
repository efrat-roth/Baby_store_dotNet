

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    public int Add(Product p)
    {
        products[nextEmptyProduct] = p;
        nextCountProductID();
        return 1;
    }
    
}
