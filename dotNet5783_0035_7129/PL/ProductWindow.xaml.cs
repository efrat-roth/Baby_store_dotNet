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
using BlApi;
using Dal;
using DalApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
            IBl bl;
        public ProductWindow(IBl bl1)
        {
            InitializeComponent();
            bl = bl1;
            ChooseCategory.ItemsSource=Enum.GetValues(typeof(BO.Category));
        }
        private void AddProducts(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (EnterInStock.Text.Length == 0 || EnterID.Text.Length == 0 || EnterName.Text.Length == 0
                || EnterPrice.Text.Length == 0 || ChooseCategory.SelectedItem==null)
            {
                messageBoxResult = MessageBox.Show("one or more of the required data is missed");
                return;
            }
            DO.Product product = new DO.Product
            {
                ID=int.Parse(EnterID.Text),
                Name=EnterName.Text,
                category=(DO.Category)ChooseCategory.SelectedItem,
                Price=double.Parse(EnterPrice.Text),
                InStock=int.Parse(EnterInStock.Text)
            };
            try 
            { 
                bl.Product.AddProduct(product);
                messageBoxResult = MessageBox.Show("The product has been successfully added");
            }

            catch (Exception ex) { messageBoxResult = MessageBox.Show(ex.ToString()); }
        }

        private void InStockIsNumber(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift))) return;

        }

        private void PriceIsNumber(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift))) return;

        }

        private void NameIsString(object sender, KeyEventArgs e)
        {

        }

        private void IdIsNumber(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift))) return;

        }
    }
}
