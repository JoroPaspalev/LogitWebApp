using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Drivers
{
    public class DeleteDriverInputModel
    {
        [Display(Name = "Въведете Email на шофьора")]
        [Required(ErrorMessage ="Полето Email не може да е празно!")]
        [EmailAddress(ErrorMessage ="Невалиден Email!")]
        public string Email { get; set; }
    }
}
