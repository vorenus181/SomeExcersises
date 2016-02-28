using System;
using System.Globalization;
using System.Windows.Controls;

namespace PersonBook.Validation
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, PersonBookResources.DateValidationRule_DateEmpty);
            var dateTime = (DateTime)value;
            return new ValidationResult(dateTime < DateTime.Now, PersonBookResources.DateValidationRule_DateInvalid);
        }
    }
}
