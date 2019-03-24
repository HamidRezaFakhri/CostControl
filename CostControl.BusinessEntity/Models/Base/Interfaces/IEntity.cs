namespace CostControl.BusinessEntity.Models.Base.Interfaces
{
	public interface IEntity<T> : IBaseEntity
	{
		T Id { get; set; }
	}
}