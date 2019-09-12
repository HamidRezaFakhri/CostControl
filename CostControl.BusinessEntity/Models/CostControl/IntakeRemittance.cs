namespace CostControl.BusinessEntity.Models.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using BusinessEntity.Validations;

	public class IntakeRemittance : BaseValidating, Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		[Required(ErrorMessage = "مرکز فروش-مرکز هزینه اجباریست!")]
		[Display(Name = "مرکز فروش-مرکز هزینه")]
		public long SaleCostPointId { get; set; }

		[Required(ErrorMessage = "تاریخ حواله اجباریست!")]
		[Display(Name = "تاریخ حواله")]
		public DateTime IntakeFromDate { get; set; }

		//[Required(ErrorMessage = "تاریخ حواله اجباریست!")]
		//[Display(Name = "تاریخ پایان حواله")]
		public DateTime IntakeToDate { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "شرح")]
		public string Description { get; set; }

		public bool IsConfirmed { get; set; } = false;

		public DateTime RegisteredDate { get; set; } = DateTime.Now;

		public long RegisteredUserId { get; set; }
			
		public ICollection<IntakeRemittanceItem> IntakeRemittanceItems { get; set; }

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			#region SaleCostPointId validation rules
			if (SaleCostPointId <= 0)
			{
				yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(SaleCostPointId)), new[] { nameof(SaleCostPointId) });
			}
			#endregion

			#region IntakeDate validation rules
			if (IntakeFromDate == null || IntakeFromDate == default(DateTime))
			{
				yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IntakeFromDate)), new[] { nameof(IntakeFromDate) });
			}

			//if (IntakeToDate == null || IntakeToDate == default(DateTime))
			//{
			//	yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IntakeToDate)), new[] { nameof(IntakeToDate) });
			//}

			//if (IntakeToDate <= IntakeFromDate)
			//{
			//	yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IntakeFromDate)), new[] { nameof(IntakeFromDate) });
			//}
			#endregion
		}
	}
}