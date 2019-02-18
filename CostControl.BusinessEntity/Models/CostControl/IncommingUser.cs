namespace CostControl.BusinessEntity.Models.CostControl
{
    public class IncommingUser : Base.Interfaces.IEntity<int>//, IValidatableObject
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public int OperatorCode { get; set; }
    }
}