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
        public ObservableCollection<BO.OrderItem?>? OrderItems { get; set; }
        private IEnumerable<BO.OrderItem?>? orderItems { get; }
        public CartWindow(IBl bl, Cart c)
        {
            _bl= bl;
            cart = c;
            orderItems = cart.Items;
            //orderItems = from i in cart.Items
            //             select new ProductDataBiding.OrderItem()
            //             {
            //                 ID=i.ID,
            //                 IDProduct=i.ProductID,
            //                 Name=i.Name,
            //                 TotalPrice=i.TotalPrice,
            //                 Price=i.Price,
            //                 Amount = i.Amount,
            //             };
            OrderItems = new ObservableCollection<BO.OrderItem?>(orderItems);
            DataContext = this;
            InitializeComponent();
        }

        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order= _bl.Cart.MakeOrder(cart, cart?.CustomerAdress!, cart?.CustomerName!, cart?.CustomerEmail!);
                
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

        /// <summary>
        /// The method is singing to event of changing in the product amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, RoutedEventArgs  e)
        {
            try
            {
                FrameworkElement? f = sender as FrameworkElement;
                BO.OrderItem? p = (BO.OrderItem?)f?.DataContext;//gets the product to change
                int productId = p.ProductID;
                int amount = p.Amount;
                cart=_bl.Cart.UpdateProductAmount(cart, productId, amount);
                f.DataContext = cart?.Items?.FirstOrDefault(oi => oi?.ID == p.ID);
                
            }
            catch (FailedGet) { MessageBox.Show("The product is not in the store"); return; }
            catch (CanNotDOActionException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
        
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
