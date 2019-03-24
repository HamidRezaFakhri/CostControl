namespace CostControl.Entity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class Recipe : SuperEntity<long>
	{
		[Required]
		public long FoodId { get; set; }

		public virtual Food Food { get; set; }

		[Required]
		public long IngredientId { get; set; }

		public virtual Ingredient Ingredient { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public long ConsumptionUnitId { get; set; }

		public virtual ConsumptionUnit ConsumptionUnit { get; set; }

		public decimal ConvertionRate { get; set; }
	}
}