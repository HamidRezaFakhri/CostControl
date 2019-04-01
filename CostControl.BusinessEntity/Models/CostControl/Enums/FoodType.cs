namespace CostControl.BusinessEntity.Models.CostControl.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum FoodType
    {
        [DisplayName("وعده اصلی")]
        MainMeal = 1,
        [Display(Name = "پیش غذا")]
        Appetizer,
        [Display(Name = "دسر")]
        Desert,
        [Display(Name = "سالاد")]
        Salan,
        [Display(Name = "نوشیدنی")]
        Beverage
    }
}