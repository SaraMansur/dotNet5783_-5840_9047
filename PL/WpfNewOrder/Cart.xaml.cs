using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        public BO.Cart C { get; set; }
        private ObservableCollection<BO.OrderItem?> orderItems;
        IBl bl = Factory.Get();
        public Cart( BO.Cart cart)
        {
            InitializeComponent();
            C= cart;    
            this.DataContext = C;
            create();
            Items.DataContext= orderItems;  
        }

        private void create()
        {
            for (int i = 0; i < C.m_orderItems.Count; i++)
                orderItems.Add(C.m_orderItems[i]);
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new createNewOrder().Show(); this.Close(); }
    }
}
