using CostControl.Entity.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class Depo : SuperEntity<long>
    {
        [Required]
        public long SaleCostPointId { get; set; }

        public SaleCostPoint SaleCostPoint { get; set; }

        [Required]
        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public long ConsumptionUnitId { get; set; }

        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public virtual ICollection<Draft> Drafts { get; set; }
    }
}