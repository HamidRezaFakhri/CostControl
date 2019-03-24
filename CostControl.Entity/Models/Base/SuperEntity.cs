namespace CostControl.Entity.Models.Base
{
	using Entity.Models.Base.Interfaces;

	public class SuperEntity<T> : BaseEntity, ISuperEntity<T>
	{
		//[Column(Order = 0)]
		public virtual T Id { get; set; }
	}
}