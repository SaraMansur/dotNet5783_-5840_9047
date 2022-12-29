using BlApi;
using BO;
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

namespace PL.WpfOrderManager
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        private ObservableCollection<OrderForList?> Orderlist;
        public IBl bl = Factory.Get();

        public OrderList()
        {
            InitializeComponent();
            Orderlist = new(bl.Order.OrderList());
            this.DataContext = Orderlist;
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
    }
}
