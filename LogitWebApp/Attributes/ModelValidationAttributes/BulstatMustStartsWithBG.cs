using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class BulstatMustStartsWithBG : ValidationAttribute
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
