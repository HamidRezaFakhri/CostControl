namespace CostControl.BusinessEntity.Models.CostControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class DataImport : BaseValidating, Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; } = Base.Enums.ObjectState.Active;

        public DateTime ImportTime { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}