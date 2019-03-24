﻿namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class ConsumptionUnit : Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

		[Required(ErrorMessage = "نام اجباریست!")]
		[Display(Name = "نام/عنوان")]
		public string Name { get; set; }

		[Required(ErrorMessage = "کد اجباریست!")]
		[Display(Name = "کد")]
		public string Code { get; set; }
	}
}