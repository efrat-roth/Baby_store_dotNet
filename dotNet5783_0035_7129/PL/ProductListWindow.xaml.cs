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
using AutoMapper;
using BlApi;

namespace PL
{
    
    public enum Category { Clothes, Bottles, Toys, Socks, Accessories, BabyCarriages, AllProducts }
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? _bl ;
        public ObservableCollection<ProductForList?>? _ProductForLists {get;set;}
        private IEnumerable<ProductForList?>? _productForLists { get; }
        public Array _Category { get; set; } = Enum.GetValues(typeof(Category));
        public ProductListWindow(BlApi.IBl bl1)//constractor
        {
            _bl = bl1;
            _productForLists = _bl.Product.GetListOfProduct().ToList();
            _ProductForLists = new ObservableCollection<ProductForList?>(_productForLists);//convert to observel in order to update the details           
            InitializeComponent(); 
            
        }
        /// <summary>
        /// Filter the list view by Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategroyFilter(object sender, SelectionChangedEventArgs e)
        {
            Category? category = CategorySelector.SelectedItem as Category?;
            if (category != null)//if the selected item is all products
            {
                if (category.Equals(Category.AllProducts))//Back to the state where you see the whole list
                {
                    var products = _productForLists;
                    addProducts(products);
                }
                else
                {
                    var products = _bl!.Product.GetProductByCondition(product => product.Category == (BO.Category)category);

                    addProducts(products);
                }
            }

        }

        /// <summary>
        /// Helping method to rebuild the list in the filter
        /// </summary>
        /// <param name="products"></param>
        private void addProducts(IEnumerable<ProductForList?> products)
        {
            if (products.Any())
            {
                _ProductForLists?.Clear();
                foreach (var item in products)
                {
                    _ProductForLists?.Add(item);
                }
            }
        }
        private void addP(BO.ProductForList productForList) => _ProductForLists?.Add(productForList);
        /// <summary>
        /// Add product by click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new ProductWindow(addP!,_bl ?? throw new BO.ObgectNullableException());
            p.ShowDialog();
        }
        private void UpdateP(ProductForList? productForList)
        {
            var p = _ProductForLists?.FirstOrDefault(item => item?.ID == productForList?.ID);
            int index = _ProductForLists.IndexOf(p);
            _ProductForLists[index] = productForList;

        }
        private void DeleteP(ProductForList? productForList)
        {
            var p = _ProductForLists?.FirstOrDefault(item => item?.ID == productForList?.ID);
            int index = _ProductForLists.IndexOf(p);
            _ProductForLists.Remove(_ProductForLists[index]);
        }
        /// <summary>
        /// Update product by click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProduct(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList? productForList = (BO.ProductForList?)ProductsListView.SelectedItem;
           
            ProductWindow updateProduct =new ProductWindow(UpdateP, DeleteP, _bl ?? throw new BO.ObgectNullableException(), productForList);
            updateProduct.ShowDialog();

        }
      
    }
}
