using BlApi;
using BO;
using System;
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
        BlApi.IBl? bl;
        public ObservableCollection<OrderForList?>? OrderForLists { get; set; }
        private IEnumerable<OrderForList?>? orderForLists { get; }
        public BackgroundWorker? updateStatus;
        DateTime time;
        public Simulator(BlApi.IBl bl1)
        {
            try
            {
                bl = bl1;
                orderForLists = bl.Order.GetListOfOrders();
                OrderForLists = new ObservableCollection<OrderForList?>(orderForLists);//convert to observel in order to update the details
                updateStatus = new BackgroundWorker();
                updateStatus.DoWork += Status_DoWork;
                updateStatus.ProgressChanged += Status_ProgressChanged;
                updateStatus.RunWorkerCompleted += Status_RunWorkerCompleted;
                updateStatus.WorkerReportsProgress = true;
                updateStatus.WorkerSupportsCancellation = true;
                time = DateTime.Now;
                InitializeComponent();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Status_DoWork(object sender, DoWorkEventArgs e)
        {
            //int timeToArrived = int.Parse(e.Argument.ToString());
            //for (int i = 0; i <= timeToArrived; i++)
            //{
            try
            {
                if (updateStatus?.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    Thread.Sleep(2000);
                    if (updateStatus?.WorkerReportsProgress == true)
                        updateStatus.ReportProgress(0);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //}


        }

        private void UpdateList()
        {
            try
            {
                OrderForLists?.Clear();
                foreach (var o in bl?.Order.GetListOfOrders())
                {
                    OrderForLists?.Add(o);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Status_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                UpdateList();
                for (int i = 0; i < OrderForLists?.Count()*2; i++)
                {
                    var confirmed = bl?.Order.GetListOfOrders().Where(o => (BO.OrderStatus?)o?.Status == BO.OrderStatus.ConfirmedOrder);
                    if (confirmed?.Count() != 0)
                    {
                        OrderForList? maxC = confirmed?.MaxBy(o => bl?.Order.DateStatus(o!.ID) - time);
                        int index = OrderForLists!.IndexOf(maxC);
                        var oUpdated=bl?.Order.DeliveredOrder(maxC?.ID ?? throw new ObgectNullableException());
                        OrderForLists![index] = bl?.Order.GetListOfOrders().Find(o=>o?.ID==maxC?.ID);
                    }
                    UpdateList();
                    Thread.Sleep(2000);
                    var delivered = bl?.Order.GetListOfOrders().Where(o => o?.Status == BO.OrderStatus.DeliveredOrder);
                    if (delivered?.Count() != 0)
                    {
                        OrderForList? maxD = delivered?.MaxBy(o => bl?.Order.DateStatus(o!.ID) - time);
                        int index = OrderForLists!.IndexOf(maxD);
                        bl?.Order.ArrivedOrder(maxD!.ID);
                        OrderForLists![index] = bl?.Order.GetListOfOrders().Find(o => o?.ID == maxD?.ID);
                    }
                    UpdateList();
                    Thread.Sleep(100000);

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }


        }
        private void Status_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                {
                    OrderForLists = new ObservableCollection<OrderForList?>(orderForLists);
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

        private void StopTracking(object sender, RoutedEventArgs e)
        {
            try
            {
                if (updateStatus?.WorkerSupportsCancellation == true)
                    updateStatus.CancelAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        //למלא
        private void OrderTracingWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($@"");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
    }
}
