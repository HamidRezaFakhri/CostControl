namespace CostControl.BusinessLogic.Logics.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using AutoMapper.Extensions.ExpressionMapping;
	using Microsoft.EntityFrameworkCore.Query;
	using CostControlBusinessEntity = BusinessEntity.Models.CostControl;
	using CostControlEntity = Entity.Models.CostControl;

	public class SalePointLogic : Base.IGenericLogic<CostControlBusinessEntity.SalePoint>, IDisposable
	{
		private MapperConfiguration SalePointMapperConfig { get; set; }

		private IMapper SalePointIMapper { get; set; }

		private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

		protected Data.Repository.IRepository<CostControlEntity.SalePoint> Repository;

		public SalePointLogic()
		{
			SalePointMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
			SalePointIMapper = SalePointMapperConfig.CreateMapper();
			_unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
			Repository = _unitOfWork.GetRepository<CostControlEntity.SalePoint>();
		}

		//private Expression<Func<TDestination, TProperty>> GetMappedSelector<TSource, TDestination, TProperty>(Expression<Func<TSource, TProperty>> selector)
		//{
		//    var map = SalePointMapperConfig.FindTypeMapFor<TSource, TDestination>();

		//    var mInfo = ReflectionHelper.GetMemberInfo(selector);

		//    if (mInfo == null)
		//    {
		//        throw new Exception(string.Format(
		//            "Can't get PropertyMap. \"{0}\" is not a member expression", selector));
		//    }

		//    PropertyMap propmap = map
		//        .GetPropertyMaps()
		//        .SingleOrDefault(m =>
		//            m.SourceMember != null &&
		//            m.SourceMember.MetadataToken == mInfo.MetadataToken);

		//    if (propmap == null)
		//    {
		//        throw new Exception(
		//            string.Format(
		//            "Can't map selector. Could not find a PropertyMap for {0}", selector.Name));
		//    }

		//    var param = Expression.Parameter(typeof(TDestination));
		//    var body = Expression.MakeMemberAccess(param, propmap.DestinationProperty);
		//    var lambda = Expression.Lambda<Func<TDestination, TProperty>>(body, param);

		//    return lambda;
		//}

		private static class ReflectionHelper
		{
			public static MemberInfo GetMemberInfo(Expression memberExpression)
			{
				var memberExpr = memberExpression as MemberExpression;

				if (memberExpr == null && memberExpression is LambdaExpression)
				{
					memberExpr = (memberExpression as LambdaExpression).Body as MemberExpression;
				}

				return memberExpr != null ? memberExpr.Member : null;
			}
		}

		public CostControlBusinessEntity.SalePoint Remove(object id)
		{
			if (id == null)
				return null;

			if (Repository.GetById(id) != null)
			{
				var result = SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(Repository.Remove(id));
				_unitOfWork.Commit();

				return result;
			}

			return null;
		}

		public IEnumerable<CostControlBusinessEntity.SalePoint> Remove(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter)
		{
			if (filter == null)
				return null;

			List<CostControlBusinessEntity.SalePoint> result = null;

			var deleteLst = Repository.Get(SalePointIMapper
								.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
									Expression<Func<CostControlEntity.SalePoint, bool>>>(filter));

			if (deleteLst != null)
			{
				(deleteLst as List<CostControlBusinessEntity.SalePoint>)
					.ForEach(s => result.Add(SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(Repository.Remove(s))));

				_unitOfWork.Commit();

				return result;
			}

			return null;
		}

		public async Task<CostControlBusinessEntity.SalePoint> RemoveAsync(object id,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (id == null)
				return null;

			var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

			if (entity != null)
			{
				var result = SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(Repository.Remove(id));
				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}

			return null;
		}

		public async Task<IEnumerable<CostControlBusinessEntity.SalePoint>> RemoveAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				var result = SalePointIMapper.Map<IEnumerable<CostControlBusinessEntity.SalePoint>>(
				  Repository
				  .Remove(SalePointIMapper.Map<Expression<Func<CostControlEntity.SalePoint, bool>>>(filter)));

				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public CostControlBusinessEntity.SalePoint Exists(object primaryKey)
			=> SalePointIMapper.Map<CostControlBusinessEntity.SalePoint>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.SalePoint> ExistsAsync(object primaryKey,
			CancellationToken cancellationToken = default(CancellationToken))
		=> SalePointIMapper.Map<CostControlBusinessEntity.SalePoint>(await Repository.ExistsAsync(cancellationToken, primaryKey));


		//public Expression<Func<CostControlBusinessEntity.SalePoint, bool>> GetMappedSelectorForAB(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> selector)
		//{
		//    Expression<Func<CostControlBusinessEntity.SalePoint, CostControlBusinessEntity.SalePoint>> mapper =
		//        SalePointMapperConfig.ExpressionBuilder.CreateMapExpression(CostControlBusinessEntity.SalePoint, CostControlBusinessEntity.SalePoint);
		//    Expression<Func<CostControlBusinessEntity.SalePoint, bool>> mappedSelector = selector.Compose(mapper);
		//    return mappedSelector;
		//}

		public IEnumerable<CostControlBusinessEntity.SalePoint> Get(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
				Func<IQueryable<CostControlBusinessEntity.SalePoint>, IOrderedQueryable<CostControlBusinessEntity.SalePoint>> orderBy = null,
				ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SalePoint>, IIncludableQueryable<CostControlBusinessEntity.SalePoint, object>>>> includeProperties = null,
				int? pageNumber = null,
				int? pageSize = null)
			//{

			//Expression<Func<Category, bool>> mappedExpression = ExpressionMapper.GetMappedSelector<Category, CategoryViewModel>(whereCondition);

			//(new ParameterReplacer()).Visit(filter);
			//}

			=> SalePointIMapper.Map<IEnumerable<CostControlBusinessEntity.SalePoint>>(
					Repository.Get(
						SalePointIMapper.Map<Expression<Func<CostControlEntity.SalePoint, bool>>>(filter),
						SalePointIMapper.Map<Func<IQueryable<CostControlEntity.SalePoint>, IOrderedQueryable<CostControlEntity.SalePoint>>>(orderBy),
						SalePointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SalePoint>, IIncludableQueryable<CostControlEntity.SalePoint, object>>>>(includeProperties),
						pageNumber,
						pageSize));

		public async Task<IEnumerable<CostControlBusinessEntity.SalePoint>> GetAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
			Func<IQueryable<CostControlBusinessEntity.SalePoint>, IOrderedQueryable<CostControlBusinessEntity.SalePoint>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SalePoint>, IIncludableQueryable<CostControlBusinessEntity.SalePoint, object>>>> includeProperties = null,
			int? pageNumber = null, int? pageSize = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await SalePointIMapper.Map<Task<IEnumerable<CostControlEntity.SalePoint>>, Task<IEnumerable<CostControlBusinessEntity.SalePoint>>>(
				Repository.GetAsync(
					SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>, Expression<Func<CostControlEntity.SalePoint, bool>>>(filter),
					SalePointIMapper.Map<Func<IQueryable<CostControlBusinessEntity.SalePoint>, IOrderedQueryable<CostControlBusinessEntity.SalePoint>>,
					Func<IQueryable<CostControlEntity.SalePoint>, IOrderedQueryable<CostControlEntity.SalePoint>>>(orderBy),
					SalePointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SalePoint>, IIncludableQueryable<CostControlEntity.SalePoint, object>>>>(includeProperties),
					pageNumber, pageSize, cancellationToken));

		public CostControlBusinessEntity.SalePoint GetById(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SalePoint>, IIncludableQueryable<CostControlBusinessEntity.SalePoint, object>>>> includeProperties = null)
		=> id == null ? null : SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>
			(Repository.GetById(id, SalePointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SalePoint>, IIncludableQueryable<CostControlEntity.SalePoint, object>>>>(includeProperties)));

		public async Task<CostControlBusinessEntity.SalePoint> GetByIdAsync(object id,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SalePoint>, IIncludableQueryable<CostControlBusinessEntity.SalePoint, object>>>> includeProperties = null,
			CancellationToken cancellationToken = default(CancellationToken))
		//=> await await Task.FromResult(SalePointIMapper.Map<Task<Entity.Models.SalePoint>, Task<SalePoint>>(Repository.GetByIdAsync(id, cancellationToken)));
		=> id == null ? null : SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>
			(await Repository.GetByIdAsync(id, SalePointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SalePoint>, IIncludableQueryable<CostControlEntity.SalePoint, object>>>>(includeProperties), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.SalePoint> GetWithRawSql(string query, params object[] parameters)
		=> SalePointIMapper.Map<IEnumerable<CostControlEntity.SalePoint>, IEnumerable<CostControlBusinessEntity.SalePoint>>(Repository.GetWithRawSql(query, parameters));

		public async Task<IEnumerable<CostControlBusinessEntity.SalePoint>> GetWithRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> SalePointIMapper.Map<IEnumerable<CostControlEntity.SalePoint>, IEnumerable<CostControlBusinessEntity.SalePoint>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

		public CostControlBusinessEntity.SalePoint Add(CostControlBusinessEntity.SalePoint entity)
		{
			//using (var transaction = objectContext.Connection.BeginTransaction())

			if (entity == null)
				return null;

			try
			{
				var result = SalePointIMapper
					.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(
						Repository.Add(SalePointIMapper.Map<CostControlBusinessEntity.SalePoint, CostControlEntity.SalePoint>(entity)));
				_unitOfWork.Commit();

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<CostControlBusinessEntity.SalePoint> AddAsync(CostControlBusinessEntity.SalePoint entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			try
			{
				var SalePoint = SalePointIMapper.Map<CostControlBusinessEntity.SalePoint, CostControlEntity.SalePoint>(entity);

				var result = SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(Repository.Add(SalePoint));
				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public CostControlBusinessEntity.SalePoint Update(CostControlBusinessEntity.SalePoint entity)
		{
			if (entity == null)
				return null;

			try
			{
				CostControlEntity.SalePoint SalePoint = SalePointIMapper.Map<CostControlBusinessEntity.SalePoint, CostControlEntity.SalePoint>(entity);

				var result = SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(Repository.Update(SalePoint));
				_unitOfWork.Commit();

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<CostControlBusinessEntity.SalePoint> UpdateAsync(CostControlBusinessEntity.SalePoint entity,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (entity == null)
				return null;

			try
			{
				var SalePoint = SalePointIMapper.Map<CostControlBusinessEntity.SalePoint, CostControlEntity.SalePoint>(entity);

				var result = SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(Repository.Update(SalePoint));

				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int RunRawSql(string query,
			params object[] parameters)
		=> Repository.RunRawSql(query, parameters);

		public async Task<int> RunRawSqlAsync(string query,
			CancellationToken cancellationToken = default(CancellationToken),
			params object[] parameters)
		=> await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

		public CostControlBusinessEntity.SalePoint SingleOrDefault(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null)
		=> SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(
			Repository.SingleOrDefault(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
				Expression<Func<CostControlEntity.SalePoint, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.SalePoint> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await SalePointIMapper.Map<Task<CostControlEntity.SalePoint>, Task<CostControlBusinessEntity.SalePoint>>(
				Repository.SingleOrDefaultAsync(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
					Expression<Func<CostControlEntity.SalePoint, bool>>>(filter), cancellationToken));

		public CostControlBusinessEntity.SalePoint FirstOrDefault(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null)
		=> SalePointIMapper.Map<CostControlEntity.SalePoint, CostControlBusinessEntity.SalePoint>(
				Repository.FirstOrDefault(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
					Expression<Func<CostControlEntity.SalePoint, bool>>>(filter)));

		public async Task<CostControlBusinessEntity.SalePoint> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await SalePointIMapper.Map<Task<CostControlEntity.SalePoint>, Task<CostControlBusinessEntity.SalePoint>>(
				Repository.FirstOrDefaultAsync(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
					Expression<Func<CostControlEntity.SalePoint, bool>>>(filter), cancellationToken));

		public IEnumerable<CostControlBusinessEntity.SalePoint> AddRange(IEnumerable<CostControlBusinessEntity.SalePoint> entities)
		{
			try
			{
				var result =
				SalePointIMapper.Map<IEnumerable<CostControlBusinessEntity.SalePoint>>(
					  Repository.AddRange(SalePointIMapper.Map<IEnumerable<CostControlEntity.SalePoint>>(entities)));

				_unitOfWork.Commit();

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<IEnumerable<CostControlBusinessEntity.SalePoint>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.SalePoint> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				var result = await SalePointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SalePoint>>>(
					  Repository
					  .AddRange(SalePointIMapper.Map<IEnumerable<CostControlEntity.SalePoint>>(entities)));

				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<CostControlBusinessEntity.SalePoint> RemoveFiltered(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter)
		{
			try
			{
				var result = SalePointIMapper.Map<IEnumerable<CostControlBusinessEntity.SalePoint>>(
					Repository.RemoveFiltered(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
						Expression<Func<CostControlEntity.SalePoint, bool>>>(filter)));

				_unitOfWork.Commit();

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<IEnumerable<CostControlBusinessEntity.SalePoint>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				var result = await SalePointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SalePoint>>>(
					Repository.RemoveFilteredAsync(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
					Expression<Func<CostControlEntity.SalePoint, bool>>>(filter), cancellationToken));

				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<CostControlBusinessEntity.SalePoint> RemoveRange(IEnumerable<CostControlBusinessEntity.SalePoint> entities)
		{
			try
			{
				var result = SalePointIMapper.Map<IEnumerable<CostControlBusinessEntity.SalePoint>>(
					Repository.RemoveRange(SalePointIMapper.Map<IEnumerable<CostControlEntity.SalePoint>>(entities)));

				_unitOfWork.Commit();

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<IEnumerable<CostControlBusinessEntity.SalePoint>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.SalePoint> entities,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				var result = await SalePointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SalePoint>>>(
						Repository
						.Remove(SalePointIMapper.Map<IEnumerable<CostControlEntity.SalePoint>>(entities)));

				await _unitOfWork.CommitAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public CostControlBusinessEntity.SalePoint Exists(params object[] primaryKey)
		=> SalePointIMapper.Map<CostControlBusinessEntity.SalePoint>(Repository.Exists(primaryKey));

		public async Task<CostControlBusinessEntity.SalePoint> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
			params object[] primaryKey)
		=> await SalePointIMapper.Map<Task<CostControlBusinessEntity.SalePoint>>(Repository.ExistsAsync(cancellationToken, primaryKey));

		public bool Exists(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null)
		=> Repository.Exists(SalePointIMapper.Map<Expression<Func<CostControlEntity.SalePoint, bool>>>(filter));

		public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.ExistsAsync(SalePointIMapper.Map<Expression<Func<CostControlEntity.SalePoint, bool>>>(filter), cancellationToken);

		public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.CountAsync(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
			Expression<Func<CostControlEntity.SalePoint, bool>>>(filter), cancellationToken);

		public int GetCount(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null)
			=> Repository.Count(SalePointIMapper.Map<Expression<Func<CostControlBusinessEntity.SalePoint, bool>>,
				Expression<Func<CostControlEntity.SalePoint, bool>>>(filter));

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					//Context?.Dispose();
					SalePointMapperConfig = null;
					SalePointIMapper = null;
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

		public bool Any(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null)
		=> Repository.Any(SalePointIMapper.Map<Expression<Func<CostControlEntity.SalePoint, bool>>>(filter));

		public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.SalePoint, bool>> filter = null,
			CancellationToken cancellationToken = default(CancellationToken))
		=> await Repository.AnyAsync(SalePointIMapper.Map<Expression<Func<CostControlEntity.SalePoint, bool>>>(filter), cancellationToken);

		public Task LoadPropertyAsync(CostControlBusinessEntity.SalePoint item,
			Expression<Func<CostControlBusinessEntity.SalePoint, object>> property,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CostControlBusinessEntity.SalePoint> GetByParentId(long parentId,
			Func<IQueryable<CostControlBusinessEntity.SalePoint>, IOrderedQueryable<CostControlBusinessEntity.SalePoint>> orderBy = null,
			ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SalePoint>, IIncludableQueryable<CostControlBusinessEntity.SalePoint, object>>>> includeProperties = null,
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

		~SalePointLogic()
		{
			Dispose(false);
		}
	}
}