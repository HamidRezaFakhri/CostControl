namespace CostControl.Entity.Models.Base.Interfaces
{
	public interface IBaseEntity
	{
		//Guid? InstanceId { get; }

		Enums.ObjectState State { get; set; }
	}
}