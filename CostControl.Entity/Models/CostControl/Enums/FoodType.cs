namespace CostControl.Entity.Models.CostControl.Enums
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	[Flags]
	public enum FoodType : byte
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