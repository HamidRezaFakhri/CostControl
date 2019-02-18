using CostControl.Entity.Models.Base;
using CostControl.Entity.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class Draft : SuperEntity<long>
    {
        [Required]
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        [Required]
        public long InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        public long DepoId { get; set; }

        public virtual Depo Depo { get; set; }

        [Required]
        public DateTime DraftDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        [Required]
        public long RegisteredUserId { get; set; }

        public User RegisteredUser { get; set; }

        [Required]
        public ICollection<DraftItem> DraftItems { get; set; }

        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Please enter a vlid description, it must be greater than {2} characters and less than {1} characters.")]
        public string Description { get; set; }
    }
}