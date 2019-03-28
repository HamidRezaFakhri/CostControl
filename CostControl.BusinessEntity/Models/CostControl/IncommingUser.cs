namespace CostControl.BusinessEntity.Models.CostControl
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BusinessEntity.Validations;

    public class IncommingUser : BaseValidating, Base.Interfaces.IEntity<int>
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public int OperatorCode { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}