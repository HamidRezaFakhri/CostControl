namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class CostPoint : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

        [Required]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "گروه مرکز هزینه")]
        public long CostPointGroupId { get; set; }

        public string CostPointGroupName { get; set; }

        public CostPointGroup CostPointGroup { get; set; }

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

            #region Code validation rules
            if (IsNullOrEmpty(Code))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(Code)), new[] { nameof(Code) });
            }

            if (Code?.Length > 10 || Code?.Length < 3)
            {
                yield return new ValidationResult(ValidationMessages.StringLengthRange(nameof(Code), 3, 10), new[] { nameof(Code) });
            }
            #endregion

            #region CostPointGroupId validation rules
            if (CostPointGroupId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(CostPointGroupId)), new[] { nameof(CostPointGroupId) });
            }
            #endregion
        }
    }
}