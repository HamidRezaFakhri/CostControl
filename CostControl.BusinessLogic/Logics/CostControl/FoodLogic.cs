using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using CostControl.BusinessLogic.Logics.Base;
using CostControl.BusinessLogic.Mapper;
using CostControl.Data.DAL;
using CostControl.Data.Repository;
using CostControl.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore.Query;
using CostControlBusinessEntity = CostControl.BusinessEntity.Models.CostControl;
using CostControlEntity = CostControl.Entity.Models.CostControl;

namespace CostControl.BusinessLogic.Logics.CostControl
{
	public class FoodLogic : IGenericLogic<CostControlBusinessEntity.Food>, IDisposable
	{
		private MapperConfiguration FoodMapperConfig { get; set; }

		private IMapper FoodIMapper { get; set; }

		private readonly UnitOfWork _unitOfWork;

		protected IRepository<CostControlEntity.Food> Repository;

		public FoodLogic()
		{
			FoodMapperConfig = new AutoMapperConfiguration().Configure();
			FoodIMapper = FoodMapperConfig.CreateMapper();
			_unitOfWork = new UnitOfWork(new CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.Food>();
		}

		public CostControlBusinessEntity.Food Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.Food> Remove(
			Expression<Func<CostControlBusinessEntity.Food, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.Food> result = null;

			var deleteLst = Repository.Get(FoodIMapper
								.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.Food>)
					.ForEach(s => result.Add(FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.Food> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Food>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.Food, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(
							  Repository
							  .Remove(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Food Exists(object primaryKey)
		=> FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Food> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> FoodIMapper.Map<CostControlBusinessEntity.Food>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.Food> Get(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Food>, IOrderedQueryable<CostControlBusinessEntity.Food>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Food>, IIncludableQueryable<CostControlBusinessEntity.Food, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(
						   Repository.Get(
							   FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter),
							   FoodIMapper.Map<Func<IQueryable<CostControlEntity.Food>, IOrderedQueryable<CostControlEntity.Food>>>(orderBy),
							   FoodIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Food>, IIncludableQueryable<CostControlEntity.Food, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.Food>> GetAsync(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Food>, IOrderedQueryable<CostControlBusinessEntity.Food>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Food>, IIncludableQueryable<CostControlBusinessEntity.Food, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await FoodIMapper.Map<Task<IEnumerable<CostControlEntity.Food>>, Task<IEnumerable<CostControlBusinessEntity.Food>>>(
						   Repository.GetAsync(
							   FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter),
							   FoodIMapper.Map<Func<IQueryable<CostControlEntity.Food>, IOrderedQueryable<CostControlEntity.Food>>>(orderBy),
							   FoodIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Food>, IIncludableQueryable<CostControlEntity.Food, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.Food GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Food>, IIncludableQueryable<CostControlBusinessEntity.Food, object>>>> includeProperties = null)
		=> id == null ? null : FoodIMapper.Map<CostControlBusinessEntity.Food>
					   (Repository.GetById(id, FoodIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Food>, IIncludableQueryable<CostControlEntity.Food, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.Food> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Food>, IIncludableQueryable<CostControlBusinessEntity.Food, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : FoodIMapper.Map<CostControlBusinessEntity.Food>
					   (await Repository.GetByIdAsync(id, FoodIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Food>, IIncludableQueryable<CostControlEntity.Food, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Food> GetWithRawSql(string query, params object[] parameters)
		=> FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.Food>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.Food Add(CostControlBusinessEntity.Food entity)
		{
			if (entity == null)
				return null;

			var result = FoodIMapper
					.Map<CostControlBusinessEntity.Food>(
						Repository.Add(FoodIMapper.Map<CostControlEntity.Food>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Food> AddAsync(CostControlBusinessEntity.Food entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Food = FoodIMapper.Map<CostControlEntity.Food>(entity);

			var result = FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Add(Food));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Food Update(CostControlBusinessEntity.Food entity)
		{
			if (entity == null)
				return null;

			var Food = FoodIMapper.Map<CostControlEntity.Food>(entity);

			var result = FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Update(Food));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Food> UpdateAsync(CostControlBusinessEntity.Food entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Food = FoodIMapper.Map<CostControlEntity.Food>(entity);

			var result = FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Update(Food));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.Food SingleOrDefault(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null)
		=> FoodIMapper.Map<CostControlBusinessEntity.Food>(
					   Repository.SingleOrDefault(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Food> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await FoodIMapper.Map<Task<CostControlBusinessEntity.Food>>(
						   Repository.SingleOrDefaultAsync(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.Food FirstOrDefault(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null)
		=> FoodIMapper.Map<CostControlBusinessEntity.Food>(
						   Repository.FirstOrDefault(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Food> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await FoodIMapper.Map<Task<CostControlBusinessEntity.Food>>(
						   Repository.FirstOrDefaultAsync(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Food> AddRange(IEnumerable<CostControlBusinessEntity.Food> entities)
		{
			IEnumerable<CostControlBusinessEntity.Food> result =
			FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(
				  Repository.AddRange(FoodIMapper.Map<IEnumerable<CostControlEntity.Food>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Food>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Food> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await FoodIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Food>>>(
				  Repository
				  .AddRange(FoodIMapper.Map<IEnumerable<CostControlEntity.Food>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Food> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Food, bool>> filter)
		{
			var result = FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(
				Repository.RemoveFiltered(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Food>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.Food, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await FoodIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Food>>>(
				Repository.RemoveFilteredAsync(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Food> RemoveRange(IEnumerable<CostControlBusinessEntity.Food> entities)
		{
			var result = FoodIMapper.Map<IEnumerable<CostControlBusinessEntity.Food>>(
					Repository.RemoveRange(FoodIMapper.Map<IEnumerable<CostControlEntity.Food>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Food>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.Food> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await FoodIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Food>>>(
						Repository
						.Remove(FoodIMapper.Map<IEnumerable<CostControlEntity.Food>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Food Exists(params object[] primaryKey)
		=> FoodIMapper.Map<CostControlBusinessEntity.Food>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Food> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await FoodIMapper.Map<Task<CostControlBusinessEntity.Food>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null)
		=> Repository.Exists(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null)
		=> Repository.Count(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					FoodMapperConfig = null;
					FoodIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null)
		=> Repository.Any(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Food, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(FoodIMapper.Map<Expression<Func<CostControlEntity.Food, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.Food item,
			Expression<Func<CostControlBusinessEntity.Food, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.Food> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.Food>, IOrderedQueryable<CostControlBusinessEntity.Food>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Food>, IIncludableQueryable<CostControlBusinessEntity.Food, object>>>> includeProperties = null,
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

		~FoodLogic()
		{
			Dispose(false);
		}
	}
}