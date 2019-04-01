namespace CostControl.BusinessEntity.Validations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseValidating : IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
        
        public bool IsNullOrEmpty(string property)
        {
            return string.IsNullOrEmpty(property);
        }
        
        public IEnumerable<ValidationResult> Validate()
        {
            var validationErrors = new List<ValidationResult>();
            
            var ctx = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, ctx, validationErrors, true);
            return validationErrors;
        }
    }
}