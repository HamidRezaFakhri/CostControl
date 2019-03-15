using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl.Enums
{
    [Flags]
    public enum ServeType : byte
    {
        [DisplayName("وعده اصلی")]
        MainMeal = 1,
        [Display(Name = "پیش غذا")]
        Appetizer,
        [Display(Name = "دسر")]
        Desert,
        [Display(Name = "وعده")]
        Meal
    }
}