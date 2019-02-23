using CostControl.Entity.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class IntakeRemittance : Base.SuperEntity<long>
    {
        [Required]
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        [Required]
        public DateTime IntakeDate { get; set; }

        [StringLength(500, MinimumLength = 10,
                ErrorMessage = "Please enter a vlid description, it must be greater than {2} characters and less than {1} characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        [Required]
        public long RegisteredUserId { get; set; }

        public virtual User RegisteredUser { get; set; }

        public virtual ICollection<IntakeRemittanceItem> IntakeRemittanceItems { get; set; }
    }
}