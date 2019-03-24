namespace CostControl.Entity.Models.Base.Interfaces
{
	//public interface ISuperEntity<out T> : IBaseEntity
	public interface ISuperEntity<T> : IBaseEntity
	{
		T Id { get; set; }
	}
}