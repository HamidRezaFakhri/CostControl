using CostControl.Entity.Models.Base;
using CostControl.Entity.Models.CostControl.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class Inventory : SupperNameEntity<long>
    {
        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string Code { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
        
        public bool IsWasted { get; set; } = false;

        public virtual ICollection<Draft> Drafts { get; set; }
    }
}