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
}

