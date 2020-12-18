using System;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class ValidateCurrentDateAttribute : ValidationAttribute
    {
        //value - е стойността, присвоена на property-то върху, което се слага този атрибут. В случая му подавам 11/24/2020 и то проверява дали тази дата е по-голяма от текущата такава или не
        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return false;
            }

            if ((DateTime)value > DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }
    }
}
