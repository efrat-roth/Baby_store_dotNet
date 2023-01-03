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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? _bl;
        public ObservableCollection<OrderForList?>? OrderForLists { get; set; }
        private IEnumerable<OrderForList?>? orderForLists { get; }
        public OrderListWindow(BlApi.IBl? bl)
        {
            _bl = bl;
            orderForLists = _bl!.Order.GetListOfOrders();
            OrderForLists = new ObservableCollection<OrderForList?>(orderForLists);//convert to observel in order to update the details 
            InitializeComponent();         
        }
        private void UpdateO(OrderForList orderForList)
        {
            var o = OrderForLists?.FirstOrDefault(item => item.ID == orderForList.ID);
            int index = OrderForLists!.IndexOf(o);
            OrderForLists[index] = orderForList;

        }
        private void UpdateOrder(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? O = (BO.OrderForList?)OrdersListView.SelectedItem;
            OrderWindow orderWindow = new OrderWindow(UpdateO,_bl??throw new BO.ObgectNullableException(), O);
            orderWindow.ShowDialog();
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
