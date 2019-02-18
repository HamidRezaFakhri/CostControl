using AutoMapper;
using CostControl.BusinessLogic.Logics.Base;
using CostControl.BusinessLogic.Mapper;
using CostControl.Data.DAL;
using CostControl.Data.Repository;
using CostControl.Data.UnitOfWork;
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
    public class IngredientLogic : IGenericLogic<CostControlBusinessEntity.Ingredient>, IDisposable
    {
        private MapperConfiguration IngredientMapperConfig { get; set; }

        private IMapper IngredientIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Ingredient> Repository;

        public IngredientLogic()
        {
            IngredientMapperConfig = new AutoMapperConfiguration().Configure();
            IngredientIMapper = IngredientMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Ingredient>();
        }

        public CostControlBusinessEntity.Ingredient Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> Remove(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Ingredient> result = null;

            var deleteLst = Repository.Get(IngredientIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                                    Expression<Func<CostControlEntity.Ingredient, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Ingredient>)
                    .ForEach(s => result.Add(IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Ingredient> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                  Repository
                  .Remove(IngredientIMapper.Map<Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Ingredient Exists(object primaryKey)
            => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Ingredient> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => IngredientIMapper.Map<CostControlBusinessEntity.Ingredient>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Ingredient> Get(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.Ingredient, object>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>, IEnumerable<CostControlBusinessEntity.Ingredient>>(
                Repository.Get(
                    IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>, Expression<Func<CostControlEntity.Ingredient, bool>>>(filter),
                    IngredientIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>>,
                    Func<IQueryable<CostControlEntity.Ingredient>, IOrderedQueryable<CostControlEntity.Ingredient>>>(orderBy),
                    IngredientIMapper.Map<List<Expression<Func<CostControlEntity.Ingredient, object>>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> GetAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.Ingredient, object>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<IEnumerable<CostControlEntity.Ingredient>>, Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                Repository.GetAsync(
                    IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>, Expression<Func<CostControlEntity.Ingredient, bool>>>(filter),
                    IngredientIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>>,
                    Func<IQueryable<CostControlEntity.Ingredient>, IOrderedQueryable<CostControlEntity.Ingredient>>>(orderBy),
                    IngredientIMapper.Map<List<Expression<Func<CostControlEntity.Ingredient, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Ingredient GetById(object id,
            List<Expression<Func<CostControlBusinessEntity.Ingredient, object>>> includeProperties = null)
        => id == null ? null : IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>
            (Repository.GetById(id, IngredientIMapper.Map<List<Expression<Func<CostControlEntity.Ingredient, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Ingredient> GetByIdAsync(object id,
            List<Expression<Func<CostControlBusinessEntity.Ingredient, object>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(IngredientIMapper.Map<Task<Entity.Models.Ingredient>, Task<Ingredient>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>
            (await Repository.GetByIdAsync(id, IngredientIMapper.Map<List<Expression<Func<CostControlEntity.Ingredient, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Ingredient> GetWithRawSql(string query, params object[] parameters)
        => IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>, IEnumerable<CostControlBusinessEntity.Ingredient>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>, IEnumerable<CostControlBusinessEntity.Ingredient>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Ingredient Add(CostControlBusinessEntity.Ingredient entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = IngredientIMapper
                    .Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(
                        Repository.Add(IngredientIMapper.Map<CostControlBusinessEntity.Ingredient, CostControlEntity.Ingredient>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Ingredient> AddAsync(CostControlBusinessEntity.Ingredient entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Ingredient = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient, CostControlEntity.Ingredient>(entity);

                var result = IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(Repository.Add(Ingredient));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Ingredient Update(CostControlBusinessEntity.Ingredient entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Ingredient Ingredient = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient, CostControlEntity.Ingredient>(entity);

                var result = IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(Repository.Update(Ingredient));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Ingredient> UpdateAsync(CostControlBusinessEntity.Ingredient entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Ingredient = IngredientIMapper.Map<CostControlBusinessEntity.Ingredient, CostControlEntity.Ingredient>(entity);

                var result = IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(Repository.Update(Ingredient));

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

        public CostControlBusinessEntity.Ingredient SingleOrDefault(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(
            Repository.SingleOrDefault(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Ingredient> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<CostControlEntity.Ingredient>, Task<CostControlBusinessEntity.Ingredient>>(
                Repository.SingleOrDefaultAsync(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                    Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Ingredient FirstOrDefault(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        => IngredientIMapper.Map<CostControlEntity.Ingredient, CostControlBusinessEntity.Ingredient>(
                Repository.SingleOrDefault(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                    Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Ingredient> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IngredientIMapper.Map<Task<CostControlEntity.Ingredient>, Task<CostControlBusinessEntity.Ingredient>>(
                Repository.SingleOrDefaultAsync(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                    Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Ingredient> AddRange(IEnumerable<CostControlBusinessEntity.Ingredient> entities)
        {
            try
            {
                var result =
                IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                      Repository.AddRange(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Ingredient> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IngredientIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                      Repository
                      .AddRange(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter)
        {
            try
            {
                var result = IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                    Repository.RemoveFiltered(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                        Expression<Func<CostControlEntity.Ingredient, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IngredientIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                    Repository.RemoveFilteredAsync(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                    Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> RemoveRange(IEnumerable<CostControlBusinessEntity.Ingredient> entities)
        {
            try
            {
                var result = IngredientIMapper.Map<IEnumerable<CostControlBusinessEntity.Ingredient>>(
                    Repository.RemoveRange(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Ingredient>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Ingredient> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IngredientIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Ingredient>>>(
                        Repository
                        .Remove(IngredientIMapper.Map<IEnumerable<CostControlEntity.Ingredient>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        => await Repository.CountAsync(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
            Expression<Func<CostControlEntity.Ingredient, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
            => Repository.Count(IngredientIMapper.Map<Expression<Func<CostControlBusinessEntity.Ingredient, bool>>,
                Expression<Func<CostControlEntity.Ingredient, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    IngredientMapperConfig = null;
                    IngredientIMapper = null;
                    this.Repository = null;
                    _unitOfWork?.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Any(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Ingredient, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Ingredient item, Expression<Func<CostControlBusinessEntity.Ingredient, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Ingredient> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Ingredient>, IOrderedQueryable<CostControlBusinessEntity.Ingredient>> orderBy = null, List<Expression<Func<CostControlBusinessEntity.Ingredient, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~IngredientLogic()
        {
            Dispose(false);
        }
    }
}