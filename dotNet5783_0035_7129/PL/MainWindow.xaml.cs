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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using OrderTrackingDataBiding;



namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        //constracotr
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Show the admin window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProductList(object sender, RoutedEventArgs e)//In click event. open the ProductListWindow
        {
            Admin admin = new Admin(bl ?? throw new BO.ObgectNullableException());
            admin.ShowDialog();

        }

        /// <summary>
        /// Show the customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            UserDetails user = new UserDetails(bl);
            user.ShowDialog();
        }
    }
}
