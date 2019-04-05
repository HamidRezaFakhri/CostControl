namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class OverCostType : BaseValidating, Base.Interfaces.IEntity<byte>
    {
        public byte Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

        [Required(ErrorMessage = "نام/عنوان اجباریست!")]
        [Display(Name = "نام/عنوان")]
        [StringLength(250, MinimumLength = 3,
            ErrorMessage = "تعداد کاراکترها باید بیشتر از {2} و کمتر از {1} باشد.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "کد اجباریست!")]
        [Display(Name = "کد")]
        [StringLength(10, MinimumLength = 1,
            ErrorMessage = "تعداد کاراکترها باید بیشتر از {2} و کمتر از {1} باشد.")]
        public string FinancialCode { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region Name validation rules
            if (IsNullOrEmpty(Name))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(Name)), new[] { nameof(Name) });
            }

            if (Name?.Length > 100 || Name?.Length < 3)
            {
                yield return new ValidationResult(ValidationMessages.StringLengthRange(nameof(Name), 3, 100), new[] { nameof(Name) });
            }
            #endregion

            #region FinancialCode validation rules
            if (IsNullOrEmpty(FinancialCode))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(FinancialCode)), new[] { nameof(FinancialCode) });
            }

            if (FinancialCode?.Length > 10 || FinancialCode?.Length < 1)
            {
                yield return new ValidationResult(ValidationMessages.StringLengthRange(nameof(FinancialCode), 3, 10), new[] { nameof(FinancialCode) });
            }
            #endregion
        }
    }
}