using CostControl.Entity.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class CostPoint : SupperNameEntity<long>
    {
        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string Code { get; set; }

        [Required]
        public long CostPointGroupId { get; set; }

        public CostPointGroup CostPointGroup { get; set; }

        public virtual ICollection<SaleCostPoint> SaleCostPoints { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({CostPointGroup?.ToString()} - {Code?.ToString()})";
    }
}