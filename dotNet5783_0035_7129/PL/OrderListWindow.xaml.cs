using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? _bl;
        public OrderListWindow(BlApi.IBl? bl)
        {
            InitializeComponent();
            _bl = bl;
            OrdersListView.ItemsSource = _bl?.Order.GetListOfOrders();
        }

        private void UpdateOrder(object sender, MouseButtonEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(_bl??throw new BO.ObgectNullableException(), (BO.OrderForList)OrdersListView.SelectedItem);
            orderWindow.Show();
            OrdersListView.ItemsSource = _bl?.Order.GetListOfOrders();
        }

        /// <summary>
        /// return to admin window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnAdmin(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
