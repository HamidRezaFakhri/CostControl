using CostControl.Entity.Models.Base;

namespace CostControl.Entity.Models.CostControl
{
    public sealed class Setting : SuperEntity<int>
    {
        public decimal IngredientUsageRate { get; set; } = 70;
    }
}