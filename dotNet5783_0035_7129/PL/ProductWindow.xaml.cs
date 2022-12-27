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
using System.Xml.Linq;





namespace PL
{
    
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? _bl ;
        BO.ProductForList  product=new BO.ProductForList();
        /// <summary>
        /// Constractor for adding product
        /// </summary>
        /// <param name="bl1"></param>The contract with the logic layer
        public ProductWindow(BlApi.IBl bl1)
        {
            InitializeComponent();
            _bl = bl1;
            ChooseCategory.ItemsSource=Enum.GetValues(typeof(BO.Category));
            UpdateProductxamel.Visibility = Visibility.Collapsed;//Hides the add button
            showCurrent.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Constractor for update product
        /// </summary>
        /// <param name="bl1"></param>The contract with the logic layer
        /// <param name="p"></param>The product to update
        public ProductWindow(BlApi.IBl bl1 ,BO.ProductForList p)
        {
            InitializeComponent();
            _bl = bl1;
            ChooseCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            product = p;
            AddProductxamel.Visibility = Visibility.Collapsed;//Hides the add button
            ///The details of the current product:
            EnterID.Visibility = Visibility.Collapsed;
            OrderDataBiding.Product pToPrint = new OrderDataBiding.Product()
            {
                ID = p.ID,
                Price = p.Price,
                Name = p.Name,
                Amount = _bl.Product.GetProductManager(p.ID).InStock,
                Category = p.Category,
            };
            MainGrid.DataContext=pToPrint;//resets the value to be the product
            
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProducts(object sender, RoutedEventArgs e)
        {
            if (EnterInStock.Text.Length == 0 || EnterID.Text.Length == 0 || EnterName.Text.Length == 0
                || EnterPrice.Text.Length == 0 || ChooseCategory.SelectedItem==null)
            {//Input integrity check in case the uset didn't input the all details
                MessageBox.Show("one or more of the required data is missed");
                return;
            }
            try 
            {
                BO.Product p = new BO.Product//The product to add
                {
                    ID = int.Parse(EnterID.Text),
                    Name = EnterName.Text,
                    category = (BO.Category)ChooseCategory.SelectedItem!,
                    Price = double.Parse(EnterPrice.Text),
                    InStock = int.Parse(EnterInStock.Text),
                };
                _bl?.Product.AddProduct(p);//Add the product
                 MessageBox.Show("The product has been successfully added");
                this.Close();
            }

            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProducts(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            int inStock1=_bl?.Product.GetProductManager(product.ID).InStock ?? throw new BO.ObgectNullableException();
            if (EnterInStock.Text.Length == 0 && EnterName.Text.Length == 0
               && EnterPrice.Text.Length == 0 && ChooseCategory.SelectedItem == null)
            {//Input integrity check in case the uset didn't input the all details
                MessageBox.Show("The product has not been updated");
                return;
            }

            //check wether the fields to update
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
                BO.Product p= new BO.Product//The updating product
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    category = (BO.Category)product.Category!,
                    InStock = inStock1,
                };
                _bl.Product.UpdatingProductDetails(p);//Update the product
                messageBoxResult = MessageBox.Show("The product has been successfuly updated"); 
                this.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        /// <summary>
        /// Check the values of InStock field, in order to get valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        ///  Check the values of price field, in order to get valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
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
