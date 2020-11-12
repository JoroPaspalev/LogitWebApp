using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.ViewModels.Test.Enums;

namespace LogitWebApp.ViewModels.Test
{
    public class AddRecepieInputModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
       
        public string Description { get; set; }

        [Required(ErrorMessage ="Cannot be empty")]
        public int? CookingTime { get; set; }

        [Required(ErrorMessage = "FirstCooked filed is in invalid format")]
        public DateTime? FirstCooked { get; set; }

        [Required(ErrorMessage ="Cannot be empty")]
        public double? Minutes { get; set; }

        public bool IsDelicious { get; set; }

        public Dog Dog { get; set; }

        public ICollection<Dog> Dogs { get; set; }

        public LogitWebApp.ViewModels.Test.Enums.Type Type { get; set; }
    }
}
