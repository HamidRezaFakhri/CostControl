namespace CostControl.Presentation.ModelBinding.PersianDate
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using System;

    public class PersianDateModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(DateTime))
            {
                return new BinderTypeModelBinder(typeof(PersianDateModelBinder));
            }

            return null;
        }
    }
}