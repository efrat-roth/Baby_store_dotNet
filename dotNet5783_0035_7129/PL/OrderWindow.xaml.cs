using System;
using System.Collections.Generic;
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
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
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
                    Status = (BO.OrderStatus?)o.Status
                };
                order = order1;
                OrderDetailsRows.DataContext = order;

            }
        

        private void UpdateProducts(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (GetProduct.Text.Length == 0 || amountContent.Text.Length == 0)
                    {//Input integrity check in case the uset didn't input the all details
                        MessageBox.Show("Not all the details are typed");
                        return;
                    }
                    int idProduct, amount;
                    int.TryParse(GetProduct.Text, out idProduct);
                    int.TryParse(amountContent.Text, out amount);
                    _bl?.Order.UpdateOrder(order.ID, idProduct, amount);
                    MessageBox.Show("The order has been successfuly updated");
                    this.Close();
                }
                catch (CanNotDOActionException inner)
                {
                    MessageBox.Show("The order is already shiped");
                }
                catch (InvalidVariableException inner)
                {
                    MessageBox.Show("The details are invalid, or the product is not in the order, please check again");
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
