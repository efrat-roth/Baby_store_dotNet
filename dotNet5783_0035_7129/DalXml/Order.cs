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
    XElement? OrderRoot;
    string FPath= "C:\\Users\\משתמש\\source\\repos\\efrat-roth\\dotNet5783_0035_7129\\dotNet5783_0035_7129\\xml\\Order.xml"

    public static void saveListToXML(List<DO.Order?> list, string path)
    {
        XmlSerializer x = new XmlSerializer(list.GetType());
        FileStream fs = new FileStream(path, FileMode.Create);
        x.Serialize(fs, list);
    }

    public static List<DO.Order?> loadListFromXML(string path)
    {
        List<DO.Order?> list;
        XmlSerializer x = new XmlSerializer(typeof(List<DO.Order>));
        FileStream fs = new FileStream(path, FileMode.Open);
        list = (List<DO.Order?>)x.Deserialize(fs);
        return list;

    }
    public List<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        List<DO.Order?> orders=loadListFromXML(FPath);
        if (func == null)
        {
            return orders;
        }
        IEnumerable<DO.Order?> o = orders.Where(i => func(i));
        return o.ToList();
    }
   
    public int Add(DO.Order? order)
    {
        List<DO.Order?> orders = loadListFromXML(FPath);
        bool exist = orders.Exists(o => o?.ID == order?.ID);
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = order?.ID ?? throw new InvalidVariableException();
        orders.Add(order);
        saveListToXML(orders,FPath);
        return y;
    }
    
    public bool Update(DO.Order? order)
    {
        List<DO.Order?>? orders = loadListFromXML(FPath);
        DO.Order? o = orders.FirstOrDefault(order1 => order1?.ID == order?.ID) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        orders.Add(order);
        saveListToXML(orders, FPath);
        return true;
    }
    
    public bool Delete(int id)
    {
        List<DO.Order?>? orders = loadListFromXML(FPath);
        if (id < 0)
            throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        orders.Remove(o);
        saveListToXML(orders, FPath);
        return true;
    }

    public DO.Order? GetByID(int id)
    {
        List<DO.Order?>? orders = loadListFromXML(FPath);
        if (id < 0)
            throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }

    public DO.Order? GetByCondition(Func<DO.Order?, bool>? func)
    {
        List<DO.Order?>? orders = loadListFromXML(FPath);
        func = func ?? throw new InvalidVariableException();
        DO.Order? o = orders.FirstOrDefault(i => func(i)) ?? throw new IdDoesNotExistException();
        return o;
    }

}
