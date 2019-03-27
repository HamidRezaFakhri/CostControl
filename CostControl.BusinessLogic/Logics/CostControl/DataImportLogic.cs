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

    public class DataImportLogic : Base.IGenericLogic<CostControlBusinessEntity.DataImport>, IDisposable
    {
        private MapperConfiguration DataImportMapperConfig { get; set; }

        private IMapper DataImportIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.DataImport> Repository;

        public DataImportLogic()
        {
            DataImportMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            DataImportIMapper = DataImportMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.DataImport>();
        }

        public CostControlBusinessEntity.DataImport Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> Remove(
            Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.DataImport> result = null;

            var deleteLst = Repository.Get(DataImportIMapper
                                .Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.DataImport>)
                    .ForEach(s => result.Add(DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.DataImport> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                              Repository
                              .Remove(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.DataImport Exists(object primaryKey)
        => DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.DataImport> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.DataImport> Get(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                           Repository.Get(
                               DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter),
                               DataImportIMapper.Map<Func<IQueryable<CostControlEntity.DataImport>, IOrderedQueryable<CostControlEntity.DataImport>>>(orderBy),
                               DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> GetAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DataImportIMapper.Map<Task<IEnumerable<CostControlEntity.DataImport>>, Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                           Repository.GetAsync(
                               DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter),
                               DataImportIMapper.Map<Func<IQueryable<CostControlEntity.DataImport>, IOrderedQueryable<CostControlEntity.DataImport>>>(orderBy),
                               DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.DataImport GetById<TKey>(TKey id)
        => id == null ? null : DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.GetById(id));

        public CostControlBusinessEntity.DataImport GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null)
        => id == null ? null : DataImportIMapper.Map<CostControlBusinessEntity.DataImport>
                       (Repository.GetById(id, DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.DataImport> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.DataImport> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : DataImportIMapper.Map<CostControlBusinessEntity.DataImport>
                       (await Repository.GetByIdAsync(id, DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.DataImport> GetWithRawSql(string query, params object[] parameters)
        => DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.DataImport Add(CostControlBusinessEntity.DataImport entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = DataImportIMapper
                    .Map<CostControlBusinessEntity.DataImport>(
                        Repository.Add(DataImportIMapper.Map<CostControlEntity.DataImport>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.DataImport> AddAsync(CostControlBusinessEntity.DataImport entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var DataImport = DataImportIMapper.Map<CostControlEntity.DataImport>(entity);

            var result = DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Add(DataImport));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.DataImport Update(CostControlBusinessEntity.DataImport entity)
        {
            if (entity == null)
            {
                return null;
            }

            var DataImport = DataImportIMapper.Map<CostControlEntity.DataImport>(entity);

            var result = DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Update(DataImport));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.DataImport> UpdateAsync(CostControlBusinessEntity.DataImport entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var DataImport = DataImportIMapper.Map<CostControlEntity.DataImport>(entity);

            var result = DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Update(DataImport));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.DataImport SingleOrDefault(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        => DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(
                       Repository.SingleOrDefault(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.DataImport> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DataImportIMapper.Map<Task<CostControlBusinessEntity.DataImport>>(
                           Repository.SingleOrDefaultAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.DataImport FirstOrDefault(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        => DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(
                           Repository.FirstOrDefault(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.DataImport> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DataImportIMapper.Map<Task<CostControlBusinessEntity.DataImport>>(
                           Repository.FirstOrDefaultAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.DataImport LastOrDefault(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        => DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(
                           Repository.LastOrDefault(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.DataImport> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DataImportIMapper.Map<Task<CostControlBusinessEntity.DataImport>>(
                           Repository.LastOrDefaultAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.DataImport> AddRange(IEnumerable<CostControlBusinessEntity.DataImport> entities)
        {
            IEnumerable<CostControlBusinessEntity.DataImport> result =
            DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                  Repository.AddRange(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.DataImport> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await DataImportIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                  Repository
                  .AddRange(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> RemoveFiltered(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter)
        {
            var result = DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                Repository.RemoveFiltered(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await DataImportIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                Repository.RemoveFilteredAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> RemoveRange(IEnumerable<CostControlBusinessEntity.DataImport> entities)
        {
            var result = DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                    Repository.RemoveRange(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.DataImport> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await DataImportIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                        Repository
                        .Remove(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.DataImport Exists(params object[] primaryKey)
        => DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.DataImport> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await DataImportIMapper.Map<Task<CostControlBusinessEntity.DataImport>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        => Repository.Exists(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        => Repository.Count(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DataImportMapperConfig = null;
                    DataImportIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        => Repository.Any(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.DataImport item,
            Expression<Func<CostControlBusinessEntity.DataImport, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
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

        public int GetData()
        {
            return Repository.RunRawSql("EXEC dbo.Sp_GetData");
        }

        ~DataImportLogic()
        {
            Dispose(false);
        }
    }
}