namespace CostControl.Entity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class DraftItem : SuperEntity<long>
	{
		[Required]
		public long DraftId { get; set; }

		public virtual Draft Draft { get; set; }

		[Required]
		public long IngredientId { get; set; }

		public virtual Ingredient Ingredient { get; set; }

		[Required]
		public long ConsumptionUnitId { get; set; }

		public virtual ConsumptionUnit ConsumptionUnit { get; set; }

		[Required]
		public decimal Amount { get; set; }
	}
}