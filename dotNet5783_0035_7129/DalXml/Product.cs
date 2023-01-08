using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal;

internal class Product : IProduct
{
    XElement? ProductRoot;
    string FPath = "C:\\Users\\משתמש\\source\\repos\\efrat-roth\\dotNet5783_0035_7129\\dotNet5783_0035_7129\\xml\\Product.xml";
    public Product()
    {
        if (!File.Exists(FPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        ProductRoot = new XElement("products");
        ProductRoot.Save(FPath);
    }

    private void LoadData()
    {
        try
        {
            ProductRoot = XElement.Load(FPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
    }
    public void saveListToXML(List<DO.Product> productsList)
    {
        var v = from p in productsList
                select new XElement("product",
                    new XElement("id", p.ID),
                   new XElement("name",p.Name),
                   new XElement("InStock", p.InStock),
                   new XElement("Price", p.Price),
                   new XElement("Category", p.Category)                  
                    );
        ProductRoot = new XElement("products", v);
        ProductRoot.Save(FPath);
    }
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
    {
        LoadData();
        IEnumerable<DO.Product> products=new List<DO.Product>();
        if (func == null)
        { 
         
            try
            {
                products = (from p in ProductRoot?.Elements()
                            select new DO.Product()
                            {
                                ID = Convert.ToInt32(p.Element("id")!.Value),
                                Name = p.Element("name")!.Value,
                                Price = Convert.ToDouble(p.Element("Price")!.Value),
                                Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                                InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                            }).ToList();
            }
            catch
            {
                products = null;
            }
        }
        else
        {
            func = func ?? throw new InvalidVariableException();
            products = from p in ProductRoot?.Elements()
                        let pro = new DO.Product()
                        {
                            ID = Convert.ToInt32(p.Element("id")!.Value),
                            Name = p.Element("name")!.Value,
                            Price = Convert.ToDouble(p.Element("Price")!.Value),
                            Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                            InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                        }
                        where func(pro)
                        select pro;
        }
        return (IEnumerable<DO.Product?>)products;
    }
    public DO.Product? GetByID(int id)
    {
        LoadData();
        DO.Product? product;
        try
        {
            product = (from p in ProductRoot?.Elements()
                       where Convert.ToInt32(p.Element("id")!.Value) == id
                       select new DO.Product()
                       {
                           ID = Convert.ToInt32(p.Element("id")!.Value),
                           Name = p.Element("name")!.Value,
                           Price = Convert.ToDouble(p.Element("Price")!.Value),
                           Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                           InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                       }).FirstOrDefault();
        }
        catch
        {
            product = null;
        }
        return product;
    }
    public string? GetProductName(int id)
    {
        LoadData();
        string? productName;
        try
        {
            productName = (from p in ProductRoot?.Elements()
                           where Convert.ToInt32(p.Element("id")!.Value) == id
                           select p.Element("name")!.Value
                              ).FirstOrDefault();
        }
        catch
        {
            productName = null;
        }
        return productName;
    }
    public int Add(DO.Product? product)
    {
        XElement id = new XElement("id", product?.ID);
        XElement name = new XElement("name",product?.Name);
        XElement Price = new XElement("price",product?.Price);
        XElement inStock = new XElement("InSrock",product?.InStock);
        XElement Category = new XElement("Category",product?.Category);
        ProductRoot?.Add(new XElement("product", id, name));
        ProductRoot?.Save(FPath);
        return product?.ID??throw new ObgectNullableException();
    }
    public bool Delete(int id)
    {
        XElement? ProductElement;
        try
        {
            ProductElement = (from p in ProductRoot?.Elements()
                              where Convert.ToInt32(p.Element("id")!.Value) == id
                              select p).FirstOrDefault();
            ProductElement!.Remove();
            ProductRoot?.Save(FPath);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Update(DO.Product? product)
    {
        try
        {
            XElement? productElement = (from p in ProductRoot?.Elements()
                                        where Convert.ToInt32(p!.Element("id")!.Value) == product.ID
                                        select p).FirstOrDefault();

            productElement!.Element("name")!.Value = product?.Name!;
            productElement!.Element("Price")!.Value = product?.Price.ToString() ?? throw new ObgectNullableException();
            productElement!.Element("InStock")!.Value = product?.InStock.ToString()!;
            productElement!.Element("Category")!.Value = product?.Category.ToString()!;

            ProductRoot?.Save(FPath);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public DO.Product? GetByCondition(Func<DO.Product?, bool>? func)
    {
        LoadData();
        DO.Product? product;
        func = func ?? throw new InvalidVariableException();
        product = (from p in ProductRoot?.Elements()
                   let pro = new DO.Product()
                   {
                       ID = Convert.ToInt32(p.Element("id")!.Value),
                       Name = p.Element("name")!.Value,
                       Price = Convert.ToDouble(p.Element("Price")!.Value),
                       Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                       InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                   }
                   where func(pro)
                   select pro).FirstOrDefault();

        return product;

    }
}
