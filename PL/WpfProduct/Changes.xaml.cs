using BlApi;

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
    /// Interaction logic for Changes.xaml
    /// </summary>
    public partial class Changes : Window
    {
        IBl bl = Factory.Get();
        BO.Product P = new BO.Product();
        public Changes(int ID=-1)
        {
            InitializeComponent();
            category.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            if (ID != -1)
            {
                AddP.Visibility = Visibility.Collapsed;
                AddP.IsEnabled = false;
                P =bl.Product.ProductId(ID);
                Id.Text = Convert.ToString(P.m_Id);
                Name.Text = Convert.ToString(P.m_Name);
                Price.Text = Convert.ToString(P.m_Price);
                InStock.Text = Convert.ToString(P.m_InStock);
                category.SelectedItem = P.m_Category;
            }
            else
                UpdateP.Visibility = Visibility.Collapsed;
        }

        private void AddP_Click(object sender, RoutedEventArgs e)
        {
            int ID = P.m_Id;
            if (int.TryParse(Id.Text, out ID))
            P.m_Category = (BO.Enums.Category)category.SelectedItem;
            P.m_Id = int.Parse(Id.Text);
            P.m_Name = Name.Text;
            P.m_Price = int.Parse(Price.Text);
            P.m_InStock = int.Parse(InStock.Text);
            bl.Product.AddProduct(P);
        }

        private void UpdateP_Click(object sender, RoutedEventArgs e)
        {
            P.m_Category = (BO.Enums.Category)category.SelectedItem;
            P.m_Id = int.Parse(Id.Text);
            P.m_Name = Name.Text;
            P.m_Price = int.Parse(Price.Text);
            P.m_InStock = int.Parse(InStock.Text);
            bl.Product.UpdateProduct(P);
          
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e)
        {
            new Catalog().Show();
            this.Close();
        }
    }

}
