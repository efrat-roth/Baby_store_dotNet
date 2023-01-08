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
    XElement ProductRoot;
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
    public void SaveProductListLinq(List<DO.Product> productsList)
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
    public List<DO.Product?>? GetProductsList()
    {
        LoadData();
        List<DO.Product?>? products;      
        try
        {
            products = (from p in ProductRoot.Elements()
                        select  new DO.Product() 
                        {                           
                            ID= Convert.ToInt32(p.Element("id")!.Value),
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
        return products;
    }
}
