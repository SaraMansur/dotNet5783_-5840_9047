using BlApi;
using BO;
using PL.WpfOrderManager;
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

namespace PL.WPFOrderTacking
{
    /// <summary>
    /// Interaction logic for CstomerTracking.xaml
    /// </summary>
    public partial class CstomerTracking : Window
    {
        public IBl bl = Factory.Get();
        public CstomerTracking()
        {
            InitializeComponent();
        }

        private void Orderdetails_click(object sender, RoutedEventArgs e) {
            try { new OrderDetails(null, int.Parse(OrderId.Text)).Show();}
            catch (Exception ex) { MessageBox.Show("Please enter correct details again."); }
        }
        private void View_click(object sender, RoutedEventArgs e) {
            try { new View(int.Parse(OrderId.Text)).Show(); }
            catch (Exception ex) { MessageBox.Show("Please enter correct details again."); }
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new MainWindow().Show();this.Close(); }   

    }
}
