using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class Setting : Base.Interfaces.IEntity<int>
    {
        public int Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "درصد قابل استفاده اجباریست!")]
        [Display(Name = "درصد قابل استفاده")]
        public decimal IngredientUsageRate { get; set; }
    }
}