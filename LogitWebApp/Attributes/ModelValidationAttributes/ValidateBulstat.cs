using LogitWebApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class ValidateBulstat : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Така си вземам db за да имам достъп до нея
            var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var currBulstat = db.Users.FirstOrDefault(u => u.Bulstat == (int)value);

            if (currBulstat != null)
            {
                return new ValidationResult("Този Булстат вече фигурира в базата ни данни!", new List<string> { "Bulstat" });
            }
            return ValidationResult.Success;

        }
    }
}
