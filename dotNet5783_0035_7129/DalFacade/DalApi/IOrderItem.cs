using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IOrderItem: ICrud<OrderItem>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    IEnumerable<OrderItem> PrintAllByOrder(int idOrder);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="orderID"></param>
    /// <returns></returns>
    OrderItem PrintByTwoId(int productID, int orderID);
    int  Add(OrderItem orderItem)
    {

    }
}

