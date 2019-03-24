namespace CostControl.Entity.Models.CostControl
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class OverCostType : SupperNameEntity<byte>
	{
		[StringLength(25,
			ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
		public string FinancialCode { get; set; }

		public virtual ICollection<OverCost> OverCosts { get; set; }

		public override string ToString() => $"{Name?.ToString()} ({FinancialCode?.ToString()})";
	}
}