namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Food : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "مرکز فروش-مرکز هزینه اجباریست!")]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        [Required(ErrorMessage = "نام اجباریست!")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "نام لاتین اجباریست!")]
        [Display(Name = "نام لاتین")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "کد اجباریست!")]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required(ErrorMessage = "نحوه سرو اجباریست!")]
        [Display(Name = "نحوه سرو")]
        //[EnumDataType(typeof(ServeType))]
        public byte ServeType { get; set; }

        [Required(ErrorMessage = "قیمت اجباریست!")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}