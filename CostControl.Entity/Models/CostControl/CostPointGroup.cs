namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class CostPointGroup : SupperNameEntity<long>
    {
        public string Code { get; set; }

        public virtual ICollection<CostPoint> CostPoints { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}