﻿using BlApi;
using BO;
using DO;
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

        private ObservableCollection<BO.OrderItem?> Items { get; set; }

        private Action<OrderForList> action;

        BO.Order order;

        public OrderDetails(OrderForList p = null, int orderId = 0)
        {
            InitializeComponent();
            if (p != null) order = bl.Order.orderDetails(p.m_Id);
            else
            {
                order = bl.Order.orderDetails(orderId);
                shipping.Visibility = Visibility.Collapsed;
                delivery.Visibility = Visibility.Collapsed;
                update.Visibility = Visibility.Collapsed;
                gradeNumUpDown.Visibility = Visibility.Collapsed;
            }
            if (order.m_DeliveryrDate == null || order.m_DeliveryrDate == DateTime.MinValue) { deliver.Text = "This order dont Deliver yet"; }
            else { deliver.Text = order.m_DeliveryrDate.ToString(); }
            if (order.m_ShipDate == null || order.m_ShipDate == DateTime.MinValue) { ship.Text = "This order dont Ship yet"; }
            else { ship.Text = order.m_ShipDate.ToString();
                   update.Visibility = Visibility.Collapsed;
                   gradeNumUpDown.Visibility = Visibility.Collapsed;
            }
            DataContext = order;
            Items = new(order.m_orderItems);
            myItems.DataContext = Items;
        }

        public OrderDetails(Action<OrderForList> a, OrderForList p) : this(p) { action = a; }

        private void OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order o = bl.Order.orderDelivery(order.m_Id);
                OrderForList ofl = new OrderForList() { m_AmountItems = o.m_orderItems.Count(), m_CustomerName = o.m_CustomerName, m_Id = o.m_Id, m_OrderStatus = o.m_OrderStatus, m_TotalPrice = o.m_TotalPrice };
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
                BO.Order o = bl.Order.sendingAnInvitation(order.m_Id);
                OrderForList ofl = new OrderForList() { m_CustomerName = o.m_CustomerName, m_Id = o.m_Id, m_OrderStatus = o.m_OrderStatus, m_TotalPrice = o.m_TotalPrice };
                for (int i = 0; i < o.m_orderItems.Count; i++)
                    ofl.m_AmountItems = o.m_orderItems[i].m_AmountInCart;
                action(ofl);
                OrderList win = new OrderList();
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
                p= p?? throw new("Please select a product to update amount");
                BO.Order OI = new BO.Order();
                OI = bl.Order.changeOrder(order.m_Id, p.m_IdProduct, amount);
                OrderForList ofl = new OrderForList() { m_AmountItems = 0, m_CustomerName = OI.m_CustomerName, m_Id = OI.m_Id, m_OrderStatus = OI.m_OrderStatus, m_TotalPrice = OI.m_TotalPrice };
                for (int i = 0; i < OI.m_orderItems.Count; i++)
                    ofl.m_AmountItems += OI.m_orderItems[i].m_AmountInCart;
                action(ofl);
                for (int i = 0; i < Items.Count(); i++)
                {
                    var item = Items[i];
                    item = OI.m_orderItems[i];
                    Items.RemoveAt(i);
                    Items.Insert(i, item);
                }
                price.Text = OI.m_TotalPrice.ToString();
                MessageBox.Show("The order is updat in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

    }
}

