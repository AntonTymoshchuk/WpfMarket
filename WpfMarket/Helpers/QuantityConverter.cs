using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfMarket.Helpers
{
    public class QuantityConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int quantity = System.Convert.ToInt32(values[0]);

            SolidColorBrush emeraldSolidColorBrush = new SolidColorBrush(Color.FromRgb(46, 204, 113));
            SolidColorBrush sunFlowerSolidColorBrush = new SolidColorBrush(Color.FromRgb(241, 196, 15));
            SolidColorBrush alizarinSolidColorBrush = new SolidColorBrush(Color.FromRgb(231, 76, 60));

            if (quantity <= 1000 && quantity >= 700)
                return emeraldSolidColorBrush;
            else if (quantity <= 700 && quantity >= 400)
                return sunFlowerSolidColorBrush;
            else
                return alizarinSolidColorBrush;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
