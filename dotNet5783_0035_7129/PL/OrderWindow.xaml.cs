﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using BO;




namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl;
        public OrderDataBiding.Order? order { get; set; }
        public Action<OrderForList?>? Action1 { get; set; }
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="a"></param>Action
        /// <param name="bl1"></param>IBl
        /// <param name="o"></param>OrderForList?
        public OrderWindow(Action<OrderForList?>? a, IBl? bl1, BO.OrderForList? o)
        {
            try
            {
                bl = bl1;
                Action1 = a;
                BO.Order? orderHelp = bl?.Order.GetDetailsOrderManager(o?.ID??throw new ObgectNullableException());
                OrderDataBiding.Order? order1 = new OrderDataBiding.Order()//build the order with dependency properties for biding
                {
                    ID = o!.ID,
                    TotalPrice = o.TotalPrice,
                    Name = o.CustomerName,
                    AmountOfItems = o.AmountOfItems,
                    Status = (BO.OrderStatus?)o.Status,
                    Email = orderHelp?.CustomerEmail,
                    Adress = orderHelp?.CustomerAdress,
                    OrderDate = orderHelp?.OrderDate,
                    ShipDate = orderHelp?.ShipDate,
                    DeliveryDate = orderHelp?.DeliveryDate,
                    Items = orderHelp?.Items
                };
                order = order1;
                InitializeComponent();
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        /// <summary>
        /// Update an order, adding or update amount of product, update status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order1 = new BO.Order();
                if (updateShiped.IsChecked == false && updateDelivery.IsChecked == false)
                {//Input integrity check in case the uset didn't input the all details
                    MessageBox.Show("Please input at least one detail to update");
                    return;
                }

                if (updateShiped.IsChecked == true)
                {
                    order1 = bl?.Order.DeliveredOrder(order.ID)!;
                    Action1!(bl?.Order.GetListOfOrders().FirstOrDefault(o => o?.ID == order1?.ID));//update in the list of products
                }
                if (updateDelivery.IsChecked == true)
                {
                    order1 = bl?.Order.ArrivedOrder(order.ID)!;
                    Action1!(bl?.Order.GetListOfOrders().FirstOrDefault(o => o?.ID == order1?.ID));//update in the list of products
                }
                MessageBox.Show("The order has been successfuly updated");
                this.Close();
            }
            catch (CanNotDOActionException)
            {
                MessageBox.Show("The order is already shiped or arrived, can't change the date"); return;
            }
            catch (InvalidVariableException)
            {
                MessageBox.Show("The details are invalid, please check again"); return;
            }
            catch (FailedGet)
            {
                MessageBox.Show("The order wasn't found"); return;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }


        /// <summary>
        ///  Check the values of IDProduct field, in order to get valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdIsNumber(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt))) return;
            e.Handled = true;
            return;

        }

        /// <summary>
        ///  Check the values of amount field, in order to get valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void amountIsNumber(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt))) return;
            e.Handled = true;
            return;

        }

        private void UpdateAmountOfProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order? order1 = new BO.Order();

                if (GetProduct.Text.Length == 0 && amountContent.Text.Length == 0)
                {
                    MessageBox.Show("Please input at least one detail to update");
                    return;
                }
                if ((GetProduct.Text.Length > 0 && amountContent.Text.Length == 0) || (GetProduct.Text.Length == 0 && amountContent.Text.Length > 0))
                {//if only one depended detail typed
                    MessageBox.Show("The amount of product is depended on IDProduct product input, you have to fill both, or neither");
                    return;
                }
                if (GetProduct.Text.Length > 0 && amountContent.Text.Length > 0)
                {//case of update a product
                    int idProduct, amount;
                    if (!int.TryParse(GetProduct.Text, out idProduct) ||
                    !int.TryParse(amountContent.Text, out amount))
                    {
                        MessageBox.Show("The data wasn't succeded to convert to int, please input the datails again ");
                        return;
                    }
                    order1 = bl?.Order.UpdateOrder(order.ID, idProduct, amount)!;
                    if (order1?.Items?.Count() == 0)//if delete the last product in the order, delete the order
                    {
                        OrderForList? orderFor = new OrderForList()
                        {
                            ID = order1.ID,
                            AmountOfItems = 0,
                        };
                        Action1!(orderFor);//update in the list of products
                    }
                    else
                        Action1!(bl?.Order.GetListOfOrders().FirstOrDefault(o => o?.ID == order1?.ID));//update in the list of products
                    MessageBox.Show("The order has been successfuly updated");
                    this.Close();
                }
            }
            catch (CanNotDOActionException)
            {
                MessageBox.Show("The order is already shiped or arrived, can't change the date and the product details"); return;
            }
            catch (InvalidVariableException)
            {
                MessageBox.Show("The details are invalid, or the amount of product is bigger than in the stock, please check again"); return;
            }
            catch (FailedGet)
            {
                MessageBox.Show("The order wasn't found"); return;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
    
}
