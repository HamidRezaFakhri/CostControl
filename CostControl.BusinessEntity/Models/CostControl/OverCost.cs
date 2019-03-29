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

        [Required]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        [Required]
        [Display(Name = "سرفصل هزینه سربار")]
        public long OverCostTypeId { get; set; }

        public virtual OverCostType OverCostType { get; set; }

        [Required]
        [Display(Name = "تاریخ شروع")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EndDate { get; set; }

        [Required]
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

            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(ValidationMessages.WrongSequence(nameof(StartDate), nameof(EndDate)), new[] { nameof(EndDate) });
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