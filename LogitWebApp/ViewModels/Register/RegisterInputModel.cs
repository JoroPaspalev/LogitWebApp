using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Register
{
    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match")]
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
