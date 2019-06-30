namespace CostControl.Entity.Models.CostControl
{
    using System;
    using System.Collections.Generic;
    using Entity.Models.Base;

    public class Draft : SuperEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public long InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        public long DepoId { get; set; }

        public virtual Depo Depo { get; set; }

        public DateTime DraftDate { get; set; }

        public DateTime RegisteredDate { get; set; }

        public int RegisteredUserId { get; set; }

        public virtual IncommingUser RegisteredUser { get; set; }

        public virtual ICollection<DraftItem> DraftItems { get; set; }

        public string Description { get; set; }
    }
}