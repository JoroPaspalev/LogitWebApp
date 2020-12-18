using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    public class Accept_Only_jpg_pngAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Използвай ImageSharp library да провериш дали файла е наистина .png или .jpg!!!!!

            if (value != null)
            {
                foreach (var file in ((IEnumerable<IFormFile>)value))
                {
                    if (!file.FileName.EndsWith(".png") && !file.FileName.EndsWith(".jpg"))
                    {
                        return new ValidationResult("Можете да качвате само файлове с разширение .png и .jpg!", new List<string>() { "Pictures" });
                    }
                }
            }

            //А ако е null направи нещо друго
            //Например върни грешка със съобщение "ddddddd" ---> return new ValidationResult("ddddddd");
            
            //Или пък върни резултат, който казва че проверката е минала успешно без грешки
            return ValidationResult.Success;
        }
    }
}
