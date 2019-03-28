namespace CostControl.Entity.Models.CostControl
{
    using System;
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class Menu : SupperNameEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public string Code { get; set; }

        public string EnglishName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public decimal CostRatio { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}