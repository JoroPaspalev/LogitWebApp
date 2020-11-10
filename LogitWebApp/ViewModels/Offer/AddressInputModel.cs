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

        [Required]
        public string LoadingName { get; set; }

        [Required]
        public string LoadingPhone { get; set; }

        [Required]
        [EmailAddress]
        public string LoadingEmail { get; set; }

        [Required]
        public string LoadingTown { get; set; }

        [Required]
        public string LoadingStreet { get; set; }

        [Required]
        [Phone]
        public string LoadingNumber { get; set; }

        [ValidateCurrentDateAttribute]
        [ModelBinder(typeof(ExtractDateModelBinderAttribute))]        
        public DateTime LoadingDate { get; set; }

        [Required]
        public string UnloadingName { get; set; }

        [Required]
        [Phone]
        public string UnloadingPhone { get; set; }

        [Required]
        [EmailAddress]
        public string UnloadingEmail { get; set; }

        [Required]
        public string UnloadingTown { get; set; }

        [Required]
        public string UnloadingStreet { get; set; }

        [Required] //не може да е int защото има адреси 2B или Черни връх 23A
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
