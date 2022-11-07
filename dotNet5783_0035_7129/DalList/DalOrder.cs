using System.Data;
using DO;

namespace Dal;

public class DalOrder
{
    int Add(Order o) 
    {
        //check if this ID already exists
        //if exists throw exception
        //otherwise -go to list or array
        //add obejct
        //return 0; }
    bool Update(Order o) { }
    bool Delete(Order o) { }
    Order GetByID(int ID);
    IEnumerable<Order> GetOrders();
}
