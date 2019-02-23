using CostControl.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class Menu : SupperNameEntity<long>
    {
        [Required]
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string Code { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2,
            ErrorMessage = "Please enter a unique English Name, it must be greater than {2} characters and less than {1} characters.")]
        public string EnglishName { get; set; }

        [Required]
        public DateTime FromDate { get; set; } = DateTime.Now;

        public DateTime? ToDate { get; set; }

        [Required]
        public decimal CostRatio { get; set; } = 100;

        [Required]
        public decimal Price { get; set; } = 0;

        [Required]
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}