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

	public class DraftItemLogic : Base.IGenericLogic<CostControlBusinessEntity.DraftItem>, IDisposable
	{
		private MapperConfiguration DraftItemMapperConfig { get; set; }

		private IMapper DraftItemIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.DraftItem> Repository;

		public DraftItemLogic()
		{
			DraftItemMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			DraftItemIMapper = DraftItemMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.DraftItem>();
		}

		public CostControlBusinessEntity.DraftItem Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.DraftItem> Remove(
			Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.DraftItem> result = null;

			var deleteLst = Repository.Get(DraftItemIMapper
								.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.DraftItem>)
					.ForEach(s => result.Add(DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.DraftItem> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
							  Repository
							  .Remove(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.DraftItem Exists(object primaryKey)
		=> DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.DraftItem> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.DraftItem> Get(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IIncludableQueryable<CostControlBusinessEntity.DraftItem, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
						   Repository.Get(
							   DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter),
							   DraftItemIMapper.Map<Func<IQueryable<CostControlEntity.DraftItem>, IOrderedQueryable<CostControlEntity.DraftItem>>>(orderBy),
							   DraftItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DraftItem>, IIncludableQueryable<CostControlEntity.DraftItem, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> GetAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IIncludableQueryable<CostControlBusinessEntity.DraftItem, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await DraftItemIMapper.Map<Task<IEnumerable<CostControlEntity.DraftItem>>, Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
						   Repository.GetAsync(
							   DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter),
							   DraftItemIMapper.Map<Func<IQueryable<CostControlEntity.DraftItem>, IOrderedQueryable<CostControlEntity.DraftItem>>>(orderBy),
							   DraftItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DraftItem>, IIncludableQueryable<CostControlEntity.DraftItem, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.DraftItem GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IIncludableQueryable<CostControlBusinessEntity.DraftItem, object>>>> includeProperties = null)
		=> id == null ? null : DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>
					   (Repository.GetById(id, DraftItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DraftItem>, IIncludableQueryable<CostControlEntity.DraftItem, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.DraftItem> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IIncludableQueryable<CostControlBusinessEntity.DraftItem, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>
					   (await Repository.GetByIdAsync(id, DraftItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DraftItem>, IIncludableQueryable<CostControlEntity.DraftItem, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.DraftItem> GetWithRawSql(string query, params object[] parameters)
		=> DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.DraftItem Add(CostControlBusinessEntity.DraftItem entity)
		{
			if (entity == null)
				return null;

			var result = DraftItemIMapper
					.Map<CostControlBusinessEntity.DraftItem>(
						Repository.Add(DraftItemIMapper.Map<CostControlEntity.DraftItem>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.DraftItem> AddAsync(CostControlBusinessEntity.DraftItem entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var DraftItem = DraftItemIMapper.Map<CostControlEntity.DraftItem>(entity);

			var result = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Add(DraftItem));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.DraftItem Update(CostControlBusinessEntity.DraftItem entity)
		{
			if (entity == null)
				return null;

			var DraftItem = DraftItemIMapper.Map<CostControlEntity.DraftItem>(entity);

			var result = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Update(DraftItem));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.DraftItem> UpdateAsync(CostControlBusinessEntity.DraftItem entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var DraftItem = DraftItemIMapper.Map<CostControlEntity.DraftItem>(entity);

			var result = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Update(DraftItem));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.DraftItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
		=> DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(
					   Repository.SingleOrDefault(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.DraftItem> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await DraftItemIMapper.Map<Task<CostControlBusinessEntity.DraftItem>>(
						   Repository.SingleOrDefaultAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.DraftItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
		=> DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(
						   Repository.FirstOrDefault(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.DraftItem> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await DraftItemIMapper.Map<Task<CostControlBusinessEntity.DraftItem>>(
						   Repository.FirstOrDefaultAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.DraftItem> AddRange(IEnumerable<CostControlBusinessEntity.DraftItem> entities)
		{
			IEnumerable<CostControlBusinessEntity.DraftItem> result =
			DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
				  Repository.AddRange(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.DraftItem> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await DraftItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
				  Repository
				  .AddRange(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.DraftItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter)
		{
			var result = DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
				Repository.RemoveFiltered(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await DraftItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
				Repository.RemoveFilteredAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.DraftItem> RemoveRange(IEnumerable<CostControlBusinessEntity.DraftItem> entities)
		{
			var result = DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
					Repository.RemoveRange(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.DraftItem> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await DraftItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
						Repository
						.Remove(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.DraftItem Exists(params object[] primaryKey)
		=> DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.DraftItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await DraftItemIMapper.Map<Task<CostControlBusinessEntity.DraftItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
		=> Repository.Exists(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
		=> Repository.Count(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					DraftItemMapperConfig = null;
					DraftItemIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
		=> Repository.Any(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.DraftItem item,
			Expression<Func<CostControlBusinessEntity.DraftItem, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.DraftItem> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IIncludableQueryable<CostControlBusinessEntity.DraftItem, object>>>> includeProperties = null,
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

		~DraftItemLogic()
		{
			Dispose(false);
		}
	}
}