using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using Tools;




namespace PL
{      
        /// <summary>
        /// Interaction logic for OrderWindow.xaml
        /// </summary>
        public partial class OrderWindow : Window
        {
            BlApi.IBl? _bl;
            OrderDataBiding.Order order;
            public OrderWindow(IBl bl1, BO.OrderForList o)
            {
                InitializeComponent();
                _bl = bl1;
                OrderDataBiding.Order order1 = new OrderDataBiding.Order()
                {
                    ID = o.ID,
                    Price = o.TotalPrice,
                    Name = o.CustomerName,
                    AmountOfItems = o.AmountOfItems,
                    Status = (BO.OrderStatus?)o.Status,
                    Email=_bl.Order.GetDetailsOrderManager(o.ID).CustomerEmail,
                    Adress=_bl.Order.GetDetailsOrderManager(o.ID).CustomerAdress,
                    OrderDate= _bl.Order.GetDetailsOrderManager(o.ID).OrderDate,
                    ShipDate=_bl.Order.GetDetailsOrderManager(o.ID).ShipDate,
                    DeliveryDate= _bl.Order.GetDetailsOrderManager(o.ID).DeliveryDate,                    
                };
                showItems.DataContext= new ObservableCollection<BO.OrderItem?>(_bl.Order.GetDetailsOrderManager(o.ID).Items);
                order = order1;
                OrderDetailsRows.DataContext = order;
            }


        private void UpdateProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GetProduct.Text.Length == 0 && amountContent.Text.Length == 0 && updateShiped.IsChecked == false && updateDelivery.IsChecked == false)
                {//Input integrity check in case the uset didn't input the all details
                    MessageBox.Show("please input at least one detail to update");
                    return;
                }
                if ((GetProduct.Text.Length > 0 && amountContent.Text.Length == 0) || (GetProduct.Text.Length == 0 && amountContent.Text.Length > 0))
                {
                    MessageBox.Show("The amount of product is depended on ID product input, you habe to fill both, or neither");
                    return;
                }
                if (GetProduct.Text.Length > 0 && amountContent.Text.Length > 0)
                {
                    int idProduct, amount;
                    int.TryParse(GetProduct.Text, out idProduct);
                    int.TryParse(amountContent.Text, out amount);
                    _bl?.Order.UpdateOrder(order.ID, idProduct, amount);
                }
                BO.Order order1=new BO.Order();
                if (updateShiped.IsChecked == true)
                {
                    order1 = _bl?.Order.DeliveredOrder(order.ID);
                    
                }
                if (updateDelivery.IsChecked == true)
                   order1= _bl?.Order.ArrivedOrder(order.ID);
                MessageBox.Show("The order has been successfuly updated");
                this.Close();
            }
            catch (CanNotDOActionException inner)
            {
                MessageBox.Show("The order is already shiped or arrived, can't change the date and the product details");
            }
            catch (InvalidVariableException inner)
            {
                MessageBox.Show("The details are invalid, or the product is not in the order, please check again");
            }
            catch (FailedGet inner)
            {
                MessageBox.Show("The order wasn't found");
            }
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

            /// <summary>
            ///  Check the values of amount field, in order to get valid input
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void amountIsNumber(object sender, KeyEventArgs e)
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
