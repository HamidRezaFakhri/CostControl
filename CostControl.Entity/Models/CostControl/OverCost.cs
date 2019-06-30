namespace CostControl.Entity.Models.CostControl
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Entity.Models.Base;

    public class OverCost : SuperEntity<long>
    {
        public long SaleCostPointId { get; set; }

        public virtual SaleCostPoint SaleCostPoint { get; set; }

        public byte OverCostTypeId { get; set; }

        public virtual OverCostType OverCostType { get; set; }

        //[BindNever]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //[DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18,2)")]
        //[Compare()]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        //    ErrorMessage = "Email is not valid.")]
        //[ScaffoldColumn(false)]
        public decimal Price { get; set; }

        [StringLength(500, MinimumLength = 10,
                ErrorMessage = "Please enter a vlid description, it must be greater than {2} characters and less than {1} characters.")]
        public string Description { get; set; }
        
        public DateTime RegisteredDate { get; set; }
        
        public int RegisteredUserId { get; set; }

        public virtual IncommingUser RegisteredUser { get; set; }
    }
}