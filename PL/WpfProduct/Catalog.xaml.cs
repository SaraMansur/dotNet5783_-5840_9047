using BlApi;
using BlImplementation;
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


namespace PL.WpfProduct
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        IBl bl = Factory.Get();
        public Catalog()
        {
            InitializeComponent();
            productsList.ItemsSource = bl.Product.ProductList();
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsList.ItemsSource=bl.Product.FilterBycategory((BO.Enums.Category)AttributeSelector.SelectedItem); 
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e) { new Changes().Show(); this.Close();  }

        private void MouseDoubleClick_list(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ProductForList pl = (ProductForList)productsList.SelectedItem ?? throw new ArgumentNull(); 
                new Changes(pl.m_ID).Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }  

        private void Click_buttonBack(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}



