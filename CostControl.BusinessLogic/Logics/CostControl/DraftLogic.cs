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

	public class DraftLogic : Base.IGenericLogic<CostControlBusinessEntity.Draft>, IDisposable
	{
		private MapperConfiguration DraftMapperConfig { get; set; }

		private IMapper DraftIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.Draft> Repository;

		public DraftLogic()
		{
			DraftMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			DraftIMapper = DraftMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.Draft>();
		}

		public CostControlBusinessEntity.Draft Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.Draft> Remove(
			Expression<Func<CostControlBusinessEntity.Draft, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.Draft> result = null;

			var deleteLst = Repository.Get(DraftIMapper
								.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.Draft>)
					.ForEach(s => result.Add(DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.Draft> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Draft>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.Draft, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(
							  Repository
							  .Remove(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Draft Exists(object primaryKey)
		=> DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Draft> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> DraftIMapper.Map<CostControlBusinessEntity.Draft>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.Draft> Get(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Draft>, IOrderedQueryable<CostControlBusinessEntity.Draft>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Draft>, IIncludableQueryable<CostControlBusinessEntity.Draft, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(
						   Repository.Get(
							   DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter),
							   DraftIMapper.Map<Func<IQueryable<CostControlEntity.Draft>, IOrderedQueryable<CostControlEntity.Draft>>>(orderBy),
							   DraftIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Draft>, IIncludableQueryable<CostControlEntity.Draft, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.Draft>> GetAsync(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Draft>, IOrderedQueryable<CostControlBusinessEntity.Draft>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Draft>, IIncludableQueryable<CostControlBusinessEntity.Draft, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await DraftIMapper.Map<Task<IEnumerable<CostControlEntity.Draft>>, Task<IEnumerable<CostControlBusinessEntity.Draft>>>(
						   Repository.GetAsync(
							   DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter),
							   DraftIMapper.Map<Func<IQueryable<CostControlEntity.Draft>, IOrderedQueryable<CostControlEntity.Draft>>>(orderBy),
							   DraftIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Draft>, IIncludableQueryable<CostControlEntity.Draft, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.Draft GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Draft>, IIncludableQueryable<CostControlBusinessEntity.Draft, object>>>> includeProperties = null)
		=> id == null ? null : DraftIMapper.Map<CostControlBusinessEntity.Draft>
					   (Repository.GetById(id, DraftIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Draft>, IIncludableQueryable<CostControlEntity.Draft, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.Draft> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Draft>, IIncludableQueryable<CostControlBusinessEntity.Draft, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : DraftIMapper.Map<CostControlBusinessEntity.Draft>
					   (await Repository.GetByIdAsync(id, DraftIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Draft>, IIncludableQueryable<CostControlEntity.Draft, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Draft> GetWithRawSql(string query, params object[] parameters)
		=> DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.Draft>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.Draft Add(CostControlBusinessEntity.Draft entity)
		{
			if (entity == null)
				return null;

			var result = DraftIMapper
					.Map<CostControlBusinessEntity.Draft>(
						Repository.Add(DraftIMapper.Map<CostControlEntity.Draft>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Draft> AddAsync(CostControlBusinessEntity.Draft entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Draft = DraftIMapper.Map<CostControlEntity.Draft>(entity);

			var result = DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Add(Draft));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Draft Update(CostControlBusinessEntity.Draft entity)
		{
			if (entity == null)
				return null;

			var Draft = DraftIMapper.Map<CostControlEntity.Draft>(entity);

			var result = DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Update(Draft));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Draft> UpdateAsync(CostControlBusinessEntity.Draft entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Draft = DraftIMapper.Map<CostControlEntity.Draft>(entity);

			var result = DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Update(Draft));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.Draft SingleOrDefault(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null)
		=> DraftIMapper.Map<CostControlBusinessEntity.Draft>(
					   Repository.SingleOrDefault(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Draft> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await DraftIMapper.Map<Task<CostControlBusinessEntity.Draft>>(
						   Repository.SingleOrDefaultAsync(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.Draft FirstOrDefault(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null)
		=> DraftIMapper.Map<CostControlBusinessEntity.Draft>(
						   Repository.FirstOrDefault(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Draft> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await DraftIMapper.Map<Task<CostControlBusinessEntity.Draft>>(
						   Repository.FirstOrDefaultAsync(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Draft> AddRange(IEnumerable<CostControlBusinessEntity.Draft> entities)
		{
			IEnumerable<CostControlBusinessEntity.Draft> result =
			DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(
				  Repository.AddRange(DraftIMapper.Map<IEnumerable<CostControlEntity.Draft>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Draft>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Draft> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await DraftIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Draft>>>(
				  Repository
				  .AddRange(DraftIMapper.Map<IEnumerable<CostControlEntity.Draft>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Draft> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter)
		{
			var result = DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(
				Repository.RemoveFiltered(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Draft>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.Draft, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await DraftIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Draft>>>(
				Repository.RemoveFilteredAsync(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Draft> RemoveRange(IEnumerable<CostControlBusinessEntity.Draft> entities)
		{
			var result = DraftIMapper.Map<IEnumerable<CostControlBusinessEntity.Draft>>(
					Repository.RemoveRange(DraftIMapper.Map<IEnumerable<CostControlEntity.Draft>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Draft>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.Draft> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await DraftIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Draft>>>(
						Repository
						.Remove(DraftIMapper.Map<IEnumerable<CostControlEntity.Draft>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Draft Exists(params object[] primaryKey)
		=> DraftIMapper.Map<CostControlBusinessEntity.Draft>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Draft> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await DraftIMapper.Map<Task<CostControlBusinessEntity.Draft>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null)
		=> Repository.Exists(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null)
		=> Repository.Count(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					DraftMapperConfig = null;
					DraftIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null)
		=> Repository.Any(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Draft, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(DraftIMapper.Map<Expression<Func<CostControlEntity.Draft, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.Draft item,
			Expression<Func<CostControlBusinessEntity.Draft, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.Draft> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.Draft>, IOrderedQueryable<CostControlBusinessEntity.Draft>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Draft>, IIncludableQueryable<CostControlBusinessEntity.Draft, object>>>> includeProperties = null,
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

		~DraftLogic()
		{
			Dispose(false);
		}
	}
}