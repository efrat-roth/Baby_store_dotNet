using System;
using System.CodeDom;
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
using BO;

namespace PL
{
    
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? _bl ;
        public ProductDataBiding.Product? product { get; set; }
        private Action<BO.ProductForList?> _action { get; set; }
        private Action<BO.ProductForList?> _actionDel { get; set; }

        public Array _Category { get; set; } = Enum.GetValues(typeof(BO.Category));
        /// <summary>
        /// Constractor for adding product
        /// </summary>
        /// <param name="bl1"></param>The contract with the logic layer
        public ProductWindow(Action<BO.ProductForList?> action,BlApi.IBl bl1)
        {
            _bl = bl1;
            _action = action;
            InitializeComponent();
            
        }

        /// <summary>
        /// Constractor for update product
        /// </summary>
        /// <param name="bl1"></param>The contract with the logic layer
        /// <param name="p"></param>The product to update
        public ProductWindow(Action<BO.ProductForList?> action, Action<BO.ProductForList?> delete, BlApi.IBl bl1 ,BO.ProductForList? p)
        {
            _bl = bl1;
            _action = action;
            _actionDel = delete;
            ///The details of the current product:
            ProductDataBiding.Product? pToPrint = new ProductDataBiding.Product()
            {
                IDProduct = p.ID,
                PriceP = p.Price,
                NameP = p?.Name,
                AmountP = _bl.Product.GetProductManager(p.ID).InStock,
                CategoryP = p?.Category,
            };
            product = pToPrint;
            InitializeComponent();
     
            
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
                _action(_bl?.Product.GetProductByCondition(pr => pr.ID == p?.ID).FirstOrDefault());
                 MessageBox.Show("The product has been successfully added");
                this.Close();
            }

            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        /// <summary>
        /// Update details of product in the store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProducts(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            int inStock1=_bl?.Product.GetProductManager(product.IDProduct).InStock ?? throw new BO.ObgectNullableException();
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
                product.NameP = EnterName.Text;

            if (EnterPrice.Text.Length != 0)
                product.PriceP = double.Parse(EnterPrice.Text);

            if (ChooseCategory.SelectedItem != null)
                product.CategoryP = (BO.Category)ChooseCategory.SelectedItem;

            try
            {
                BO.Product? p= new BO.Product//The updating product
                {
                    ID = product.IDProduct,
                    Name = product.NameP,
                    Price = product.PriceP,
                    category = (BO.Category)product.CategoryP!,
                    InStock = inStock1,
                };
                _bl.Product.UpdatingProductDetails(p);//Update the product
                _action(_bl?.Product.GetProductByCondition(item => item.ID == p.ID).FirstOrDefault());
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
        ///  Check the values of IDProduct field, in order to get valid input
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
        /// delete product from the store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                _actionDel(_bl?.Product?.GetProductByCondition(item => item?.ID == product?.IDProduct)?.FirstOrDefault());
                _bl?.Product.DeleteProduct(product?.IDProduct??throw new ObgectNullableException());
                this.Close();
            }
            catch (IdDoesNotExistException) { MessageBox.Show("The product is not in the store"); }
            catch (CanNotDOActionException) { MessageBox.Show("The product is exist in orders in the store, can't delete"); }
            catch (InvalidVariableException) { MessageBox.Show("The minimum value of product ID is 100000"); }
            catch (ObgectNullableException) { MessageBox.Show("ERROR, please try again"); }
            catch (FailedGet) { MessageBox.Show("The product coudn't reload"); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        
    }
}
