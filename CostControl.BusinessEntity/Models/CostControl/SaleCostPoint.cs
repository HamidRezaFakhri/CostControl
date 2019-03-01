using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class SaleCostPoint : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "مرکز فروش اجباریست!")]
        [Display(Name = "مرکز فروش")]
        public long SalePointId { get; set; }

        [Display(Name = "نام مرکز فروش")]
        public string SalePointName { get; set; }

        public SalePoint SalePoint { get; set; }

        [Required(ErrorMessage = "مرکز هزینه اجباریست!")]
        [Display(Name = "مرکز هزینه")]
        public long CostPointId { get; set; }
        
        [Display(Name = "نام مرکز هزینه")]
        public string CostPointName { get; set; }

        public CostPoint CostPoint { get; set; }
    }
}