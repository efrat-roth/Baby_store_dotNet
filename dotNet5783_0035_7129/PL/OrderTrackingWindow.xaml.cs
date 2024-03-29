﻿using System;
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
using OrderTrackingDataBiding;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        BlApi.IBl? bl;
        Order? order;
        public OrderTrackingDataBiding.OrderTracking? orderToTrack { get; set; }
        public OrderTrackingWindow(BlApi.IBl? bl1 , OrderTrackingDataBiding.OrderTracking o)
        {
            bl = bl1;
            order = bl?.Order.GetDetailsOrderManager(o.ID);
            orderToTrack = o;            
            InitializeComponent();
        }  

        /// <summary>
        /// Show the details of the order, by clicking on a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.ObgectNullableException"></exception>
        private void ShowOrder(object sender, RoutedEventArgs e)
        {
            try
            {                                  
                OrderForList? o = new OrderForList()//convert to orderForList in order to send to order window
                {
                    ID = order?.ID??throw new BO.ObgectNullableException(),
                    AmountOfItems = bl!.Order.GetListOfOrders().FirstOrDefault(ord => ord!.ID == order.ID)!.AmountOfItems,
                    CustomerName = order.CustomerName,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice
                };
                OrderWindow orderWindow = new OrderWindow(null,bl, o);
                orderWindow.ShowDialog();
            }
            catch(FailedGet g) { MessageBox.Show(g.ToString()); }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); } 
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

    }
}
