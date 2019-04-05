namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Setting : BaseValidating, Base.Interfaces.IEntity<int>
    {
        public int Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage ="درصد قابل استفاده اجباریست!")]
        [Display(Name = "درصد قابل استفاده")]
        [Range(minimum: 0d, maximum: 100d, ErrorMessage = "مقدار وارد شده باید بین {1} و {2} باشد")]
        public decimal IngredientUsageRate { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region IngredientUsageRate validation rules
            if (IngredientUsageRate <= 0 || IngredientUsageRate > 100)
            {
                yield return new ValidationResult(ValidationMessages.Range(nameof(IngredientUsageRate), 1, 100), new[] { nameof(IngredientUsageRate) });
            }
            #endregion
        }
    }
}