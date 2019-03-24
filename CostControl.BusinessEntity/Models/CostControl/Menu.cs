namespace CostControl.BusinessEntity.Models.CostControl
{
	using System.ComponentModel.DataAnnotations;

	public class Menu : Base.Interfaces.IEntity<long>
	{
		public long Id { get; set; }

		public System.Guid? InstanceId { get; set; }

		public Base.Enums.ObjectState State { get; set; }

		[Required(ErrorMessage = "نام اجباریست!")]
		[Display(Name = "نام/عنوان")]
		public string Name { get; set; }

		[Required(ErrorMessage = "نام لاتین اجباریست!")]
		[Display(Name = "نام لاتین")]
		public string EnglishName { get; set; }

		[Required(ErrorMessage = "کد اجباریست!")]
		[Display(Name = "کد")]
		public string Code { get; set; }

		[Required(ErrorMessage = "مرکز فروش-مرکز هزینه اجباریست!")]
		[Display(Name = "مرکز فروش-مرکز هزینه")]
		public long SaleCostPointId { get; set; }

		[Required(ErrorMessage = "تاریخ ایجاد اجباریست!")]
		[Display(Name = "تاریخ ایجاد")]
		public System.DateTime FromDate { get; set; } = System.DateTime.Now;

		[Display(Name = "تاریخ انقضاء")]
		public System.DateTime? ToDate { get; set; }

		[Required(ErrorMessage = "نرخ اجباریست!")]
		[Display(Name = "نرخ")]
		public decimal CostRatio { get; set; } = 0;

		[Required(ErrorMessage = "قیمت اجباریست!")]
		[Display(Name = "قیمت")]
		public decimal Price { get; set; } = 0;
	}
}