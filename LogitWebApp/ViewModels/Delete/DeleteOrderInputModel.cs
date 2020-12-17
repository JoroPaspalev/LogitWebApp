using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.ViewModels.Delete
{
    public class DeleteOrderInputModel
    {
        [Display(Name = "Въведете номер на поръчката")]
        public string OrderId { get; set; }
    }
}
