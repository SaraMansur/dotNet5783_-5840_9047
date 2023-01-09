using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;
using System.Security.Policy;
using System.Reflection.Emit;
using System.Data;
using System.Windows.Media.TextFormatting;
using System.Security.Cryptography;

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for createNewOrder.xaml
    /// </summary>
    public partial class createNewOrder : Window
    {
        IBl bl = Factory.Get();

        public ObservableCollection<ProductItem?> ProductItemlist { get; set; }
        BO.Cart cart;

        public createNewOrder(BO.Cart c = null)
        {
            try
            {
                cart = c;
                InitializeComponent();
                ProductItemlist = new ObservableCollection<ProductItem?>(bl.Product.CatalogList(cart).ToList());
                ProductItems.ItemsSource = ProductItemlist;
                GroupBy.ItemsSource = new string[] { "None", "Grouping by category", "Watches", "Bracelets", "Earrings", "Rings", "Necklaces" };
            }
            catch(Exception e) { MessageBox.Show(e.Message); }
        }   

        public Predicate<object> Filtering { get; private set; }

        public void GroupList()
        {
            ProductItems.ItemsSource = ProductItemlist;
            ProductItems.Items.GroupDescriptions.Clear();
            var property = GroupBy.SelectedItem as string;
            if (property == "None")  return;
            if (property == "Grouping by category")
            { 
                ProductItems.Items.GroupDescriptions.Add(new PropertyGroupDescription("Category"));  return;
            }
            ProductItems.ItemsSource = bl.Product.FilterBycategoryCustomer((BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), property));
        }

        private void GroupBy_SelectionChanged(object sender, SelectionChangedEventArgs e) { GroupList(); }

        private void updateAmount(ProductItem productItem , BO.Cart _c )
        {
            var item = ProductItemlist.FirstOrDefault(x => x.m_ID == productItem.m_ID);
            int index = ProductItemlist.IndexOf(item);
            ProductItemlist.RemoveAt(index);
            item.m_AmountInCart += 1;// cart.m_orderItems.FirstOrDefault(x => x.m_ID == item.m_ID).m_AmountInCart;
            ProductItemlist.Insert(index,item);
        }

        private void Cart_Click(object sender, RoutedEventArgs e) { new Cart(cart).Show(); this.Close(); }

        private void Click_buttonBack(object sender, RoutedEventArgs e) { new MainWindow().Show(); this.Close(); }

        private void AddToCart_click(object sender, RoutedEventArgs e)
        {
            ProductItem productItem = (sender as Button).DataContext as BO.ProductItem;
            updateAmount(productItem, cart);
            cart = bl.Cart.AddItemToCart(cart, productItem.m_ID);
        }

        private void ProductItems_SelectionChanged(object sender, SelectionChangedEventArgs e) { GroupList(); }
    }
}
