using LogitWebApp.Services.Drivers;
using LogitWebApp.ViewModels.Drivers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class CheckDriverExist : ValidationAttribute
    {       
        private readonly DriverInputModel input;       

        //Как да взема input модела runTime????? и да го подам тук?
        public CheckDriverExist(DriverInputModel input)
        {
            this.input = input;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var servise = validationContext.GetService(typeof(IDriversService)) as DriversService;

            if (servise.IsDriverExist(input))
            {
                return new ValidationResult("Шофьор с подадените данни вече съществува в базата данни!");
            }

            return new ValidationResult("");
        }
    }
}
