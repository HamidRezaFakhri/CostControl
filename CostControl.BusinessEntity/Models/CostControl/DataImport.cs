using System;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class DataImport : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

        public DateTime ImportTime { get; set; }
    }
}