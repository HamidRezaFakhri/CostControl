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

        [Required]
        [Display(Name = "حواله مصرف")]
        public long IntakeRemittanceID { get; set; }

        [Required]
        [Display(Name = "ماده خام")]
        public long IngredientId { get; set; }

        [Required]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "واحد مصرفی")]
        public long ConsumptionUnitId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region IntakeRemittanceID validation rules
            if(IntakeRemittanceID <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(IntakeRemittanceID)), new[] { nameof(IntakeRemittanceID) });
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