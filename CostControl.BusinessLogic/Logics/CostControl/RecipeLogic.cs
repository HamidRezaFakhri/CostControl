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

	public class RecipeLogic : Base.IGenericLogic<CostControlBusinessEntity.Recipe>, IDisposable
	{
		private MapperConfiguration RecipeMapperConfig { get; set; }

		private IMapper RecipeIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.Recipe> Repository;

		public RecipeLogic()
		{
			RecipeMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			RecipeIMapper = RecipeMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.Recipe>();
		}

		public CostControlBusinessEntity.Recipe Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.Recipe> Remove(
			Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.Recipe> result = null;

			var deleteLst = Repository.Get(RecipeIMapper
								.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.Recipe>)
					.ForEach(s => result.Add(RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.Recipe> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
							  Repository
							  .Remove(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Recipe Exists(object primaryKey)
		=> RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Recipe> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.Recipe> Get(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
						   Repository.Get(
							   RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter),
							   RecipeIMapper.Map<Func<IQueryable<CostControlEntity.Recipe>, IOrderedQueryable<CostControlEntity.Recipe>>>(orderBy),
							   RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> GetAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await RecipeIMapper.Map<Task<IEnumerable<CostControlEntity.Recipe>>, Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
						   Repository.GetAsync(
							   RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter),
							   RecipeIMapper.Map<Func<IQueryable<CostControlEntity.Recipe>, IOrderedQueryable<CostControlEntity.Recipe>>>(orderBy),
							   RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.Recipe GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null)
		=> id == null ? null : RecipeIMapper.Map<CostControlBusinessEntity.Recipe>
					   (Repository.GetById(id, RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.Recipe> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : RecipeIMapper.Map<CostControlBusinessEntity.Recipe>
					   (await Repository.GetByIdAsync(id, RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Recipe> GetWithRawSql(string query, params object[] parameters)
		=> RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.Recipe Add(CostControlBusinessEntity.Recipe entity)
		{
			if (entity == null)
				return null;

			var result = RecipeIMapper
					.Map<CostControlBusinessEntity.Recipe>(
						Repository.Add(RecipeIMapper.Map<CostControlEntity.Recipe>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Recipe> AddAsync(CostControlBusinessEntity.Recipe entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Recipe = RecipeIMapper.Map<CostControlEntity.Recipe>(entity);

			var result = RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Add(Recipe));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Recipe Update(CostControlBusinessEntity.Recipe entity)
		{
			if (entity == null)
				return null;

			var Recipe = RecipeIMapper.Map<CostControlEntity.Recipe>(entity);

			var result = RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Update(Recipe));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Recipe> UpdateAsync(CostControlBusinessEntity.Recipe entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Recipe = RecipeIMapper.Map<CostControlEntity.Recipe>(entity);

			var result = RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Update(Recipe));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.Recipe SingleOrDefault(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
		=> RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(
					   Repository.SingleOrDefault(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Recipe> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await RecipeIMapper.Map<Task<CostControlBusinessEntity.Recipe>>(
						   Repository.SingleOrDefaultAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.Recipe FirstOrDefault(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
		=> RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(
						   Repository.FirstOrDefault(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Recipe> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await RecipeIMapper.Map<Task<CostControlBusinessEntity.Recipe>>(
						   Repository.FirstOrDefaultAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Recipe> AddRange(IEnumerable<CostControlBusinessEntity.Recipe> entities)
		{
			IEnumerable<CostControlBusinessEntity.Recipe> result =
			RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
				  Repository.AddRange(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Recipe> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await RecipeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
				  Repository
				  .AddRange(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Recipe> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter)
		{
			var result = RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
				Repository.RemoveFiltered(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await RecipeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
				Repository.RemoveFilteredAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Recipe> RemoveRange(IEnumerable<CostControlBusinessEntity.Recipe> entities)
		{
			var result = RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
					Repository.RemoveRange(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.Recipe> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await RecipeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
						Repository
						.Remove(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Recipe Exists(params object[] primaryKey)
		=> RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Recipe> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await RecipeIMapper.Map<Task<CostControlBusinessEntity.Recipe>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
		=> Repository.Exists(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
		=> Repository.Count(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					RecipeMapperConfig = null;
					RecipeIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
		=> Repository.Any(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.Recipe item,
			Expression<Func<CostControlBusinessEntity.Recipe, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.Recipe> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
			int? page = null, int? pageSize = null)
		=> RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
				Repository.Get(
					r => r.FoodId == parentId,
					RecipeIMapper.Map<Func<IQueryable<CostControlEntity.Recipe>, IOrderedQueryable<CostControlEntity.Recipe>>>(orderBy),
					RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties),
					page,
					pageSize));

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

		~RecipeLogic()
		{
			Dispose(false);
		}
	}
}