using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Register
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage ="Полето Email не може да бъде празно")]
        [EmailAddress(ErrorMessage ="Невалиден Email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [MinLength(6, ErrorMessage ="Дължината на паролата не може да е по-малка от 3 символа!")]
        [MaxLength(30, ErrorMessage ="Дължината на паролата не може да надвишава 30 символа")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Полето парола не може да бъде празно")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат!")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\+359[0-9]{9}$")]
        public string Phone { get; set; }

        public string Fax { get; set; }

        //[RegularExpression(@"^[0 - 9]{9}$")]
        public int VatNumber { get; set; }

        [Required]
        public string Bulstat { get; set; }

        [Required]
        //[RegularExpression(@"^[а-яА-Яa-zA-z]+ [а-яА-Яa-zA-z]+ [а-яА-Яa-zA-z]+$")]
        public string Manager { get; set; }

        public string Site { get; set; }

       
    }
}
