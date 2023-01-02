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
            InitializeComponent();
        }

        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            _bl.Cart.MakeOrder(cart, cart.CustomerAdress, cart.CustomerName, cart.CustomerEmail);
            MessageBox.Show("The order was created successfully");
            this.Close();
        }
    }
}
