using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class BulstatMustStartsWithBGAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!((string)value).StartsWith("BG"))
            {
                return new ValidationResult("ДДС номера трябва да започва с BG", new List<string> { "VatNumber" });
            }
            return ValidationResult.Success;
        }
    }
}
