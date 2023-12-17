using JustChores.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ValueConverters
{
    public class OccuranceToBorderThicknessConverter : IValueConverter
    {

        const double UNSELECTED_THICKNESS = 1.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FrequencyType type &&
                type.ToString().Equals(parameter.ToString(), StringComparison.CurrentCultureIgnoreCase))
                return 0;

            return UNSELECTED_THICKNESS;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OccuranceToFontColorConverter : IValueConverter
    {
        static readonly Color unselectedColor = Colors.Grey;
        static readonly Color selectedColor = Colors.White;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FrequencyType type &&
                type.ToString().Equals(parameter.ToString(), StringComparison.CurrentCultureIgnoreCase))
                return selectedColor;

            return unselectedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class OccuranceToBackgroundColorConverter : IValueConverter
    {
        static readonly Color unselectedColor = Colors.Transparent;
        static readonly Color selectedColor = Color.FromArgb("77D4FC");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FrequencyType type &&
                type.ToString().Equals(parameter.ToString(), StringComparison.CurrentCultureIgnoreCase))
                return selectedColor;

            return unselectedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
