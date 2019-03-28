namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;
    using Entity.Models.CostControl.Enums;

    public class Ingredient : SupperNameEntity<long>
    {
        public string Code { get; set; }

        public string EnglishName { get; set; }

        public IngredientType Type { get; set; }

        public decimal Price { get; set; }

        public decimal UsefullRatio { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Recipe> RecipeItems { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}