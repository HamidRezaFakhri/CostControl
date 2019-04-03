namespace CostControl.BusinessEntity.Models.CostControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class OverCost : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "مرکز فروش-مرکز هزینه اجباریست!")]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        [Required(ErrorMessage = "سرفصل هزینه سربار اجباریست!")]
        [Display(Name = "سرفصل هزینه سربار")]
        public long OverCostTypeId { get; set; }

        public virtual OverCostType OverCostType { get; set; }

        [Required(ErrorMessage = "تاریخ شروع اجباریست!")]
        [Display(Name = "تاریخ شروع")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "تاریخ پایان اجباریست!")]
        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "قیمت اجباریست!")]
        [Display(Name = "قیمت")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; } = 0;

        [Display(Name = "شرح")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        public long RegisteredUserId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region SaleCostPoint validation rules
            if (SaleCostPointId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(SaleCostPointId)), new[] { nameof(SaleCostPointId) });
            }
            #endregion

            #region OverCostTypeId validation rules
            if (OverCostTypeId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(OverCostTypeId)), new[] { nameof(OverCostTypeId) });
            }
            #endregion

            #region StartDate validation rules
            if (StartDate == null || StartDate == default(DateTime))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(StartDate)), new[] { nameof(StartDate) });
            }
            #endregion

            #region EndDate validation rules
            if (EndDate == null || EndDate == default(DateTime))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(EndDate)), new[] { nameof(EndDate) });
            }
            #endregion

            #region Dates
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(ValidationMessages.WrongSequence(nameof(StartDate), nameof(EndDate)), new[] { nameof(StartDate), nameof(EndDate) });
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