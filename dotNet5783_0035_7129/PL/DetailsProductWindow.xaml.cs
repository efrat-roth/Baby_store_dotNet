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
using BO;
using OrderDataBiding;
namespace PL
{
    /// <summary>
    /// Interaction logic for DetailsProductWindow.xaml
    /// </summary>
    public partial class DetailsProductWindow : Window
    {
        public ProductDataBiding.ProductItem? Product {
            get { return (ProductDataBiding.ProductItem?)GetValue(ProductItemProperty); }
            set { SetValue(ProductItemProperty, value); }
        }
        public static readonly DependencyProperty ProductItemProperty =
          DependencyProperty.Register("Product", typeof(ProductDataBiding.ProductItem), typeof(Window), new PropertyMetadata(null));


        BlApi.IBl? bl;
        Func<int,Cart? ,bool> action;
        Cart? cart;
        public DetailsProductWindow(BlApi.IBl? bl1, ProductDataBiding.ProductItem? p,Func<int,Cart?,bool> f,Cart? c)
        {
            bl = bl1;
            Product = p;
            action = f;
            cart = c;
            InitializeComponent();
            
        }

        /// <summary>
        /// Minus 1 in the amount in the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinusAmount(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Product?.Amount-1<0)
                {
                    MessageBox.Show("The amoun can't be minus"); 
                    return;
                }
                cart = bl?.Cart.UpdateProductAmount(cart!, Product!.ID, Product!.Amount - 1); // if the amount is bigger than 1 
            }
            catch (InvalidVariableException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
            catch (CanNotDOActionException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            Product!.Amount = Product.Amount - 1;
            Product = new ProductDataBiding.ProductItem()
            {
                ID = Product.ID,
                Amount = Product.Amount,
                Category = Product.Category,
                InStock = Product.InStock,
                Name = Product.Name,
                Price = Product.Price,

            };
            action(Product.ID,cart);
        }

        /// <summary>
        /// Adding 1 to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlusAmount(object sender, RoutedEventArgs e)
        {
            if (Product?.Amount == 0)//if the product is not in the cart
            {
                try { cart = bl?.Cart.AddProductToCart(cart!, Product.ID); }
                catch (InvalidVariableException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
                catch (CanNotDOActionException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
            }
            else//if the product is in the cart already update the amount
            {
                try
                {
                    cart = bl?.Cart.UpdateProductAmount(cart!, Product!.ID, Product.Amount + 1); // if the amount is bigger than 1 
                }
                catch (InvalidVariableException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
                catch (CanNotDOActionException) { MessageBox.Show("The amount is bigger than the amount in stock"); return; }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
            }
                
            Product!.Amount=Product.Amount+1;//change the amount
            Product = new ProductDataBiding.ProductItem()
            {
                ID = Product.ID,
                Amount = Product.Amount,
                Category = Product.Category,
                InStock = Product.InStock,
                Name = Product.Name,
                Price = Product.Price,
            };
            action(Product.ID,cart);//Send the new product to the catalog, in order to update it
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
