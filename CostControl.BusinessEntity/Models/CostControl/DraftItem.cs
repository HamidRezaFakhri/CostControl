﻿namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class DraftItem : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "حواله انبار اجباریست!")]
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
        [Range(minimum: 0d, maximum: 9999999d, ErrorMessage = "مقدار وارد شده باید بین {1} و {2} باشد")]
        public decimal Amount { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region DraftId validation rules
            if (DraftId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(DraftId)), new[] { nameof(DraftId) });
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