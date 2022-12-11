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
using BlImplementation;
using BO;

namespace PL
{
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages,AllProducts }
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl;
        public ProductListWindow(IBl bl1)
        {
            InitializeComponent();
            bl = bl1;
            ProductsListView.ItemsSource = bl.Product.GetListOfProduct();
            CategorySelector.ItemsSource =Enum.GetValues(typeof(Category));
            
        }
        /// <summary>
        /// Filter the list view by Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategroyFilter(object sender, SelectionChangedEventArgs e)
        {
            if(CategorySelector.SelectedItem.Equals(Category.AllProducts))
            {
                ProductsListView.ItemsSource= bl.Product.GetListOfProduct();
                return;
            }
            ProductsListView.ItemsSource=bl.Product.GetProductByCondition(p=>p!.Category==(BO.Category)CategorySelector.SelectedItem);
        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new ProductWindow(bl);
            p.Show();
            ProductsListView.ItemsSource = bl.Product.GetListOfProduct();
        }

        private void UpdateProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList productForList= (BO.ProductForList)ProductsListView.SelectedItem;        
            ProductWindow updateProduct =new ProductWindow(bl, productForList);
            updateProduct.Show();
            ProductsListView.ItemsSource =bl.Product.GetListOfProduct();

        }
    }
}
