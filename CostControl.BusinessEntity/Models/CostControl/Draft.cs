namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class Draft : Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		[Required]
		public long SaleCostPointId { get; set; }

		public virtual SaleCostPoint SaleCostPoint { get; set; }

		[Display(Name = "انبار")]
		public long InventoryId { get; set; }

		public virtual Inventory Inventory { get; set; }

		[Display(Name = "انبار موقت")]
		public long DepoId { get; set; }

		public virtual Depo Depo { get; set; }

		[Required(ErrorMessage = "تاریخ اجباریست!")]
		[Display(Name = "تاریخ")]
		public System.DateTime DraftDate { get; set; }

		public System.DateTime RegisteredDate { get; set; }

		public long RegisteredUserId { get; set; }

		public virtual IncommingUser RegisteredUser { get; set; }

		[Display(Name = "شرح")]
		public string Description { get; set; }
	}
}