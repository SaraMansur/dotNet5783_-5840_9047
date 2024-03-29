﻿using BlApi;
using PL.WPFOrderTacking;
using PL.WpfNewOrder;
using PL.WpfProduct;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Mangager;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BO.Cart cart = new BO.Cart() { m_CustomerAdress = "", m_CustomerName = "", m_CustomerMail = "", m_orderItems = new List<BO.OrderItem>(), m_TotalPrice = 0 };
        IBl bl = Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Manager_Click(object sender, RoutedEventArgs e) { new SignIn().Show(); this.Close(); }

        private void Neworder_Click(object sender, RoutedEventArgs e) { new createNewOrder(cart).Show(); this.Close(); }

        private void OrderTracking_Click(object sender, RoutedEventArgs e) { new CstomerTracking().Show(); this.Close(); }

        private void Customer_Click(object sender, RoutedEventArgs e) { new SignIn(4).Show(); this.Close(); }

    }
}
