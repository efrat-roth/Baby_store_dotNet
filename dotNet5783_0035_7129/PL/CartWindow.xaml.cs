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
        public ObservableCollection<ProductDataBiding.OrderItem?>? OrderItems { get; set; }
        private IEnumerable<ProductDataBiding.OrderItem?>? orderItems { get; }
        public CartWindow(IBl bl, Cart c)
        {
            _bl= bl;
            cart = c;
            orderItems = from i in cart.Items
                         select new ProductDataBiding.OrderItem()
                         {
                             IDOI = i.ID,
                             ProductID = i.ProductID,
                             NameOI = i.Name,
                             TotalPrice = i.TotalPrice,
                             PriceOI = i.Price,
                             AmountOI = i.Amount,
                         };
            OrderItems = new ObservableCollection<ProductDataBiding.OrderItem?>(orderItems);
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
                ProductDataBiding.OrderItem? p = (ProductDataBiding.OrderItem?)f?.DataContext;//gets the product to change
                int productId = p.ProductID;
                int amount = p.AmountOI;
                cart=_bl.Cart.UpdateProductAmount(cart, productId, amount);
                ProductDataBiding.OrderItem orderItem = new ProductDataBiding.OrderItem()
                {
                    IDOI = productId,
                    ProductID = productId,
                    AmountOI = amount,
                    NameOI = p.NameOI,
                    PriceOI = p.PriceOI,
                    TotalPrice = cart.Items.FirstOrDefault(oi => oi.ID == p.IDOI).TotalPrice
                };
                OrderItems[OrderItems.IndexOf(p)]=orderItem;
            }
            catch (FailedGet) { MessageBox.Show("The product is not in the store"); return; }
            catch (CanNotDOActionException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
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
