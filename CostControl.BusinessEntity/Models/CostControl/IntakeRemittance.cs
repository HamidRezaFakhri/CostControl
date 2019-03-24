namespace CostControl.BusinessEntity.Models.CostControl
{
	using System;

	public class IntakeRemittance : Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		public long SaleCostPointId { get; set; }

		public DateTime IntakeDate { get; set; }

		public string Description { get; set; }

		public DateTime RegisteredDate { get; set; }

		public long RegisteredUserId { get; set; }
	}
}