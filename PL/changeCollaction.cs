using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public interface INotifyCollectionChanged<T>
    {
        public ObservableCollection<T> Orderlist { get; set; }
    }
}
