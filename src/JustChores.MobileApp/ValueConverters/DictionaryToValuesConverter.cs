using Microsoft.Maui.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ValueConverters;

public class DictionaryToValuesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is IDictionary dictionary && dictionary.Values is IEnumerable<object> values
                        ? values.Select(v => v.ToString()).ToArray() : Array.Empty<string>();
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException("No need to implement this method unless you're using two-way binding.");
}
