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

    public class BuffetLogic : Base.IGenericLogic<CostControlBusinessEntity.Buffet>, IDisposable
    {
        private MapperConfiguration BuffetMapperConfig { get; set; }

        private IMapper BuffetIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.Buffet> Repository;

        public BuffetLogic()
        {
            BuffetMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            BuffetIMapper = BuffetMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Buffet>();
        }

        public CostControlBusinessEntity.Buffet Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> Remove(
            Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.Buffet> result = null;

            var deleteLst = Repository.Get(BuffetIMapper
                                .Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Buffet>)
                    .ForEach(s => result.Add(BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Buffet> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                              Repository
                              .Remove(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Buffet Exists(object primaryKey)
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Buffet> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Buffet> Get(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                           Repository.Get(
                               BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter),
                               BuffetIMapper.Map<Func<IQueryable<CostControlEntity.Buffet>, IOrderedQueryable<CostControlEntity.Buffet>>>(orderBy),
                               BuffetIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> GetAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<IEnumerable<CostControlEntity.Buffet>>, Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                           Repository.GetAsync(
                               BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter),
                               BuffetIMapper.Map<Func<IQueryable<CostControlEntity.Buffet>, IOrderedQueryable<CostControlEntity.Buffet>>>(orderBy),
                               BuffetIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Buffet GetById<TKey>(TKey id)
        => id == null ? null : BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.GetById(id));

        public CostControlBusinessEntity.Buffet GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>>>> includeProperties = null)
        => id == null ? null : BuffetIMapper.Map<CostControlBusinessEntity.Buffet>
                       (Repository.GetById(id, BuffetIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Buffet> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.Buffet> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : BuffetIMapper.Map<CostControlBusinessEntity.Buffet>
                       (await Repository.GetByIdAsync(id, BuffetIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Buffet> GetWithRawSql(string query, params object[] parameters)
        => BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Buffet Add(CostControlBusinessEntity.Buffet entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = BuffetIMapper
                    .Map<CostControlBusinessEntity.Buffet>(
                        Repository.Add(BuffetIMapper.Map<CostControlEntity.Buffet>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Buffet> AddAsync(CostControlBusinessEntity.Buffet entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Buffet = BuffetIMapper.Map<CostControlEntity.Buffet>(entity);

            var result = BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Add(Buffet));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Buffet Update(CostControlBusinessEntity.Buffet entity)
        {
            if (entity == null)
            {
                return null;
            }

            var Buffet = BuffetIMapper.Map<CostControlEntity.Buffet>(entity);

            var result = BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Update(Buffet));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Buffet> UpdateAsync(CostControlBusinessEntity.Buffet entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Buffet = BuffetIMapper.Map<CostControlEntity.Buffet>(entity);

            var result = BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Update(Buffet));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.Buffet SingleOrDefault(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(
                       Repository.SingleOrDefault(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Buffet> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<CostControlBusinessEntity.Buffet>>(
                           Repository.SingleOrDefaultAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Buffet FirstOrDefault(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(
                           Repository.FirstOrDefault(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Buffet> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<CostControlBusinessEntity.Buffet>>(
                           Repository.FirstOrDefaultAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Buffet LastOrDefault(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(
                           Repository.LastOrDefault(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Buffet> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<CostControlBusinessEntity.Buffet>>(
                           Repository.LastOrDefaultAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Buffet> AddRange(IEnumerable<CostControlBusinessEntity.Buffet> entities)
        {
            IEnumerable<CostControlBusinessEntity.Buffet> result =
            BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                  Repository.AddRange(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Buffet> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await BuffetIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                  Repository
                  .AddRange(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter)
        {
            var result = BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                Repository.RemoveFiltered(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await BuffetIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                Repository.RemoveFilteredAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> RemoveRange(IEnumerable<CostControlBusinessEntity.Buffet> entities)
        {
            var result = BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                    Repository.RemoveRange(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.Buffet> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await BuffetIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                        Repository
                        .Remove(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Buffet Exists(params object[] primaryKey)
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Buffet> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await BuffetIMapper.Map<Task<CostControlBusinessEntity.Buffet>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => Repository.Exists(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => Repository.Count(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    BuffetMapperConfig = null;
                    BuffetIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => Repository.Any(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.Buffet item,
            Expression<Func<CostControlBusinessEntity.Buffet, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>>>> includeProperties = null,
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
        
        ~BuffetLogic()
        {
            Dispose(false);
        }
    }
}