using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Attributes.ModelBinderAttributes
{
    public class ExtractUnloadingDateModelBinderAttribute : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue("UnloadingDate").FirstValue;

            string[] allowedFormats = { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy" };
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
