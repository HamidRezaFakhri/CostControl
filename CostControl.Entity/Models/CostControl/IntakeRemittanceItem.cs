namespace CostControl.Entity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class IntakeRemittanceItem : Base.SuperEntity<long>
	{
		[Required]
		public long IntakeRemittanceID { get; set; }

		public virtual IntakeRemittance IntakeRemittance { get; set; }

		[Required]
		public long IngredientId { get; set; }

		public virtual Ingredient Ingredient { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public long ConsumptionUnitId { get; set; }

		public virtual ConsumptionUnit ConsumptionUnit { get; set; }
	}
}