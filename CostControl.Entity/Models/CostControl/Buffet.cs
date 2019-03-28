namespace CostControl.Entity.Models.CostControl
{
    using Entity.Models.Base;

    public class Buffet : SuperEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }
    }
}