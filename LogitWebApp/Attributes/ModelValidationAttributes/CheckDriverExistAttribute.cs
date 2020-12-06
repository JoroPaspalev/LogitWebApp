using LogitWebApp.Services.Drivers;
using LogitWebApp.ViewModels.Drivers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class CheckDriverExistAttribute : ValidationAttribute
    {         
        //Как да взема input модела runTime????? и да го подам тук?
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
