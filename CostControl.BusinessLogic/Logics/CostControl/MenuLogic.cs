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

	public class MenuLogic : Base.IGenericLogic<CostControlBusinessEntity.Menu>, IDisposable
	{
		private MapperConfiguration MenuMapperConfig { get; set; }

		private IMapper MenuIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.Menu> Repository;

		public MenuLogic()
		{
			MenuMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			MenuIMapper = MenuMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.Menu>();
		}

		public CostControlBusinessEntity.Menu Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.Menu> Remove(
			Expression<Func<CostControlBusinessEntity.Menu, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.Menu> result = null;

			var deleteLst = Repository.Get(MenuIMapper
								.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.Menu>)
					.ForEach(s => result.Add(MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.Menu> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Menu>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.Menu, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
							  Repository
							  .Remove(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Menu Exists(object primaryKey)
		=> MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Menu> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> MenuIMapper.Map<CostControlBusinessEntity.Menu>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.Menu> Get(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
						   Repository.Get(
							   MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter),
							   MenuIMapper.Map<Func<IQueryable<CostControlEntity.Menu>, IOrderedQueryable<CostControlEntity.Menu>>>(orderBy),
							   MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.Menu>> GetAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await MenuIMapper.Map<Task<IEnumerable<CostControlEntity.Menu>>, Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
						   Repository.GetAsync(
							   MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter),
							   MenuIMapper.Map<Func<IQueryable<CostControlEntity.Menu>, IOrderedQueryable<CostControlEntity.Menu>>>(orderBy),
							   MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.Menu GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null)
		=> id == null ? null : MenuIMapper.Map<CostControlBusinessEntity.Menu>
					   (Repository.GetById(id, MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.Menu> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : MenuIMapper.Map<CostControlBusinessEntity.Menu>
					   (await Repository.GetByIdAsync(id, MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Menu> GetWithRawSql(string query, params object[] parameters)
		=> MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.Menu>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.Menu Add(CostControlBusinessEntity.Menu entity)
		{
			if (entity == null)
				return null;

			var result = MenuIMapper
					.Map<CostControlBusinessEntity.Menu>(
						Repository.Add(MenuIMapper.Map<CostControlEntity.Menu>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Menu> AddAsync(CostControlBusinessEntity.Menu entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Menu = MenuIMapper.Map<CostControlEntity.Menu>(entity);

			var result = MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Add(Menu));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Menu Update(CostControlBusinessEntity.Menu entity)
		{
			if (entity == null)
				return null;

			var Menu = MenuIMapper.Map<CostControlEntity.Menu>(entity);

			var result = MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Update(Menu));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Menu> UpdateAsync(CostControlBusinessEntity.Menu entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Menu = MenuIMapper.Map<CostControlEntity.Menu>(entity);

			var result = MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Update(Menu));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.Menu SingleOrDefault(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
		=> MenuIMapper.Map<CostControlBusinessEntity.Menu>(
					   Repository.SingleOrDefault(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Menu> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await MenuIMapper.Map<Task<CostControlBusinessEntity.Menu>>(
						   Repository.SingleOrDefaultAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.Menu FirstOrDefault(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
		=> MenuIMapper.Map<CostControlBusinessEntity.Menu>(
						   Repository.FirstOrDefault(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Menu> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await MenuIMapper.Map<Task<CostControlBusinessEntity.Menu>>(
						   Repository.FirstOrDefaultAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Menu> AddRange(IEnumerable<CostControlBusinessEntity.Menu> entities)
		{
			IEnumerable<CostControlBusinessEntity.Menu> result =
			MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
				  Repository.AddRange(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Menu>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Menu> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await MenuIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
				  Repository
				  .AddRange(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Menu> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter)
		{
			var result = MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
				Repository.RemoveFiltered(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Menu>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.Menu, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await MenuIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
				Repository.RemoveFilteredAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Menu> RemoveRange(IEnumerable<CostControlBusinessEntity.Menu> entities)
		{
			var result = MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
					Repository.RemoveRange(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Menu>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.Menu> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await MenuIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
						Repository
						.Remove(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Menu Exists(params object[] primaryKey)
		=> MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Menu> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await MenuIMapper.Map<Task<CostControlBusinessEntity.Menu>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
		=> Repository.Exists(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
		=> Repository.Count(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					MenuMapperConfig = null;
					MenuIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
		=> Repository.Any(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.Menu item,
			Expression<Func<CostControlBusinessEntity.Menu, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.Menu> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
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

		~MenuLogic()
		{
			Dispose(false);
		}
	}
}