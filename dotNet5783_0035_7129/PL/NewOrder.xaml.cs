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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? _bl;
        Cart? cart { get; set; }
        public Array _Category { get; set; } = Enum.GetValues(typeof(Category));
        public ObservableCollection<ProductItem?> ProductsLists { get; set; }
        private IEnumerable<ProductItem?> productsLists { get; }
        public ObservableCollection<IGrouping<BO.Category?, ProductItem?>> _ByCategory { get; set; }
        public NewOrder(BlApi.IBl bl1,Cart c)
        {
            _bl = bl1;
            productsLists = _bl.Product.GetListOfProductsItem();
            ProductsLists = new ObservableCollection<ProductItem?>(productsLists);
            cart = c;
            cart.Items = new List<OrderItem?>();
            //convert to observel in order to update the details
            _ByCategory = new ObservableCollection<IGrouping<BO.Category?, ProductItem?>>
                (from p in ProductsLists
                 orderby p.Category//order for identify the index in biding
                 group p by p.Category into g
                 select g);//divide to groups for categories view
            InitializeComponent();
               
        }
        /// <summary>
        /// Add product to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct(object sender, RoutedEventArgs e)
        { 

            if(idD.Text.Length==0||amountD.Text.Length==0)
            {
                MessageBox.Show("You have to enter the details first");
                return;
            }
            try
            {
                //find the index of product for products show:
                int index = ProductsLists.IndexOf(ProductsLists.FirstOrDefault(p=>p?.ID== int.Parse(idD.Text)));
                //find the grup of the product:
                //var group = _ByCategory.FirstOrDefault(g => g.Key == ProductsLists[index]?.Category);
                //find its index:
                //int index2= group.ToList().IndexOf(group.FirstOrDefault(p => p?.ID == int.Parse(idD.Text)));
                cart = _bl?.Cart.AddProductToCart(cart!, int.Parse(idD.Text));
                cart=_bl?.Cart.UpdateProductAmount(cart!, int.Parse(idD.Text), int.Parse(amountD.Text));    
                ProductsLists[index] = _bl?.Product.GetProductCustomer(int.Parse(idD.Text), cart);                
                MessageBox.Show("The product is added to the cart");

            }
            catch(FailedGet )
            {
                MessageBox.Show("The product is not in the store, enter the IDProduct again");
            }
            catch(InvalidVariableException )
            {
                MessageBox.Show("The amount is bigger than the amount in stock");
            }
            catch (CanNotDOActionException )
            {
                MessageBox.Show("The amount is bigger than the amount in stock");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void CategroyFilter(object sender, SelectionChangedEventArgs e)
        {
            Category? category = CategorySelector.SelectedItem as Category?;
            if (category!=null)//if the selected item is all products
            {
                if(category.Equals(Category.AllProducts))//Back to the state where you see the whole list
                {
                    var products = productsLists;
                    addProducts(products);
                }
                else
                {
                    var products = from p in _bl!.Product.GetProductByCondition( product => product.Category == (BO.Category)category).ToList()
                                   let pReturn=_bl.Product.GetProductCustomer(p.ID,cart)
                                   select pReturn;
                                              
                    addProducts(products);
                }
            }

        }
    
        /// <summary>
        /// Helping method to rebuild the list in the filter
        /// </summary>
        /// <param name="products"></param>
        private void addProducts(IEnumerable<ProductItem?> products)
        {
            if (products.Any())
            {
                ProductsLists.Clear();
                foreach (var item in products)
                {
                    ProductsLists.Add(item);
                }
            }
        }
        
        private void DetailsOfProduct(object sender, MouseButtonEventArgs e)
        {
            ProductItem? p = (ProductItem?)ProductsListView.SelectedItem;
            ProductDataBiding.ProductItem? product = new ProductDataBiding.ProductItem()
            {
                ID = p?.ID??0,
                Category = p.Category,
                Name = p.Name,
                Price = p.Price,
                Amount= p.AmountInCart,
                InStock = p.InStock,
            };
            DetailsProductWindow details = new DetailsProductWindow(_bl,product);
            details.ShowDialog();
        }
        private void ShowCart(object sender, RoutedEventArgs e)
        {
            CartWindow c = new CartWindow(_bl,cart);
            c.ShowDialog();
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
        private void AmountIsNumber(object sender, KeyEventArgs e)
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
