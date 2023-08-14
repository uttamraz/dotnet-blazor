using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DotNetBlazor.Server.Helpers.Validation
{
    public class RequiredIfOtherFieldIsNullAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;
        public RequiredIfOtherFieldIsNullAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_otherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    "Unknown property {0}",
                    new[] { _otherProperty }
                ));
            }
            var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyValue == null || otherPropertyValue as string == string.Empty)
            {
                if (value == null || value as string == string.Empty)
                {
                    return new ValidationResult(string.Format(
                        CultureInfo.CurrentCulture,
                        FormatErrorMessage(validationContext.DisplayName),
                        new[] { _otherProperty }
                    ));
                }
            }
            return ValidationResult.Success;
        }
    }

    public class StringRangeAttribute : ValidationAttribute
    {
        public StringRangeAttribute(string allowableValues)
      : base()
        {
            AllowableValues = allowableValues;
        }
        public string AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] list = AllowableValues.Split('|');
            if (list?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Please enter one of the allowable values: {string.Join(", ", list ?? new string[] { "No allowable values found" })}.");
        }
    }

    public class RequiredIfFieldIsAttribute : ValidationAttribute
    {
        private readonly string _property;
        private readonly string _compareValue;
        public RequiredIfFieldIsAttribute(string property, string compareValue)
        {
            _property = property;
            _compareValue = compareValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var prop = validationContext.ObjectType.GetProperty(_property);
            if (prop != null)
            {
                var propertyValue = prop.GetValue(validationContext.ObjectInstance, null);
                if (propertyValue != null && !string.IsNullOrEmpty(propertyValue as string))
                {
                    if (propertyValue == _compareValue && value as string == string.Empty)
                    {
                        return new ValidationResult(string.Format(
                            CultureInfo.CurrentCulture,
                            FormatErrorMessage(validationContext.DisplayName),
                            new[] { _property }
                        ));
                    }
                }
                else
                {
                    return new ValidationResult(string.Format(
                        CultureInfo.CurrentCulture,
                        "Unknown property {0}",
                        new[] { _property }
                    ));
                }
            }
            return ValidationResult.Success;
        }
    }
}
