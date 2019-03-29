namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Depo : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        [Required]
        [Display(Name = "ماده خام")]
        public long IngredientId { get; set; }

        [Required]
        [Display(Name = "واحد مصرفی")]
        public long ConsumptionUnitId { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region SaleCostPoint validation rules
            if (SaleCostPointId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(SaleCostPointId)), new[] { nameof(SaleCostPointId) });
            }
            #endregion

            #region IngredientId validation rules
            if (IngredientId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IngredientId)), new[] { nameof(IngredientId) });
            }
            #endregion

            #region ConsumptionUnitId validation rules
            if (ConsumptionUnitId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(ConsumptionUnitId)), new[] { nameof(ConsumptionUnitId) });
            }
            #endregion

            #region Amount validation rules
            if (Amount <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(Amount)), new[] { nameof(Amount) });
            }
            #endregion
        }
    }
}