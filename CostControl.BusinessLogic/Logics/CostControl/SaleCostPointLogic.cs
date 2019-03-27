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

    public class SaleCostPointLogic : Base.IGenericLogic<CostControlBusinessEntity.SaleCostPoint>, IDisposable
    {
        private MapperConfiguration SaleCostPointMapperConfig { get; set; }

        private IMapper SaleCostPointIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.SaleCostPoint> Repository;

        public SaleCostPointLogic()
        {
            SaleCostPointMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            SaleCostPointIMapper = SaleCostPointMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.SaleCostPoint>();
        }

        public CostControlBusinessEntity.SaleCostPoint Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = SaleCostPointIMapper
                                .Map<CostControlEntity.SaleCostPoint, CostControlBusinessEntity.SaleCostPoint>(Repository.Remove(id));
                Commit();

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

            var deleteLst = Repository.Get(SaleCostPointIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>>,
                                    Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.SaleCostPoint>)
                    .ForEach(s => result.Add(SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Remove(s))));

                Commit();

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

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> RemoveAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
              Repository
              .Remove(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.SaleCostPoint Exists(object primaryKey)
        => SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.SaleCostPoint> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> Get(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
                Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>> orderBy = null,
                ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IIncludableQueryable<CostControlBusinessEntity.SaleCostPoint, object>>>> includeProperties = null,
                int? pageNumber = null,
                int? pageSize = null)
        => SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.Get(
                               SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter),
                               SaleCostPointIMapper.Map<Func<IQueryable<CostControlEntity.SaleCostPoint>, IOrderedQueryable<CostControlEntity.SaleCostPoint>>>(orderBy),
                               SaleCostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SaleCostPoint>, IIncludableQueryable<CostControlEntity.SaleCostPoint, object>>>>(includeProperties),
                               pageNumber,
                               pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> GetAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IIncludableQueryable<CostControlBusinessEntity.SaleCostPoint, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                           Repository.GetAsync(
                               SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter),
                               SaleCostPointIMapper.Map<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>>,
                               Func<IQueryable<CostControlEntity.SaleCostPoint>, IOrderedQueryable<CostControlEntity.SaleCostPoint>>>(orderBy),
                               SaleCostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SaleCostPoint>, IIncludableQueryable<CostControlEntity.SaleCostPoint, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.SaleCostPoint GetById<TKey>(TKey id)
        => id == null ? null : SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.GetById(id));

        public CostControlBusinessEntity.SaleCostPoint GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IIncludableQueryable<CostControlBusinessEntity.SaleCostPoint, object>>>> includeProperties = null)
        => id == null ? null : SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>
                       (Repository.GetById(id, SaleCostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SaleCostPoint>, IIncludableQueryable<CostControlEntity.SaleCostPoint, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.SaleCostPoint> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.SaleCostPoint> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IIncludableQueryable<CostControlBusinessEntity.SaleCostPoint, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>
                       (await Repository.GetByIdAsync(id, SaleCostPointIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.SaleCostPoint>, IIncludableQueryable<CostControlEntity.SaleCostPoint, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> GetWithRawSql(string query, params object[] parameters)
        => SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.SaleCostPoint Add(CostControlBusinessEntity.SaleCostPoint entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null)
            {
                return null;
            }

            var result = SaleCostPointIMapper
                .Map<CostControlBusinessEntity.SaleCostPoint>(
                    Repository.Add(SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> AddAsync(CostControlBusinessEntity.SaleCostPoint entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var SaleCostPoint = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint>(entity);

            var result = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Add(SaleCostPoint));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.SaleCostPoint Update(CostControlBusinessEntity.SaleCostPoint entity)
        {
            if (entity == null)
            {
                return null;
            }

            var SaleCostPoint = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint>(entity);

            var result = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Update(SaleCostPoint));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.SaleCostPoint> UpdateAsync(CostControlBusinessEntity.SaleCostPoint entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var SaleCostPoint = SaleCostPointIMapper.Map<CostControlEntity.SaleCostPoint>(entity);

            var result = SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Update(SaleCostPoint));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query,
            params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.SaleCostPoint SingleOrDefault(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        => SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(
                       Repository.SingleOrDefault(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.SaleCostPoint> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleCostPointIMapper.Map<Task<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.SingleOrDefaultAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.SaleCostPoint FirstOrDefault(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        => SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(
                           Repository.FirstOrDefault(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.SaleCostPoint> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleCostPointIMapper.Map<Task<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.FirstOrDefaultAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.SaleCostPoint LastOrDefault(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        => SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(
                           Repository.LastOrDefault(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.SaleCostPoint> LastOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleCostPointIMapper.Map<Task<CostControlBusinessEntity.SaleCostPoint>>(
                           Repository.LastOrDefaultAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> AddRange(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities)
        {
            var result =
                SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                      Repository.AddRange(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                      Repository
                      .AddRange(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> RemoveFiltered(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter)
        {
            var result = SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                    Repository.RemoveFiltered(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                    Repository.RemoveFilteredAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> RemoveRange(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities)
        {
            var result = SaleCostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>(
                    Repository.RemoveRange(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.SaleCostPoint> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await SaleCostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleCostPoint>>>(
                        Repository
                        .Remove(SaleCostPointIMapper.Map<IEnumerable<CostControlEntity.SaleCostPoint>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.SaleCostPoint Exists(params object[] primaryKey)
        => SaleCostPointIMapper.Map<CostControlBusinessEntity.SaleCostPoint>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.SaleCostPoint> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await SaleCostPointIMapper.Map<Task<CostControlBusinessEntity.SaleCostPoint>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        => Repository.Exists(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null)
        => Repository.Count(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));

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
        => Repository.Any(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.SaleCostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(SaleCostPointIMapper.Map<Expression<Func<CostControlEntity.SaleCostPoint, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.SaleCostPoint item, Expression<Func<CostControlBusinessEntity.SaleCostPoint, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.SaleCostPoint> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IOrderedQueryable<CostControlBusinessEntity.SaleCostPoint>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.SaleCostPoint>, IIncludableQueryable<CostControlBusinessEntity.SaleCostPoint, object>>>> includeProperties = null,
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

        ~SaleCostPointLogic()
        {
            Dispose(false);
        }
    }
}