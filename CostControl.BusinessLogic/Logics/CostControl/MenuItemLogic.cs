﻿namespace CostControl.BusinessLogic.Logics.CostControl
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

	public class MenuItemLogic : Base.IGenericLogic<CostControlBusinessEntity.MenuItem>, IDisposable
	{
		private MapperConfiguration MenuItemMapperConfig { get; set; }

		private IMapper MenuItemIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.MenuItem> Repository;

		public MenuItemLogic()
		{
			MenuItemMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			MenuItemIMapper = MenuItemMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.MenuItem>();
		}

		public CostControlBusinessEntity.MenuItem Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.MenuItem> Remove(
			Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.MenuItem> result = null;

			var deleteLst = Repository.Get(MenuItemIMapper
								.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.MenuItem>)
					.ForEach(s => result.Add(MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.MenuItem> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
							  Repository
							  .Remove(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.MenuItem Exists(object primaryKey)
		=> MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.MenuItem> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.MenuItem> Get(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
						   Repository.Get(
							   MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter),
							   MenuItemIMapper.Map<Func<IQueryable<CostControlEntity.MenuItem>, IOrderedQueryable<CostControlEntity.MenuItem>>>(orderBy),
							   MenuItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> GetAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await MenuItemIMapper.Map<Task<IEnumerable<CostControlEntity.MenuItem>>, Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
						   Repository.GetAsync(
							   MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter),
							   MenuItemIMapper.Map<Func<IQueryable<CostControlEntity.MenuItem>, IOrderedQueryable<CostControlEntity.MenuItem>>>(orderBy),
							   MenuItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.MenuItem GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>>>> includeProperties = null)
		=> id == null ? null : MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>
					   (Repository.GetById(id, MenuItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.MenuItem> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>
					   (await Repository.GetByIdAsync(id, MenuItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.MenuItem> GetWithRawSql(string query, params object[] parameters)
		=> MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.MenuItem Add(CostControlBusinessEntity.MenuItem entity)
		{
			if (entity == null)
				return null;

			var result = MenuItemIMapper
					.Map<CostControlBusinessEntity.MenuItem>(
						Repository.Add(MenuItemIMapper.Map<CostControlEntity.MenuItem>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.MenuItem> AddAsync(CostControlBusinessEntity.MenuItem entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var MenuItem = MenuItemIMapper.Map<CostControlEntity.MenuItem>(entity);

			var result = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Add(MenuItem));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.MenuItem Update(CostControlBusinessEntity.MenuItem entity)
		{
			if (entity == null)
				return null;

			var MenuItem = MenuItemIMapper.Map<CostControlEntity.MenuItem>(entity);

			var result = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Update(MenuItem));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.MenuItem> UpdateAsync(CostControlBusinessEntity.MenuItem entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var MenuItem = MenuItemIMapper.Map<CostControlEntity.MenuItem>(entity);

			var result = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Update(MenuItem));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.MenuItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
		=> MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(
					   Repository.SingleOrDefault(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.MenuItem> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await MenuItemIMapper.Map<Task<CostControlBusinessEntity.MenuItem>>(
						   Repository.SingleOrDefaultAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.MenuItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
		=> MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(
						   Repository.FirstOrDefault(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.MenuItem> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await MenuItemIMapper.Map<Task<CostControlBusinessEntity.MenuItem>>(
						   Repository.FirstOrDefaultAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.MenuItem> AddRange(IEnumerable<CostControlBusinessEntity.MenuItem> entities)
		{
			IEnumerable<CostControlBusinessEntity.MenuItem> result =
			MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
				  Repository.AddRange(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.MenuItem> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await MenuItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
				  Repository
				  .AddRange(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.MenuItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter)
		{
			var result = MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
				Repository.RemoveFiltered(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await MenuItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
				Repository.RemoveFilteredAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.MenuItem> RemoveRange(IEnumerable<CostControlBusinessEntity.MenuItem> entities)
		{
			var result = MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
					Repository.RemoveRange(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.MenuItem> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await MenuItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
						Repository
						.Remove(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.MenuItem Exists(params object[] primaryKey)
		=> MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.MenuItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await MenuItemIMapper.Map<Task<CostControlBusinessEntity.MenuItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
		=> Repository.Exists(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
		=> Repository.Count(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					MenuItemMapperConfig = null;
					MenuItemIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
		=> Repository.Any(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.MenuItem item,
			Expression<Func<CostControlBusinessEntity.MenuItem, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.MenuItem> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>>>> includeProperties = null,
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

		~MenuItemLogic()
		{
			Dispose(false);
		}
	}
}