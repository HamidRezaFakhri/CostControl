﻿namespace CostControl.Entity.Models.CostControl
{
    using Entity.Models.Base;

    public class Recipe : SuperEntity<long>
    {
        public long FoodId { get; set; }

        public virtual Food Food { get; set; }

        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }

        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        public decimal ConvertionRate { get; set; }
    }
}