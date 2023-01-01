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

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for createNewOrder.xaml
    /// </summary>
    public partial class createNewOrder : Window
    {
        IBl bl = Factory.Get();
        private ObservableCollection<ProductItem?> ProductItemlist;
        BO.Cart cart;
        public createNewOrder()
        {
            InitializeComponent();
            ProductItemlist = new(bl.Product.CatalogList());
            ProductItems.ItemsSource = ProductItemlist;
            GroupBy.ItemsSource = new string[] { "None","Grouping by category" , "Watches", "Bracelets", "Earrings", "Rings", "Necklaces" };
        }

        public Predicate<object> Filtering { get; private set; }

        public void GroupList()
        {
            ProductItems.ItemsSource = ProductItemlist;
            ProductItems.Items.GroupDescriptions.Clear();
            var property = GroupBy.SelectedItem as string;
            if (property == "None")
                return;
            if (property == "Grouping by category")
            { 
                ProductItems.Items.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                return;
            }
            //ProductItems.Items.Filter = filtering;
            ProductItems.ItemsSource = bl.Product.FilterBycategoryCustomer((BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), property));
          
        }

        //private bool filtering(object obj)
        //{
        //    var f = obj as ProductItem;
        //    string s = GroupBy.SelectedItem as string;
        //    Enums.Category p = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), s);
        //    if (f != null) 
        //        return f.Category!.Value.Equals(p);
        //    return false;      
        //}

        private void GroupBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupList();
        }

        private void ProductItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductItem p = ProductItems.SelectedItem as ProductItem;
            new ShowProductItem(p).Show();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            new Cart(cart).Show();
        }
    }
}
