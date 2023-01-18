using BlApi;
using BO;
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

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for ConfirOrder.xaml
    /// </summary>
    public partial class ConfirOrder : Window
    {
        BO.Cart _myOrder;
        BO.Customer customer;    
        IBl bl = Factory.Get();
        private Action< List<BO.OrderItem?>?> action;

        public ConfirOrder(BO.Cart cart, BO.Customer cc)
        {
            InitializeComponent();
            _myOrder = new BO.Cart() { m_CustomerAdress = "", m_CustomerMail = "", m_CustomerName = "", m_orderItems = cart.m_orderItems, m_TotalPrice = cart.m_TotalPrice };
            if (cc != null) { _myOrder.m_CustomerAdress = cc.m_Cart.m_CustomerAdress; _myOrder.m_CustomerMail = cc.m_Cart.m_CustomerMail; _myOrder.m_CustomerName = cc.Name; }
            customer = cc;
             this.DataContext = _myOrder;
           // else this.DataContext = cc.m_Cart; 
        }

        public ConfirOrder(Action<List<BO.OrderItem?>?> a, BO.Cart cart, BO.Customer cc) :this(cart ,cc)
        {
            action= a;
        }

        private void Confirm_Order_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                bl.Cart.OrderConfirmation(_myOrder, _myOrder.m_CustomerName, _myOrder.m_CustomerMail, _myOrder.m_CustomerAdress);
                List<BO.OrderItem?>? list = _myOrder.m_orderItems;
                action(list);
                MessageBox.Show("Your order has been successfully placed! Thank you for choosing to buy from us"); this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
