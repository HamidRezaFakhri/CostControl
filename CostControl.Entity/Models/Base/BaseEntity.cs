namespace CostControl.Entity.Models.Base
{
	using System;

	[Serializable]
	public abstract class BaseEntity : Interfaces.IBaseEntity
	{
		//public virtual Guid? InstanceId { get; private set; }

		public virtual Enums.ObjectState State { get; set; } = Enums.ObjectState.Active;
	}
}