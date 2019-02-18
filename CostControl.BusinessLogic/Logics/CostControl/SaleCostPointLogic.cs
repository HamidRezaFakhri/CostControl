using AutoMapper;
using CostControl.BusinessLogic.Logics.Base;
using CostControl.BusinessLogic.Mapper;
using CostControl.Data.DAL;
using CostControl.Data.Repository;
using CostControl.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CostControlBusinessEntity = CostControl.BusinessEntity.Models.CostControl;
using CostControlEntity = CostControl.Entity.Models.CostControl;

namespace CostControl.BusinessLogic.Logics.CostControl
{
    public class SaleCostPointLogic : IGenericLogic<CostControlBusinessEntity.SaleCostPoint>, IDisposable
    {
        private MapperConfiguration SaleCostPointMapperConfig { get; set; }

        private IMapper SaleCostPointIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.SaleCostPoint> Repository;

        public SaleCostPointLogic()
        {
            SaleCostPointMapperConfig = new AutoMapperConfiguration().Configure();
            SaleCostPointIMapper = SaleCostPointMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.SaleCostPoint>();
        }

        //private Expression<Func<TDestination, TProperty>> GetMappedSelector<TSource, TDestination, TProperty>(Expression<Func<TSource, TProperty>> selector)
        //{
        //    var map = SaleCostPointMapperConfig.FindTypeMapFor<TSource, TDestination>();

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
                MemberExpression memberExpr = memberExpression as MemberExpression;

                if (memberExpr == null && memberExpression is LambdaExpression)
                {
                    memberExpr = (memberExpression as LambdaExpression).Body as MemberExpression;
                }

                return memberExpr != null ? memberExpr.Member : null;
            }
        }

        public CostControlBusinessEntity.SaleCostPoint Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                CostControlBusinessEntity.SaleCostPoint result = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> Remove(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.SaleCostPoint> result = null;

            IEnumerable<CostControlEntity.SaleCostPoint> deleteLst = Repository.Get(SaleCostPointIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                                    Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.SaleCostPoint>)
                    .ForEach(s => result.Add(SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            CostControlEntity.SaleCostPoint entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                CostControlBusinessEntity.SaleCostPoint result = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> RemoveAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result = SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                  Repository
                  .Remove(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.SaleCostPoint Exists(object primaryKey)
        {
            return SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Exists(primaryKey));
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(await Repository.ExistsAsync(cancellationToken, primaryKey));
        }


        //public Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> GetMappedSelectorForAB(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> selector)
        //{
        //    Expression<Func<CostControlBusinessEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>> mapper =
        //        SaleCostPointMapperConfig.ExpressionBuilder.CreateMapExpression(CostControlBusinessEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint);
        //    Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> mappedSelector = selector.Compose(mapper);
        //    return mappedSelector;
        //}


        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> Get(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
                Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>> orderBy = null,
                List<Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>>> includeProperties = null,
                int? pageNumber = null,
                int? pageSize = null)
        {
            List<Expression<Func<CostControlEntity.SaleCostPoint, object>>> includePropertiesNew = null;

            if (includeProperties == null)
                includePropertiesNew = new List<Expression<Func<CostControlEntity.SaleCostPoint, object>>>
                {
                    a => a.SalePoint,
                    b => b.CostPoint
                };

            return SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>, IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.Get(
                               SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter),
                               SaleCostPointIMapper.Map<Func<IQueryable<CostControlEntity.SaleCostPoint>, IOrderedQueryable<CostControlEntity.SaleCostPoint>>>(orderBy),
                               includeProperties == null ? includePropertiesNew :
                               SaleCostPointIMapper.Map<List<Expression<Func<CostControlEntity.SaleCostPoint, object>>>>(includeProperties),
                               pageNumber,
                               pageSize));
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> GetAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlEntity.SaleCostPoint>>, Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                           Repository.GetAsync(
                               SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>, Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter),
                               SaleCostPointIMapper.Map<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>>,
                               Func<IQueryable<CostControlEntity.SaleCostPoint>, IOrderedQueryable<CostControlEntity.SaleCostPoint>>>(orderBy),
                               SaleCostPointIMapper.Map<List<Expression<Func<CostControlEntity.SaleCostPoint, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));
        }

        public CostControlBusinessEntity.SaleCostPoint GetById(object id,
            List<Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>>> includeProperties = null)
        {
            return id == null ? null : SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>
                       (Repository.GetById(id, SaleCostPointIMapper.Map<List<Expression<Func<CostControlEntity.SaleCostPoint, object>>>>(includeProperties)));
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> GetByIdAsync(object id,
            List<Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return id == null ? null : SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>
                       (await Repository.GetByIdAsync(id, SaleCostPointIMapper.Map<List<Expression<Func<CostControlEntity.SaleCostPoint, object>>>>(includeProperties), cancellationToken));
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> GetWithRawSql(string query, params object[] parameters)
        {
            return SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>, IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(Repository.GetWithRawSql(query, parameters));
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        {
            return SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>, IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));
        }

        public CostControlBusinessEntity.SaleCostPoint Add(CostControlBusinessEntity.SaleCostPoint entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlBusinessEntity.SaleCostPoint result = SaleCostPointIMapper
                    .Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(
                        Repository.Add(SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint, CostControlEntity.SaleCostPoint>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> AddAsync(CostControlBusinessEntity.SaleCostPoint entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.SaleCostPoint SaleCostPoint = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint, CostControlEntity.SaleCostPoint>(entity);

                CostControlBusinessEntity.SaleCostPoint result = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Add(SaleCostPoint));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.SaleCostPoint Update(CostControlBusinessEntity.SaleCostPoint entity)
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.SaleCostPoint SaleCostPoint = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint, CostControlEntity.SaleCostPoint>(entity);

                CostControlBusinessEntity.SaleCostPoint result = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Update(SaleCostPoint));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> UpdateAsync(CostControlBusinessEntity.SaleCostPoint entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.SaleCostPoint SaleCostPoint = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint, CostControlEntity.SaleCostPoint>(entity);

                CostControlBusinessEntity.SaleCostPoint result = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Update(SaleCostPoint));

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
        {
            return Repository.RunRawSql(query, parameters);
        }

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        {
            return await Repository.RunRawSqlAsync(query, cancellationToken, parameters);
        }

        public CostControlBusinessEntity.SaleCostPoint SingleOrDefault(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        {
            return SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(
                       Repository.SingleOrDefault(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                           Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaleCostPointIMapper.Map<Task<CostControlEntity.SaleCostPoint>, Task<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.SingleOrDefaultAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                               Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));
        }

        public CostControlBusinessEntity.SaleCostPoint FirstOrDefault(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        {
            return SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(
                           Repository.SingleOrDefault(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                               Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaleCostPointIMapper.Map<Task<CostControlEntity.SaleCostPoint>, Task<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.SingleOrDefaultAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                               Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> AddRange(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result =
                SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                      Repository.AddRange(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result = await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                      Repository
                      .AddRange(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> RemoveFiltered(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result = SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                    Repository.RemoveFiltered(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                        Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result = await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                    Repository.RemoveFilteredAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                    Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> RemoveRange(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result = SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                    Repository.RemoveRange(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.SaleCostPoint> result = await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                        Repository
                        .Remove(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.SaleCostPoint Exists(params object[] primaryKey)
        {
            return SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Exists(primaryKey));
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        {
            return await SaleCostPointIMapper.Map<Task<CostControlBusinessEntity.SaleCostPoint>>(Repository.ExistsAsync(cancellationToken, primaryKey));
        }

        public bool Exists(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        {
            return Repository.Exists(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));
        }

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.ExistsAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken);
        }

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.CountAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                       Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken);
        }

        public int GetCount(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        {
                return Repository.Count(SaleCostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                               Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    SaleCostPointMapperConfig = null;
                    SaleCostPointIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.SaleCostPoint item, Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>> orderBy = null, List<Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~SaleCostPointLogic()
        {
            Dispose(false);
        }
    }
}