using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.Base.Enums
{
    public enum ObjectState
    {
        [Display(Name = "Active")]
        [Description("Object is active")]
        Active = 1,

        [Display(Name = "Passive")]
        [Description("Object is not active")]
        Passive,

        [Display(Name = "Deleted")]
        [Description("Object is deleted")]
        Deleted,

        [Display(Name = "Archived")]
        [Description("Object is archived")]
        Archived
    }
}