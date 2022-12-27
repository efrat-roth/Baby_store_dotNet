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
using Tools;

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
            ObservableCollection<OrderForList> ordersList = new ObservableCollection<OrderForList>(_bl!.Order.GetListOfOrders());//convert to observel in order to update the details 
            OrdersListView.ItemsSource = ordersList;
            DataContext= ordersList;
        }

        private void UpdateOrder(object sender, MouseButtonEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(_bl??throw new BO.ObgectNullableException(), (BO.OrderForList)OrdersListView.SelectedItem);
            orderWindow.Show();
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
