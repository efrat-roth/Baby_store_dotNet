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
    static XElement? ProductRoot;
    static string ProductPath = @"Product.xml";
    public Product()
    {
        if (!File.Exists(ProductPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        ProductRoot = new XElement("products");
        ProductRoot.Save(ProductPath);
    }

    private void LoadData()
    {
        try
        {
            ProductRoot = XElement.Load(ProductPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
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
        return (List<DO.Product?>)products;
    }
    public DO.Product GetByID(int id)
    {
        LoadData();
        DO.Product product;
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
            throw new ObgectNullableException();
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
        ProductRoot?.Save(ProductPath);
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
            ProductRoot?.Save(ProductPath);
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
                                        where Convert.ToInt32(p!.Element("id")!.Value) == product?.ID
                                        select p).FirstOrDefault();

            productElement!.Element("name")!.Value = product?.Name!;
            productElement!.Element("Price")!.Value = product?.Price.ToString() ?? throw new ObgectNullableException();
            productElement!.Element("InStock")!.Value = product?.InStock.ToString()!;
            productElement!.Element("Category")!.Value = product?.Category.ToString()!;

            ProductRoot?.Save(ProductPath);
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
