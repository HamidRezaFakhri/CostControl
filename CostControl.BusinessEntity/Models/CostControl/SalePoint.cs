namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class SalePoint : Base.Interfaces.IEntity<long>//, IValidatableObject
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

		[Required(ErrorMessage = "کد اجباریست!")]
		[Display(Name = "کد")]
		public string Code { get; set; }

		[Required(ErrorMessage = "نام اجباریست!")]
		[Display(Name = "نام/عنوان")]
		public string Name { get; set; }

		[Required(ErrorMessage = "نام لاتین اجباریست!")]
		[Display(Name = "نام/عنوان لاتین")]
		public string EnglishName { get; set; }

		[Display(Name = "سالن")]
		public bool IsHall { get; set; } = false;

		//public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		//{
		//    var validator = new MovieViewModelValidator();
		//    var result = validator.Validate(this);
		//    return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
		//}
	}
}