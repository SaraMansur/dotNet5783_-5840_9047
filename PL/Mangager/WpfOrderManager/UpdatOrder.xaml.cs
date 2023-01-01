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
using BlApi;
namespace PL.WpfOrderManager
{
    /// <summary>
    /// Interaction logic for UpdatOrder.xaml
    /// </summary>
    /// 
    public partial class UpdatOrder : Window
    {
        public IBl bl = BlApi.Factory.Get();
        OrderForList m;
        public UpdatOrder(OrderForList p)
        {
            InitializeComponent();
            m = p;
        }

        private void OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try {
                bl.Order.orderDelivery(m.m_Id);
                OrderList win = new OrderList();
                this.Close();
                MessageBox.Show("The order is updat to be Delivery in succsesfuly!");
                //bool showActivated = win.ShowActivated;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
       
            //new ProductWindow(OrderDelivery).ShowDialog();
        }

        //private void OrderDelivery(OrderForList p)
        //{
        //    try { bl.Order.orderDelivery(m.m_Id); }
        //    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        //}

        private void OrderShipping_Click(object sender, RoutedEventArgs e)
        {
            try { 
                bl.Order.sendingAnInvitation(m.m_Id); 
                OrderList win = new OrderList();
                this.Close();
                MessageBox.Show("The order is updat to be shipping in succsesfuly!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            //new ProductWindow(OrderShipping).ShowDialog();
        }

        //private void OrderShipping(OrderForList p)
        //{
        //    try { bl.Order.sendingAnInvitation(m.m_Id); }
        //    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        //}
    }

    //public partial class ProductWindow : Window
    //{
    //    public IBl bl = BlApi.Factory.Get();
    //    private Action<OrderForList> orderAction;

    //    public ProductWindow()
    //    {
    //        InitializeComponent();
    //    }

    //    public ProductWindow(Action<OrderForList> orderAction) : this()
    //    {
    //        this.orderAction = orderAction;
    //    }
    //}
}
