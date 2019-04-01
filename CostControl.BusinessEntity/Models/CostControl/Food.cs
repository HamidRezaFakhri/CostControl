namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Food : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        [Required]
        [Display(Name = "نام/عنوان")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "نام لاتین")]
        public string EnglishName { get; set; }

        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "نحوه سرو")]
        //[EnumDataType(typeof(FoodType))]
        public byte FoodType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region SaleCostPoint validation rules
            if (SaleCostPointId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(SaleCostPointId)), new[] { nameof(SaleCostPointId) });
            }
            #endregion

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

            #region EnglishName validation rules
            if (IsNullOrEmpty(EnglishName))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(EnglishName)), new[] { nameof(EnglishName) });
            }

            if (EnglishName?.Length > 100 || EnglishName?.Length < 3)
            {
                yield return new ValidationResult(ValidationMessages.StringLengthRange(nameof(EnglishName), 3, 100), new[] { nameof(EnglishName) });
            }
            #endregion

            #region FoodType validation rules
            if (FoodType <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(FoodType)), new[] { nameof(FoodType) });
            }
            #endregion

            #region Price validation rules
            if (Price <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(Price)), new[] { nameof(Price) });
            }
            #endregion
        }
    }
}