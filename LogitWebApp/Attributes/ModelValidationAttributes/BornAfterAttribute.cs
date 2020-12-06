using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Attributes.ModelValidationAttributes
{
    //This is how we make custom Validation Attribute
    public class BornAfterAttribute : ValidationAttribute
    {
        public BornAfterAttribute(int year)
        {
            Year = year;
            this.ErrorMessage = $"The value of the field should be more than {year}";
        }

        public int Year { get; }

        public override bool IsValid(object value)
        {
            if ((int)value > Year)
            {
                return true;
            }
            return false;
        }
    }
}
