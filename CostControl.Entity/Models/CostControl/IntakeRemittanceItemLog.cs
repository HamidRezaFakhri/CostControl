using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class IntakeRemittanceItemLog : Base.SuperEntity<long>
    {
        public long IntakeRemittanceItemId { get; set; }

        public virtual IntakeRemittanceItem IntakeRemittanceItem { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        public decimal Amount { get; set; }

        [StringLength(1000, MinimumLength = 10,
        ErrorMessage = "Please enter a vlid description, it must be greater than {2} characters and less than {1} characters.")]
        public string Descripton { get; set; }

        public bool IsAddedManualy { get; set; } = false;
    }
}