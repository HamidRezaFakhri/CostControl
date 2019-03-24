namespace CostControl.BusinessLogic.Logics.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using AutoMapper.Extensions.ExpressionMapping;
	using Microsoft.EntityFrameworkCore.Query;
	using CostControlBusinessEntity = BusinessEntity.Models.CostControl;
	using CostControlEntity = Entity.Models.CostControl;

	public class IntakeRemittanceLogic : Base.IGenericLogic<CostControlBusinessEntity.IntakeRemittance>, IDisposable
	{
		private MapperConfiguration IntakeRemittanceMapperConfig { get; set; }

		private IMapper IntakeRemittanceIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.IntakeRemittance> Repository;

		public IntakeRemittanceLogic()
		{
			IntakeRemittanceMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			IntakeRemittanceIMapper = IntakeRemittanceMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.IntakeRemittance>();
		}

		public CostControlBusinessEntity.IntakeRemittance Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> Remove(
			Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.IntakeRemittance> result = null;

			var deleteLst = Repository.Get(IntakeRemittanceIMapper
								.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.IntakeRemittance>)
					.ForEach(s => result.Add(IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.IntakeRemittance> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
							  Repository
							  .Remove(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IntakeRemittance Exists(object primaryKey)
		=> IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.IntakeRemittance> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> Get(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
						   Repository.Get(
							   IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter),
							   IntakeRemittanceIMapper.Map<Func<IQueryable<CostControlEntity.IntakeRemittance>, IOrderedQueryable<CostControlEntity.IntakeRemittance>>>(orderBy),
							   IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> GetAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlEntity.IntakeRemittance>>, Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
						   Repository.GetAsync(
							   IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter),
							   IntakeRemittanceIMapper.Map<Func<IQueryable<CostControlEntity.IntakeRemittance>, IOrderedQueryable<CostControlEntity.IntakeRemittance>>>(orderBy),
							   IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.IntakeRemittance GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null)
		=> id == null ? null : IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>
					   (Repository.GetById(id, IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.IntakeRemittance> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>
					   (await Repository.GetByIdAsync(id, IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> GetWithRawSql(string query, params object[] parameters)
		=> IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.IntakeRemittance Add(CostControlBusinessEntity.IntakeRemittance entity)
		{
			if (entity == null)
				return null;

			var result = IntakeRemittanceIMapper
					.Map<CostControlBusinessEntity.IntakeRemittance>(
						Repository.Add(IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.IntakeRemittance> AddAsync(CostControlBusinessEntity.IntakeRemittance entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var IntakeRemittance = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance>(entity);

			var result = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Add(IntakeRemittance));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IntakeRemittance Update(CostControlBusinessEntity.IntakeRemittance entity)
		{
			if (entity == null)
				return null;

			var IntakeRemittance = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance>(entity);

			var result = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Update(IntakeRemittance));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.IntakeRemittance> UpdateAsync(CostControlBusinessEntity.IntakeRemittance entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var IntakeRemittance = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance>(entity);

			var result = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Update(IntakeRemittance));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.IntakeRemittance SingleOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
		=> IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(
					   Repository.SingleOrDefault(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.IntakeRemittance> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IntakeRemittanceIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittance>>(
						   Repository.SingleOrDefaultAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.IntakeRemittance FirstOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
		=> IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(
						   Repository.FirstOrDefault(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.IntakeRemittance> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IntakeRemittanceIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittance>>(
						   Repository.FirstOrDefaultAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> AddRange(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities)
		{
			IEnumerable<CostControlBusinessEntity.IntakeRemittance> result =
			IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
				  Repository.AddRange(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
				  Repository
				  .AddRange(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter)
		{
			var result = IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
				Repository.RemoveFiltered(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
				Repository.RemoveFilteredAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> RemoveRange(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities)
		{
			var result = IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
					Repository.RemoveRange(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
						Repository
						.Remove(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IntakeRemittance Exists(params object[] primaryKey)
		=> IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.IntakeRemittance> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await IntakeRemittanceIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittance>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
		=> Repository.Exists(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
		=> Repository.Count(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					IntakeRemittanceMapperConfig = null;
					IntakeRemittanceIMapper = null;
					Repository = null;
					_unitOfWork?.Dispose();
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public bool Any(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
		=> Repository.Any(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.IntakeRemittance item,
			Expression<Func<CostControlBusinessEntity.IntakeRemittance, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittance> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
			int? page = null, int? pageSize = null)
		{
			throw new NotImplementedException();
		}

		public int Commit()
		{
			var commit = Commit();

			if (commit < 0)
				throw new Exception("Commit failed!");

			return commit;
		}

		public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var commit = CommitAsync(cancellationToken);

			if (commit.Result < 0)
				throw new Exception("Commit failed!");

			return await commit;
		}

		~IntakeRemittanceLogic()
		{
			Dispose(false);
		}
	}
}