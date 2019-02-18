using CostControl.Entity.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class MenuItem : SuperEntity<long>
    {
        [Required]
        public long MenuId { get; set; }

        public virtual Menu Menu { get; set; }

        public Food Food { get; set; }

        public Ingredient Ingredient { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }
    }
}