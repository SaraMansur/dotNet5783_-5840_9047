using BlApi;
using BlImplementation;
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
        IBl bl = BlFactory.GetBL();
        BO.Product P = new BO.Product();
        public Changes(int ID)
        {
            InitializeComponent();
            category.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            if (ID != -1)
            {
               
                AddP.IsEnabled = false;
                P =bl.Product.ProductId(ID);
                Id.Text = Convert.ToString(P.m_Id);
                Name.Text = Convert.ToString(P.m_Name);
                Price.Text = Convert.ToString(P.m_Price);
                InStock.Text = Convert.ToString(P.m_InStock);
                category.SelectedItem = P.m_Category;
            }
            else
                UpdateP.IsEnabled = false;
        }

        private void AddP_Click(object sender, RoutedEventArgs e)
        { 
            bl.Product.AddProduct(P);
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)=> P.m_Category = (BO.Enums.Category)category.SelectedItem;

        private void TextChanged_Id(object sender, TextChangedEventArgs e) => P.m_Id = int.Parse(Id.Text);

        private void TextChanged_Name(object sender, TextChangedEventArgs e) => P.m_Name = Name.Text;


        private void TextChanged_Price(object sender, TextChangedEventArgs e) => P.m_Price = int.Parse(Price.Text);


        private void TextChanged_InStock(object sender, TextChangedEventArgs e) => P.m_InStock = int.Parse(InStock.Text);

        private void UpdateP_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.UpdateProduct(P);
            Id.Clear();
            Name.Clear();
            Price.Clear();
            InStock.Clear();
            category.SelectedItem = BO.Enums.Category.None;
        }



    }

}
