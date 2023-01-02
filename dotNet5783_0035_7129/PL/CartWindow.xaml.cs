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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public Cart cart { get; set; }
        IBl _bl { get; set; }
        public ObservableCollection<OrderItem?>? OrderItems { get; set; }
        private IEnumerable<OrderItem?>? orderItems { get; }
        public CartWindow(IBl bl, Cart c)
        {
            _bl= bl;
            cart = c;
            orderItems = cart.Items;
            OrderItems = new ObservableCollection<OrderItem?>(orderItems);
            DataContext = this;
            InitializeComponent();
        }

        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                BO.Order order= _bl.Cart.MakeOrder(cart, cart.CustomerAdress, cart.CustomerName, cart.CustomerEmail);
                
                MessageBox.Show("The order was created successfully, the id of the order is"+ order.ID );
                this.Close();
            }
            catch(ListIsEmptyException)
            {
                MessageBox.Show("The order has no items"); return;
            }
            catch (ObgectNullableException)
            {
                MessageBox.Show("There are no products in the store"); return;
            }
            catch (InvalidVariableException)
            {
                MessageBox.Show("There are items with negative amount"); return;
            }
            catch (FailedUpdate)
            {
                MessageBox.Show("The amount of one of the items is bigger than in the stock"); return;
            }
            catch (Exception v)
            {
                MessageBox.Show(v.ToString()); return;
            }


        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FrameworkElement f = sender as FrameworkElement;
            OrderItem p = (BO.OrderItem)f.DataContext;
            int productId = p.ProductID;
            int amount = p.Amount;
            _bl.Cart.UpdateProductAmount(cart, productId, amount);
            orderItems.ToList().Clear();
            OrderItems = new ObservableCollection<OrderItem?>(cart.Items);
        }
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
