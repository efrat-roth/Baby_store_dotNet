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
        BlApi.IBl bl;
        public ObservableCollection<OrderForList?>? OrderForLists { get; set; }
        private IEnumerable<OrderForList?>? orderForLists { get; }
        public BackgroundWorker updateStatus;
        TimeOnly now=TimeOnly.FromDateTime(DateTime.Now);

        public Simulator(BlApi.IBl bl1)
        {
            bl= bl1;
            orderForLists = bl.Order.GetListOfOrders();
            OrderForLists = new ObservableCollection<OrderForList?>(orderForLists);//convert to observel in order to update the details
            updateStatus = new BackgroundWorker();
            updateStatus.DoWork += Status_DoWork;
            updateStatus.ProgressChanged += Status_ProgressChanged;
            updateStatus.RunWorkerCompleted += Status_RunWorkerCompleted;
            updateStatus.WorkerReportsProgress = true;
            updateStatus.WorkerSupportsCancellation = true;
            InitializeComponent();
        }

        private void Status_DoWork(object sender, DoWorkEventArgs e)
        {
            int timeStatus = int.Parse(e.Argument.ToString());
            for(int i=0;i<=timeStatus;i++)
            {
                if(updateStatus.CancellationPending==true)
                {
                    e.Cancel=true;
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    if (updateStatus.WorkerReportsProgress == true)
                        updateStatus.ReportProgress(i * 100 / timeStatus);
                }
            }

        } 
        private void Status_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //OrderForLists.Clear();//clear the list
            //for (int i = 0; i < bl.Order.GetListOfOrders().Count(); i++)//update the orders in the store
            //{
            //    OrderForLists.Add(bl.Order.GetListOfOrders()[i]);
            //}

            //updates the orders on the store
            OrderForLists = new ObservableCollection<OrderForList?>(bl.Order.GetListOfOrders());            
            for (int i=0;i<OrderForLists.Count();i++)
            {
                OrderForList? order = OrderForLists[i];
                if(order?.Status==OrderStatus.ConfirmedOrder) 
                {
                    OrderForLists[i].Status = OrderStatus.DeliveredOrder;
                }
                else if(order?.Status == OrderStatus.DeliveredOrder)
                {
                    OrderForLists[i].Status = OrderStatus.ArrivedOrder;
                }
                Thread.Sleep(100);
                var orders = from o in bl.Order.GetListOfOrders()
                             let selectOrder = OrderForLists.Where(ofl => ofl.ID == o.ID)
                             where selectOrder.Count() == 0
                             select o;
                for (int j = 0; j < orders.Count(); j++)//adding the orders that added to the store
                {
                    OrderForLists.Add(orders.ToList()[j]);
                }
            }
            
        }
        private void Status_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled==true)
            {
                OrderForLists = new ObservableCollection<OrderForList?>(orderForLists);
            }
            else
            {
                MessageBox.Show(@$"         Well Done!
                                   All the orders have arrived");
                this.Cursor = Cursors.Arrow;
            }
        }
        private void StartTracking(object sender, RoutedEventArgs e)
        {
            Thread simulator = Thread.CurrentThread;
            new Thread(() => { Thread.Sleep(1000); simulator.Interrupt();  }).Start();
        }

        private void StopTracking(object sender, RoutedEventArgs e)
        {
            if(updateStatus.WorkerSupportsCancellation==true)
                updateStatus.CancelAsync();
        }

        private void OrderTracingWindow(object sender, RoutedEventArgs e)
        {
            //Button button = (Button)sender;
            //OrderForList orderForList = (OrderForList)OrdersListView.SelectedItem;
            //OrderTrackingDataBiding.OrderTracking orderTracking = new OrderTrackingDataBiding.OrderTracking()
            //{
            //    ID = orderForList.ID,
            //    Status = orderForList.Status,
            //    ListDateStatus = new ObservableCollection<NodeDateStatus?>(bl.Order.OrderTracking(orderForList.ID).ListDateStatus)
            //};
            //OrderTrackingWindow orderTracingWindow = new OrderTrackingWindow(bl, orderTracking);
            //orderTracingWindow.Show();
        }
    }
}
