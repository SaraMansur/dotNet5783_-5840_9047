using BlApi;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Manager_Click(object sender, RoutedEventArgs e) { new Manager().Show(); this.Close(); }

        private void Neworder_Click(object sender, RoutedEventArgs e) { new createNewOrder().Show(); }

        private void OrderTracking_Click(object sender, RoutedEventArgs e) { new OdrerTacking().Show(); this.Close(); }

        private void registeredUser_Click(object sender, RoutedEventArgs e) { }
    }
}
