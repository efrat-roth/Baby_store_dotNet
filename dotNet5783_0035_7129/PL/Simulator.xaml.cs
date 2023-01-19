using BlApi;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;




    namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        ProgressBar ProgressBar=new ProgressBar();
        IBl? bl;
        public BackgroundWorker? updateStatus;
        BO.Order? order=new BO.Order();

        ///The date of the window
        public DateTime time=DateTime.Now;

        //Global variable for all time sleep
        private const int c_timeSleep = 2000;

        
        /// field  for check if the all order was arrives
        bool allOrderArrived ;

        public List<BO.OrderForList?> OrderForLists
        {
            get { return (List<BO.OrderForList?>)GetValue(OrderForListsProperty); }
            set { SetValue(OrderForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderForLists.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderForListsProperty =
            DependencyProperty.Register("OrderForLists", typeof(List<BO.OrderForList>), typeof(Simulator), new PropertyMetadata(null));

        public Simulator(BlApi.IBl bl1)
        
        {
            try
            {
                InitializeComponent();
                bl = bl1;
                allOrderArrived = false;
                OrderForLists = bl.Order.GetListOfOrders();//convert to observel in order to update the details
                updateStatus = new BackgroundWorker();
                updateStatus.DoWork += Status_DoWork;
                updateStatus.ProgressChanged += Status_ProgressChanged;
                updateStatus.RunWorkerCompleted += Status_RunWorkerCompleted;
                updateStatus.WorkerReportsProgress = true;
                updateStatus.WorkerSupportsCancellation = true;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Start the process of changing the dates of order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!allOrderArrived)
                {
                    if (updateStatus?.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(c_timeSleep);
                        time.AddMonths(1);
                        if (updateStatus?.WorkerReportsProgress == true)
                        {  
                            updateStatus.ReportProgress(11);//Go to the change in the process
                        }
                    }
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //}


        }

        /// <summary>
        /// The process of changing the dates of order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                time.AddMonths(1);
                for (int i = 0; i < OrderForLists.Count; i++)
                {
                    if (OrderForLists[i]!.Status != BO.OrderStatus.ArrivedOrder)//find only orders that not arrived
                    {
                        order = bl?.Order.GetDetailsOrderManager(OrderForLists[i]!.ID);
                    }
                    if (time - order?.OrderDate >= new TimeSpan(0, 2, 0, 0)
                        && OrderForLists[i]!.Status == BO.OrderStatus.ConfirmedOrder)//if the order was created before more 2 days
                    {
                        order!.OrderDate = time.AddDays(1);
                        order = bl?.Order.DeliveredOrder(OrderForLists[i]!.ID);

                    }
                    else if (time - order?.OrderDate >= new TimeSpan(0, 3, 0, 0) &&
                        OrderForLists[i]!.Status == BO.OrderStatus.DeliveredOrder)//if the order was created before more than 7 days
                    {
                        order.ShipDate = time.AddDays(2);
                        order = bl?.Order.ArrivedOrder(OrderForLists[i]!.ID);
                    }
                    OrderForLists = new List<OrderForList?>(bl?.Order.GetListOfOrders()!);
                    if (OrderForLists.All(o => o?.Status == OrderStatus.ArrivedOrder)) 
                            allOrderArrived = true;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        /// <summary>
        /// Finish the process of update dates of orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                {
                    OrderForLists = new List<OrderForList?>(bl.Order.GetListOfOrders());
                }
                else
                {
                    MessageBox.Show(@$"             Well Done!
All the orders have arrived");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void StartTracking(object sender, RoutedEventArgs e)
        {
            try{
                if (updateStatus?.IsBusy != true)
                    updateStatus?.RunWorkerAsync();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Cancel the process of changing dates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopTracking(object sender, RoutedEventArgs e)
        {
            try
            {
                if (updateStatus?.WorkerSupportsCancellation == true)
                { updateStatus.CancelAsync();
                    MessageBox.Show("Stop update status");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        /// <summary>
        /// The method show the track details of order
        /// </summary>
        /// <param name="sender"></param>Button
        /// <param name="e"></param>
        private void OrderTracingWindow(object sender, RoutedEventArgs e)
        {
                Button button = (sender as Button);
                int x = (int)button.Tag;//Gets the ID
            try
            {
                MessageBox.Show($@" ID: {x}
status: {bl?.Order.OrderTracking(x).Status}
Dates: 
                { bl.Order.OrderTracking(x).ListDateStatus.First()}
                { bl.Order.OrderTracking(x).ListDateStatus.FirstOrDefault(node => node?.status == "The order was delivered")}
                { bl.Order.OrderTracking(x).ListDateStatus.FirstOrDefault(node => node?.status == "The order was arrived")}
                ");//Gets the dates by bl method
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
    }
   


}
