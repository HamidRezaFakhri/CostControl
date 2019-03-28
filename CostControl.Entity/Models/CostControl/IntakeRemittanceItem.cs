namespace CostControl.Entity.Models.CostControl
{
    public class IntakeRemittanceItem : Base.SuperEntity<long>
    {
        public long IntakeRemittanceID { get; set; }

        public virtual IntakeRemittance IntakeRemittance { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }
    }
}