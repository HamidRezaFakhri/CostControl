using CostControl.Entity.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class DraftItem : SuperEntity<long>
    {
        [Required]
        public long DraftId { get; set; }

        public virtual Draft Draft { get; set; }

        [Required]
        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public long ConsumptionUnitId { get; set; }
        
        public virtual ConsumptionUnit ConsumptionUnit { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}