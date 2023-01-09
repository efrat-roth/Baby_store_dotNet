using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class Order : IOrder
{
    static XElement? OrderRoot;
    static string OrderPath = @"Order.xml";
    public Order()
    {
        if (!File.Exists(OrderPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        OrderRoot = new XElement("orders");
        OrderRoot.Save(OrderPath);
    }
    private void LoadData()
    {
        try
        {
            OrderRoot = XElement.Load(OrderPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
    }
    
    
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        LoadData();
        List<DO.Order?> orders=Tools<DO.Order?>.loadListFromXML(OrderPath, OrderRoot);
        if (func == null)
        {
            return orders;
        }
        IEnumerable<DO.Order?> o = orders.Where(i => func(i));
        return o.ToList();
    }
   
    public int Add(DO.Order? order)
    {

        List<DO.Order?> orders = Tools<DO.Order?>.loadListFromXML(OrderPath, OrderRoot);
        bool exist = orders.Exists(o => o?.ID == order?.ID);
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = order?.ID ?? throw new InvalidVariableException();
        orders.Add(order);
        Tools<DO.Order?>.saveListToXML(orders, OrderPath, OrderRoot);
        return y;
    }
    
    public bool Update(DO.Order? order)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath, OrderRoot);
        DO.Order? o = orders.FirstOrDefault(order1 => order1?.ID == order?.ID) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        orders.Add(order);
        Tools<DO.Order?>.saveListToXML(orders, OrderPath, OrderRoot);
        return true;
    }
    
    public bool Delete(int id)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath, OrderRoot);
        if (id < 0)
            throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        Tools<DO.Order?>.saveListToXML(orders,OrderPath, OrderRoot);
        return true;
    }

    public DO.Order GetByID(int id)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath, OrderRoot);
        if (id < 0)
            throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }

    public DO.Order? GetByCondition(Func<DO.Order?, bool>? func)
    {
        List<DO.Order?>? orders = Tools<DO.Order?>.loadListFromXML(OrderPath, OrderRoot);
        func = func ?? throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(i => func(i)) ?? throw new IdDoesNotExistException();
        return o;
    }

}
