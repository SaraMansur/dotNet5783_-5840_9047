using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 

    public partial class Changes : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        private Action<ProductForList> action;

        public Changes()
        {
            InitializeComponent();

            category.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//combobox source of info- categories
        }

        public Changes(Action<ProductForList> action, Product? myData) : this()
        {
            this.action = action;
            AddP.Visibility = Visibility.Hidden;
            GridProduct.DataContext = myData;//putting all the data in the textboxes via binding
            Id.IsEnabled = false;
        }

        public Changes(Action<ProductForList> action) : this()//to add a product
        {
            this.action = action;
            UpdateP.Visibility = Visibility.Hidden;//update butten invisable
        }

        private void AddP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (category.SelectedItem == null) //if they didnt enter a category 
                {
                    MessageBox.Show("please select a category");
                    return;
                }
                BO.Product product = new BO.Product()//creating a new product
                {
                    m_Id = int.Parse(Id.Text),
                    m_Name = Name.Text,
                    m_Price = double.Parse(Price.Text),
                    m_InStock = int.Parse(InStock.Text),
                    m_Category = (BO.Enums.Category)category.SelectedItem
                };
                try
                {
                    bl?.Product.AddProduct(product);//adds the product to the do
                   // action(bl?.Product.ProductId(product.m_Id) ?? throw new ArgumentNull());//goes back to the window b4 and adds the product to the observablcollection
                    MessageBox.Show("product added successfully");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)//if missing any data
            {
                MessageBox.Show("Please enter missing data");
            }
        }

        private void UpdateP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Name.Text == "")//if there isnt a name entered
                {
                    MessageBox.Show("Please enter a name");
                    return;
                }
                BO.Product product = new BO.Product()//crating a new product
                {
                    m_Id = int.Parse(Id.Text),
                    m_Name = Name.Text,
                    m_Price = double.Parse(Price.Text),
                    m_InStock = int.Parse(InStock.Text),
                    m_Category = (BO.Enums.Category)category.SelectedItem
                };
                try
                {
                    bl?.Product.UpdateProduct(product);//adds the product to the do
                  //  action(bl?.Product.ProductId(product.m_Id) ?? throw new ArgumentNull());//goes back a window and does the update to the oc
                    MessageBox.Show("product updated successfully");
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)//if missing any data
            {
                MessageBox.Show("Please enter missing data");
            }
        }
    }
}

