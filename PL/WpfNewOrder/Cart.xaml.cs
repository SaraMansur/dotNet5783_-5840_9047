using BlApi;
using BO;
using DO;
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

        public BO.Cart C;
        BO.Customer customer;
        BO.ProductItem productItem;
        private readonly ObservableCollection<BO.OrderItem?> _dates = new ObservableCollection<BO.OrderItem?>();
        public ObservableCollection<BO.OrderItem?> Dates { get { return _dates; } }

        IBl bl = Factory.Get();

        public Cart( BO.Cart cart , BO.ProductItem p=null, BO.Customer cc=null)
        {
            InitializeComponent();
            productItem = p;
            customer=cc;    
            C = new BO.Cart() { m_orderItems = cart.m_orderItems, m_TotalPrice = cart.m_TotalPrice };
            if(cc!=null ) { C.m_CustomerAdress = cc.m_Cart.m_CustomerAdress; C.m_CustomerMail = cc.m_Cart.m_CustomerMail; C.m_CustomerName = cc.Name; }
            this.DataContext = C;
            create();
            Items.DataContext= Dates;
        }

        private void create()
        {
            if (C.m_orderItems == null) return;
            for (int i = 0; i < C.m_orderItems.Count; i++)
                _dates.Add(C.m_orderItems[i]);
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e) {
            if (customer != null)
            {
                customer.m_Cart = C;
                bl.Customer.UpdateCustomer(customer);
            }
            new createNewOrder(C, customer).Show(); this.Close(); }

        private void Make_Empy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = C.m_orderItems!.Count-1; i >= 0; i--)
                {
                    _dates.Remove(C.m_orderItems![i]);
                    C = bl.Cart.UpdateAmount(C, C.m_orderItems[i]!.m_IdProduct, 0);
                    if(customer!=null)
                    {
                        customer.m_Cart = C;
                        bl.Customer.UpdateCustomer(customer);
                    }
                }
                MessageBox.Show("You have successfully emptied the cart!");
                Cart win = new Cart(C);
                Totul_Price.Text = "0";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Confir_Order(object sender, RoutedEventArgs e) {
            if (customer != null)
            {
                customer.m_Cart = C;
                bl.Customer.UpdateCustomer(customer);
            }
            new ConfirOrder(UpdateListView,C ,customer).Show(); }

        void UpdateListView(List<BO.OrderItem?>? l)
        {
            try
            {
                List<BO.OrderForList?> helper = bl.Order.OrderList().ToList();
                int orderId = helper[helper.Count - 1].m_Id;
                int OI = bl.Order.orderDetails(orderId).m_orderItems[0].m_ID;
                for (int i = 0; i < l.Count(); i++)
                {
                    var item = Dates[i];
                    item.m_ID = OI;
                    Dates.RemoveAt(i);
                    Dates.Insert(i, item);
                    OI++;
                }
                if(customer!= null) 
                { 
                    customer.m_OrderId = orderId;   
                    customer.m_Cart.m_orderItems = l;   
                    //bl.Customer.UpdateCustomer(customer);
                    customer.m_Cart.m_orderItems.Clear();   
                    customer.m_Cart.m_TotalPrice = 0;
                    bl.Customer.UpdateCustomer(customer , 4);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void updateAmountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = C.m_orderItems.Count;
                int amount = (int)this.gradeNumUpDown.Value;
                BO.OrderItem p = (Items.SelectedItem as BO.OrderItem) ?? throw new Exception("You have not selected a product to update. Please select a product");
                C = bl.Cart.UpdateAmount(C, p.m_IdProduct, amount);
                //if (customer != null)
                //{
                //    customer.m_Cart = C;
                //    bl.Customer.UpdateCustomer(customer);
                //}
                var item = Dates.FirstOrDefault(x => x.m_IdProduct == p.m_IdProduct);
                int index= Dates.IndexOf(item);
                Dates.RemoveAt(index);
                
                if (num == C.m_orderItems.Count) Dates.Insert(index, item);
                Totul_Price.Text = C.m_TotalPrice.ToString();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message.ToString());}   
            }
    }
}
