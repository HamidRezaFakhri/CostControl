namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class Depo : SuperEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<Draft> Drafts { get; set; }
    }
}