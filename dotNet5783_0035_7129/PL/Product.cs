using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductDataBiding
{
    public class Product : DependencyObject
    {
        /// <summary>
        /// The id of the product.
        /// </summary>
        public int IDProduct { get => (int)GetValue(IDPProperty); set => SetValue(IDPProperty, value); }

        public static readonly DependencyProperty IDPProperty =
            DependencyProperty.Register(nameof(IDProduct), typeof(int), typeof(Product));
        /// <summary>
        /// The name of the product.
        /// </summary>
        public static readonly DependencyProperty NamePProperty =
              DependencyProperty.Register(nameof(NameP), typeof(string), typeof(Product));
        public string? NameP { get => (string)GetValue(NamePProperty); set => SetValue(NamePProperty, value); }

        /// <summary>
        /// The category of the product.
        /// </summary>
        public static readonly DependencyProperty CategoryPProperty =
                     DependencyProperty.Register(nameof(CategoryP), typeof(BO.Category), typeof(Product));
        public BO.Category? CategoryP { get => (BO.Category)GetValue(CategoryPProperty); set => SetValue(CategoryPProperty, value); }
        // <summary>
        /// The amount of the product.
        /// </summary>
        public static readonly DependencyProperty AmountPProperty =
                     DependencyProperty.Register(nameof(AmountP), typeof(int), typeof(Product));
        public int AmountP { get => (int)GetValue(AmountPProperty); set => SetValue(AmountPProperty, value); }
        /// <summary>
        ///  The price of the product.
        /// </summary>
        public static readonly DependencyProperty PricePProperty =
                      DependencyProperty.Register(nameof(PriceP), typeof(double), typeof(Product));
        public double PriceP { get => (double)GetValue(PricePProperty); set => SetValue(PricePProperty, value); }
        /// Prints all the details of the order.
        /// </summary>
        /// <returns></returns>details of the order
        public override string ToString()
        {
            return BO.Tools.ToStringProperty(this);
        }
    }
    public class ProductItem : DependencyObject
    {
        /// <summary>
        /// The id of the product.
        /// </summary>
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register(nameof(ID), typeof(int), typeof(Product));
        /// <summary>
        /// The name of the product.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
              DependencyProperty.Register(nameof(Name), typeof(string), typeof(Product));
        public string? Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }

        /// <summary>
        /// The category of the product.
        /// </summary>
        public static readonly DependencyProperty CategoryProperty =
                     DependencyProperty.Register(nameof(Category), typeof(BO.Category), typeof(Product));
        public BO.Category? Category { get => (BO.Category)GetValue(CategoryProperty); set => SetValue(CategoryProperty, value); }
        /// <summary>
        /// If the product is in stock
        /// </summary>
        public static readonly DependencyProperty InStockProperty =
                      DependencyProperty.Register(nameof(InStock), typeof(bool), typeof(ProductItem));
        public bool InStock { get => (bool)GetValue(InStockProperty); set => SetValue(InStockProperty, value); }
        // <summary>
        /// The amount of the product.
        /// </summary>
        public static readonly DependencyProperty AmountProperty =
                     DependencyProperty.Register(nameof(Amount), typeof(int), typeof(Product));
        public int Amount { get => (int)GetValue(AmountProperty); set => SetValue(AmountProperty, value); }
        /// <summary>
        ///  The price of the product.
        /// </summary>
        public static readonly DependencyProperty PriceProperty =
                      DependencyProperty.Register(nameof(Price), typeof(double), typeof(Product));
        public double Price { get => (double)GetValue(PriceProperty); set => SetValue(PriceProperty, value); }
        /// Prints all the details of the order.
        /// </summary>
        /// <returns></returns>details of the order
        public override string ToString()
        {
            return BO.Tools.ToStringProperty(this);
        }
    }
    public class OrderItem : DependencyObject
    {
        /// <summary>
        /// The id of the product.
        /// </summary>
        public int IDOI { get => (int)GetValue(IDOIProperty); set => SetValue(IDOIProperty, value); }

        public static readonly DependencyProperty IDOIProperty =
            DependencyProperty.Register(nameof(IDOI), typeof(int), typeof(Product));
        /// <summary>
        /// The id of the product.
        /// </summary>
        public int ProductID { get => (int)GetValue(IDProductProperty); set => SetValue(IDProductProperty, value); }

        public static readonly DependencyProperty IDProductProperty =
            DependencyProperty.Register(nameof(ProductID), typeof(int), typeof(Product));/// <summary>
                                                                                         ///
                                                                                         /// The name of the product.
                                                                                         /// </summary>
        public static readonly DependencyProperty NameOIProperty =
              DependencyProperty.Register(nameof(NameOI), typeof(string), typeof(Product));
        public string? NameOI { get => (string)GetValue(NameOIProperty); set => SetValue(NameOIProperty, value); }

        // <summary>
        /// The amount of the product.
        /// </summary>
        public static readonly DependencyProperty AmountOIProperty =
                     DependencyProperty.Register(nameof(AmountOI), typeof(int), typeof(Product));
        public int AmountOI { get => (int)GetValue(AmountOIProperty); set => SetValue(AmountOIProperty, value); }

        /// <summary>
        ///  The price of the product.
        /// </summary>
        public static readonly DependencyProperty PriceOIProperty =
                      DependencyProperty.Register(nameof(PriceOI), typeof(double), typeof(Product));
        public double PriceOI { get => (double)GetValue(PriceOIProperty); set => SetValue(PriceOIProperty, value); }

        /// <summary>
        ///  The price of the product.
        /// </summary>
        public static readonly DependencyProperty TotalPriceProperty =
                      DependencyProperty.Register(nameof(TotalPrice), typeof(double), typeof(Product));
        public double TotalPrice { get => (double)GetValue(TotalPriceProperty); set => SetValue(TotalPriceProperty, value); }

        /// Prints all the details of the order.
        /// </summary>
        /// <returns></returns>details of the order
        public override string ToString()
        {
            return BO.Tools.ToStringProperty(this);
        }
    }
}

