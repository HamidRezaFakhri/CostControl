using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using CostControl.BusinessLogic.Logics.Base;
using CostControl.BusinessLogic.Mapper;
using CostControl.Data.DAL;
using CostControl.Data.Repository;
using CostControl.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore.Query;
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

        //private Expression<Func<TDestination, TProperty>> GetMappedSelector<TSource, TDestination, TProperty>(Expression<Func<TSource, TProperty>> selector)
        //{
        //    var map = IncommingUserMapperConfig.FindTypeMapFor<TSource, TDestination>();

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

        public CostControlBusinessEntity.IncommingUser Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.IncommingUser> Remove(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.IncommingUser> result = null;

            var deleteLst = Repository.Get(IncommingUserIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                                    Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.IncommingUser>)
                    .ForEach(s => result.Add(IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.IncommingUser> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> RemoveAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
                  Repository
                  .Remove(IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IncommingUser Exists(object primaryKey)
            => IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IncommingUser> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser>(await Repository.ExistsAsync(cancellationToken, primaryKey));


        //public Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> GetMappedSelectorForAB(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> selector)
        //{
        //    Expression<Func<CostControlBusinessEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>> mapper =
        //        IncommingUserMapperConfig.ExpressionBuilder.CreateMapExpression(CostControlBusinessEntity.IncommingUser, CostControlBusinessEntity.IncommingUser);
        //    Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> mappedSelector = selector.Compose(mapper);
        //    return mappedSelector;
        //}


        public IEnumerable<CostControlBusinessEntity.IncommingUser> Get(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
                Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>> orderBy = null,
                ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
                int? pageNumber = null,
                int? pageSize = null)
            //{

            //Expression<Func<Category, bool>> mappedExpression = ExpressionMapper.GetMappedSelector<Category, CategoryViewModel>(whereCondition);

            //(new ParameterReplacer()).Visit(filter);
            //}

            => IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>, IEnumerable<CostControlBusinessEntity.IncommingUser>>(
                    Repository.Get(
                        IncommingUserIMapper.Map<Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter),
                        IncommingUserIMapper.Map<Func<IQueryable<CostControlEntity.IncommingUser>, IOrderedQueryable<CostControlEntity.IncommingUser>>>(orderBy),
                        IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties),
                        pageNumber,
                        pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> GetAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IncommingUserIMapper.Map<Task<IEnumerable<CostControlEntity.IncommingUser>>, Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
                Repository.GetAsync(
                    IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>, Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter),
                    IncommingUserIMapper.Map<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>>,
                    Func<IQueryable<CostControlEntity.IncommingUser>, IOrderedQueryable<CostControlEntity.IncommingUser>>>(orderBy),
                    IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.IncommingUser GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null)
        => id == null ? null : IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>
            (Repository.GetById(id, IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.IncommingUser> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(IncommingUserIMapper.Map<Task<Entity.Models.IncommingUser>, Task<IncommingUser>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>
            (await Repository.GetByIdAsync(id, IncommingUserIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IncommingUser>, IIncludableQueryable<CostControlEntity.IncommingUser, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IncommingUser> GetWithRawSql(string query, params object[] parameters)
        => IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>, IEnumerable<CostControlBusinessEntity.IncommingUser>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>, IEnumerable<CostControlBusinessEntity.IncommingUser>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.IncommingUser Add(CostControlBusinessEntity.IncommingUser entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = IncommingUserIMapper
                    .Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(
                        Repository.Add(IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser, CostControlEntity.IncommingUser>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.IncommingUser> AddAsync(CostControlBusinessEntity.IncommingUser entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var IncommingUser = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser, CostControlEntity.IncommingUser>(entity);

                var result = IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(Repository.Add(IncommingUser));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IncommingUser Update(CostControlBusinessEntity.IncommingUser entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.IncommingUser IncommingUser = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser, CostControlEntity.IncommingUser>(entity);

                var result = IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(Repository.Update(IncommingUser));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.IncommingUser> UpdateAsync(CostControlBusinessEntity.IncommingUser entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var IncommingUser = IncommingUserIMapper.Map<CostControlBusinessEntity.IncommingUser, CostControlEntity.IncommingUser>(entity);

                var result = IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(Repository.Update(IncommingUser));

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

        public CostControlBusinessEntity.IncommingUser SingleOrDefault(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
        => IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(
            Repository.SingleOrDefault(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IncommingUser> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IncommingUserIMapper.Map<Task<CostControlEntity.IncommingUser>, Task<CostControlBusinessEntity.IncommingUser>>(
                Repository.SingleOrDefaultAsync(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                    Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.IncommingUser FirstOrDefault(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
        => IncommingUserIMapper.Map<CostControlEntity.IncommingUser, CostControlBusinessEntity.IncommingUser>(
                Repository.SingleOrDefault(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                    Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IncommingUser> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IncommingUserIMapper.Map<Task<CostControlEntity.IncommingUser>, Task<CostControlBusinessEntity.IncommingUser>>(
                Repository.SingleOrDefaultAsync(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                    Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IncommingUser> AddRange(IEnumerable<CostControlBusinessEntity.IncommingUser> entities)
        {
            try
            {
                var result =
                IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
                      Repository.AddRange(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IncommingUser> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IncommingUserIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
                      Repository
                      .AddRange(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.IncommingUser> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter)
        {
            try
            {
                var result = IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
                    Repository.RemoveFiltered(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                        Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IncommingUserIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
                    Repository.RemoveFilteredAsync(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                    Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.IncommingUser> RemoveRange(IEnumerable<CostControlBusinessEntity.IncommingUser> entities)
        {
            try
            {
                var result = IncommingUserIMapper.Map<IEnumerable<CostControlBusinessEntity.IncommingUser>>(
                    Repository.RemoveRange(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IncommingUser>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.IncommingUser> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IncommingUserIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IncommingUser>>>(
                        Repository
                        .Remove(IncommingUserIMapper.Map<IEnumerable<CostControlEntity.IncommingUser>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        => await Repository.CountAsync(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
            Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null)
            => Repository.Count(IncommingUserIMapper.Map<Expression<Func<CostControlBusinessEntity.IncommingUser, bool>>,
                Expression<Func<CostControlEntity.IncommingUser, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
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
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IncommingUser, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.IncommingUser item, Expression<Func<CostControlBusinessEntity.IncommingUser, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.IncommingUser> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IOrderedQueryable<CostControlBusinessEntity.IncommingUser>> orderBy = null, ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IncommingUser>, IIncludableQueryable<CostControlBusinessEntity.IncommingUser, object>>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~IncommingUserLogic()
        {
            Dispose(false);
        }
    }
}