using CostControl.Entity.Models.Base;

namespace CostControl.Entity.Models.CostControl
{
    public class IncommingUser : SuperEntity<int>
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public int OperatorCode { get; set; }
    }
}