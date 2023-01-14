using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        BlApi.IBl? bl;
        Cart? cart { get; set; }
        public Array _Category { get; set; } = Enum.GetValues(typeof(Category));
        public ObservableCollection<ProductItem?>? ProductsLists { get; set; }
        private IEnumerable<ProductItem?>? productsLists { get; }
        public ObservableCollection<IGrouping<BO.Category?, ProductItem?>>? ByCategory { get; set; }
        public NewOrder(BlApi.IBl bl1,Cart c)
        {
            try
            {
                bl = bl1;
                productsLists = bl.Product.GetListOfProductsItem();
                ProductsLists = new ObservableCollection<ProductItem?>(productsLists);
                cart = c;
                cart.Items = new List<OrderItem?>();
                //convert to observel in order to update the details
                ByCategory = new ObservableCollection<IGrouping<BO.Category?, ProductItem?>>
                    (from p in ProductsLists
                     orderby p.Category//order for identify the index in biding
                     group p by p.Category into g
                     select g);//divide to groups for categories view

                InitializeComponent();
            }
            catch (FailedGet f) { MessageBox.Show(f.Message); }
               
        }

        /// <summary>
        /// Delegate to update thr=e amount from product window
        /// </summary>
        /// <param name="id"></param>id of product to change
        /// <param name="amount"></param>new amount
        private bool AmountChanged(int id,Cart? c)
        {
            try
            {
                var p = ProductsLists!.FirstOrDefault(p => p?.ID == id);
                int index = ProductsLists!.IndexOf(p);
                ProductsLists[index] = bl?.Product.GetProductCustomer(id, cart!);
                cart = c;
                //Change in the category show
                int indexCategory = ByCategory!.IndexOf(ByCategory!.FirstOrDefault(g => g.Key == p?.Category)!);
                int indexCategoryitem=ByCategory[indexCategory].ToList().IndexOf(p);
                ByCategory[indexCategory].ToList()[indexCategoryitem]= bl?.Product.GetProductCustomer(id, cart!);
                
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// show only one category of product, or the all products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    var products = from p in bl?.Product?.GetProductByCondition( product => product.Category == (BO.Category)category)?.ToList()
                                   let pReturn=bl?.Product.GetProductCustomer(p.ID,cart!)
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
                ProductsLists?.Clear();
                foreach (var item in products)
                {
                    ProductsLists?.Add(item);
                }
            }
        }
        
        /// <summary>
        /// Show details of product, by click it twice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsOfProduct(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            if (list.SelectedItem == null) return;
            ProductItem? p = (ProductItem?)list.SelectedItem;
            ProductDataBiding.ProductItem? product = new ProductDataBiding.ProductItem()
            {
                ID = p?.ID??0,
                Category = p?.Category,
                Name = p?.Name,
                Price = p.Price,
                Amount= p.AmountInCart,
                InStock = p.InStock,
            };
            DetailsProductWindow details = new DetailsProductWindow(bl,product,AmountChanged,cart);
            details.ShowDialog();
        }
        
        private void ShowCart(object sender, RoutedEventArgs e)
        {
            CartWindow c = new CartWindow(bl!,cart!);
            c.ShowDialog();
        }

        /// <summary>
        /// Can input only numbers to the ID field
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
        /// Can input only numbers to the amount field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //private void CategoryCheck_Checked(object sender, RoutedEventArgs e)
        //{
        //    ByCategory = new ObservableCollection<IGrouping<BO.Category?, ProductItem?>>
        //            (from p in ProductsLists
        //             orderby p.Category//order for identify the index in biding
        //             group p by p.Category into g
        //             select g);//divide to groups for categories view
            
        //}
    }
}
