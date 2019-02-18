namespace CostControl.BusinessEntity.Models.Security
{
    public class Role : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        public string Name { get; set; }
    }
}