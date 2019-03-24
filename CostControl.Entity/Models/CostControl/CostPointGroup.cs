namespace CostControl.Entity.Models.CostControl
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Entity.Models.Base;

	public class CostPointGroup : SupperNameEntity<long>
	{
		[StringLength(25,
			ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
		public string Code { get; set; }

		public virtual ICollection<CostPoint> CostPoints { get; set; }

		public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
	}
}