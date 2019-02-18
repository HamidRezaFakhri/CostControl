namespace CostControl.BusinessEntity.Models.CostControl
{
    public class Setting : Base.Interfaces.IEntity<int>
    {
        public int Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        public decimal IngredientUsageRate { get; set; }
    }
}