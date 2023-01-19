using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PL;

internal class Convert2 : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Enums.Status status = (BO.Enums.Status)value;

        if (status == BO.Enums.Status.Ordered)
        {
            return 10;
        }
        else if (status == BO.Enums.Status.Shipped)
        {
            return 60;
        }
        return 100;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
