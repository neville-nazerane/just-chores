using System;
using System.Globalization;
using JustChores.MobileApp.Models;

namespace JustChores.MobileApp.ValueConverters
{
    public class ChoreToSummaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Chore chore)
            {
                if (chore.Frequency <= 0)
                    return string.Empty;

                return chore.GetSummary();

                //string suffix = GetOrdinalSuffix(chore.Frequency);
                //string frequencyTypeString = chore.GetFrequencyTypeToDisplay();

                //return $"every {chore.Frequency}{suffix} {frequencyTypeString}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed for this example
            throw new NotImplementedException();
        }

        private static string GetOrdinalSuffix(int number)
        {
            if (number % 100 >= 11 && number % 100 <= 13)
            {
                return "th";
            }

            switch (number % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}