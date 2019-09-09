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

    public class IntakeRemittanceItemLogLogic : Base.IGenericLogic<CostControlBusinessEntity.IntakeRemittanceItemLog>, IDisposable
    {
        private MapperConfiguration IntakeRemittanceItemLogMapperConfig { get; set; }

        private IMapper IntakeRemittanceItemLogIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.IntakeRemittanceItemLog> Repository;

        public IntakeRemittanceItemLogLogic()
        {
            IntakeRemittanceItemLogMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            IntakeRemittanceItemLogIMapper = IntakeRemittanceItemLogMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.IntakeRemittanceItemLog>();
        }

        public CostControlBusinessEntity.IntakeRemittanceItemLog Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> Remove(
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.IntakeRemittanceItemLog> result = null;

            var deleteLst = Repository.Get(IntakeRemittanceItemLogIMapper
                                .Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.IntakeRemittanceItemLog>)
                    .ForEach(s => result.Add(IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                              Repository
                              .Remove(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.IntakeRemittanceItemLog Exists(object primaryKey)
        => IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> Get(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                           Repository.Get(
                               IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter),
                               IntakeRemittanceItemLogIMapper.Map<Func<IQueryable<CostControlEntity.IntakeRemittanceItemLog>, IOrderedQueryable<CostControlEntity.IntakeRemittanceItemLog>>>(orderBy),
                               IntakeRemittanceItemLogIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItemLog, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>> GetAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemLogIMapper.Map<Task<IEnumerable<CostControlEntity.IntakeRemittanceItemLog>>, Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>>(
                           Repository.GetAsync(
                               IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter),
                               IntakeRemittanceItemLogIMapper.Map<Func<IQueryable<CostControlEntity.IntakeRemittanceItemLog>, IOrderedQueryable<CostControlEntity.IntakeRemittanceItemLog>>>(orderBy),
                               IntakeRemittanceItemLogIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItemLog, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.IntakeRemittanceItemLog GetById<TKey>(TKey id)
        => id == null ? null : IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.GetById(id));

        public CostControlBusinessEntity.IntakeRemittanceItemLog GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog, object>>>> includeProperties = null)
        => id == null ? null : IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>
                       (Repository.GetById(id, IntakeRemittanceItemLogIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItemLog, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>
                       (await Repository.GetByIdAsync(id, IntakeRemittanceItemLogIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItemLog, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> GetWithRawSql(string query, params object[] parameters)
        => IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.IntakeRemittanceItemLog Add(CostControlBusinessEntity.IntakeRemittanceItemLog entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = IntakeRemittanceItemLogIMapper
                    .Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(
                        Repository.Add(IntakeRemittanceItemLogIMapper.Map<CostControlEntity.IntakeRemittanceItemLog>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> AddAsync(CostControlBusinessEntity.IntakeRemittanceItemLog entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var IntakeRemittanceItemLog = IntakeRemittanceItemLogIMapper.Map<CostControlEntity.IntakeRemittanceItemLog>(entity);

            var result = IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Add(IntakeRemittanceItemLog));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.IntakeRemittanceItemLog Update(CostControlBusinessEntity.IntakeRemittanceItemLog entity)
        {
            if (entity == null)
            {
                return null;
            }

            var IntakeRemittanceItemLog = IntakeRemittanceItemLogIMapper.Map<CostControlEntity.IntakeRemittanceItemLog>(entity);

            var result = IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Update(IntakeRemittanceItemLog));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> UpdateAsync(CostControlBusinessEntity.IntakeRemittanceItemLog entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var IntakeRemittanceItemLog = IntakeRemittanceItemLogIMapper.Map<CostControlEntity.IntakeRemittanceItemLog>(entity);

            var result = IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Update(IntakeRemittanceItemLog));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.IntakeRemittanceItemLog SingleOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null)
        => IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(
                       Repository.SingleOrDefault(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemLogIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                           Repository.SingleOrDefaultAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.IntakeRemittanceItemLog FirstOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null)
        => IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(
                           Repository.FirstOrDefault(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemLogIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                           Repository.FirstOrDefaultAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.IntakeRemittanceItemLog LastOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null)
        => IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(
                           Repository.LastOrDefault(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemLogIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                           Repository.LastOrDefaultAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> AddRange(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> entities)
        {
            IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> result =
            IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                  Repository.AddRange(IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItemLog>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IntakeRemittanceItemLogIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>>(
                  Repository
                  .AddRange(IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItemLog>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter)
        {
            var result = IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                Repository.RemoveFiltered(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IntakeRemittanceItemLogIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>>(
                Repository.RemoveFilteredAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> RemoveRange(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> entities)
        {
            var result = IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>(
                    Repository.RemoveRange(IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItemLog>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IntakeRemittanceItemLogIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog>>>(
                        Repository
                        .Remove(IntakeRemittanceItemLogIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItemLog>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.IntakeRemittanceItemLog Exists(params object[] primaryKey)
        => IntakeRemittanceItemLogIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItemLog>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItemLog> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await IntakeRemittanceItemLogIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItemLog>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null)
        => Repository.Exists(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null)
        => Repository.Count(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    IntakeRemittanceItemLogMapperConfig = null;
                    IntakeRemittanceItemLogIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null)
        => Repository.Any(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(IntakeRemittanceItemLogIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItemLog, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.IntakeRemittanceItemLog item,
            Expression<Func<CostControlBusinessEntity.IntakeRemittanceItemLog, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItemLog> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItemLog, object>>>> includeProperties = null,
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

        ~IntakeRemittanceItemLogLogic()
        {
            Dispose(false);
        }
    }
}