using BlApi;
using BO;
using DalApi;
using DO;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using PL.WpfOrderManager;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace PL.WPFOrderTacking;
 

public partial class OdrerTacking : Window
{
    ProgressBar progbar = new ProgressBar();
    public IBl bl = BlApi.Factory.Get();
    Users user;
   // private ObservableCollection<OrderForList> OrderList;
    int flag = 0;
    BackgroundWorker Track;
    BO.Order order;
    DateTime time = DateTime.Now;
    public List<BO.OrderForList?> OrderList
    {
        get { return (List<BO.OrderForList?>)GetValue(OrderListProperty); }
        set { SetValue(OrderListProperty, value); }
    }
    public static readonly DependencyProperty OrderListProperty =
    DependencyProperty.Register("OrderList", typeof(List<BO.OrderForList?>), typeof(OdrerTacking), new PropertyMetadata(null));




    public OdrerTacking (Users u, int num = 0)
    {
        flag = num;
        OrderList = new List<OrderForList?>(bl.Order.OrderList()!);
        user = u;

        Track = new BackgroundWorker();
        Track.DoWork += Track_DoWork;
        Track.ProgressChanged += Track_ProgressChanged;
        Track.RunWorkerCompleted += Track_RunWorkerCompleted;
        Track.WorkerReportsProgress = true;
        Track.WorkerSupportsCancellation = true;
        InitializeComponent();
    }

    private void Track_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled == true)
        {
            MessageBox.Show("The process is cencelled");
        }
        else
        {
            MessageBox.Show("The process ended successfully");
        }
        //this.Cursor = Cursors.Arrow;
    }

    private void Track_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        OrderList = new List<OrderForList?>(bl.Order.OrderList()!);
        try
        {
            foreach( var Item in OrderList)
            {
                if(Item.m_OrderStatus!= BO.Enums.Status.Received)
                    order = bl.Order.orderDetails(Item.m_Id) ?? throw new Exception("order is null");
                if (order!=null)
                {
                    if (time - order.m_OrderTime >= new TimeSpan(0, 2, 0, 0) && Item.m_OrderStatus == BO.Enums.Status.Ordered)
                    {
                        order.m_OrderTime = time.AddMinutes(1);
                        order = bl.Order.sendingAnInvitation(Item.m_Id);
                    }
                    else if (time - order.m_OrderTime >= new TimeSpan(0, 1, 0, 0) && Item.m_OrderStatus == BO.Enums.Status.Shipped)
                    {
                        order.m_ShipDate = time.AddDays(1);
                        order = bl.Order.orderDelivery(Item.m_Id);
                    }
                    
                }
                OrderList = new List<OrderForList?>(bl.Order.OrderList()!);
            }
        }
        catch(Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK); }
    }

    private void Track_DoWork(object? sender, DoWorkEventArgs e)
    {
        while (true)
        {
            if (Track.CancellationPending == true)
            {
                e.Cancel = true;
                break;
            }
            else
            {
                Thread.Sleep(6000);
                time = time.AddDays(1);
                if (Track.WorkerReportsProgress == true) { Track.ReportProgress(11); }
            }
        }
    }

    private void OrderTackindView_Click(object sender, RoutedEventArgs e)
    {
        OrderForList p = (sender as Button).DataContext as OrderForList;
        if (p != null)
            new View(p.m_Id).Show();
    }


    private void Click_buttonBack(object sender, RoutedEventArgs e)
    {
      //  if (flag == 0) { new MainWindow().Show(); this.Close(); }
        new Manager(user).Show(); this.Close(); 
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        if (Track.IsBusy != true)
        {
            Track.RunWorkerAsync();
        }
    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        if (Track.WorkerSupportsCancellation == true)
            Track.CancelAsync();
    }
}

