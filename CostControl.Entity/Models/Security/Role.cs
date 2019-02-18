using CostControl.Entity.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostControl.Entity.Models.Security
{
    public class Role : SupperNameEntity<long>
    {
        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string Code { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}