namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class Setting : BaseValidating, Base.Interfaces.IEntity<int>
    {
        public int Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "درصد قابل استفاده اجباریست!")]
        [Display(Name = "درصد قابل استفاده")]
        public decimal IngredientUsageRate { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}