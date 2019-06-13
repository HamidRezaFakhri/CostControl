﻿namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;
    using BusinessEntity.Models.CostControl.Enums;

    public class Food : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }
        
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long? SaleCostPointId { get; set; }

        [Required(ErrorMessage = "نام/عنوان اجباریست!")]
        [Display(Name = "نام/عنوان")]
        [StringLength(250, MinimumLength = 3,
            ErrorMessage = "تعداد کاراکترها باید بیشتر از {2} و کمتر از {1} باشد.")]
        public string Name { get; set; }
        
        [Display(Name = "نام لاتین")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "کد اجباریست!")]
        [Display(Name = "کد")]
        [StringLength(10, MinimumLength = 1,
            ErrorMessage = "تعداد کاراکترها باید بیشتر از {2} و کمتر از {1} باشد.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "نوع اجباریست!")]
        [Display(Name = "نوع")]
        [EnumDataType(typeof(FoodType))]
        public FoodType FoodType { get; set; }

        //[Required(ErrorMessage = "قیمت اجباریست!")]
        //[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        //[Display(Name = "قیمت")]
        //public decimal Price { get; set; }

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

            if (Code?.Length > 10 || Code?.Length < 1)
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
        }
    }
}