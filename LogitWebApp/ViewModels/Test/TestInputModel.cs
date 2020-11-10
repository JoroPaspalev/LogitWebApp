using LogitWebApp.Attributes.ModelValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Test
{
    public class TestInputModel
    {       
        [BornAfter(1983)]
        public int Age { get; set; }
    }
}
