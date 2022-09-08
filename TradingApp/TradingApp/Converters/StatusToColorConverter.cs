using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TradingApp.Enums;
using Xamarin.Forms;

namespace TradingApp.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return "#FF0000";

            if ((string)value == Status.InProgress.ToString())
                return "#F5DEB3";
            else if ((string)value == Status.Gain.ToString())
                return "#32CD32";
            else if ((string)value == Status.Loss.ToString())
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
