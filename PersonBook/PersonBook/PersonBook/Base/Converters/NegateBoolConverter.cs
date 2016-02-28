using System;
using System.Globalization;
using System.Windows.Data;

namespace PersonBook.Base.Converters
{
    public class NegateBoolConverter : IValueConverter
    {
        #region IValueConverter members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (bool))
            {
                throw new InvalidOperationException("Target has to be bool type");
            }
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
