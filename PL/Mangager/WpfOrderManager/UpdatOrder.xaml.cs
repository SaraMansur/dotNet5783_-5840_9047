using BlApi;
using BO;
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

namespace PL.WpfOrderManager
{
    /// <summary>
    /// Interaction logic for UpdatOrder.xaml
    /// </summary>
    /// 
    public partial class UpdatOrder : Window
    {
        public IBl bl = Factory.Get();
        OrderForList m;
        public UpdatOrder(OrderForList p)
        {
            InitializeComponent();
            m = p;
        }

        private void OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try { bl.Order.orderDelivery(m.m_Id); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void OrderShipping_Click(object sender, RoutedEventArgs e)
        {
            try { bl.Order.sendingAnInvitation(m.m_Id); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
    }
}
