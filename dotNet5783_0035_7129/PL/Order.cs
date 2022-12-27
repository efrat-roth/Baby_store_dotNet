using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Windows;

namespace OrderDataBiding
{
    public class Order : DependencyObject
    {
        /// <summary>
        /// The id of the order.
        /// </summary>
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register(nameof(ID), typeof(int), typeof(Order));
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }
        /// <summary>
        /// The name of the customer.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
              DependencyProperty.Register(nameof(Name), typeof(string), typeof(Order));
        public string? Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }

        /// <summary>
        /// The status of the order.
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
                     DependencyProperty.Register(nameof(Status), typeof(BO.OrderStatus), typeof(Order));
        public BO.OrderStatus? Status { get => (BO.OrderStatus)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        // <summary>
        /// The status of the order.
        /// </summary>
        public static readonly DependencyProperty AmountOfItemsProperty =
                     DependencyProperty.Register(nameof(AmountOfItems), typeof(int), typeof(Order));
        public int AmountOfItems { get => (int)GetValue(AmountOfItemsProperty); set => SetValue(AmountOfItemsProperty, value); }
        /// <summary>
        ///  The total price of the order.
        /// </summary>
        public static readonly DependencyProperty PriceProperty =
                      DependencyProperty.Register(nameof(Price), typeof(double), typeof(Order));
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
