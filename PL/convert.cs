using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PL
{
    public class convert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Status = value as string; 
            if (string.IsNullOrEmpty(Status))
                throw new ArgumentException("Cannot convert null or empty string");
            if (Status == "Ordered")
            {
                return Brushes.Red;
            }
            if (Status == "Shipped")
            {
                return Brushes.Yellow;
            }
            if (Status == "Received")
            {
                return Brushes.Green;
            }
            throw new ArgumentException("Invalid status");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
