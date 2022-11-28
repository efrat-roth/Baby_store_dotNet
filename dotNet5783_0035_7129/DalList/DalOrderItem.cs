using DalApi;
using DO;
using static Dal.DataSource;


namespace Dal;

internal class DalOrderItem:IOrderItem
{
    /// <summary>
    /// Adding new IOrderItem to the database
    /// </summary>
    /// <param name="oi"></param>IOrderItem variable
    /// <returns></returns>ID of the new orderItem
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem oi)
    {
        foreach (OrderItem oI in orderItems)
        {
            if (oi.ID == oI.ID)
            {
                throw new IdAlreadyExistException();
            };
        }
        orderItems.Add(oi);
        return oi.ID;
    }
    /// <summary>
    /// Returns IOrderItem by its ID
    /// </summary>
    /// <param name="id"></param>integer-ID of the IOrderItem
    /// <returns></returns>IOrderItem
    public OrderItem PrintByID(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        foreach (OrderItem oI in orderItems)
        {
            if (id == oI.ID)
            {
                return oI;
            }
        };
        throw new IdDoesNotExistException();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>Returns the all database of IOrderItem
    public IEnumerable<OrderItem> PrintAll()
    {
       
        return orderItems;
    }
    /// <summary>
    /// Delete an orderItem by its ID
    /// </summary>
    /// <param name="id"></param>Integer-ID of the orderItem
    /// <returns></returns>True if the orderItem in the database, else returns false
    public bool Delete(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        foreach (OrderItem oI in orderItems )
        {
            if (oI.ID==id)
            {
                orderItems.Remove(oI);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Update the details of an orderItem
    /// </summary>
    /// <param name="oi"></param>IOrderItem variable
    /// <returns></returns>True if the id is in the database, else returns false
    public bool Update(OrderItem oi)
    {
        foreach (OrderItem o in orderItems)
        {
            if (o.ID == oi.ID)
            {
                orderItems.Remove(o);
                orderItems.Add(oi);
                return true;
            }
        };
        return false;
    }
    /// <summary>
    /// Return IOrderItem by the id of the order and the ID of the IProduct
    /// </summary>
    /// <param name="productID"></param>Integer variable
    /// <param name="orderID"></param>Integer variable
    /// <returns></returns>IOrderItem
    public OrderItem PrintByTwoId(int productID, int orderID)
    {
        if (productID < 0||orderID<0)
            throw new InvalidVariableException();
        foreach (OrderItem oI in orderItems)
        {
            if (oI.OrderID==orderID)
            {
                if (oI.ProductID == productID)
                    return oI;
            }
        }
        throw new IdDoesNotExistException();
    }
    /// <summary>
    /// Return array of orderItem that include the ID
    /// </summary>
    /// <returns></returns>array of oderItem
    public IEnumerable<OrderItem> PrintAllByOrder(int idOrder)
    {
        if (idOrder < 0)
            throw new InvalidVariableException();
        List<OrderItem> orderItemsByOrder=new List<OrderItem>();
        for(int i=0;i<orderItems.Count();i++)
        {
            if(orderItems.ElementAt(i).OrderID==idOrder)
            {
                orderItemsByOrder.Add(orderItems.ElementAt(i)); 
            }
        }
        return orderItemsByOrder;
    }

}
