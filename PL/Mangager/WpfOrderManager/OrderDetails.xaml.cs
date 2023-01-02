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
        private Action<OrderForList> action;
        Order order;
        public OrderDetails(OrderForList p)
        {
            InitializeComponent();
            order = bl.Order.orderDetails(p.m_Id);
            DataContext = order;
            Items = new(order.m_orderItems);
            myItems.DataContext = Items;    
        }

        public OrderDetails(Action<OrderForList> a,OrderForList p):this(p)
        {
            action = a;
        }
        private void OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order o=bl.Order.orderDelivery(order.m_Id);
                OrderForList ofl=new OrderForList() { m_AmountItems=o.m_orderItems.Count(),m_CustomerName=o.m_CustomerName,m_Id=o.m_Id,m_OrderStatus=o.m_OrderStatus,m_TotalPrice=o.m_TotalPrice};
                action(ofl);
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
                Order o=bl.Order.sendingAnInvitation(order.m_Id);
                OrderForList ofl = new OrderForList() { m_AmountItems = o.m_orderItems.Count(), m_CustomerName = o.m_CustomerName, m_Id = o.m_Id, m_OrderStatus = o.m_OrderStatus, m_TotalPrice = o.m_TotalPrice };
                action(ofl);
                OrderList win = new OrderList();
                this.Close();
                MessageBox.Show("The order is updat to be shipping in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
    }
}
