using BlApi;
using BO;
using PL.WpfOrderManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PL.WPFOrderTacking
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        public OrderTracking _OrderTracking { get; set; }
        private readonly ObservableCollection<Tuple<string?, DateTime?>> _dates = new ObservableCollection<Tuple<string?, DateTime?>>();
        public ObservableCollection<Tuple<string?, DateTime?>> Dates { get { return _dates; } }
        public IBl bl = Factory.Get();

        public View(int id)
        {
            InitializeComponent();
            _OrderTracking = bl.Order.orderTracking(id);
            this.DataContext = _OrderTracking;
            PopulateList();
            orderT.DataContext = Dates;
        }

        private void PopulateList()
        {
            for (int i = 0; i < _OrderTracking.m_DescriptionProgress.Count; i++)
                _dates.Add(_OrderTracking.m_DescriptionProgress[i]);
        }
    }
}
