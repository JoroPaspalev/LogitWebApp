using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class Accept_Only_jpg_png : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Използвай ImageSharp library да провериш дали файла е наистина .png или .jpg!!!!!

            if (value != null)
            {
                if (!((IFormFile)value).FileName.EndsWith(".png") && !((IFormFile)value).FileName.EndsWith(".jpg"))
                {
                    return new ValidationResult("Можете да качвате само файлове с разширение .png и .jpg!", new List<string>() { "Picture" });
                }
            }

            //А ако е null направи нещо друго
            //Например върни грешка със съобщение "ddddddd"
            //return new ValidationResult("ddddddd");

            //Или пък върни резултат, който казва че проверката е минала успешно без грешки
            return ValidationResult.Success;
        }
    }
}
