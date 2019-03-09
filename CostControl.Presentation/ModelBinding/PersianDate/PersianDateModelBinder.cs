namespace CostControl.Presentation.ModelBinding.PersianDate
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    public class PersianDateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string modelName = bindingContext.ModelName;

            if (string.IsNullOrEmpty(modelName))
            {
                modelName = "model";
            }

            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            //var a = bindingContext.HttpContext.Request.Body;
            //var modelElementType = bindingContext.ModelMetadata.ElementType;

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            string value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                DateTime @new = Convert.ToDateTime(value);

                int year = @new.Year;
                int month = @new.Month;
                int day = @new.Day;

                bindingContext.Result = ModelBindingResult
                                            .Success(new DateTime(year, month, day, new PersianCalendar()));
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.TryAddModelError(
                                        modelName,
                                        e.Message);
            }

            return Task.CompletedTask;
        }
    }
}