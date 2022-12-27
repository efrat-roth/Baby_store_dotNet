﻿using System;
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
using OrderDataBiding;
namespace PL
{
    /// <summary>
    /// Interaction logic for DetailsProductWindow.xaml
    /// </summary>
    public partial class DetailsProductWindow : Window
    {
        Product Product = new Product();
        BlApi.IBl? _bl;
        public DetailsProductWindow(BlApi.IBl? bl, Product p)
        {
            InitializeComponent();
            _bl = bl;
            Product = p;
            detailsProductPresentation.DataContext = p;
        }
    }
}