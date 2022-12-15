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


namespace PL
{
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages,AllProducts }
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl ;
        public ProductListWindow(BlApi.IBl bl1)//constractor
        {
            InitializeComponent();
            bl = bl1;
            ProductsListView.ItemsSource = bl.Product.GetListOfProduct();//Resets the list by products in the store
            CategorySelector.ItemsSource =Enum.GetValues(typeof(Category));//Input the only possible categories
            
        }
        /// <summary>
        /// Filter the list view by Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategroyFilter(object sender, SelectionChangedEventArgs e)
        {
            if(CategorySelector.SelectedItem.Equals(Category.AllProducts))//If the uset want to see the all products
            {
                ProductsListView.ItemsSource= bl?.Product.GetListOfProduct();
                return;
            }
            ProductsListView.ItemsSource=bl?.Product.GetProductByCondition(p=>p!.Category==(BO.Category)CategorySelector.SelectedItem);
        }

        /// <summary>
        /// Add product by click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new ProductWindow(bl ?? throw new BO.ObgectNullableException());
            p.Show();
            ProductsListView.ItemsSource = bl.Product.GetListOfProduct();//after the adding, show the new list
        }

        /// <summary>
        /// Update product by click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList productForList= (BO.ProductForList)ProductsListView.SelectedItem;        
            ProductWindow updateProduct =new ProductWindow(bl ?? throw new BO.ObgectNullableException(), productForList);
            updateProduct.Show();
            ProductsListView.ItemsSource =bl.Product.GetListOfProduct();//after the updating, show the new list

        }
    }
}
