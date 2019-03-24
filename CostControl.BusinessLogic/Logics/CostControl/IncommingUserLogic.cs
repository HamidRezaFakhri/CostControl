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
	public class IncommingUserLogic : IGenericLogic<CostControlBusinessEntity.IncommingUser>, IDisposable
	{
		private MapperConfiguration IncommingUserMapperConfig { get; set; }

		private IMapper IncommingUserIMapper { get; set; }

		private readonly UnitOfWork _unitOfWork;

		protected IRepository<CostControlEntity.IncommingUser> Repository;

		public IncommingUserLogic()
		{
			IncommingUserMapperConfig = new AutoMapperConfiguration().Configure();
			IncommingUserIMapper = IncommingUserMapperConfig.CreateMapper();
			_unitOfWork = new UnitOfWork(new CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.IncommingUser>();
		}

		public CostControlBusinessEntity.IncommingUser Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.IncommingUser> Remove(
			Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.IncommingUser> result = null;

			var deleteLst = Repository.Get(IncommingUserIMapper
								.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.IncommingUser>)
					.ForEach(s => result.Add(IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.IncommingUser> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
							  Repository
							  .Remove(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IncommingUser Exists(object primaryKey)
		=> IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.IncommingUser> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.IncommingUser> Get(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
						   Repository.Get(
							   IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter),
							   IncommingUserIMapper.Map<Func<IQueryable<CostControlEntity.IncommingUser>, IOrderedQueryable<CostControlEntity.IncommingUser>>>(orderBy),
							   IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> GetAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IncommingUserIMapper.Map<Task<IEnumerable<CostControlEntity.IncommingUser>>, Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
						   Repository.GetAsync(
							   IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter),
							   IncommingUserIMapper.Map<Func<IQueryable<CostControlEntity.IncommingUser>, IOrderedQueryable<CostControlEntity.IncommingUser>>>(orderBy),
							   IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.IncommingUser GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null)
		=> id == null ? null : IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>
					   (Repository.GetById(id, IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.IncommingUser> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>
					   (await Repository.GetByIdAsync(id, IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.IncommingUser> GetWithRawSql(string query, params object[] parameters)
		=> IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.IncommingUser Add(CostControlBusinessEntity.IncommingUser entity)
		{
			if (entity == null)
				return null;

			var result = IncommingUserIMapper
					.Map<CostControlBusinessEntity.IncommingUser>(
						Repository.Add(IncommingUserIMapper.Map<CostControlEntity.IncommingUser>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.IncommingUser> AddAsync(CostControlBusinessEntity.IncommingUser entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var IncommingUser = IncommingUserIMapper.Map<CostControlEntity.IncommingUser>(entity);

			var result = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Add(IncommingUser));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IncommingUser Update(CostControlBusinessEntity.IncommingUser entity)
		{
			if (entity == null)
				return null;

			var IncommingUser = IncommingUserIMapper.Map<CostControlEntity.IncommingUser>(entity);

			var result = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Update(IncommingUser));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.IncommingUser> UpdateAsync(CostControlBusinessEntity.IncommingUser entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var IncommingUser = IncommingUserIMapper.Map<CostControlEntity.IncommingUser>(entity);

			var result = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Update(IncommingUser));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.IncommingUser SingleOrDefault(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
		=> IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(
					   Repository.SingleOrDefault(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.IncommingUser> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IncommingUserIMapper.Map<Task<CostControlBusinessEntity.IncommingUser>>(
						   Repository.SingleOrDefaultAsync(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.IncommingUser FirstOrDefault(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
		=> IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(
						   Repository.FirstOrDefault(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.IncommingUser> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await IncommingUserIMapper.Map<Task<CostControlBusinessEntity.IncommingUser>>(
						   Repository.FirstOrDefaultAsync(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.IncommingUser> AddRange(IEnumerable<CostControlBusinessEntity.IncommingUser> entities)
		{
			IEnumerable<CostControlBusinessEntity.IncommingUser> result =
			IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
				  Repository.AddRange(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IncommingUser> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IncommingUserIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
				  Repository
				  .AddRange(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.IncommingUser> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter)
		{
			var result = IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
				Repository.RemoveFiltered(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IncommingUserIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
				Repository.RemoveFilteredAsync(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.IncommingUser> RemoveRange(IEnumerable<CostControlBusinessEntity.IncommingUser> entities)
		{
			var result = IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
					Repository.RemoveRange(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.IncommingUser> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await IncommingUserIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
						Repository
						.Remove(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.IncommingUser Exists(params object[] primaryKey)
		=> IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.IncommingUser> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await IncommingUserIMapper.Map<Task<CostControlBusinessEntity.IncommingUser>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
		=> Repository.Exists(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
		=> Repository.Count(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					IncommingUserMapperConfig = null;
					IncommingUserIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
		=> Repository.Any(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.IncommingUser item,
			Expression<Func<CostControlBusinessEntity.IncommingUser, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.IncommingUser> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
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

		~IncommingUserLogic()
		{
			Dispose(false);
		}
	}
}