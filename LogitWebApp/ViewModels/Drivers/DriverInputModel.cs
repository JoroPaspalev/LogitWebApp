using LogitWebApp.Attributes.ModelValidationAttributes;
using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Drivers
{
    public class DriverInputModel //: IValidatableObject
    {
        //private readonly ApplicationDbContext db;

        //public DriverInputModel(ApplicationDbContext db)
        //{
        //    this.db = db; 
        //}


        [Required(ErrorMessage = "Полето Email не може да бъде празно")]
        [EmailAddress(ErrorMessage = "Невалиден Email")]
        [Display(Name = "Email")]
        [CheckDriverExistAttribute]
        public string Email { get; set; }

        [Required(ErrorMessage ="Полето парола не може да бъде празно")]
        [StringLength(50, ErrorMessage = "Дължината на {0}та трябва да е между {2} и {1} символа!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повтори паролата")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат!")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage ="Името на шофьора е задължително!")]
        [RegularExpression(@"^[A-ZА-Я]{1}[a-zа-я]+$", ErrorMessage ="Името трябва да започва с една голяма буква, следвана само от малки!")]
        [MaxLength(20, ErrorMessage ="Името не може да навдишава 20 символа!")]
        [Display(Name ="Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията на шофьора е задължителна!")]
        [RegularExpression(@"^[A-ZА-Я]{1}[a-zа-я]+$", ErrorMessage = "Фамилията трябва да започва с една голяма буква, следвана само от малки!")]
        [MaxLength(20, ErrorMessage = "Фамилията не може да навдишава 20 символа!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Телефонния номер е задължителен!")]
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}", ErrorMessage ="Телефонния номер е във формат xxxx-xxx-xxx")]
        [Display(Name = "Мобилен номер")]
        [CheckPhoneExistAttribute]
        public string PhoneNumber { get; set; }

        //[CheckDriverExist(new DriverInputModel()
        //{Email = this.Email,

        //})]
        public string Check { get; set; }

        ////Това не работи - дава грешка 500
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{



        //    Driver driver = this.db.Drivers.FirstOrDefault(d => d.FirstName == this.FirstName && d.LastName == this.LastName && d.PhoneNumber == this.PhoneNumber);

        //    if (driver != null)
        //    {
        //        yield return new ValidationResult("Вече има регистриран шофьор с подадените данни!!!");
        //    }
        //}
    }
}
