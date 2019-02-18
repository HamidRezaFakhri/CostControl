namespace CostControl.BusinessEntity.Models.CostControl
{
    public class SaleItem : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        public long SaleId { get; set; }

        public long FoodId { get; set; }

        public long IngredientId { get; set; }
        
        public int Count { get; set; }
        
        public decimal Price { get; set; }

    }
}