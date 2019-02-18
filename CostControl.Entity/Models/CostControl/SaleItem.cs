using CostControl.Entity.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class SaleItem : SuperEntity<long>
    {
        [Required]
        public long SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        public long FoodId { get; set; }

        public virtual Food Food { get; set; }
        
        public long IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public int Count { get; set; } = 1;

        [Required]
        public decimal Price { get; set; } = 0;
    }
}