using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CostControl.Presentation.ModelBinding.PersianDate
{
    public class HumanizerMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.PropertyAttributes != null)
            {
                IReadOnlyList<object> propertyAttributes = context.Attributes;
                DisplayMetadata modelMetadata = context.DisplayMetadata;
                string propertyName = context.Key.Name;

                if (IsTransformRequired(propertyName, modelMetadata, propertyAttributes))
                {
                    //modelMetadata.DisplayName = () => propertyName.Humanize().Transform(To.TitleCase);
                    modelMetadata.AdditionalValues.Add(propertyName, propertyAttributes);
                }
            }
        }

        private static bool IsTransformRequired(string propertyName, DisplayMetadata modelMetadata, IReadOnlyList<object> propertyAttributes)
        {
            if (!string.IsNullOrEmpty(modelMetadata.SimpleDisplayProperty))
            {
                return false;
            }

            if (propertyAttributes.OfType<DisplayNameAttribute>().Any())
            {
                return false;
            }

            if (propertyAttributes.OfType<DisplayAttribute>().Any())
            {
                return false;
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            return true;
        }
    }
}