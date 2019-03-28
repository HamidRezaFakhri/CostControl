namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Recipe : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Display(Name = "خوراک")]
        public long FoodId { get; set; }

        [Display(Name = "مواد خام/اولیه")]
        public long IngredientId { get; set; }

        [Required(ErrorMessage = "مقدار اجباریست!")]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "واحد مصرفی اجباریست!")]
        [Display(Name = "واحد مصرفی")]
        public long ConsumptionUnitId { get; set; }

        [Required(ErrorMessage = "نرخ تبدیل اجباریست!")]
        [Display(Name = "نرخ تبدیل")]
        public decimal ConvertionRate { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}