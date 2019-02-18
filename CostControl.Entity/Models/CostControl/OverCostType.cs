using CostControl.Entity.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class OverCostType : SupperNameEntity<byte>
    {
        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string FinancialCode { get; set; }
        
        public virtual ICollection<OverCost> OverCosts { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({FinancialCode?.ToString()})";
    }
}