namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;
    using Entity.Models.CostControl.Enums;

    public class Food : SupperNameEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public string Code { get; set; }

        public string EnglishName { get; set; }

        //public RecipeType Type { get; set; } = RecipeType.Carnivorous;

        public ServeType ServeType { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Recipe> RecipeItems { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}