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
            admin.Show();
            
        }

        /// <summary>
        /// Show the new order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.ObgectNullableException"></exception>
        private void ShowNewOrder(object sender, RoutedEventArgs e)//In click event. open the ProductListWindow
        {
            NewOrder newOrder = new NewOrder(bl ?? throw new BO.ObgectNullableException());
            newOrder.Show();

        }

        /// <summary>
        /// Show the track order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.ObgectNullableException"></exception>
        private void ShowTrackOrder(object sender, RoutedEventArgs e)//In click event. open the ProductListWindow
        {
            TrackOrder track = new TrackOrder(bl ?? throw new BO.ObgectNullableException());
            track.Show();

        }
    }
}
