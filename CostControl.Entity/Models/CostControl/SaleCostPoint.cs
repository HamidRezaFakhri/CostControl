namespace CostControl.Entity.Models.CostControl
{
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class SaleCostPoint : SuperEntity<long>
    {
        public long SalePointId { get; set; }

        public virtual SalePoint SalePoint { get; set; }

        public long CostPointId { get; set; }

        public virtual CostPoint CostPoint { get; set; }

        public virtual ICollection<Food> Foods { get; set; }

        public virtual ICollection<IntakeRemittance> IntakeRemittances { get; set; }

        public virtual ICollection<OverCost> OverCosts { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<Draft> Drafts { get; set; }

        public virtual ICollection<Depo> Depos { get; set; }

        public override string ToString() => $"{SalePoint?.ToString()} - {CostPoint?.ToString()}";
    }
}