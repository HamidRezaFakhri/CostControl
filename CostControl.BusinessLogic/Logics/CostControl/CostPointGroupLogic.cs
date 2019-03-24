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

	public class CostPointGroupLogic : Base.IGenericLogic<CostControlBusinessEntity.CostPointGroup>, IDisposable
	{
		private MapperConfiguration CostPointGroupMapperConfig { get; set; }

		private IMapper CostPointGroupIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.CostPointGroup> Repository;

		public CostPointGroupLogic()
		{
			CostPointGroupMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			CostPointGroupIMapper = CostPointGroupMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.CostPointGroup>();
		}

		public CostControlBusinessEntity.CostPointGroup Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> Remove(
			Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.CostPointGroup> result = null;

			var deleteLst = Repository.Get(CostPointGroupIMapper
								.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.CostPointGroup>)
					.ForEach(s => result.Add(CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.CostPointGroup> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
							  Repository
							  .Remove(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.CostPointGroup Exists(object primaryKey)
		=> CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.CostPointGroup> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> Get(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
						   Repository.Get(
							   CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter),
							   CostPointGroupIMapper.Map<Func<IQueryable<CostControlEntity.CostPointGroup>, IOrderedQueryable<CostControlEntity.CostPointGroup>>>(orderBy),
							   CostPointGroupIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> GetAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlEntity.CostPointGroup>>, Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
						   Repository.GetAsync(
							   CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter),
							   CostPointGroupIMapper.Map<Func<IQueryable<CostControlEntity.CostPointGroup>, IOrderedQueryable<CostControlEntity.CostPointGroup>>>(orderBy),
							   CostPointGroupIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.CostPointGroup GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>>>> includeProperties = null)
		=> id == null ? null : CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>
					   (Repository.GetById(id, CostPointGroupIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.CostPointGroup> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>
					   (await Repository.GetByIdAsync(id, CostPointGroupIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> GetWithRawSql(string query, params object[] parameters)
		=> CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.CostPointGroup Add(CostControlBusinessEntity.CostPointGroup entity)
		{
			if (entity == null)
				return null;

			var result = CostPointGroupIMapper
					.Map<CostControlBusinessEntity.CostPointGroup>(
						Repository.Add(CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.CostPointGroup> AddAsync(CostControlBusinessEntity.CostPointGroup entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var CostPointGroup = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup>(entity);

			var result = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Add(CostPointGroup));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.CostPointGroup Update(CostControlBusinessEntity.CostPointGroup entity)
		{
			if (entity == null)
				return null;

			var CostPointGroup = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup>(entity);

			var result = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Update(CostPointGroup));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.CostPointGroup> UpdateAsync(CostControlBusinessEntity.CostPointGroup entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var CostPointGroup = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup>(entity);

			var result = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Update(CostPointGroup));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.CostPointGroup SingleOrDefault(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
		=> CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(
					   Repository.SingleOrDefault(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.CostPointGroup> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointGroupIMapper.Map<Task<CostControlBusinessEntity.CostPointGroup>>(
						   Repository.SingleOrDefaultAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.CostPointGroup FirstOrDefault(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
		=> CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(
						   Repository.FirstOrDefault(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.CostPointGroup> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointGroupIMapper.Map<Task<CostControlBusinessEntity.CostPointGroup>>(
						   Repository.FirstOrDefaultAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> AddRange(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities)
		{
			IEnumerable<CostControlBusinessEntity.CostPointGroup> result =
			CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
				  Repository.AddRange(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
				  Repository
				  .AddRange(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> RemoveFiltered(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter)
		{
			var result = CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
				Repository.RemoveFiltered(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
				Repository.RemoveFilteredAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> RemoveRange(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities)
		{
			var result = CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
					Repository.RemoveRange(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.CostPointGroup> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
						Repository
						.Remove(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.CostPointGroup Exists(params object[] primaryKey)
		=> CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.CostPointGroup> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await CostPointGroupIMapper.Map<Task<CostControlBusinessEntity.CostPointGroup>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
		=> Repository.Exists(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
		=> Repository.Count(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					CostPointGroupMapperConfig = null;
					CostPointGroupIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
		=> Repository.Any(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.CostPointGroup item,
			Expression<Func<CostControlBusinessEntity.CostPointGroup, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.CostPointGroup> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>>>> includeProperties = null,
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

		~CostPointGroupLogic()
		{
			Dispose(false);
		}
	}
}