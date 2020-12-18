using LogitWebApp.Services.Drivers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class CheckDriverExistAttribute : ValidationAttribute
    {                 
        public CheckDriverExistAttribute()
        {           
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var servise = validationContext.GetService(typeof(IDriversService)) as DriversService;

            if (servise.IsDriverExist((string)value))
            {
                return new ValidationResult("Шофьор с подадения Email вече съществува в базата данни!", new List<string>() { "Email"});
            }

            return ValidationResult.Success;
        }
    }
}
