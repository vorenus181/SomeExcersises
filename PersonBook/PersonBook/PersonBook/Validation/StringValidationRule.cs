using System;
using System.Globalization;
using System.Windows.Controls;

namespace PersonBook.Validation
{
    public class StringValidationRule : ValidationRule
    {
        private int _minimumLength = -1;
        private int _maximumLength = -1;
        private string _message;

        public int MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
            set { _maximumLength = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = new ValidationResult(true, null);
            var inputString = (value ?? string.Empty).ToString();
            if (inputString.Length < this.MinimumLength 
                || (MaximumLength > 0 && inputString.Length > MaximumLength))
            {
                result = new ValidationResult(false, Message);
            }
            return result;
        }
    }
}
