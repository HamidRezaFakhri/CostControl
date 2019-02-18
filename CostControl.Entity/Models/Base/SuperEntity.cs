using CostControl.Entity.Models.Base.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostControl.Entity.Models.Base
{
    public class SuperEntity<T> : BaseEntity, ISuperEntity<T>
    {
        [Column(Order = 0)]
        public virtual T Id { get; set; }
    }
}