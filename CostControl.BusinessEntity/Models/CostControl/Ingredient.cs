namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class Ingredient : Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		[Required(ErrorMessage = "نام اجباریست!")]
		[Display(Name = "نام")]
		public string Name { get; set; }

		[Required(ErrorMessage = "کد اجباریست!")]
		[Display(Name = "کد")]
		public string Code { get; set; }

		[Required(ErrorMessage = "نام لاتین اجباریست!")]
		[Display(Name = "نام لاتین")]
		public string EnglishName { get; set; }

		[Required(ErrorMessage = "نوع اجباریست!")]
		[Display(Name = "نوع")]
		public Enums.IngredientType Type { get; set; }

		[Required(ErrorMessage = "قیمت اجباریست!")]
		[Display(Name = "قیمت")]
		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "نرخ اجباریست!")]
		[Display(Name = "نرخ (درصد)")]
		//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
		public decimal UsefullRatio { get; set; }

		[Display(Name = "شرح")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
	}
}