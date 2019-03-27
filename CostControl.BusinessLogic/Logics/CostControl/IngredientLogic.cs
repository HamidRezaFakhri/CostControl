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

    public class IngredientLogic : Base.IGenericLogic<CostControlBusinessEntity.Ingredient>, IDisposable
    {
        private MapperConfiguration IngredientMapperConfig { get; set; }

        private IMapper IngredientIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<CostControlEntity.Ingredient> Repository;

        public IngredientLogic()
        {
            IngredientMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            IngredientIMapper = IngredientMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Ingredient>();
        }

        public CostControlBusinessEntity.Ingredient Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> Remove(
            Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.Ingredient> result = null;

            var deleteLst = Repository.Get(IngredientIMapper
                                .Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Ingredient>)
                    .ForEach(s => result.Add(IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Ingredient> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> RemoveAsync(
            Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                              Repository
                              .Remove(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Ingredient Exists(object primaryKey)
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Ingredient> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Ingredient> Get(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IIncludableQueryable<CostControlBusinessEntity.Ingredient, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                           Repository.Get(
                               IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter),
                               IngredientIMapper.Map<Func<IQueryable<CostControlEntity.Ingredient>, IOrderedQueryable<CostControlEntity.Ingredient>>>(orderBy),
                               IngredientIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Ingredient>, IIncludableQueryable<CostControlEntity.Ingredient, object>>>>(includeProperties),
                               pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> GetAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IIncludableQueryable<CostControlBusinessEntity.Ingredient, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<IEnumerable<CostControlEntity.Ingredient>>, Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                           Repository.GetAsync(
                               IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter),
                               IngredientIMapper.Map<Func<IQueryable<CostControlEntity.Ingredient>, IOrderedQueryable<CostControlEntity.Ingredient>>>(orderBy),
                               IngredientIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Ingredient>, IIncludableQueryable<CostControlEntity.Ingredient, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Ingredient GetById<TKey>(TKey id)
        => id == null ? null : IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.GetById(id));

        public CostControlBusinessEntity.Ingredient GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IIncludableQueryable<CostControlBusinessEntity.Ingredient, object>>>> includeProperties = null)
        => id == null ? null : IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>
                       (Repository.GetById(id, IngredientIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Ingredient>, IIncludableQueryable<CostControlEntity.Ingredient, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Ingredient> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<CostControlBusinessEntity.Ingredient> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IIncludableQueryable<CostControlBusinessEntity.Ingredient, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>
                       (await Repository.GetByIdAsync(id, IngredientIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Ingredient>, IIncludableQueryable<CostControlEntity.Ingredient, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Ingredient> GetWithRawSql(string query, params object[] parameters)
        => IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Ingredient Add(CostControlBusinessEntity.Ingredient entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = IngredientIMapper
                    .Map<CostControlBusinessEntity.Ingredient>(
                        Repository.Add(IngredientIMapper.Map<CostControlEntity.Ingredient>(entity)));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Ingredient> AddAsync(CostControlBusinessEntity.Ingredient entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Ingredient = IngredientIMapper.Map<CostControlEntity.Ingredient>(entity);

            var result = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Add(Ingredient));
            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Ingredient Update(CostControlBusinessEntity.Ingredient entity)
        {
            if (entity == null)
            {
                return null;
            }

            var Ingredient = IngredientIMapper.Map<CostControlEntity.Ingredient>(entity);

            var result = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Update(Ingredient));
            Commit();

            return result;
        }

        public async Task<CostControlBusinessEntity.Ingredient> UpdateAsync(CostControlBusinessEntity.Ingredient entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Ingredient = IngredientIMapper.Map<CostControlEntity.Ingredient>(entity);

            var result = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Update(Ingredient));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query, params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public CostControlBusinessEntity.Ingredient SingleOrDefault(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(
                       Repository.SingleOrDefault(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Ingredient> SingleOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<CostControlBusinessEntity.Ingredient>>(
                           Repository.SingleOrDefaultAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Ingredient FirstOrDefault(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(
                           Repository.FirstOrDefault(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Ingredient> FirstOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<CostControlBusinessEntity.Ingredient>>(
                           Repository.FirstOrDefaultAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Ingredient LastOrDefault(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(
                           Repository.LastOrDefault(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Ingredient> LastOrDefaultAsync(
            Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<CostControlBusinessEntity.Ingredient>>(
                           Repository.LastOrDefaultAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Ingredient> AddRange(IEnumerable<CostControlBusinessEntity.Ingredient> entities)
        {
            IEnumerable<CostControlBusinessEntity.Ingredient> result =
            IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                  Repository.AddRange(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Ingredient> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IngredientIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                  Repository
                  .AddRange(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter)
        {
            var result = IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                Repository.RemoveFiltered(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> RemoveFilteredAsync(
            Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IngredientIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                Repository.RemoveFilteredAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> RemoveRange(IEnumerable<CostControlBusinessEntity.Ingredient> entities)
        {
            var result = IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                    Repository.RemoveRange(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> RemoveRangeAsync(
            IEnumerable<CostControlBusinessEntity.Ingredient> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IngredientIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                        Repository
                        .Remove(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public CostControlBusinessEntity.Ingredient Exists(params object[] primaryKey)
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Ingredient> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await IngredientIMapper.Map<Task<CostControlBusinessEntity.Ingredient>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => Repository.Exists(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => Repository.Count(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    IngredientMapperConfig = null;
                    IngredientIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => Repository.Any(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(CostControlBusinessEntity.Ingredient item,
            Expression<Func<CostControlBusinessEntity.Ingredient, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IIncludableQueryable<CostControlBusinessEntity.Ingredient, object>>>> includeProperties = null,
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

        ~IngredientLogic()
        {
            Dispose(false);
        }
    }
}