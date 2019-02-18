namespace CostControl.BusinessEntity.Models.CostControl
{
    public class Buffet : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }
    }
}