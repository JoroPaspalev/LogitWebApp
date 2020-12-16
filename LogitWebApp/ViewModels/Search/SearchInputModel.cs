using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Search
{
    public class SearchInputModel
    {
        [Display(Name = "Изпращач")]
        public string SenderName { get; set; }

        [Display(Name = "Получател")]
        public string ReceiverName { get; set; }

        [Display(Name = "Дата на товарене")]
        public DateTime? LoadingDate { get; set; }

        [Display(Name = "Дата на разтоварване")]
        public DateTime? UnloadingDate { get; set; }

        public IEnumerable<string> SearchParameters { get; set; }
    }
}
