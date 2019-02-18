using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class Draft : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Display(Name = "انبار")]
        public long InventoryId { get; set; }

        [Display(Name = "انبار موقت")]
        public long DepoId { get; set; }
        
        [Required(ErrorMessage = "تاریخ اجباریست!")]
        [Display(Name = "تاریخ")]
        public System.DateTime DraftDate { get; set; }

        public System.DateTime RegisteredDate { get; set; }

        public long RegisteredUserId { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }
    }
}