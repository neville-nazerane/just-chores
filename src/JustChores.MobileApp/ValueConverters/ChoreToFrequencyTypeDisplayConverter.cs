using JustChores.MobileApp.Models;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace JustChores.MobileApp.ValueConverters
{
    public class ChoreToFrequencyTypeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Chore chore)
            {
                return chore.GetFrequencyTypeToDisplay();
            }

            // Return some default value or throw an exception
            return "Invalid";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("No need to implement this method unless you're using two-way binding.");
        }
    }
}