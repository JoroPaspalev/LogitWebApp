using LogitWebApp.Attributes.ModelBinderAttributes;
using LogitWebApp.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Offer
{
    public class AddressInputModel : IValidatableObject
    {
        [Required]
        public string ShipmentId { get; set; }

        [Required(ErrorMessage ="Име/Фирма е задължително!")]
        public string LoadingName { get; set; }

        [Required(ErrorMessage = "Телефонния номер е задължителен!")]   
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}", ErrorMessage = "Телефонния номер е във формат xxxx-xxx-xxx")]
        public string LoadingPhone { get; set; }

        [Required(ErrorMessage ="Email е задължителено поле!")]
        [EmailAddress(ErrorMessage ="Въведения Email е в неправилен формат!")]
        public string LoadingEmail { get; set; }

        //[Required]
        //public string LoadingTown { get; set; }

        [Required(ErrorMessage = "Улицата е задължителено поле!")]
        public string LoadingStreet { get; set; }

        [Required(ErrorMessage = "Полето номер на улица е задължително!")]        
        public string LoadingNumber { get; set; }

        [ValidateCurrentDateAttribute]
        [ModelBinder(typeof(ExtractDateModelBinderAttribute))]        
        public DateTime LoadingDate { get; set; }

        [Required(ErrorMessage = "Име/Фирма е задължително!")]
        public string UnloadingName { get; set; }

        [Required(ErrorMessage = "Телефонния номер е задължителен!")]
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}", ErrorMessage = "Телефонния номер е във формат xxxx-xxx-xxx")]
        public string UnloadingPhone { get; set; }

        [Required(ErrorMessage = "Email е задължителено поле!")]
        [EmailAddress(ErrorMessage = "Въведения Email е в неправилен формат!")]
        public string UnloadingEmail { get; set; }

        //[Required]
        //public string UnloadingTown { get; set; }

        [Required(ErrorMessage = "Улицата е задължителено поле!")]
        public string UnloadingStreet { get; set; }

        [Required(ErrorMessage = "Полето номер на улица е задължително!")] //не може да е int защото има адреси 2B или Черни връх 23A
        public string UnloadingNumber { get; set; }

        [ValidateCurrentDateAttribute]//Това проверява дали unloadingDate е преди текущата дата и гърми
        [ModelBinder(typeof(ExtractUnloadingDateModelBinderAttribute))]//Това ми Parse датата по custom format
        public DateTime UnloadingDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.LoadingDate >= this.UnloadingDate)
            {
                //yield return new ValidationResult("Датата на товарене, не може да бъде преди датата на разтоварване!");
                yield return new ValidationResult("Loading date cannot be after unloading date or in the same day!");
            }

            //Искам датата на товарене да не е след повече от 1 месец от датата на офертата
            if (this.LoadingDate >= DateTime.UtcNow.AddMonths(1))
            {
                yield return new ValidationResult("Your loading date cannot be more than 30 days after date of your order!");
            }

            //Искам датата на разтоварване да не е след повече от 7 дни, защото тази стока ще ми заема място в склада
            if ((this.UnloadingDate - this.LoadingDate).Days > 7)
            {
                yield return new ValidationResult("Your shipment cannot stay more than 1 week in our warehouse!");
            }

        }
    }
}
