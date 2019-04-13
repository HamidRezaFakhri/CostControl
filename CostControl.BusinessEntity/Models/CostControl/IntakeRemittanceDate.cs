namespace CostControl.BusinessEntity.Models.CostControl
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IntakeRemittanceDate
    {
        [Required(ErrorMessage = "تاریخ حواله اجباریست!")]
        [Display(Name = "تاریخ حواله")]
        public DateTime IntakeDate { get; set; }
    }
}