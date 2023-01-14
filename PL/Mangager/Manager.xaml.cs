using BO;
using PL.WpfOrderManager;
using PL.WPFOrderTacking;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        Users user;
        public Manager(Users u)
        {
            InitializeComponent();
            hello.Text = "Hello " + u.Name + "!";
        }

        private void ProductForList_Click(object sender, RoutedEventArgs e) { new Catalog(user).Show(); this.Close(); }

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new MainWindow().Show(); this.Close();}

        private void OrderForList_Click(object sender, RoutedEventArgs e) { new OrderList(user).Show(); this.Close(); }

        private void OrderTracking_Click(object sender, RoutedEventArgs e) { new OdrerTacking(user,4).Show(); this.Close(); }
    }
}
