using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class CostPoint : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

        [Required(ErrorMessage = "نام اجباریست!")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "کد اجباریست!")]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required(ErrorMessage = "گروه مرکز هزینه اجباریست!")]
        [Display(Name = "گروه مرکز هزینه")]
        public long CostPointGroupId { get; set; }
    }
}