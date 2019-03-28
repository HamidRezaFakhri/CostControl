namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class SalePoint : SupperNameEntity<long>
    {
        public string Code { get; set; }

        public string EnglishName { get; set; }

        public bool IsHall { get; set; }

        public virtual ICollection<SaleCostPoint> SaleCostPoints { get; set; }

        public virtual ICollection<Buffet> Buffets { get; set; }

        public override string ToString() => $"{Name?.ToString()} ({Code?.ToString()})";
    }
}