namespace CostControl.Entity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class Buffet : SuperEntity<long>
	{
		[Required]
		public long SalePointId { get; set; }

		public virtual SalePoint SalePoint { get; set; }
	}
}