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

    public class DepoLogic : Base.IGenericLogic<CostControlBusinessEntity.Depo>, IDisposable
    {
        private MapperConfiguration DepoMapperConfig { get; set; }

        private IMapper DepoIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.Depo> Repository;

        public DepoLogic()
        {
            DepoMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            DepoIMapper = DepoMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Depo>();
        }

        public CostControlBusinessEntity.Depo Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Depo> Remove(
            Expression<Func<CostControlBusinessEntity.Depo, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.Depo> result = null;

            var deleteLst = Repository.Get(DepoIMapper
                                .Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Depo>)
                    .ForEach(s => result.Add(DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Depo> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.Depo, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                              Repository
                              .Remove(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Depo Exists(object primaryKey)
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Depo> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Depo> Get(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Depo>, IIncludableQueryable<CostControlBusinessEntity.Depo, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                           Repository.Get(
                               DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter),
                               DepoIMapper.Map<Func<IQueryable<CostControlEntity.Depo>, IOrderedQueryable<CostControlEntity.Depo>>>(orderBy),
                               DepoIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Depo>, IIncludableQueryable<CostControlEntity.Depo, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> GetAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Depo>, IIncludableQueryable<CostControlBusinessEntity.Depo, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<IEnumerable<CostControlEntity.Depo>>, Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                           Repository.GetAsync(
                               DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter),
                               DepoIMapper.Map<Func<IQueryable<CostControlEntity.Depo>, IOrderedQueryable<CostControlEntity.Depo>>>(orderBy),
                               DepoIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Depo>, IIncludableQueryable<CostControlEntity.Depo, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Depo GetById<TKey>(TKey id)
        => id == null ? null : DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.GetById(id));

        public CostControlBusinessEntity.Depo GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Depo>, IIncludableQueryable<CostControlBusinessEntity.Depo, object>>>> includeProperties = null)
        => id == null ? null : DepoIMapper.Map<CostControlBusinessEntity.Depo>
                       (Repository.GetById(id, DepoIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Depo>, IIncludableQueryable<CostControlEntity.Depo, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Depo> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : DepoIMapper.Map<CostControlBusinessEntity.Depo>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.Depo> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Depo>, IIncludableQueryable<CostControlBusinessEntity.Depo, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : DepoIMapper.Map<CostControlBusinessEntity.Depo>
                       (await Repository.GetByIdAsync(id, DepoIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Depo>, IIncludableQueryable<CostControlEntity.Depo, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Depo> GetWithRawSql(string query, params object[] parameters)
        => DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Depo Add(CostControlBusinessEntity.Depo entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = DepoIMapper
                    .Map<CostControlBusinessEntity.Depo>(
                        Repository.Add(DepoIMapper.Map<CostControlEntity.Depo>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Depo> AddAsync(CostControlBusinessEntity.Depo entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Depo = DepoIMapper.Map<CostControlEntity.Depo>(entity);

            var result = DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Add(Depo));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Depo Update(CostControlBusinessEntity.Depo entity)
        {
            if (entity == null)
            {
                return null;
            }

            var Depo = DepoIMapper.Map<CostControlEntity.Depo>(entity);

            var result = DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Update(Depo));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Depo> UpdateAsync(CostControlBusinessEntity.Depo entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Depo = DepoIMapper.Map<CostControlEntity.Depo>(entity);

            var result = DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Update(Depo));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.Depo SingleOrDefault(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(
                       Repository.SingleOrDefault(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Depo> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<CostControlBusinessEntity.Depo>>(
                           Repository.SingleOrDefaultAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Depo FirstOrDefault(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(
                           Repository.FirstOrDefault(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Depo> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<CostControlBusinessEntity.Depo>>(
                           Repository.FirstOrDefaultAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Depo LastOrDefault(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(
                           Repository.LastOrDefault(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Depo> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<CostControlBusinessEntity.Depo>>(
                           Repository.LastOrDefaultAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Depo> AddRange(IEnumerable<CostControlBusinessEntity.Depo> entities)
        {
            IEnumerable<CostControlBusinessEntity.Depo> result =
            DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                  Repository.AddRange(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Depo> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await DepoIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                  Repository
                  .AddRange(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Depo> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter)
        {
            var result = DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                Repository.RemoveFiltered(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.Depo, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await DepoIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                Repository.RemoveFilteredAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Depo> RemoveRange(IEnumerable<CostControlBusinessEntity.Depo> entities)
        {
            var result = DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                    Repository.RemoveRange(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.Depo> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await DepoIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                        Repository
                        .Remove(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Depo Exists(params object[] primaryKey)
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Depo> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await DepoIMapper.Map<Task<CostControlBusinessEntity.Depo>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => Repository.Exists(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => Repository.Count(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DepoMapperConfig = null;
                    DepoIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => Repository.Any(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.Depo item,
            Expression<Func<CostControlBusinessEntity.Depo, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Depo> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Depo>, IIncludableQueryable<CostControlBusinessEntity.Depo, object>>>> includeProperties = null,
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

        ~DepoLogic()
        {
            Dispose(false);
        }
    }
}