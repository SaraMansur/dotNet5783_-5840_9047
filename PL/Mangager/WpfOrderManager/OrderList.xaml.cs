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

        public OrderList(int num=0)
        {
            InitializeComponent();
            Orderlist = new(bl.Order.OrderList()!);
            this.DataContext = Orderlist;
            Orderlist.CollectionChanged += this.OnCollectionChanged; 
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Get the sender observable collection
            ObservableCollection<OrderForList> obsSender = sender as ObservableCollection<OrderForList >;

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

        private void List_Order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList p = (List_Order.SelectedItem as OrderForList);
            if (p != null)
                new OrderDetails(p).Show();
        }
    }
}

public interface INotifyCollectionChanged 
{
    void NotifyCollectionChanged() { }
}