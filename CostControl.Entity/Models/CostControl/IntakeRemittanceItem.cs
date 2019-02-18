using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class IntakeRemittanceItem : Base.SuperEntity<long>
    {
        [Required]
        public long IntakeRemittanceID { get; set; }
        
        public virtual IntakeRemittance IntakeRemittance { get; set; }

        [Required]
        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }
    }
}