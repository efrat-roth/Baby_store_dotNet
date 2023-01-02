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
using Tools;
using AutoMapper;

namespace PL
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        BlApi.IBl? _bl;

        public Admin(BlApi.IBl bl1)
        {
            InitializeComponent();
            _bl = bl1;

        }

        private void ShowListProducts(object sender, RoutedEventArgs e)
        {
            ProductListWindow productList=new ProductListWindow(_bl??throw new BO.ObgectNullableException());
            productList.ShowDialog();
        }

        private void ShowListOrders(object sender, RoutedEventArgs e)
        {
            OrderListWindow orderList = new OrderListWindow(_bl);
            orderList.ShowDialog();
        }
    }
}
