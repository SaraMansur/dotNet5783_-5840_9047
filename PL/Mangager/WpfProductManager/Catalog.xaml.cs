using BlApi;
using BlImplementation;
using BO;
using PL.WpfOrderManager;
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


namespace PL.WpfProduct
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<ProductForList> productForLists { get; set; }

        public Catalog()
        {
            InitializeComponent();
            productForLists = new ObservableCollection<ProductForList>(bl.Product.ProductList().ToList());
            this.DataContext = productForLists;
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

        }

        private void addProduct(ProductForList productForList) => productForLists.Add(productForList);
        private void Button_Click(object sender, RoutedEventArgs e) => new Changes(addProduct).Show();//opens product window

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _productsList.ItemsSource = bl.Product.FilterBycategory((BO.Enums.Category)AttributeSelector.SelectedItem);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e) { new Changes().Show();  }

        private void MouseDoubleClick_list(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ProductForList? p1 = (_productsList.SelectedItem as ProductForList);//creats a new productforlist
                Product product = bl?.Product.ProductId(p1.m_ID);
                if (product != null)
                {
                    //ProductWindow productWindow = new ProductWindow(p1);
                    //productWindow.ShowDialog();
                    new Changes(updateProduct, product).ShowDialog();

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void updateProduct(ProductForList productForList)
        {
            var item = productForLists.FirstOrDefault(x => x.m_ID == productForList.m_ID);
            int index = productForLists.IndexOf(item);
            productForLists[index] = productForList;
        }
    }
}



