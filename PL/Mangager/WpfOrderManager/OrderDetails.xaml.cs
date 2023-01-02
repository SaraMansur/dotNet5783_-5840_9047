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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        public IBl bl = Factory.Get();
        private ObservableCollection<OrderForList> Orderlist;
        private ObservableCollection<OrderItem?> Items;
        //private ObservableCollection<>
        Order order;
        public OrderDetails(OrderForList p)
        {
            InitializeComponent();
            order = bl.Order.orderDetails(p.m_Id);
            DataContext = order;
            Items = new(order.m_orderItems);
            myItems.DataContext = Items;    
        }

        private void OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.orderDelivery(order.m_Id);
                OrderList win = new OrderList();
                this.Close();
                MessageBox.Show("The order is updat to be Delivery in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void OrderShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.sendingAnInvitation(order.m_Id);
                OrderList win = new OrderList();
                this.Close();
                MessageBox.Show("The order is updat to be shipping in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
    }
}
