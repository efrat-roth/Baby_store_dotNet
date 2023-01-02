using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Windows;
using System.Collections.ObjectModel;

namespace OrderTrackingDataBiding
{
    
    
    public class OrderTracking : DependencyObject
    {
      
        /// <summary>
        /// The id of the order.
        /// </summary>
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register(nameof(ID), typeof(int), typeof(OrderTracking));
        
        /// <summary>
        /// The status of the order.
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
                     DependencyProperty.Register(nameof(Status), typeof(BO.OrderStatus?), typeof(OrderTracking));
        public BO.OrderStatus? Status { get => (BO.OrderStatus)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }


        /// <summary>
        /// The list of status and created date of order
        /// </summary>
        public static readonly DependencyProperty DateStatusProperty =
                     DependencyProperty.Register(nameof(ListDateStatus), typeof(ObservableCollection<NodeDateStatus?>), typeof(OrderTracking));
        public ObservableCollection<NodeDateStatus?>? ListDateStatus { get => (ObservableCollection<NodeDateStatus?>?)GetValue(DateStatusProperty); set => SetValue(DateStatusProperty, value); }
        

        /// Prints all the details of the order.
        /// </summary>
        /// <returns></returns>details of the order
        public override string ToString()
        {
            return BO.Tools.ToStringProperty(this);
        }
    }
}
