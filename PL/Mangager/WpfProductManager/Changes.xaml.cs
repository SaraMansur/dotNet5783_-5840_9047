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
using static BO.Enums;

namespace PL.WpfProduct
{
    /// <summary>
    /// Interaction logic for Changes.xaml
    /// </summary>
    public partial class Changes : Window
    {
        IBl bl = Factory.Get();
        BO.Product P = new BO.Product();
        public Changes(int id=0)
        {
            InitializeComponent();

            category.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            if (id != null)
            {
                P = bl.Product.ProductId(id);
                this.DataContext = P;
                AddP.Visibility = Visibility.Collapsed;
            }
            else
                UpdateP.Visibility = Visibility.Collapsed;
        }

        private void AddP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.AddProduct(P);
                new Catalog().Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void UpdateP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.UpdateProduct(P);
                new Catalog().Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}