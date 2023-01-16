using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static BO.Enums;

namespace PL
{
    public class convert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.Enums.Status status = (BO.Enums.Status)value;

            if (status == BO.Enums.Status.Ordered)
            {
                return Brushes.Red;
            }
            else if (status == BO.Enums.Status.Shipped)
            {
                return Brushes.Yellow;
            }
            return Brushes.Green;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
