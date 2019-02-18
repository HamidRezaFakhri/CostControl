namespace CostControl.BusinessEntity.Models.CostControl
{
    public class IntakeRemittance : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        public long SaleCostPointId { get; set; }

        public System.DateTime IntakeDate { get; set; }

        public string Description { get; set; }

        public System.DateTime RegisteredDate { get; set; }

        public long RegisteredUserId { get; set; }
    }
}