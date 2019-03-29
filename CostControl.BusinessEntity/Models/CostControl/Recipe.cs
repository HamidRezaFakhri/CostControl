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

        [Required]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "واحد مصرفی")]
        public long ConsumptionUnitId { get; set; }

        [Required]
        [Display(Name = "نرخ تبدیل")]
        public decimal ConvertionRate { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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

            #region ConvertionRate validation rules
            if (ConvertionRate <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(ConvertionRate)), new[] { nameof(ConvertionRate) });
            }
            #endregion
        }
    }
}