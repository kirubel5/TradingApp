using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TradingApp.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return "#FF0000";

            if ((string)value == "InProgress" || (string)value == "inProgress")
                return "#F5DEB3";
            else if ((string)value == "Gain" || (string)value == "gain")
                return "#32CD32";
            else if ((string)value == "Loss" || (string)value == "loss")
                return "#FF0000";
            else
                return "#FF0000";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
