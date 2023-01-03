using BlApi;
using BO;
using DO;
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

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for ShowProductItem.xaml
    /// </summary>
    public partial class ShowProductItem : Window
    {
        IBl bl = Factory.Get();
        BO.Cart cart; BO.ProductItem productItem;

        private Action<ProductItem, BO.Cart> action;

        public ShowProductItem(BO.ProductItem p,BO.Cart C)
        {
            InitializeComponent();
            cart = C; productItem= p;
            this.DataContext = p;
        }

        public ShowProductItem(Action<ProductItem,BO.Cart> action, ProductItem? myData , BO.Cart C) : this(myData, C)//gets value from selecting on te comboBox to update
        {
            cart = C;
            this.action = action;
            GridProduct.DataContext = myData;//putting all the data in the textboxes via binding
        }

        private void AddCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl.Cart.AddItemToCart(cart, productItem.m_ID);//adds the product to the do
                action(bl?.Product.CatalogProductId(productItem.m_ID), cart);// ?? throw new BO.ArgumentNull());//goes back a window and does the update to the oc
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void RemoveCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                createNewOrder win = new createNewOrder(bl.Cart.UpdateAmount(cart, productItem.m_ID, -1));
                MessageBox.Show("Remove One From This Item To The Cart!");
                //bool showActivated = win.ShowActivated;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ConfirmAmount_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
