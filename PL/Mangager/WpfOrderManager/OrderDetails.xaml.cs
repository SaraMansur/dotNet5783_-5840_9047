using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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
        public BO.Order order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public IBl bl = BlApi.Factory.Get();
        private ObservableCollection<OrderForList> Orderlist;

        private ObservableCollection<BO.OrderItem?> Items { get; set; }

        private Action<OrderForList> action;

        Users user;

        public OrderDetails(Users u, OrderForList p = null, int orderId = 0)
        {
            InitializeComponent();
            try
            {
                if (p != null) order = bl.Order.orderDetails(p.m_Id);
                else
                {
                    order = bl.Order.orderDetails(orderId);
                    shipping.Visibility = Visibility.Collapsed;
                    delivery.Visibility = Visibility.Collapsed;
                    update.Visibility = Visibility.Collapsed;
                    gradeNumUpDown.Visibility = Visibility.Collapsed;
                }
                if (order.m_DeliveryrDate == null || order.m_DeliveryrDate == DateTime.MinValue) { deliver.Text = "This order hasn't Delivered yet"; }
                else { deliver.Text = order.m_DeliveryrDate.ToString(); }
                if (order.m_ShipDate == null || order.m_ShipDate == DateTime.MinValue) { ship.Text = "This order hasn't Shiped yet"; }
                else
                {
                    ship.Text = order.m_ShipDate.ToString();
                    update.Visibility = Visibility.Collapsed;
                    gradeNumUpDown.Visibility = Visibility.Collapsed;
                }
                Items = new(order.m_orderItems);
                myItems.DataContext = Items;
                user = u;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        public OrderDetails(Action<OrderForList> a, Users u, OrderForList p) : this(u, p) { action = a; }

        private void OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl.Order.orderDelivery(order.m_Id);
                OrderForList ofl = new OrderForList() { m_CustomerName = order.m_CustomerName, m_Id = order.m_Id, m_OrderStatus = order.m_OrderStatus, m_TotalPrice = order.m_TotalPrice };
                for (int i = 0; i < order.m_orderItems.Count; i++)
                    ofl.m_AmountItems = order.m_orderItems[i].m_AmountInCart;
                action(ofl);
                OrderList win = new OrderList(user);
                this.Close();
                MessageBox.Show("The order is updat to be Delivery in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void OrderShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl.Order.sendingAnInvitation(order.m_Id);
                OrderForList ofl = new OrderForList() { m_CustomerName = order.m_CustomerName, m_Id = order.m_Id, m_OrderStatus = order.m_OrderStatus, m_TotalPrice = order.m_TotalPrice };
                for (int i = 0; i < order.m_orderItems.Count; i++)
                    ofl.m_AmountItems = order.m_orderItems[i].m_AmountInCart;
                action(ofl);
                OrderList win = new OrderList(user);
                this.Close();
                MessageBox.Show("The order is updat to be shipping in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void UpdateAmount_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = (int)this.gradeNumUpDown.Value;
                BO.OrderItem p = (myItems.SelectedItem as BO.OrderItem);
                p = p ?? throw new("Please select a product to update amount");
                order = bl.Order.changeOrder(order.m_Id, p.m_IdProduct, amount);
                OrderForList ofl = new OrderForList() { m_AmountItems = 0, m_CustomerName = order.m_CustomerName, m_Id = order.m_Id, m_OrderStatus = order.m_OrderStatus, m_TotalPrice = order.m_TotalPrice };
                for (int i = 0; i < order.m_orderItems.Count; i++)
                    ofl.m_AmountItems += order.m_orderItems[i].m_AmountInCart;
                action(ofl);
                if(amount!=0)
                {
                    for (int j = 0; j < Items.Count(); j++)
                    {
                        var item = order.m_orderItems[j];
                        Items.RemoveAt(j);
                        Items.Insert(j, item);
                    }
                }
                if(amount == 0)
                {
                    var item = Items.FirstOrDefault(x => x.m_ID == p.m_ID);
                    int index = Items.IndexOf(item);
                    Items.RemoveAt(index);
                }
                MessageBox.Show("The order is updat in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        
        }

    }
}


