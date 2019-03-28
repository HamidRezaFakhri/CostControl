namespace CostControl.Entity.Models.CostControl
{
    using Entity.Models.Base;

    public class DraftItem : SuperEntity<long>
    {
        public long DraftId { get; set; }

        public virtual Draft Draft { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        public decimal Amount { get; set; }
    }
}