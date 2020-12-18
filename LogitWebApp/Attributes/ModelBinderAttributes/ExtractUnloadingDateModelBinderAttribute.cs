using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LogitWebApp.Attributes.ModelBinderAttributes
{
    public class ExtractUnloadingDateModelBinderAttribute : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue("UnloadingDate").FirstValue;

            //string[] allowedFormats = { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy" };
            string[] allowedFormats = { "yyyy-MM-dd" };
            DateTime parsedUnloadingDate;
            bool isParsedDate = DateTime.TryParseExact(value, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedUnloadingDate);

            if (isParsedDate)
            {
                bindingContext.Result = ModelBindingResult.Success(parsedUnloadingDate);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
