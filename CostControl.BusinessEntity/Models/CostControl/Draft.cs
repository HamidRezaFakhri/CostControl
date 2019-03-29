namespace CostControl.BusinessEntity.Models.CostControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Draft : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        [Display(Name = "انبار")]
        public long InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        [Display(Name = "انبار موقت")]
        public long DepoId { get; set; }

        public virtual Depo Depo { get; set; }

        [Required]
        [Display(Name = "تاریخ")]
        public DateTime DraftDate { get; set; }

        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisteredDate { get; set; }

        public long RegisteredUserId { get; set; }

        public virtual IncommingUser RegisteredUser { get; set; }

        [Display(Name = "شرح")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region SaleCostPoint validation rules
            if (SaleCostPointId <= 0)
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(SaleCostPointId)), new[] { nameof(SaleCostPointId) });
            }
            #endregion

            #region DraftDate validation rules
            if (DraftDate == null || DraftDate == default(DateTime))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(DraftDate)), new[] { nameof(DraftDate) });
            }
            #endregion

            #region RegisteredDate validation rules
            if (RegisteredDate == null || RegisteredDate == default(DateTime))
            {
                yield return new ValidationResult(ValidationMessages.CanNotBeEmpty(nameof(RegisteredDate)), new[] { nameof(RegisteredDate) });
            }
            #endregion
        }
    }
}