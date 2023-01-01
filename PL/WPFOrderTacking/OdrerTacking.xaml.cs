using BlApi;
using BO;
using DalApi;
using DO;
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
using System.Collections.ObjectModel;
using System.Xml.Linq;
using PL.WpfOrderManager;

namespace PL.WPFOrderTacking
{
    ///// <summary>
    // Interaction logic for OdrerTacking.xaml
    //</summary>
    public partial class OdrerTacking : Window
    {
        public IBl bl = BlApi.Factory.Get();
        private ObservableCollection<OrderForList> _OrderList;
        public OdrerTacking()
        {
            InitializeComponent();
            _OrderList = new(bl.Order.OrderList()!);
            this.DataContext = _OrderList;
        }

        private void OrderTackindView_Click(object sender, RoutedEventArgs e) 
        {
            OrderForList p = (List_Order.SelectedItem as OrderForList);
            if (p != null)
                new View(p).Show();
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new MainWindow().Show(); this.Close(); }

    }
}
