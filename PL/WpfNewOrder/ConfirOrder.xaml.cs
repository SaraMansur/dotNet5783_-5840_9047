using BlApi;
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

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for ConfirOrder.xaml
    /// </summary>
    public partial class ConfirOrder : Window
    {
        BO.Cart _myOrder;
        IBl bl = Factory.Get();

        public ConfirOrder(BO.Cart cart)
        {
            InitializeComponent();
            _myOrder = new BO.Cart() { m_CustomerAdress = "", m_CustomerMail = "", m_CustomerName = "", m_orderItems = cart.m_orderItems, m_TotalPrice = cart.m_TotalPrice };

            this.DataContext = _myOrder;
        }

        private void Confirm_Order_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.OrderConfirmation(_myOrder, _myOrder.m_CustomerName, _myOrder.m_CustomerMail, _myOrder.m_CustomerAdress);
            MessageBox.Show("Your order has been successfully placed! Thank you for choosing to buy from us"); this.Close();
        }
    }
}
