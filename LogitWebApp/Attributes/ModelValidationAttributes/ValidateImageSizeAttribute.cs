using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class ValidateImageSizeAttribute : ValidationAttribute
    {
        private readonly int imgSize;

        public ValidateImageSizeAttribute(int imgSize)
        {
            this.imgSize = imgSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                foreach (var file in ((IEnumerable<IFormFile>)value))
                {
                    if (file.Length > imgSize * 1024 * 1024) //по-голяма ли е от imgSize MB --> Error
                    {
                        return new ValidationResult($"Можете да качвате файлове до {imgSize}MB!", new List<string>() { "Pictures" });
                    }
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
