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
using System.Threading;
using System.Threading.Tasks;
using CostControlBusinessEntity = CostControl.BusinessEntity.Models.CostControl;
using CostControlEntity = CostControl.Entity.Models.CostControl;

namespace CostControl.BusinessLogic.Logics.CostControl
{
    public class OverCostTypeLogic : IGenericLogic<CostControlBusinessEntity.OverCostType>, IDisposable
    {
        private MapperConfiguration OverCostTypeMapperConfig { get; set; }

        private IMapper OverCostTypeIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.OverCostType> Repository;

        public OverCostTypeLogic()
        {
            OverCostTypeMapperConfig = new AutoMapperConfiguration().Configure();
            OverCostTypeIMapper = OverCostTypeMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.OverCostType>();
        }

        public CostControlBusinessEntity.OverCostType Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> Remove(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.OverCostType> result = null;

            var deleteLst = Repository.Get(OverCostTypeIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                                    Expression<Func<CostControlEntity.OverCostType, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.OverCostType>)
                    .ForEach(s => result.Add(OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.OverCostType> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> RemoveAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                  Repository
                  .Remove(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.OverCostType Exists(object primaryKey)
            => OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.OverCostType> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.OverCostType> Get(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>, IEnumerable<CostControlBusinessEntity.OverCostType>>(
                Repository.Get(
                    OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>, Expression<Func<CostControlEntity.OverCostType, bool>>>(filter),
                    OverCostTypeIMapper.Map<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>>,
                    Func<IQueryable<CostControlEntity.OverCostType>, IOrderedQueryable<CostControlEntity.OverCostType>>>(orderBy),
                    OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties),
                    pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> GetAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlEntity.OverCostType>>, Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                Repository.GetAsync(
                    OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>, Expression<Func<CostControlEntity.OverCostType, bool>>>(filter),
                    OverCostTypeIMapper.Map<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>>,
                    Func<IQueryable<CostControlEntity.OverCostType>, IOrderedQueryable<CostControlEntity.OverCostType>>>(orderBy),
                    OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.OverCostType GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null)
        => id == null ? null : OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>
            (Repository.GetById(id, OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.OverCostType> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(OverCostTypeIMapper.Map<Task<Entity.Models.OverCostType>, Task<OverCostType>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>
            (await Repository.GetByIdAsync(id, OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.OverCostType> GetWithRawSql(string query, params object[] parameters)
        => OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>, IEnumerable<CostControlBusinessEntity.OverCostType>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>, IEnumerable<CostControlBusinessEntity.OverCostType>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.OverCostType Add(CostControlBusinessEntity.OverCostType entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = OverCostTypeIMapper
                    .Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(
                        Repository.Add(OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType, CostControlEntity.OverCostType>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.OverCostType> AddAsync(CostControlBusinessEntity.OverCostType entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var OverCostType = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType, CostControlEntity.OverCostType>(entity);

                var result = OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(Repository.Add(OverCostType));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.OverCostType Update(CostControlBusinessEntity.OverCostType entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.OverCostType OverCostType = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType, CostControlEntity.OverCostType>(entity);

                var result = OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(Repository.Update(OverCostType));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.OverCostType> UpdateAsync(CostControlBusinessEntity.OverCostType entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var OverCostType = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType, CostControlEntity.OverCostType>(entity);

                var result = OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(Repository.Update(OverCostType));

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

        public CostControlBusinessEntity.OverCostType SingleOrDefault(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(
            Repository.SingleOrDefault(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCostType> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<CostControlEntity.OverCostType>, Task<CostControlBusinessEntity.OverCostType>>(
                Repository.SingleOrDefaultAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                    Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.OverCostType FirstOrDefault(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => OverCostTypeIMapper.Map<CostControlEntity.OverCostType, CostControlBusinessEntity.OverCostType>(
                Repository.SingleOrDefault(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                    Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCostType> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<CostControlEntity.OverCostType>, Task<CostControlBusinessEntity.OverCostType>>(
                Repository.SingleOrDefaultAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                    Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.OverCostType> AddRange(IEnumerable<CostControlBusinessEntity.OverCostType> entities)
        {
            try
            {
                var result =
                OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                      Repository.AddRange(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.OverCostType> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                      Repository
                      .AddRange(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> RemoveFiltered(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter)
        {
            try
            {
                var result = OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                    Repository.RemoveFiltered(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                        Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                    Repository.RemoveFilteredAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                    Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> RemoveRange(IEnumerable<CostControlBusinessEntity.OverCostType> entities)
        {
            try
            {
                var result = OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                    Repository.RemoveRange(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.OverCostType> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                        Repository
                        .Remove(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.OverCostType Exists(params object[] primaryKey)
        => OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.OverCostType> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await OverCostTypeIMapper.Map<Task<CostControlBusinessEntity.OverCostType>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => Repository.Exists(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
            Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
            => Repository.Count(OverCostTypeIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCostType, bool>>,
                Expression<Func<CostControlEntity.OverCostType, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    OverCostTypeMapperConfig = null;
                    OverCostTypeIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.OverCostType item, Expression<Func<CostControlBusinessEntity.OverCostType, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>> orderBy = null, ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~OverCostTypeLogic()
        {
            Dispose(false);
        }
    }
}