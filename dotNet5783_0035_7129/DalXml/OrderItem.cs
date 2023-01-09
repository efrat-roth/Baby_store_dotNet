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

internal class OrderItem : IOrderItem
{
    static XElement? OrderItemRoot;
    static string OrderItemPath = @"OrderItem.xml";
    public OrderItem()
    {
        if (!File.Exists(OrderItemPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        OrderItemRoot = new XElement("orderItems");
        OrderItemRoot.Save(OrderItemPath);
    }
    private void LoadData()
    {
        try
        {
            OrderItemRoot = XElement.Load(OrderItemPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
    }
   
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
    {
        LoadData();
        List<DO.OrderItem?> OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath,OrderItemRoot);
        if (func == null)
        {
            return OrderItems;
        }
        IEnumerable<DO.OrderItem?> o = OrderItems.Where(i => func(i));
        return o.ToList();
    }

    public int Add(DO.OrderItem? orderItem)
    {
        List<DO.OrderItem?> OrderItems = Tools<DO.OrderItem?>.loadListFromXML( OrderItemPath, OrderItemRoot);
        bool exist = OrderItems.Exists(o => o?.ID == orderItem?.ID);
        if (exist)
        {
            throw new IdAlreadyExistException();
        }
        int y = orderItem?.ID ?? throw new InvalidVariableException();
        OrderItems.Add(orderItem);
        Tools<DO.OrderItem?>.saveListToXML(OrderItems, OrderItemPath, OrderItemRoot);
        return y;
    }

    public bool Update(DO.OrderItem? order)
    {
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath, OrderItemRoot);
        DO.OrderItem? o = OrderItems.FirstOrDefault(order1 => order1?.ID == order?.ID) ?? throw new IdDoesNotExistException(); ;
        OrderItems.Remove(o);
        OrderItems.Add(order);
        Tools<DO.OrderItem?>.saveListToXML(OrderItems, OrderItemPath, OrderItemRoot); return true;
    }

    public bool Delete(int id)
    {
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath, OrderItemRoot);
        if (id < 0)
            throw new InvalidVariableException();
        DO.OrderItem? o = OrderItems.FirstOrDefault(o => o?.ID == id) ?? throw new IdDoesNotExistException(); ;
        OrderItems.Remove(o);
        Tools<DO.OrderItem?>.saveListToXML(OrderItems, OrderItemPath, OrderItemRoot); return true;
    }

    public DO.OrderItem GetByID(int id)
    {
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath, OrderItemRoot);
        if (id < 0)
            throw new InvalidVariableException();
        DO.OrderItem? o = OrderItems.FirstOrDefault(o => o?.ID == id);
        return o ?? throw new IdDoesNotExistException();
    }

    public DO.OrderItem? GetByCondition(Func<DO.OrderItem?, bool>? func)
    {
        List<DO.OrderItem?>? OrderItems = Tools<DO.OrderItem?>.loadListFromXML(OrderItemPath, OrderItemRoot);
        func = func ?? throw new InvalidVariableException();
        DO.OrderItem? o = OrderItems.FirstOrDefault(i => func(i)) ?? throw new IdDoesNotExistException();
        return o;
    }

}
