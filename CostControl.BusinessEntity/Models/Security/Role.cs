using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.Security
{
    public class Role : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }
        
        [Required(ErrorMessage = "نام اجباریست!")]
        [Display(Name = "نام/عنوان")]
        public string Name { get; set; }
    }
}