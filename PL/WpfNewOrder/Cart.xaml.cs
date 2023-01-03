using BlApi;
using BO;
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
        BO.Cart C; BO.ProductItem productItem;
        private readonly ObservableCollection<BO.OrderItem?> _dates = new ObservableCollection<BO.OrderItem?>();
        public ObservableCollection<BO.OrderItem?> Dates { get { return _dates; } }

        IBl bl = Factory.Get();

        public Cart( BO.Cart cart , BO.ProductItem p=null)
        {
            InitializeComponent();
            productItem = p;
            C = new BO.Cart() { m_orderItems = cart.m_orderItems, m_TotalPrice = cart.m_TotalPrice };
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

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new createNewOrder(C).Show(); this.Close(); }

        private void Make_Empy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = C.m_orderItems!.Count-1; i >= 0; i--)
                {
                    _dates.Remove(C.m_orderItems![i]);
                    C = bl.Cart.UpdateAmount(C, C.m_orderItems[i]!.m_IdProduct, 0);
                }
                MessageBox.Show("You have successfully emptied the cart!");
                Cart win = new Cart(C);
                Totul_Price.Text = "0";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Confir_Order(object sender, RoutedEventArgs e) { new ConfirOrder(UpdateListView,C).Show(); }

        void UpdateListView(List<BO.OrderItem?>? l)
        {
            List<BO.OrderForList?> helper = bl.Order.OrderList().ToList();
            int index = 0;
            foreach(BO.OrderForList item in helper) index++;
            int orderId = helper[index-1].m_Id;
            int OI = bl.Order.orderDetails(orderId).m_orderItems[0].m_ID;
            for (int i = 0; i < l.Count(); i++)
            {
                var item = Dates[i];
                item.m_ID = OI;
                Dates.RemoveAt(i);  
                Dates.Insert(i, item);
                OI++;
            }
        }

        private void Items_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            BO.OrderItem OI = (Items.SelectedItem as OrderItem);
            new ShowProductItem((bl.Product.CatalogProductId(OI.m_IdProduct)), C).Show(); 
        }

        private void updateAmountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = (int)this.gradeNumUpDown.Value;
                OrderItem p = (Items.SelectedItem as OrderItem);
                if (p == null) throw new Exception();
                C = bl.Cart.UpdateAmount(C, p.m_IdProduct, amount);
                for (int i = 0; i < C.m_orderItems.Count(); i++)
                {
                    var item = C.m_orderItems[i];
                    Dates.RemoveAt(i);
                    if (amount != 0) Dates.Insert(i, item);
                }
                Totul_Price.Text = amount.ToString();
            }
            catch(Exception ex) { MessageBox.Show("You have not selected a product to update. Please select a product"); }   
        }
    }
}
