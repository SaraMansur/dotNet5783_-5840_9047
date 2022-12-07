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
        public Changes()
        {
            InitializeComponent();
            categoryP.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void AddP_Click(object sender, RoutedEventArgs e)
        {
            BO.Product P = new BO.Product() { m_Category = (BO.Enums.Category)categoryP.SelectedItem };
            bl.Product.AddProduct(P);
        }
    }

}
