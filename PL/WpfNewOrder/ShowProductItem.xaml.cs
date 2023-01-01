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

namespace PL.WpfNewOrder
{
    /// <summary>
    /// Interaction logic for ShowProductItem.xaml
    /// </summary>
    public partial class ShowProductItem : Window
    {
        IBl bl = Factory.Get();
        public ShowProductItem(ProductItem p)
        {
            InitializeComponent();
            this.DataContext = p;
        }
    }
}
