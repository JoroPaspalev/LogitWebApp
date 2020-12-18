using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LogitWebApp.Attributes.ModelBinderAttributes
{
    //This is how to make Custom ModelBinder, which mapp given property to exact pattern and return success or fail
    public class ExtractDateModelBinderAttribute : IModelBinder
    {
       public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue("loadingDate").FirstValue;

            //string[] allowedFormats = { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy" };
            string[] allowedFormats = { "yyyy-MM-dd"};
            DateTime parsedLoadingDate;
            bool isParsedDate = DateTime.TryParseExact(value, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedLoadingDate);

            if (isParsedDate)
            {
                bindingContext.Result = ModelBindingResult.Success(parsedLoadingDate);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
