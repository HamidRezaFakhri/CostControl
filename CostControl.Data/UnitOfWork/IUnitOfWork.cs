namespace CostControl.Data.UnitOfWork
{
	using System;
	using System.Data;
	using System.Threading;
	using System.Threading.Tasks;

	public interface IUnitOfWork : IDisposable
	{
		//IRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity, new();

		/// <summary>
		/// Commit and save all changes
		/// </summary>
		int Commit();

		/// <summary>
		/// Commit and save all changes async
		/// </summary>
		Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Discards all changes that has not been commited
		/// </summary>
		void RollBack();

		/// <summary>
		/// Discards all changes that has not been commited async
		/// </summary>
		Task RollBackAsync(CancellationToken cancellationToken = default(CancellationToken));

		int ExecuteSqlCommand(string commandText, params object[] parameters);

		Task<int> ExecuteSqlCommandAsync(string commandText,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters);

		//bool IsInTransaction { get; }

		void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
	}
}