using LogitWebApp.Data;
using LogitWebApp.Services.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class CheckCityExist : ValidationAttribute
    {
        public CheckCityExist()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var homeService = validationContext.GetService(typeof(IHomeService)) as HomeService;

            var isThereThatCity = homeService.IsThereThatCity((int)value);

            if (!isThereThatCity)
            {
                return new ValidationResult("Не предлагаме транспорт до избрания от Вас град!", new List<string> { "From", "To" });
            }

            return ValidationResult.Success;
        }
    }
}
