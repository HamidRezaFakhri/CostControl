using CostControl.Entity.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.CostControl
{
    public class SaleCostPoint : SuperEntity<long>
    {
        [Required]
        public long SalePointId { get; set; }

        public virtual SalePoint SalePoint { get; set; }

        [Required]
        public long CostPointId { get; set; }

        public virtual CostPoint CostPoint { get; set; }

        public virtual ICollection<Food> Foods { get; set; }

        public virtual ICollection<IntakeRemittance> IntakeRemittances { get; set; }

        public virtual ICollection<OverCost> OverCosts { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<Draft> Drafts { get; set; }

        public virtual ICollection<Depo> Depos { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public override string ToString() => $"{SalePoint?.ToString()} - {CostPoint?.ToString()}";
    }
}