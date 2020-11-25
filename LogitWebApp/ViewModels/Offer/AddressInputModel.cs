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

        [Required(ErrorMessage = "Име/Фирма е задължително!")]
        [Display(Name ="Адрес на товарене")]// Това сменя <label> name от LoadingName да се показва Адрес на товарене
        [MaxLength(45)]
        public string LoadingName { get; set; }

        [Required(ErrorMessage = "Телефонния номер е задължителен!")]        
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}", ErrorMessage = "Телефонния номер е във формат xxxx-xxx-xxx")]
        public string LoadingPhone { get; set; }

        [Required(ErrorMessage = "Email е задължителено поле!")]
        [EmailAddress(ErrorMessage = "Въведения Email е в неправилен формат!")]
        [MaxLength(45)]
        public string LoadingEmail { get; set; }

        //[Required]
        //public string LoadingTown { get; set; }

        [Required(ErrorMessage = "Улицата е задължителено поле!")]
        [RegularExpression(@"^[А-Яа-я. ]*$", ErrorMessage ="Полето Улица може да съдържа само малки или големи букви на кирилица, точка и интервал")]
        [MaxLength(30)]
        public string LoadingStreet { get; set; }

        [Required(ErrorMessage = "Полето номер на улица е задължително!")]
        [MaxLength(5)]
        public string LoadingNumber { get; set; }

        [ValidateCurrentDateAttribute(ErrorMessage = "Датата за товарене трябва да е минимум 1 ден след датата на поръчката")]
        [ModelBinder(typeof(ExtractDateModelBinderAttribute))]
        [Required(ErrorMessage = "Полето Дата на товарене е задъжително")]
        [DataType(DataType.Date)]//Този attribute не е свързан с валидация. Той ми форматира датата избрана от календара от клиента, да не ми я показва в формат 2020-12-29Т19:23:32, а ми изрязва само датата от всичкото това, за прегледност
        public DateTime? LoadingDate { get; set; }

        [Required(ErrorMessage = "Име/Фирма е задължително!")]
        [MaxLength(45)]
        public string UnloadingName { get; set; }

        [Required(ErrorMessage = "Телефонния номер е задължителен!")]
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}", ErrorMessage = "Телефонния номер е във формат xxxx-xxx-xxx")]
        public string UnloadingPhone { get; set; }

        [Required(ErrorMessage = "Email е задължителено поле!")]
        [EmailAddress(ErrorMessage = "Въведения Email е в неправилен формат!")]
        [MaxLength(30)]
        public string UnloadingEmail { get; set; }

        [Required(ErrorMessage = "Улицата е задължителено поле!")]
        [RegularExpression(@"^[А-Яа-я. ]*$", ErrorMessage = "Полето Улица може да съдържа само малки или големи букви на кирилица, точка и интервал")]
        [MaxLength(30)]
        public string UnloadingStreet { get; set; }

        [Required(ErrorMessage = "Полето номер на улица е задължително!")] //не може да е int защото има адреси 2B или Черни връх 23A
        [MaxLength(5)]
        public string UnloadingNumber { get; set; }

        [ValidateCurrentDateAttribute(ErrorMessage = "Датата на разтоварене неможе да е днес или в миналото")]//Това проверява дали unloadingDate е преди текущата дата и гърми
        [ModelBinder(typeof(ExtractUnloadingDateModelBinderAttribute))]//Това ми Parse датата по custom format
        [Required(ErrorMessage = "Полето Дата на разтоварене е задъжително")]
        [DataType(DataType.Date)]
        public DateTime? UnloadingDate { get; set; }

        [MaxLength(1000, ErrorMessage = "Максималната дължина на това поле е 1000 символа")]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.LoadingDate >= this.UnloadingDate)
            {
                yield return new ValidationResult("Датата на товарене, не може да бъде преди датата на разтоварване или същия ден!", new List<string>() { "loadingDate" });
                //yield return new ValidationResult("Loading date cannot be after unloading date or in the same day!", new List<string>() { "loadingDate"});
            }

            //Искам датата на товарене да не е след повече от 1 месец от датата на офертата
            if (this.LoadingDate >= DateTime.UtcNow.AddMonths(1))
            {
                yield return new ValidationResult("Your loading date cannot be more than 30 days after date of your order!", new List<string>() { "LoadingDate" }); //Чрез това new List<string>() { "LoadingDate" } добавям в ModelState-а за кое property name e станала тази грешка, за да може после да покаже грешката под него, иначе ако го няма просто отчита грешката, но на client-а не се показва нищо и само се презарежда страницата все едно
            }

            //Искам датата на разтоварване да не е след повече от 7 дни, защото тази стока ще ми заема място в склада
            if ((this.UnloadingDate - this.LoadingDate)?.Days > 7)
            {
                yield return new ValidationResult("Your shipment cannot stay more than 1 week in our warehouse!", new List<string>() { "UnloadingDate", "LoadingDate" });
            }

        }
    }
}
