namespace CostControl.Entity.Models.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class Sale : SuperEntity<long>
	{
		[Required]
		public long SaleCostPointId { get; set; }

		public virtual SaleCostPoint SaleCostPoint { get; set; }

		[Required]
		public DateTime SaleDate { get; set; } = DateTime.Now;

		[Required]
		[StringLength(25,
			ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
		public string Code { get; set; }

		[Required]
		public virtual ICollection<SaleItem> SaleItems { get; set; }
	}
}