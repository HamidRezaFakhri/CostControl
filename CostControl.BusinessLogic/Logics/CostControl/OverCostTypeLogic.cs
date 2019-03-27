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

    public class OverCostTypeLogic : Base.IGenericLogic<CostControlBusinessEntity.OverCostType>, IDisposable
    {
        private MapperConfiguration OverCostTypeMapperConfig { get; set; }

        private IMapper OverCostTypeIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.OverCostType> Repository;

        public OverCostTypeLogic()
        {
            OverCostTypeMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            OverCostTypeIMapper = OverCostTypeMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.OverCostType>();
        }

        public CostControlBusinessEntity.OverCostType Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> Remove(
            Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.OverCostType> result = null;

            var deleteLst = Repository.Get(OverCostTypeIMapper
                                .Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.OverCostType>)
                    .ForEach(s => result.Add(OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.OverCostType> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                              Repository
                              .Remove(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
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
        => OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                           Repository.Get(
                               OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter),
                               OverCostTypeIMapper.Map<Func<IQueryable<CostControlEntity.OverCostType>, IOrderedQueryable<CostControlEntity.OverCostType>>>(orderBy),
                               OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> GetAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlEntity.OverCostType>>, Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                           Repository.GetAsync(
                               OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter),
                               OverCostTypeIMapper.Map<Func<IQueryable<CostControlEntity.OverCostType>, IOrderedQueryable<CostControlEntity.OverCostType>>>(orderBy),
                               OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.OverCostType GetById<TKey>(TKey id)
        => id == null ? null : OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.GetById(id));

        public CostControlBusinessEntity.OverCostType GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null)
        => id == null ? null : OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>
                       (Repository.GetById(id, OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.OverCostType> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.OverCostType> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>
                       (await Repository.GetByIdAsync(id, OverCostTypeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.OverCostType>, IIncludableQueryable<CostControlEntity.OverCostType, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.OverCostType> GetWithRawSql(string query, params object[] parameters)
        => OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.OverCostType Add(CostControlBusinessEntity.OverCostType entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = OverCostTypeIMapper
                    .Map<CostControlBusinessEntity.OverCostType>(
                        Repository.Add(OverCostTypeIMapper.Map<CostControlEntity.OverCostType>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.OverCostType> AddAsync(CostControlBusinessEntity.OverCostType entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var OverCostType = OverCostTypeIMapper.Map<CostControlEntity.OverCostType>(entity);

            var result = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Add(OverCostType));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.OverCostType Update(CostControlBusinessEntity.OverCostType entity)
        {
            if (entity == null)
            {
                return null;
            }

            var OverCostType = OverCostTypeIMapper.Map<CostControlEntity.OverCostType>(entity);

            var result = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Update(OverCostType));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.OverCostType> UpdateAsync(CostControlBusinessEntity.OverCostType entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var OverCostType = OverCostTypeIMapper.Map<CostControlEntity.OverCostType>(entity);

            var result = OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(Repository.Update(OverCostType));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.OverCostType SingleOrDefault(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(
                       Repository.SingleOrDefault(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCostType> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<CostControlBusinessEntity.OverCostType>>(
                           Repository.SingleOrDefaultAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.OverCostType FirstOrDefault(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(
                           Repository.FirstOrDefault(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCostType> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<CostControlBusinessEntity.OverCostType>>(
                           Repository.FirstOrDefaultAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.OverCostType LastOrDefault(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => OverCostTypeIMapper.Map<CostControlBusinessEntity.OverCostType>(
                           Repository.LastOrDefault(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCostType> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostTypeIMapper.Map<Task<CostControlBusinessEntity.OverCostType>>(
                           Repository.LastOrDefaultAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.OverCostType> AddRange(IEnumerable<CostControlBusinessEntity.OverCostType> entities)
        {
            IEnumerable<CostControlBusinessEntity.OverCostType> result =
            OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                  Repository.AddRange(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.OverCostType> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                  Repository
                  .AddRange(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> RemoveFiltered(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter)
        {
            var result = OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                Repository.RemoveFiltered(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                Repository.RemoveFilteredAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> RemoveRange(IEnumerable<CostControlBusinessEntity.OverCostType> entities)
        {
            var result = OverCostTypeIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCostType>>(
                    Repository.RemoveRange(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCostType>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.OverCostType> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await OverCostTypeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCostType>>>(
                        Repository
                        .Remove(OverCostTypeIMapper.Map<IEnumerable<CostControlEntity.OverCostType>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
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
        => await Repository.CountAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null)
        => Repository.Count(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
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
        => Repository.Any(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.OverCostType, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(OverCostTypeIMapper.Map<Expression<Func<CostControlEntity.OverCostType, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.OverCostType item,
            Expression<Func<CostControlBusinessEntity.OverCostType, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.OverCostType> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.OverCostType>, IOrderedQueryable<CostControlBusinessEntity.OverCostType>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.OverCostType>, IIncludableQueryable<CostControlBusinessEntity.OverCostType, object>>>> includeProperties = null,
            int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            var commit = _unitOfWork.Commit();

            if (commit < 0)
            {
                throw new Exception("Commit failed!");
            }

            return commit;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var commit = CommitAsync(cancellationToken);

            if (commit.Result < 0)
            {
                throw new Exception("Commit failed!");
            }

            return await commit;
        }

        ~OverCostTypeLogic()
        {
            Dispose(false);
        }
    }
}