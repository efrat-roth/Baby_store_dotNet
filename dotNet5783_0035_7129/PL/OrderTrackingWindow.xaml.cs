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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        BlApi.IBl? _bl;
        Order? order;
        public OrderTrackingWindow(BlApi.IBl? bl)
        {
            InitializeComponent();
            _bl = bl;
        }  

        private void IDInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (IDInput.Text.Length == 0)
                {
                    MainGrid.DataContext=null;
                }
                    if (IDInput.Text.Length != 0)
                   {
                    order = _bl?.Order.GetDetailsOrderManager(int.Parse(IDInput.Text));

                    OrderTrackingDataBiding.OrderTracking orderToTrack = new OrderTrackingDataBiding.OrderTracking()
                    {
                        ID = _bl?.Order.OrderTracking(order?.ID?? throw new BO.ObgectNullableException()).ID ?? throw new BO.ObgectNullableException(),
                        Status = _bl?.Order.OrderTracking(order.ID).Status ?? throw new BO.ObgectNullableException(),
                        ListDateStatus=new ObservableCollection<BO.NodeDateStatus?>(_bl?.Order.OrderTracking(order.ID).ListDateStatus)
                    };
                    statusNodes.ItemsSource = orderToTrack.ListDateStatus;
                    MainGrid.DataContext = orderToTrack;

                }
            }
            catch (FailedGet) { MessageBox.Show("The id is invalid, or not in the database"); IDInput.Text = null; MainGrid.DataContext = null; }
            catch (BO.ObgectNullableException) { MessageBox.Show("an error accured, obect is a nullable, please try again"); }
            
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
                OrderWindow orderWindow = new OrderWindow(_bl, o);
                orderWindow.WantToUpdate.Visibility = Visibility.Collapsed;//cant update order
                orderWindow.CheckUpdate.Visibility = Visibility.Collapsed;
                orderWindow.Show();
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
