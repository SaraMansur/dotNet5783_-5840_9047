using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        IBl bl = Factory.Get();
        BO.Product P;
        private Action<ProductForList> action;
        public Changes(int id=0)
        {
            try
            {
                InitializeComponent();
                if (id != 0) P = bl.Product.ProductId(id);
                else { P = new BO.Product() { m_Category = Enums.Category.None, m_Id = 0, m_InStock = 0, m_Name = "", m_Price = 0 }; };
                this.DataContext = P;
                category.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                if (id != 0) { AddP.Visibility = Visibility.Collapsed; }
                else
                    UpdateP.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        public Changes(Action<ProductForList> a,int id=0):this(id)
        {
            action = a;
        }
        private void AddP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    bl?.Product.AddProduct(P);//adds the product to the do
                    ProductForList pfl = new ProductForList() { m_Category = P.m_Category, m_ID = P.m_Id, m_NameProduct = P.m_Name, m_PriceProduct = P.m_Price };
                    action(pfl);
                    MessageBox.Show("product added successfully");
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }   
            }
            catch (Exception) { MessageBox.Show("Please enter missing data"); }//if missing any data 
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
                try
                {
                    bl?.Product.UpdateProduct(P);//adds the product to the do
                    ProductForList pfl = new ProductForList() { m_Category = P.m_Category, m_ID = P.m_Id, m_NameProduct = P.m_Name, m_PriceProduct = P.m_Price };
                    action(pfl);
                    MessageBox.Show("product updated successfully");
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            catch (Exception) { MessageBox.Show("Please enter missing data"); }//if missing any data 
        }
    }
}
