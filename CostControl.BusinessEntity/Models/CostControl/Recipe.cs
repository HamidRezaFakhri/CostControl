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

        public Ingredient Ingredient { get; set; }

        [Required(ErrorMessage = "مقدار اجباریست!")]
        [Display(Name = "مقدار")]
        [Range(minimum: 0d, maximum: 999999d, ErrorMessage ="مقدار وارد شده باید بین {1} و {2} باشد")]
        public decimal Amount { get; set; }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region Amount validation rules
            if (Amount <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(Amount)), new[] { nameof(Amount) });
            }
            #endregion
        }
    }
}