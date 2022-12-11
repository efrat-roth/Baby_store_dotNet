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
using System.Xml.Linq;
using BlApi;
using BlImplementation;
using BO;
using Dal;
using DalApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl _bl;
        BO.ProductForList product=new BO.ProductForList();
        public ProductWindow(IBl bl1)
        {
            InitializeComponent();
            _bl = bl1;
            ChooseCategory.ItemsSource=Enum.GetValues(typeof(BO.Category));
            UpdateProductxamel.Visibility=Visibility.Collapsed;
        }
        public ProductWindow(IBl bl1 ,BO.ProductForList p)
        {
            InitializeComponent();
            _bl = bl1;
            ChooseCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            product = p;
            AddProductxamel.Visibility = Visibility.Collapsed;
            EnterID.Visibility=Visibility.Collapsed;
            IDxamel.Content = "The id of the product is:";
            showID.Content = p.ID;
            showName.Content = "The old is: "+p.Name;
            showCategory.Content = "The old is: "+ p.Category;
            showPrice.Content = "The old is: "+ p.Price;
            showInStock.Content = "The old is: "+ _bl.Product.GetProductManager(p.ID).InStock;
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
                BO.Product p = new BO.Product
                {
                    ID = product.ID,
                    Name = product.Name,
                    category = (BO.Category)product.category!,
                    Price = product.Price,
                    InStock = _bl.Product.GetProductManager(product.ID).InStock,
                };
                _bl.Product.AddProduct(p);
                messageBoxResult = MessageBox.Show("The product has been successfully added");
                this.Close();
            }

            catch (Exception ex) { messageBoxResult = MessageBox.Show(ex.ToString()); }
        }

        private void UpdateProducts(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            int inStock1=_bl.Product.GetProductManager(product.ID).InStock;
            if (EnterInStock.Text.Length == 0 && EnterName.Text.Length == 0
               && EnterPrice.Text.Length == 0 && ChooseCategory.SelectedItem == null)
            {
                messageBoxResult = MessageBox.Show("The product has not been updated");
                return;
            }
            if (EnterInStock.Text.Length != 0)
                inStock1 = int.Parse(EnterInStock.Text);

            if (EnterName.Text.Length != 0)
                product.Name = EnterName.Text;

            if (EnterPrice.Text.Length != 0)
                product.Price = double.Parse(EnterPrice.Text);

            if (ChooseCategory.SelectedItem != null)
                product.Category = (BO.Category)ChooseCategory.SelectedItem;

            try
            {
                BO.Product p= new BO.Product
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    category = (BO.Category)product.Category!,
                    InStock = inStock1,
                };
                _bl.Product.UpdatingProductDetails(p);
                messageBoxResult = MessageBox.Show("The product has been successfuly updated"); 
                this.Close();
            }
            catch (Exception ex)
            { messageBoxResult = MessageBox.Show(ex.ToString()); }
        }

        private void InStockIsNumber(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift)||Keyboard.IsKeyDown(Key.RightAlt))) return;
            e.Handled = true;
            return;

        }

        private void PriceIsNumber(object sender, KeyEventArgs e)
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
