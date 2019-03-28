namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class DraftItem : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "حوابه انبار اجباریست!")]
        [Display(Name = "حواله انبار")]
        public long DraftId { get; set; }

        [Required(ErrorMessage = "مواد خام اجباریست!")]
        [Display(Name = "مواد خام")]
        public long IngredientId { get; set; }

        [Required(ErrorMessage = "واحد مصرفی اجباریست!")]
        [Display(Name = "واحد مصرفی")]
        public long ConsumptionUnitId { get; set; }

        [Required(ErrorMessage = "مقدار اجباریست!")]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}