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
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register(nameof(ID), typeof(int), typeof(Order));
        /// <summary>
        /// The name of the customer.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
              DependencyProperty.Register(nameof(Name), typeof(string), typeof(Order));
        public string? Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }

        /// <summary>
        /// The email of the customer.
        /// </summary>
        public static readonly DependencyProperty EmailProperty =
              DependencyProperty.Register(nameof(Email), typeof(string), typeof(Order));
        public string? Email { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }

        /// <summary>
        /// The adress of the customer.
        /// </summary>
        public static readonly DependencyProperty AdressProperty =
              DependencyProperty.Register(nameof(Adress), typeof(string), typeof(Order));
        public string? Adress { get => (string)GetValue(AdressProperty); set => SetValue(AdressProperty, value); }

        public static readonly DependencyProperty ShipDateProperty =
             DependencyProperty.Register(nameof(ShipDate), typeof(DateTime?), typeof(Order));
        public DateTime? ShipDate { get => (DateTime?)GetValue(ShipDateProperty); set => SetValue(ShipDateProperty, value); }

        public static readonly DependencyProperty OrderDateProperty =
             DependencyProperty.Register(nameof(OrderDate), typeof(DateTime?), typeof(Order));
        public DateTime? OrderDate { get => (DateTime?)GetValue(OrderDateProperty); set => SetValue(OrderDateProperty, value); }

        public static readonly DependencyProperty DeliveryDateProperty =
             DependencyProperty.Register(nameof(DeliveryDate), typeof(DateTime?), typeof(Order));
        public DateTime? DeliveryDate { get => (DateTime?)GetValue(DeliveryDateProperty); set => SetValue(DeliveryDateProperty, value); }
        /// <summary>
        /// The status of the order.
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
                     DependencyProperty.Register(nameof(Status), typeof(BO.OrderStatus?), typeof(Order));
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
                      DependencyProperty.Register(nameof(TotalPrice), typeof(double), typeof(Order));
        public double TotalPrice { get => (double)GetValue(PriceProperty); set => SetValue(PriceProperty, value); }
        public static readonly DependencyProperty ItemsProperty =
                      DependencyProperty.Register(nameof(Items), typeof(IEnumerable<OrderItem?>), typeof(Order));
        public IEnumerable<OrderItem?>? Items { get => (IEnumerable<OrderItem?>?)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

        /// </summary>
        /// Prints all the details of the order.
        /// </summary>
        /// <returns></returns>details of the order
        public override string ToString()
        {
            return BO.Tools.ToStringProperty(this);
        }
    }
}
