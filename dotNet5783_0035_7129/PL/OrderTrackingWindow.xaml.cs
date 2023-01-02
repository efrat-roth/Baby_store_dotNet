using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using OrderTrackingDataBiding;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        BlApi.IBl? _bl;
        Order? order;
        public OrderTrackingDataBiding.OrderTracking? orderToTrack { get; set; }
        public OrderTrackingWindow(BlApi.IBl? bl , OrderTrackingDataBiding.OrderTracking o)
        {
            _bl = bl;
            order = _bl.Order.GetDetailsOrderManager(o.ID);
            orderToTrack = o;            
            InitializeComponent();
        }  

        private void ShowOrder(object sender, RoutedEventArgs e)
        {
            try
            {                                   
                OrderForList o = new OrderForList()//convert to orderForList in order to send to order window
                {
                    ID = order?.ID??throw new BO.ObgectNullableException(),
                    AmountOfItems = _bl!.Order.GetListOfOrders().FirstOrDefault(ord => ord!.ID == order.ID)!.AmountOfItems,
                    CustomerName = order.CustomerName,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice
                };
                OrderWindow orderWindow = new OrderWindow(null,_bl, o);
                orderWindow.ShowDialog();
            }
            catch(FailedGet g) { MessageBox.Show(g.ToString()); }
        }
        /// <summary>
        ///  Check the values of ID field, in order to get valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdIsNumber(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt))) return;
            e.Handled = true;
            return;

        }


    }
}
