namespace CostControl.Entity.Models.CostControl
{
    using System;
    using System.Collections.Generic;

    public class IntakeRemittance : Base.SuperEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public DateTime IntakeFromDate { get; set; }

        public DateTime IntakeToDate { get; set; }

        public string Description { get; set; }

        public DateTime RegisteredDate { get; set; }

        public int RegisteredUserId { get; set; }

        public virtual IncommingUser RegisteredUser { get; set; }

        public virtual ICollection<IntakeRemittanceItem> IntakeRemittanceItems { get; set; }
    }
}