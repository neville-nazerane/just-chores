using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.ValueConverters
{

    public class DueDateToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = null;
            if (value is DateTime dueDate)
                date = dueDate;
            else if (value is DateTime?)
                date = (DateTime?)value;

            if (date is not null)
                return date.Value.Date <= DateTime.Now.Date;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Bool isn't enough information to determine the due date.");
        }
    }


}
