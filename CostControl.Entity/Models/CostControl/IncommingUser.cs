using CostControl.Entity.Models.Base;
using System.Collections.Generic;

namespace CostControl.Entity.Models.CostControl
{
    public class IncommingUser : SuperEntity<int>
    {
        public long UserID { get; set; }

        public string UserName { get; set; }

        public int OperatorCode { get; set; }

        public virtual ICollection<OverCost> OverCosts { get; set; }

        public virtual ICollection<Draft> Drafts { get; set; }
    }
}