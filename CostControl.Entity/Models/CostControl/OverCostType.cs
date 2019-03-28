namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class OverCostType : SupperNameEntity<byte>
    {
        public string FinancialCode { get; set; }

        public virtual ICollection<OverCost> OverCosts { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({FinancialCode?.ToString()})";
    }
}