namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using BusinessEntity.Validations;

	public class IntakeRemittanceItem : BaseValidating, Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		[Required(ErrorMessage = "حواله مصرف اجباریست!")]
		[Display(Name = "حواله مصرف")]
		public long IntakeRemittanceId { get; set; }

		[Required(ErrorMessage = "ماده خام اجباریست!")]
		[Display(Name = "ماده خام")]
		public long IngredientId { get; set; }

		public virtual Ingredient Ingredient { get; set; }

		[Required(ErrorMessage = "مقدار اجباریست!")]
		[Display(Name = "مقدار")]
		[Range(minimum: 0d, maximum: 9999999d, ErrorMessage = "مقدار وارد شده باید بین {1} و {2} باشد")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "واحد مصرفی اجباریست!")]
		[Display(Name = "واحد مصرفی")]
		public long ConsumptionUnitId { get; set; }

		public virtual ConsumptionUnit ConsumptionUnit { get; set; }

		[Required(ErrorMessage = "شرح اجباریست!")]
		[Display(Name = "شرح")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			#region IntakeRemittanceID validation rules
			if (IntakeRemittanceId <= 0)
			{
				yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IntakeRemittanceId)), new[] { nameof(IntakeRemittanceId) });
			}
			#endregion

			#region IngredientId validation rules
			if (IngredientId <= 0)
			{
				yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IngredientId)), new[] { nameof(IngredientId) });
			}
			#endregion

			#region Amount validation rules
			if (Amount <= 0)
			{
				yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(Amount)), new[] { nameof(Amount) });
			}
			#endregion

			#region ConsumptionUnitId validation rules
			if (ConsumptionUnitId <= 0)
			{
				yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(ConsumptionUnitId)), new[] { nameof(ConsumptionUnitId) });
			}
			#endregion
		}
	}
}