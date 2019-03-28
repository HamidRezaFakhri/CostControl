namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class Inventory : SupperNameEntity<long>
    {
        public string Code { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";

        public bool IsWasted { get; set; }

        public virtual ICollection<Draft> Drafts { get; set; }
    }
}