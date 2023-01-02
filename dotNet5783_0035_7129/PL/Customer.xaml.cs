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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        IBl _bl;
        public Customer(IBl bl)
        {
            _bl = bl;
            InitializeComponent();
        }

        /// <summary>
        /// Show the new order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.ObgectNullableException"></exception>
        private void ShowNewOrder(object sender, RoutedEventArgs e)//In click event. open the ProductListWindow
        {
            UserDetails userDetails = new UserDetails(_bl ?? throw new BO.ObgectNullableException());
            userDetails.ShowDialog();

        }

        /// <summary>
        /// Show the track order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.ObgectNullableException"></exception>
        private void ShowTrackOrder(object sender, RoutedEventArgs e)//In click event. open the ProductListWindow
        {
            OrderTrackingDataBiding.OrderTracking orderToTrack1;
            try
            {
                if (id.Text.Length == 0)//if the user didn't input ID
                {
                    MessageBox.Show("Input id of order to track after the order");
                    return;
                }
                orderToTrack1 = new OrderTrackingDataBiding.OrderTracking()
                {
                    ID = int.Parse(id.Text),
                    Status = (BO.OrderStatus?)_bl?.Order.GetDetailsOrderManager(int.Parse(id.Text)).Status,
                    ListDateStatus = new ObservableCollection<NodeDateStatus?>(_bl?.Order.OrderTracking(int.Parse(id.Text)).ListDateStatus)
                };
            }
            catch (FailedGet) { MessageBox.Show("The id is invalid, or not in the database"); return; }
            catch (BO.ObgectNullableException) { MessageBox.Show("an error accured, obect is a nullable, please try again"); return; }
            OrderTrackingWindow track = new OrderTrackingWindow(_bl ?? throw new BO.ObgectNullableException(), orderToTrack1);
            track.ShowDialog();

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
