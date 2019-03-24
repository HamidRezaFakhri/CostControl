namespace CostControl.Entity.Models.CostControl
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class CostPoint : SupperNameEntity<long>
	{
		[StringLength(25,
			ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
		public string Code { get; set; }

		[Required]
		public long CostPointGroupId { get; set; }

		public virtual CostPointGroup CostPointGroup { get; set; }

		public virtual ICollection<SaleCostPoint> SaleCostPoints { get; set; }

		public override string ToString() => $"{Name?.ToString()} ({CostPointGroup?.ToString()} - {Code?.ToString()})";
	}
}