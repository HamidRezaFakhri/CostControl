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

	public class IntakeRemittanceItemLogic : Base.IGenericLogic<CostControlBusinessEntity.IntakeRemittanceItem>, IDisposable
	{
		private MapperConfiguration IntakeRemittanceItemMapperConfig { get; set; }

		private IMapper IntakeRemittanceItemIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.IntakeRemittanceItem> Repository;

		public IntakeRemittanceItemLogic()
		{
			IntakeRemittanceItemMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			IntakeRemittanceItemIMapper = IntakeRemittanceItemMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.IntakeRemittanceItem>();
		}

		public CostControlBusinessEntity.IntakeRemittanceItem Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> Remove(
			Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.IntakeRemittanceItem> result = null;

			var deleteLst = Repository.Get(IntakeRemittanceItemIMapper
								.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.IntakeRemittanceItem>)
					.ForEach(s => result.Add(IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
							  Repository
							  .Remove(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IntakeRemittanceItem Exists(object primaryKey)
		=> IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> Get(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
						   Repository.Get(
							   IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter),
							   IntakeRemittanceItemIMapper.Map<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlEntity.IntakeRemittanceItem>>>(orderBy),
							   IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> GetAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlEntity.IntakeRemittanceItem>>, Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
						   Repository.GetAsync(
							   IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter),
							   IntakeRemittanceItemIMapper.Map<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlEntity.IntakeRemittanceItem>>>(orderBy),
							   IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.IntakeRemittanceItem GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null)
		=> id == null ? null : IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>
					   (Repository.GetById(id, IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>
					   (await Repository.GetByIdAsync(id, IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> GetWithRawSql(string query, params object[] parameters)
		=> IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.IntakeRemittanceItem Add(CostControlBusinessEntity.IntakeRemittanceItem entity)
		{
			if (entity == null)
				return null;

			var result = IntakeRemittanceItemIMapper
					.Map<CostControlBusinessEntity.IntakeRemittanceItem>(
						Repository.Add(IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> AddAsync(CostControlBusinessEntity.IntakeRemittanceItem entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var IntakeRemittanceItem = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem>(entity);

			var result = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Add(IntakeRemittanceItem));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IntakeRemittanceItem Update(CostControlBusinessEntity.IntakeRemittanceItem entity)
		{
			if (entity == null)
				return null;

			var IntakeRemittanceItem = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem>(entity);

			var result = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Update(IntakeRemittanceItem));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> UpdateAsync(CostControlBusinessEntity.IntakeRemittanceItem entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var IntakeRemittanceItem = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem>(entity);

			var result = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Update(IntakeRemittanceItem));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.IntakeRemittanceItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
		=> IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(
					   Repository.SingleOrDefault(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IntakeRemittanceItemIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItem>>(
						   Repository.SingleOrDefaultAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.IntakeRemittanceItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
		=> IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(
						   Repository.FirstOrDefault(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IntakeRemittanceItemIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItem>>(
						   Repository.FirstOrDefaultAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> AddRange(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities)
		{
			IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> result =
			IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
				  Repository.AddRange(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
				  Repository
				  .AddRange(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter)
		{
			var result = IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
				Repository.RemoveFiltered(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
				Repository.RemoveFilteredAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> RemoveRange(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities)
		{
			var result = IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
					Repository.RemoveRange(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
						Repository
						.Remove(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IntakeRemittanceItem Exists(params object[] primaryKey)
		=> IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.IntakeRemittanceItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await IntakeRemittanceItemIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
		=> Repository.Exists(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
		=> Repository.Count(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					IntakeRemittanceItemMapperConfig = null;
					IntakeRemittanceItemIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
		=> Repository.Any(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.IntakeRemittanceItem item,
			Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
			int? page = null, int? pageSize = null)
		{
			throw new NotImplementedException();
		}

		public int Commit()
		{
			var commit = _unitOfWork.Commit();

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

		~IntakeRemittanceItemLogic()
		{
			Dispose(false);
		}
	}
}