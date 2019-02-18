namespace CostControl.BusinessEntity.Models.CostControl
{
    public class Depo : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        public long SaleCostPointId  { get; set; }

        public long IngredientId { get; set; }

        public long ConsumptionUnitId { get; set; }

        public decimal Amount { get; set; }
    }
}