namespace CostControl.Entity.Models.CostControl
{
    using System;
    using System.Collections.Generic;

    public class IntakeRemittance : Base.SuperEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public DateTime IntakeDate { get; set; }

        public string Description { get; set; }

        public DateTime RegisteredDate { get; set; }

        public long RegisteredUserId { get; set; }

        public virtual IncommingUser RegisteredUser { get; set; }

        public virtual ICollection<IntakeRemittanceItem> IntakeRemittanceItems { get; set; }
    }
}