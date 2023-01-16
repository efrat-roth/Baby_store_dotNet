using System.Data;
using DO;
using static Dal.DataSource;
using DalApi;


namespace Dal;

internal class DalOrder : IOrder
{


    /// <summary>
    /// Adding an IOrder       
    /// <param name="o"></param>
    /// < returns ></ returns >
    /// <returns></returns>
    /// <exception cref="IdAlreadyExistException"></exception>
    /// <exception cref="InvalidVariableException"></exception>
    public int Add(Order? o)
    {

        bool exist = orders.Exists(order => order?.ID == o?.ID); //checks if the order is already exists. 
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = o?.ID ?? throw new InvalidVariableException();
        orders.Add(o);

        return y;
    }
  
        /// <summary>
        ///  Return order by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>IOrder
        /// <exception cref="InvalidVariableException"></exception>
        /// <exception cref="IdDoesNotExistException"></exception>
    public Order GetByID(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        Order? o = orders.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }

           /// <summary>
           /// Return a specific order that matches the condition.
           /// </summary>
           /// <param name="func"></param>
           /// <returns></returns>Order ?
           /// <exception cref="InvalidVariableException"></exception>
           /// <exception cref="IdDoesNotExistException"></exception>
    public Order? GetByCondition(Func<Order?, bool>? func)
    {
        func = func ?? throw new InvalidVariableException();
        Order? o = orders.FirstOrDefault(i => func(i)) ?? throw new IdDoesNotExistException();//bring the order by the condition.
        return o;
    }


        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>The database of the all orders
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        if (func == null)
        {
            return orders;
        }
        IEnumerable<Order?> o = orders.Where(i => func(i)).ToList<Order?>();//make a list by the condition.
        return o;
    }
   
    /// <summary>
    ///  Delete a order from the database by its ID
    /// </summary>
    /// <param name="id"></param> ID of the order to delete
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    /// <exception cref="IdDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        if (id < 0)
            throw new InvalidVariableException();
        Order? o = orders.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        return true;
    }
    /// <summary>
    /// Update details of order
    /// </summary>
    /// <param name = "p" ></ param > IOrder
    /// < returns >bool</ returns > True if the ID in the database, else return false
    public bool Update(Order? o)
    {
        Order? order = orders.FirstOrDefault(order => order?.ID == o?.ID) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(order);
        orders.Add(o);
        return true;
    }
}
