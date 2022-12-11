using BlApi;
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
    /// <summary>
    /// Interaction logic for UpdateProductWindow.xaml
    /// </summary>
    public partial class UpdateProductWindow : Window
    {
        IBl _bl;
        DO.Product product;
        public UpdateProductWindow(IBl bl,DO.Product p)
        {
            InitializeComponent();
            _bl = bl;
            ChooseCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            product = p;
            name.Content = product.Name;
        }
        private void UpdateProducts(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (EnterInStock.Text.Length == 0 &&  EnterName.Text.Length == 0
               && EnterPrice.Text.Length == 0 && ChooseCategory.SelectedItem == null)
            {
                messageBoxResult = MessageBox.Show("The product has not been updated");
                return;
            }
            if (EnterInStock.Text.Length != 0)
                product.InStock = int.Parse(EnterInStock.Text);

            if (EnterName.Text.Length != 0)
                product.Name =EnterName.Text;

            if (EnterPrice.Text.Length != 0)
                product.Price = double.Parse(EnterPrice.Text);

            if (ChooseCategory.SelectedItem != null)
                product.category = (DO.Category)ChooseCategory.SelectedItem;

            try
            {
                _bl.Product.UpdatingProductDetails(product);
                messageBoxResult = MessageBox.Show("The product has been successfuly updated");
            }
            catch(Exception ex)
            { messageBoxResult = MessageBox.Show(ex.ToString()); }
        }
    }
}
