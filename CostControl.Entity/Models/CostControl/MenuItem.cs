namespace CostControl.Entity.Models.CostControl
{
    using Entity.Models.Base;

    public class MenuItem : SuperEntity<long>
    {
        public long MenuId { get; set; }

        public virtual Menu Menu { get; set; }

        public long FoodId { get; set; }

        public virtual Food Food { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }
    }
}