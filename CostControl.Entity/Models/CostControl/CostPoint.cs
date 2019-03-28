namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class CostPoint : SupperNameEntity<long>
    {
        public string Code { get; set; }

        public long CostPointGroupId { get; set; }

        public virtual CostPointGroup CostPointGroup { get; set; }

        public virtual ICollection<SaleCostPoint> SaleCostPoints { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({CostPointGroup?.ToString()} - {Code?.ToString()})";
    }
}