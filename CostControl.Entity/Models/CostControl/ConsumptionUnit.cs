namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class ConsumptionUnit : SupperNameEntity<long>
    {
        public string Code { get; set; }

        public virtual ICollection<DraftItem> DraftItems { get; set; }

        public virtual ICollection<Recipe> RecipeItems { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}