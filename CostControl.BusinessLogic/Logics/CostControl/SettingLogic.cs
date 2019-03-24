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

	public class SettingLogic : Base.IGenericLogic<CostControlBusinessEntity.Setting>, IDisposable
	{
		private MapperConfiguration SettingMapperConfig { get; set; }

		private IMapper SettingIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.Setting> Repository;

		public SettingLogic()
		{
			SettingMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			SettingIMapper = SettingMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.Setting>();
		}

		public CostControlBusinessEntity.Setting Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Remove(id));
				Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.Setting> Remove(
			Expression<Func<CostControlBusinessEntity.Setting, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.Setting> result = null;

			var deleteLst = Repository.Get(SettingIMapper
								.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.Setting>)
					.ForEach(s => result.Add(SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Remove(s))));

				Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.Setting> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

			if (entity != null)
			{
				var result = SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Remove(id));
				await CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Setting>> RemoveAsync(
			Expression<Func<CostControlBusinessEntity.Setting, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
							  Repository
							  .Remove(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Setting Exists(object primaryKey)
		=> SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Setting> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> SettingIMapper.Map<CostControlBusinessEntity.Setting>(await Repository.ExistsAsync(cancellationToken, primaryKey));

		public IEnumerable<CostControlBusinessEntity.Setting> Get(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>>>> includeProperties = null,
			int? pageNumber = null,
			int? pageSize = null)
		=> SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
						   Repository.Get(
							   SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter),
							   SettingIMapper.Map<Func<IQueryable<CostControlEntity.Setting>, IOrderedQueryable<CostControlEntity.Setting>>>(orderBy),
							   SettingIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>>(includeProperties),
							   pageNumber, pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.Setting>> GetAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await SettingIMapper.Map<Task<IEnumerable<CostControlEntity.Setting>>, Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
						   Repository.GetAsync(
							   SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter),
							   SettingIMapper.Map<Func<IQueryable<CostControlEntity.Setting>, IOrderedQueryable<CostControlEntity.Setting>>>(orderBy),
							   SettingIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>>(includeProperties),
							   pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.Setting GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>>>> includeProperties = null)
		=> id == null ? null : SettingIMapper.Map<CostControlBusinessEntity.Setting>
					   (Repository.GetById(id, SettingIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.Setting> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> id == null ? null : SettingIMapper.Map<CostControlBusinessEntity.Setting>
					   (await Repository.GetByIdAsync(id, SettingIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Setting> GetWithRawSql(string query, params object[] parameters)
		=> SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.Setting>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.Setting Add(CostControlBusinessEntity.Setting entity)
		{
			if (entity == null)
				return null;

			var result = SettingIMapper
					.Map<CostControlBusinessEntity.Setting>(
						Repository.Add(SettingIMapper.Map<CostControlEntity.Setting>(entity)));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Setting> AddAsync(CostControlBusinessEntity.Setting entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Setting = SettingIMapper.Map<CostControlEntity.Setting>(entity);

			var result = SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Add(Setting));
			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Setting Update(CostControlBusinessEntity.Setting entity)
		{
			if (entity == null)
				return null;

			var Setting = SettingIMapper.Map<CostControlEntity.Setting>(entity);

			var result = SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Update(Setting));
			Commit();

			return result;
		}

		public async Task<CostControlBusinessEntity.Setting> UpdateAsync(CostControlBusinessEntity.Setting entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			var Setting = SettingIMapper.Map<CostControlEntity.Setting>(entity);

			var result = SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Update(Setting));

			await CommitAsync(cancellationToken);

			return result;
		}

		public int RunRawSql(string query, params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.Setting SingleOrDefault(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
		=> SettingIMapper.Map<CostControlBusinessEntity.Setting>(
					   Repository.SingleOrDefault(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Setting> SingleOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await SettingIMapper.Map<Task<CostControlBusinessEntity.Setting>>(
						   Repository.SingleOrDefaultAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.Setting FirstOrDefault(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
		=> SettingIMapper.Map<CostControlBusinessEntity.Setting>(
						   Repository.FirstOrDefault(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.Setting> FirstOrDefaultAsync(
			Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await SettingIMapper.Map<Task<CostControlBusinessEntity.Setting>>(
						   Repository.FirstOrDefaultAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.Setting> AddRange(IEnumerable<CostControlBusinessEntity.Setting> entities)
		{
			IEnumerable<CostControlBusinessEntity.Setting> result =
			SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
				  Repository.AddRange(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Setting>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Setting> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await SettingIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
				  Repository
				  .AddRange(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Setting> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter)
		{
			var result = SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
				Repository.RemoveFiltered(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Setting>> RemoveFilteredAsync(
			Expression<Func<CostControlBusinessEntity.Setting, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await SettingIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
				Repository.RemoveFilteredAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken));

			await CommitAsync(cancellationToken);

			return result;
		}

		public IEnumerable<CostControlBusinessEntity.Setting> RemoveRange(IEnumerable<CostControlBusinessEntity.Setting> entities)
		{
			var result = SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
					Repository.RemoveRange(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

			Commit();

			return result;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.Setting>> RemoveRangeAsync(
			IEnumerable<CostControlBusinessEntity.Setting> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await SettingIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
						Repository
						.Remove(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

			await CommitAsync(cancellationToken);

			return result;
		}

		public CostControlBusinessEntity.Setting Exists(params object[] primaryKey)
		=> SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.Setting> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await SettingIMapper.Map<Task<CostControlBusinessEntity.Setting>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
		=> Repository.Exists(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter));
		
		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken);
		
		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken);
		
		public int GetCount(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
		=> Repository.Count(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter));
		
		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					SettingMapperConfig = null;
					SettingIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
		=> Repository.Any(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.Setting item,
			Expression<Func<CostControlBusinessEntity.Setting, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.Setting> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>>>> includeProperties = null,
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

		~SettingLogic()
		{
			Dispose(false);
		}
	}
}