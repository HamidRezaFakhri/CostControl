using CostControl.Entity.Models.Base;
using CostControl.Entity.Models.CostControl.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class Food : SupperNameEntity<long>
    {
        [Required]
        public long SaleCostPointId { get; set; }

        public SaleCostPoint SaleCostPoint { get; set; }

        [StringLength(25,
            ErrorMessage = "Please enter a unique Code, it must be less than {0} characters.")]
        public string Code { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2,
            ErrorMessage = "Please enter a unique English Name, it must be greater than {2} characters and less than {1} characters.")]
        public string EnglishName { get; set; }

        //public RecipeType Type { get; set; } = RecipeType.Carnivorous;

        public ServeType ServeType { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        public virtual ICollection<Recipe> RecipeItems { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
        
        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}