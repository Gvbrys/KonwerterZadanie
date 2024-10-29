using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KonwerterZadanie
{
    class NettoToBruttoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string param = parameter.ToString();
                double vat = 1;
                switch (param)
                {
                    case "zwolniony":
                    case "0": vat = 1.0; break;
                    case "23": vat = 1.23; break;
                    case "8": vat = 1.08; break;
                    case "7": vat = 1.07; break;
                    case "5": vat = 1.05; break;
                }
                double netto = double.Parse(value.ToString());
                return (netto * vat).ToString();
            }
            catch { return "0"; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CurrencyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter?.ToString() == "bruttoWaluta")
            {
                if (values[2] is bool isChecked && !isChecked)
                    return "";

                if (double.TryParse(values[0]?.ToString(), out double brutto) &&
                    double.TryParse(values[1]?.ToString(), out double kurs))
                {
                    return (brutto / kurs).ToString("F2");
                }
                return "";
            }
            else
            {
                if (values[1] is bool isChecked && !isChecked)
                    return "";

                string currency = values[0]?.ToString();
                switch (currency)
                {
                    case "EUR": return "4,23";
                    case "USD": return "4,54";
                    case "GBP": return "5,31";
                    default: return "";
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                return isChecked ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}