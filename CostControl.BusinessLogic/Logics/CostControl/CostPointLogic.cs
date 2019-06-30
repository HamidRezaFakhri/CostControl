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

	public class CostPointLogic : Base.IGenericLogic<CostControlBusinessEntity.CostPoint>, IDisposable
	{
		private MapperConfiguration CostPointMapperConfig { get; set; }

		private IMapper CostPointIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.CostPoint> Repository;

		public CostPointLogic()
		{
			CostPointMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			CostPointIMapper = CostPointMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.CostPoint>();
		}

		public CostControlBusinessEntity.CostPoint Remove(object id)
		{
			if (id == null)
			{
				return null;
			}

			if (Repository.GetById(id) != null)
			{
				var result = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.CostPoint> Remove(
			Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter)
		{
			if (filter == null)
			{
				return null;
			}

			List<CostControlBusinessEntity.CostPoint> result = null;

			var deleteLst = Repository.Get(CostPointIMapper
								.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.CostPoint>)
					.ForEach(s => result.Add(CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.CostPoint> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
			{
				return null;
			}

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
							  Repository
							  .Remove(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.CostPoint Exists(object primaryKey)
		=> CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.CostPoint> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.CostPoint> Get(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
						   Repository.Get(
							   CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter),
							   CostPointIMapper.Map<Func<IQueryable<CostControlEntity.CostPoint>, IOrderedQueryable<CostControlEntity.CostPoint>>>(orderBy),
							   CostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> GetAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointIMapper.Map<Task<IEnumerable<CostControlEntity.CostPoint>>, Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
						   Repository.GetAsync(
							   CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter),
							   CostPointIMapper.Map<Func<IQueryable<CostControlEntity.CostPoint>, IOrderedQueryable<CostControlEntity.CostPoint>>>(orderBy),
							   CostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.CostPoint GetById<TKey>(TKey id)
		=> id == null ? null : CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.GetById(id));

		public CostControlBusinessEntity.CostPoint GetById<TKey>(TKey id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>>>> includeProperties = null)
		=> id == null ? null : CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>
					   (Repository.GetById(id, CostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.CostPoint> GetByIdAsync<TKey>(TKey id,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(await Repository.GetByIdAsync(id, cancellationToken));

		public async Task<CostControlBusinessEntity.CostPoint> GetByIdAsync<TKey>(TKey id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>
					   (await Repository.GetByIdAsync(id, CostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.CostPoint> GetWithRawSql(string query, params object[] parameters)
		=> CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.CostPoint Add(CostControlBusinessEntity.CostPoint entity)
		{
			if (entity == null)
			{
				return null;
			}

			var result = CostPointIMapper
					.Map<CostControlBusinessEntity.CostPoint>(
						Repository.Add(CostPointIMapper.Map<CostControlEntity.CostPoint>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.CostPoint> AddAsync(CostControlBusinessEntity.CostPoint entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
			{
				return null;
			}

			var CostPoint = CostPointIMapper.Map<CostControlEntity.CostPoint>(entity);

			var result = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Add(CostPoint));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.CostPoint Update(CostControlBusinessEntity.CostPoint entity)
		{
			if (entity == null)
			{
				return null;
			}

			var CostPoint = CostPointIMapper.Map<CostControlEntity.CostPoint>(entity);

			var result = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Update(CostPoint));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.CostPoint> UpdateAsync(CostControlBusinessEntity.CostPoint entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
			{
				return null;
			}

			var CostPoint = CostPointIMapper.Map<CostControlEntity.CostPoint>(entity);

			var result = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Update(CostPoint));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.CostPoint SingleOrDefault(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
		=> CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(
					   Repository.SingleOrDefault(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.CostPoint> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointIMapper.Map<Task<CostControlBusinessEntity.CostPoint>>(
						   Repository.SingleOrDefaultAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.CostPoint FirstOrDefault(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
		=> CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(
						   Repository.FirstOrDefault(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.CostPoint> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointIMapper.Map<Task<CostControlBusinessEntity.CostPoint>>(
						   Repository.FirstOrDefaultAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.CostPoint LastOrDefault(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
		=> CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(
						   Repository.LastOrDefault(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.CostPoint> LastOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await CostPointIMapper.Map<Task<CostControlBusinessEntity.CostPoint>>(
						   Repository.LastOrDefaultAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.CostPoint> AddRange(IEnumerable<CostControlBusinessEntity.CostPoint> entities)
		{
			IEnumerable<CostControlBusinessEntity.CostPoint> result =
			CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
				  Repository.AddRange(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.CostPoint> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await CostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
				  Repository
				  .AddRange(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.CostPoint> RemoveFiltered(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter)
		{
			var result = CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
				Repository.RemoveFiltered(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await CostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
				Repository.RemoveFilteredAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.CostPoint> RemoveRange(IEnumerable<CostControlBusinessEntity.CostPoint> entities)
		{
			var result = CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
					Repository.RemoveRange(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.CostPoint> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await CostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
						Repository
						.Remove(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.CostPoint Exists(params object[] primaryKey)
		=> CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.CostPoint> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await CostPointIMapper.Map<Task<CostControlBusinessEntity.CostPoint>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
		=> Repository.Exists(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
		=> Repository.Count(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					CostPointMapperConfig = null;
					CostPointIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
		=> Repository.Any(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.CostPoint item,
			Expression<Func<CostControlBusinessEntity.CostPoint, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.CostPoint> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>>>> includeProperties = null,
			int? page = null, int? pageSize = null)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<dynamic> GetExternalData()
		{
			var costPoints = new[] {
										new { Code = "0", Name = "Test", CostPointGroupName = "CostPointGroupName"}
									}.ToList();

			Repository
				.GetRawSql("EXEC CostControl.dbo.SP_GetCostPoint")
				.ToList()
				.ForEach(sp => costPoints.Add(new
				{
					Code = (string)sp.Code,
					Name = (string)sp.Name,
					CostPointGroupName = (string)sp.CostPointGroupName
				}));

			costPoints.RemoveAt(0);

			return costPoints.AsEnumerable();
		}

		public void AddExternalData(string key)
		=> Repository.RunRawSql("EXEC CostControl.dbo.SP_AddExternallCostPoint @p0", new[] { key });

		public int Commit()
		{
			var commit = _unitOfWork.Commit();

			if (commit < 0)
			{
				throw new Exception("Commit failed!");
			}

			return commit;
		}

		public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var commit = CommitAsync(cancellationToken);

			if (commit.Result < 0)
			{
				throw new Exception("Commit failed!");
			}

			return await commit;
		}

		~CostPointLogic()
		{
			Dispose(false);
		}
	}
}