using CostControl.Entity.Models.Base;
using System;

namespace CostControl.Entity.Models.CostControl
{
    public class DataImport : SuperEntity<long>
    {
        public DateTime ImportTime { get; set; } = DateTime.Now;
    }
}