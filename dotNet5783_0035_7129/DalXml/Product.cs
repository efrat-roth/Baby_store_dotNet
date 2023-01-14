﻿using System;
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
    string dir = "..\\xml\\";
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
            ProductRoot = XElement.Load(dir+ProductPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
    }

    /// <summary>
    /// Return the all products in the store
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
    {
        LoadData();
        IEnumerable<DO.Product?>? products=new List<DO.Product?>();
        if (func == null)
        { 
         
            try
            {
                products = (from p in ProductRoot?.Elements()
                            select new DO.Product()
                            {
                                ID = Convert.ToInt32(p.Element("ID")!.Value),
                                Name = p.Element("Name")!.Value,
                                Price = Convert.ToDouble(p.Element("Price")!.Value),
                                Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                                InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                            }).ToList().Cast<DO.Product?>();
            }
            catch
            {
                products = null;
            }
        }
        else
        {
            func = func ?? throw new InvalidVariableException();
            products = (from p in ProductRoot?.Elements()
                        let pro = new DO.Product()
                        {
                            ID = Convert.ToInt32(p.Element("ID")!.Value),
                            Name = p.Element("Name")!.Value,
                            Price = Convert.ToDouble(p.Element("Price")!.Value),
                            Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                            InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                        }
                        where func(pro)
                        select pro).Cast<DO.Product?>();
        }
        return products??throw new ListIsEmptyException();
    }
    /// <summary>
    /// Return product by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ObgectNullableException"></exception>
    public DO.Product GetByID(int id)
    {
        LoadData();
        DO.Product product;
        try
        {
            product = (from p in ProductRoot?.Elements()
                       where Convert.ToInt32(p.Element("ID")!.Value) == id
                       select new DO.Product()
                       {
                           ID = Convert.ToInt32(p.Element("ID")!.Value),
                           Name = p.Element("Name")!.Value,
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

    /// <summary>
    /// Return name of product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string? GetProductName(int id)
    {
        LoadData();
        string? productName;
        try
        {
            productName = (from p in ProductRoot?.Elements()
                           where Convert.ToInt32(p.Element("ID")!.Value) == id
                           select p.Element("Name")!.Value
                              ).FirstOrDefault();
        }
        catch
        {
            productName = null;
        }
        return productName;
    }

    /// <summary>
    /// Addng product to the store
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="ObgectNullableException"></exception>
    public int Add(DO.Product? product)
    {
        XElement id = new XElement("ID", product?.ID);
        XElement name = new XElement("Name",product?.Name);
        XElement Price = new XElement("Price",product?.Price);
        XElement inStock = new XElement("InStock",product?.InStock);
        XElement Category = new XElement("Category",product?.Category);
        ProductRoot?.Add(new XElement("Product", id, name,Price,inStock,Category));
        ProductRoot?.Save(dir + ProductPath);
        return product?.ID??throw new ObgectNullableException();
    }

    /// <summary>
    /// Delete product from the store
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Delete(int id)
    {
        XElement? ProductElement;
        try
        {
            ProductElement = (from p in ProductRoot?.Elements()
                              where Convert.ToInt32(p.Element("ID")!.Value) == id
                              select p).FirstOrDefault();
            ProductElement!.Remove();
            ProductRoot?.Save(dir + ProductPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Update details of product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public bool Update(DO.Product? product)
    {
        try
        {
            XElement? productElement = (from p in ProductRoot?.Elements()
                                        where Convert.ToInt32(p!.Element("ID")!.Value) == product?.ID
                                        select p).FirstOrDefault();

            productElement!.Element("Name")!.Value = product?.Name!;
            productElement!.Element("Price")!.Value = product?.Price.ToString() ?? throw new ObgectNullableException();
            productElement!.Element("InStock")!.Value = product?.InStock.ToString()!;
            productElement!.Element("Category")!.Value = product?.Category.ToString()!;

            ProductRoot?.Save(dir+ProductPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Return product by condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="InvalidVariableException"></exception>
    public DO.Product? GetByCondition(Func<DO.Product?, bool>? func)
    {
        LoadData();
        DO.Product? product;
        func = func ?? throw new InvalidVariableException();
        product = (from p in ProductRoot?.Elements()
                   let pro = new DO.Product()
                   {
                       ID = Convert.ToInt32(p.Element("ID")!.Value),
                       Name = p.Element("Name")!.Value,
                       Price = Convert.ToDouble(p.Element("Price")!.Value),
                       Category = (DO.Category)Enum.Parse(typeof(DO.Category), (string)p.Element("Category")!),
                       InStock = Convert.ToInt32(p.Element("InStock")!.Value)
                   }
                   where func(pro)
                   select pro).FirstOrDefault();

        return product;

    }
}
