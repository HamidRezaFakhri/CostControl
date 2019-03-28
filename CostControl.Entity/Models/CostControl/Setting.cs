namespace CostControl.Entity.Models.CostControl
{
    using Entity.Models.Base;

    public class Setting : SuperEntity<int>
    {
        public decimal IngredientUsageRate { get; set; }
    }
}