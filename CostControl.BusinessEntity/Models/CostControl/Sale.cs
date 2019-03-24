namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class Sale : Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		public long SaleCostPointId { get; set; }

		[Required(ErrorMessage = "کد اجباریست!")]
		[Display(Name = "کد")]
		public string Code { get; set; }

		public System.DateTime SaleDate { get; set; }
	}
}