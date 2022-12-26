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
            OrdersListView.ItemsSource = bl?.Order.GetListOfOrders();
        }

       
    }
}
