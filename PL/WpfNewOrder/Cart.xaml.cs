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
        BO.Cart C = new BO.Cart(); BO.ProductItem productItem;
        private readonly ObservableCollection<BO.OrderItem?> _dates = new ObservableCollection<BO.OrderItem?>();
        public ObservableCollection<BO.OrderItem?> Dates { get { return _dates; } }

        //public double m_TotalPrice
        //{
        //    get { return (double)GetValue(TotalPrice); }
        //    set { SetValue(TotalPrice, value); }
        //}

        //// Using a DependencyProperty as the backing store for m_TotalPrice.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TotalPrice =
        //    DependencyProperty.Register("TotalPrice", typeof(double), typeof(Cart), new PropertyMetadata(0));

        IBl bl = Factory.Get();

        public Cart( BO.Cart cart , BO.ProductItem p=null)
        {
            //m_TotalPrice = cart.m_TotalPrice;
            InitializeComponent();
            C= cart; productItem = p;
            this.DataContext = C;//m_TotalPrice;
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
                for (int i = C.m_orderItems.Count-1; i >= 0; i--)
                {
                    _dates.Remove(C.m_orderItems[i]);
                    C = bl.Cart.UpdateAmount(C, C.m_orderItems[i].m_IdProduct, 0);
                }
                //C.m_orderItems = null;
                //m_TotalPrice = 0;
                MessageBox.Show("You have successfully emptied the cart!");
                Cart win = new Cart(C);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Confir_Order(object sender, RoutedEventArgs e) { new ConfirOrder().Show(); }
    }

    //class DP : DependencyObject
    //{

    //}
}
