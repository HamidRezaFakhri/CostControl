using System;
using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class OverCost : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }
        
        [Required(ErrorMessage = "مرکز فروش-مرکز هزینه اجباریست!")]
        [Display(Name = "مرکز فروش-مرکز هزینه")]
        public long SaleCostPointId { get; set; }

        [Required(ErrorMessage = "سرفصل هزینه سربار اجباریست!")]
        [Display(Name = "سرفصل هزینه سربار")]
        public long OverCostTypeId { get; set; }

        //[Required(ErrorMessage = "تاریخ شروع اجباریست!")]
        //[Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "تاریخ پایان اجباریست!")]
        [Display(Name = "تاریخ پایان")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "قیمت اجباریست!")]
        [Display(Name = "قیمت")]
        public decimal Price { get; set; } = 0;

        [Display(Name = "شرح")]
        public string Description { get; set; }

        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        public long RegisteredUserId { get; set; }
    }
}