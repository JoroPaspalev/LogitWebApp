using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class ValidateImageSize : ValidationAttribute
    {
        private readonly int imgSize;

        public ValidateImageSize(int imgSize)
        {
            this.imgSize = imgSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (((IFormFile)value).Length > imgSize * 1024 * 1024) //по-голяма ли е от imgSize MB --> Error
                {
                    return new ValidationResult($"Можете да качвате файлове до {imgSize}MB!", new List<string>() { "Picture" });
                }
            }

            //А ако е null направи нещо друго
            //Например върни грешка със съобщение "ssssssssssssssssssssssssssssssss"
            //return new ValidationResult("ssssssssssssssssssssssssssssssss");

            //Или пък върни резултат, който казва че проверката е минала успешно без грешки
            return ValidationResult.Success;
        }
    }
}
