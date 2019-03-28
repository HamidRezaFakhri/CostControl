namespace CostControl.Entity.Models.CostControl
{
    using System;
    using Entity.Models.Base;

    public class DataImport : SuperEntity<long>
    {
        public DateTime ImportTime { get; set; }
    }
}