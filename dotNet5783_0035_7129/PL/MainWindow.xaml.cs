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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BlImplementation;



namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = new Bl();
        //constracotr
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Show the list view window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProductList(object sender, RoutedEventArgs e)//In click event. open the ProductListWindow
        {
            ProductListWindow productListWin = new ProductListWindow(bl);
            productListWin.Show();
            
        }
    }
}
