using CostControl.Entity.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class Buffet : SuperEntity<long>
    {
        [Required]
        public long SalePointId { get; set; }

        public virtual SalePoint SalePoint { get; set; }
    }
}