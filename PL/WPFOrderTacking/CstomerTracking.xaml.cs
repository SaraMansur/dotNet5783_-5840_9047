using BlApi;
using BO;
using PL.WpfNewOrder;
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
        BO.Customer customer;
        List<DO.Order?> orders = new List<DO.Order?>();
        public CstomerTracking(BO.Customer c=null )
        {
            InitializeComponent();
            customer= c;
            if(customer== null) { myItems.Visibility = Visibility.Collapsed; }
            else {
                orders = bl.Customer.AllOrder(customer);
                myItems.DataContext= orders; }
        }

        private void Orderdetails_click(object sender, RoutedEventArgs e) 
        {
            if(OrderId.Text=="")
            {
                MessageBox.Show("Empty input!");
                return;
            }
            try { new OrderDetails(null, null,int.Parse(OrderId.Text)).Show();}
            catch (Exception ex) { MessageBox.Show("Please enter correct details again."); }
        }

        private void View_click(object sender, RoutedEventArgs e) 
        {
            if (OrderId.Text == "")
            {
                MessageBox.Show("Empty input!");
                return;
            }
            try { new View(int.Parse(OrderId.Text)).Show(); }
            catch (Exception ex) { MessageBox.Show("Please enter correct details again."); }
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e) {
            if (customer == null) { new MainWindow().Show(); this.Close(); }
            else { new createNewOrder(customer.m_Cart, customer).Show(); this.Close(); }
        }

    }
}
