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
	public class ConsumptionUnitLogic : IGenericLogic<CostControlBusinessEntity.ConsumptionUnit>, IDisposable
	{
		private MapperConfiguration ConsumptionUnitMapperConfig { get; set; }

		private IMapper ConsumptionUnitIMapper { get; set; }

		private readonly UnitOfWork _unitOfWork;

		protected IRepository<CostControlEntity.ConsumptionUnit> Repository;

		public ConsumptionUnitLogic()
		{
			ConsumptionUnitMapperConfig = new AutoMapperConfiguration().Configure();
			ConsumptionUnitIMapper = ConsumptionUnitMapperConfig.CreateMapper();
			_unitOfWork = new UnitOfWork(new CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.ConsumptionUnit>();
		}

		public CostControlBusinessEntity.ConsumptionUnit Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> Remove(
			Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.ConsumptionUnit> result = null;

			var deleteLst = Repository.Get(ConsumptionUnitIMapper
								.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.ConsumptionUnit>)
					.ForEach(s => result.Add(ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.ConsumptionUnit> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
							  Repository
							  .Remove(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.ConsumptionUnit Exists(object primaryKey)
		=> ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.ConsumptionUnit> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> Get(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
						   Repository.Get(
							   ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter),
							   ConsumptionUnitIMapper.Map<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IOrderedQueryable<CostControlEntity.ConsumptionUnit>>>(orderBy),
							   ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> GetAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlEntity.ConsumptionUnit>>, Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
						   Repository.GetAsync(
							   ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter),
							   ConsumptionUnitIMapper.Map<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IOrderedQueryable<CostControlEntity.ConsumptionUnit>>>(orderBy),
							   ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.ConsumptionUnit GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null)
		=> id == null ? null : ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>
					   (Repository.GetById(id, ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.ConsumptionUnit> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>
					   (await Repository.GetByIdAsync(id, ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> GetWithRawSql(string query, params object[] parameters)
		=> ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.ConsumptionUnit Add(CostControlBusinessEntity.ConsumptionUnit entity)
		{
			if (entity == null)
				return null;

			var result = ConsumptionUnitIMapper
					.Map<CostControlBusinessEntity.ConsumptionUnit>(
						Repository.Add(ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.ConsumptionUnit> AddAsync(CostControlBusinessEntity.ConsumptionUnit entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var ConsumptionUnit = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit>(entity);

			var result = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Add(ConsumptionUnit));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.ConsumptionUnit Update(CostControlBusinessEntity.ConsumptionUnit entity)
		{
			if (entity == null)
				return null;

			var ConsumptionUnit = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit>(entity);

			var result = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Update(ConsumptionUnit));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.ConsumptionUnit> UpdateAsync(CostControlBusinessEntity.ConsumptionUnit entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var ConsumptionUnit = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit>(entity);

			var result = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Update(ConsumptionUnit));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.ConsumptionUnit SingleOrDefault(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
		=> ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(
					   Repository.SingleOrDefault(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.ConsumptionUnit> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await ConsumptionUnitIMapper.Map<Task<CostControlBusinessEntity.ConsumptionUnit>>(
						   Repository.SingleOrDefaultAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.ConsumptionUnit FirstOrDefault(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
		=> ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(
						   Repository.FirstOrDefault(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.ConsumptionUnit> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await ConsumptionUnitIMapper.Map<Task<CostControlBusinessEntity.ConsumptionUnit>>(
						   Repository.FirstOrDefaultAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> AddRange(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities)
		{
			IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result =
			ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
				  Repository.AddRange(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
				  Repository
				  .AddRange(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> RemoveFiltered(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter)
		{
			var result = ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
				Repository.RemoveFiltered(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
				Repository.RemoveFilteredAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> RemoveRange(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities)
		{
			var result = ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
					Repository.RemoveRange(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
						Repository
						.Remove(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.ConsumptionUnit Exists(params object[] primaryKey)
		=> ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.ConsumptionUnit> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await ConsumptionUnitIMapper.Map<Task<CostControlBusinessEntity.ConsumptionUnit>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
		=> Repository.Exists(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
		=> Repository.Count(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					ConsumptionUnitMapperConfig = null;
					ConsumptionUnitIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
		=> Repository.Any(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.ConsumptionUnit item,
			Expression<Func<CostControlBusinessEntity.ConsumptionUnit, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
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

		~ConsumptionUnitLogic()
		{
			Dispose(false);
		}
	}
}