using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class IntakeRemittanceItem : Base.SuperEntity<long>
    {
        public long IntakeRemittanceId { get; set; }

        public virtual IntakeRemittance IntakeRemittance { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        public decimal Amount { get; set; }

        [StringLength(1000, MinimumLength = 10,
        ErrorMessage = "Please enter a vlid description, it must be greater than {2} characters and less than {1} characters.")]
        public string Descripton { get; set; }
    }
}