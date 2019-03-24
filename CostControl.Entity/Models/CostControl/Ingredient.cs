namespace CostControl.Entity.Models.CostControl
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;
	using Entity.Models.CostControl.Enums;

	public class Ingredient : SupperNameEntity<long>
	{
		[StringLength(25,
			ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
		public string Code { get; set; }

		[Required]
		[StringLength(250, MinimumLength = 2,
			ErrorMessage = "Please enter a unique English Name, it must be greater than {2} characters and less than {1} characters.")]
		public string EnglishName { get; set; }

		public IngredientType Type { get; set; }

		public decimal Price { get; set; }

		public decimal UsefullRatio { get; set; }

		public string Description { get; set; }

		public virtual ICollection<Recipe> RecipeItems { get; set; }

		public virtual ICollection<SaleItem> SaleItems { get; set; }

		public virtual ICollection<MenuItem> MenuItems { get; set; }

		public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
	}
}