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
        public ObservableCollection<OrderForList> Orderlist;
        public IBl bl = Factory.Get();
        Users user;

        public OrderList(Users u,int num=0)
        {
            InitializeComponent();
            Orderlist = new(bl.Order.OrderList()!);
            DataContext = Orderlist;
            user = u;
        }


        private void Click_buttonBack(object sender, RoutedEventArgs e) { new Manager(user).Show(); this.Close(); }

        private void List_Order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList p = (List_Order.SelectedItem as OrderForList);
            if (p != null)
                new OrderDetails(UpdateViewList,user, p).Show();
        }

        private void UpdateViewList(OrderForList o)
        {
            var item = Orderlist.FirstOrDefault(x => x.m_Id == o.m_Id);
            int index = Orderlist.IndexOf(item);
            Orderlist.RemoveAt(index);
            Orderlist.Insert(index, o);
        }
    }
}

