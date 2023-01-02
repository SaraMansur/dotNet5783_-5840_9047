using BlApi;
using BO;
using PL.WPFOrderTacking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized; 
using System.ComponentModel;
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

namespace PL.WpfOrderManager
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        public ObservableCollection<OrderForList> Orderlist { get; set; }
        public IBl bl = Factory.Get();

        //public Enums.Status? m_OrderStatus
        //{
        //    get { return (Enums.Status?)GetValue(DependencyStatus); }
        //    set { SetValue(DependencyStatus, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyPropertyStatus.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DependencyStatus =
        //    DependencyProperty.Register("m_OrderStatus", typeof(Enums.Status?), typeof(OrderForList), new PropertyMetadata(0));


        public OrderList(int num=0)
        {
            InitializeComponent();
            Orderlist = new(bl.Order.OrderList()!);
            DataContext = Orderlist;
            Orderlist.CollectionChanged += this.OnCollectionChanged;
        }

        private void updatOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderForList p= (List_Order.SelectedItem as OrderForList);
            if(p!=null)
              new UpdatOrder(p).Show();

        }

        private void orderDetails_Click(object sender, RoutedEventArgs e)
        {
            OrderForList p = (List_Order.SelectedItem as OrderForList);
            if (p != null)
                new OrderDetails(p).Show();
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Get the sender observable collection
            ObservableCollection<OrderForList> obsSender = sender as ObservableCollection<OrderForList>;

            List<OrderForList> editedOrRemovedItems = new List<OrderForList>();
            foreach (OrderForList newItem in e.NewItems)
            {
                editedOrRemovedItems.Add(newItem);
            }

            foreach (OrderForList oldItem in e.OldItems)
            {
                editedOrRemovedItems.Add(oldItem);
            }

            //Get the action which raised the collection changed event
            NotifyCollectionChangedAction action = e.Action;
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new Manager().Show(); this.Close(); }
    }
}

public interface INotifyCollectionChanged 
{
    void NotifyCollectionChanged() { }
}