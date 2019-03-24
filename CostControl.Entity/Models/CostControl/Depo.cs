namespace CostControl.Entity.Models.CostControl
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class Depo : SuperEntity<long>
	{
		[Required]
		public long SaleCostPointId { get; set; }

		public virtual SaleCostPoint SaleCostPoint { get; set; }

		[Required]
		public long IngredientId { get; set; }

		public virtual Ingredient Ingredient { get; set; }

		[Required]
		public long ConsumptionUnitId { get; set; }

		public virtual ConsumptionUnit ConsumptionUnit { get; set; }

		[Required]
		public decimal Amount { get; set; }

		public virtual ICollection<Draft> Drafts { get; set; }
	}
}