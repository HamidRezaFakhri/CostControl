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

    public class InventoryLogic : Base.IGenericLogic<CostControlBusinessEntity.Inventory>, IDisposable
    {
        private MapperConfiguration InventoryMapperConfig { get; set; }

        private IMapper InventoryIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.Inventory> Repository;

        public InventoryLogic()
        {
            InventoryMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            InventoryIMapper = InventoryMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Inventory>();
        }

        public CostControlBusinessEntity.Inventory Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> Remove(
            Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.Inventory> result = null;

            var deleteLst = Repository.Get(InventoryIMapper
                                .Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Inventory>)
                    .ForEach(s => result.Add(InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Inventory> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                              Repository
                              .Remove(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Inventory Exists(object primaryKey)
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Inventory> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Inventory> Get(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                           Repository.Get(
                               InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter),
                               InventoryIMapper.Map<Func<IQueryable<CostControlEntity.Inventory>, IOrderedQueryable<CostControlEntity.Inventory>>>(orderBy),
                               InventoryIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> GetAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<IEnumerable<CostControlEntity.Inventory>>, Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                           Repository.GetAsync(
                               InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter),
                               InventoryIMapper.Map<Func<IQueryable<CostControlEntity.Inventory>, IOrderedQueryable<CostControlEntity.Inventory>>>(orderBy),
                               InventoryIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Inventory GetById<TKey>(TKey id)
        => id == null ? null : InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.GetById(id));

        public CostControlBusinessEntity.Inventory GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>>>> includeProperties = null)
        => id == null ? null : InventoryIMapper.Map<CostControlBusinessEntity.Inventory>
                       (Repository.GetById(id, InventoryIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Inventory> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.Inventory> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : InventoryIMapper.Map<CostControlBusinessEntity.Inventory>
                       (await Repository.GetByIdAsync(id, InventoryIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Inventory> GetWithRawSql(string query, params object[] parameters)
        => InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Inventory Add(CostControlBusinessEntity.Inventory entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = InventoryIMapper
                    .Map<CostControlBusinessEntity.Inventory>(
                        Repository.Add(InventoryIMapper.Map<CostControlEntity.Inventory>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Inventory> AddAsync(CostControlBusinessEntity.Inventory entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Inventory = InventoryIMapper.Map<CostControlEntity.Inventory>(entity);

            var result = InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Add(Inventory));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Inventory Update(CostControlBusinessEntity.Inventory entity)
        {
            if (entity == null)
            {
                return null;
            }

            var Inventory = InventoryIMapper.Map<CostControlEntity.Inventory>(entity);

            var result = InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Update(Inventory));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Inventory> UpdateAsync(CostControlBusinessEntity.Inventory entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Inventory = InventoryIMapper.Map<CostControlEntity.Inventory>(entity);

            var result = InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Update(Inventory));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.Inventory SingleOrDefault(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(
                       Repository.SingleOrDefault(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Inventory> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<CostControlBusinessEntity.Inventory>>(
                           Repository.SingleOrDefaultAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Inventory FirstOrDefault(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(
                           Repository.FirstOrDefault(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Inventory> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<CostControlBusinessEntity.Inventory>>(
                           Repository.FirstOrDefaultAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Inventory LastOrDefault(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(
                           Repository.LastOrDefault(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Inventory> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<CostControlBusinessEntity.Inventory>>(
                           Repository.LastOrDefaultAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Inventory> AddRange(IEnumerable<CostControlBusinessEntity.Inventory> entities)
        {
            IEnumerable<CostControlBusinessEntity.Inventory> result =
            InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                  Repository.AddRange(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Inventory> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await InventoryIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                  Repository
                  .AddRange(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter)
        {
            var result = InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                Repository.RemoveFiltered(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await InventoryIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                Repository.RemoveFilteredAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> RemoveRange(IEnumerable<CostControlBusinessEntity.Inventory> entities)
        {
            var result = InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                    Repository.RemoveRange(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.Inventory> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await InventoryIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                        Repository
                        .Remove(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Inventory Exists(params object[] primaryKey)
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Inventory> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await InventoryIMapper.Map<Task<CostControlBusinessEntity.Inventory>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => Repository.Exists(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => Repository.Count(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    InventoryMapperConfig = null;
                    InventoryIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => Repository.Any(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.Inventory item,
            Expression<Func<CostControlBusinessEntity.Inventory, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>>>> includeProperties = null,
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

        ~InventoryLogic()
        {
            Dispose(false);
        }
    }
}