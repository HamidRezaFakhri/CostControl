using CostControl.Entity.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class SalePoint : SupperNameEntity<long>
    {
        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string Code { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2,
            ErrorMessage = "Please enter a unique English Name, it must be greater than {2} characters and less than {1} characters.")]
        public string EnglishName { get; set; }

        public bool IsHall { get; set; } = false;
        
        public virtual ICollection<SaleCostPoint> SaleCostPoints { get; set; }
        
        public virtual ICollection<Buffet> Buffets { get; set; }
       
        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}