namespace Olympus.WebApi.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DateRangeLessThanAttribute : ValidationAttribute
    {
        public DateRangeLessThanAttribute(string dateFromFieldName, int months)
            : base(string.Empty)
        {
            DateFromFieldName = dateFromFieldName;
            Months = months;
        }

        public string DateFromFieldName { get; }

        public int Months { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valid = IsValidationContext(validationContext) && IsValidDateRange(value, validationContext);

            return valid
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public override string FormatErrorMessage(string name) => $"Data range should not exceed {Months} months";

        private bool IsValidationContext(ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(DateFromFieldName);
            return property != null;
        }

        private bool IsValidDateRange(object value, ValidationContext validationContext)
        {
            var dateFrom = GetDateFrom(validationContext);
            return (DateTime)value <= dateFrom.AddMonths(Months);
        }

        private DateTime GetDateFrom(ValidationContext context)
        {
            var property = context.ObjectType.GetProperty(DateFromFieldName);
            return (DateTime)property.GetValue(context.ObjectInstance, null);
        }
    }
}
