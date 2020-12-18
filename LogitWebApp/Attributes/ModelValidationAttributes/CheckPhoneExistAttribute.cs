using LogitWebApp.Services.Drivers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class CheckPhoneExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           var driverService = validationContext.GetService(typeof(IDriversService)) as DriversService;

            if (driverService.IsPhoneExist((string)value))
            {
                return new ValidationResult("Телефонният номер е зает!", new List<string> { "PhoneNumber" });
            }
            return ValidationResult.Success;
        }
    }
}
