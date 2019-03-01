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
    public class RecipeLogic : IGenericLogic<CostControlBusinessEntity.Recipe>, IDisposable
    {
        private MapperConfiguration RecipeMapperConfig { get; set; }

        private IMapper RecipeIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Recipe> Repository;

        public RecipeLogic()
        {
            RecipeMapperConfig = new AutoMapperConfiguration().Configure();
            RecipeIMapper = RecipeMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Recipe>();
        }

        public CostControlBusinessEntity.Recipe Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Recipe> Remove(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.Recipe> result = null;

            var deleteLst = Repository.Get(RecipeIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                                    Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Recipe>)
                    .ForEach(s => result.Add(RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Recipe> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
                  Repository
                  .Remove(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Recipe Exists(object primaryKey)
            => RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Recipe> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Recipe> Get(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            Expression<Func<CostControlEntity.Recipe, bool>> a = r => r.Id == 1;
            Expression<Func<CostControlBusinessEntity.Recipe, bool>> b = r => r.Id == 1;

            Expression<Func<CostControlBusinessEntity.Recipe, bool>> dtoExpression = dto => dto.Id == 1;
            var expression = RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(dtoExpression);

            var model = Repository.Get(
                    expression,
                    //RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(b),
                    //RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter),
                    //a,
                    RecipeIMapper.Map<Func<IQueryable<CostControlEntity.Recipe>, IOrderedQueryable<CostControlEntity.Recipe>>>(orderBy),
                    RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties),
                    pageNumber,
                    pageSize);

            return RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
                model);
        }

        public IEnumerable<CostControlBusinessEntity.Recipe> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        => RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
                Repository.Get(
                    r => r.FoodId == parentId,
                    RecipeIMapper.Map<Func<IQueryable<CostControlEntity.Recipe>, IOrderedQueryable<CostControlEntity.Recipe>>>(orderBy),
                    RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties),
                    page,
                    pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> GetAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RecipeIMapper.Map<Task<IEnumerable<CostControlEntity.Recipe>>, Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
                Repository.GetAsync(
                    RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>, Expression<Func<CostControlEntity.Recipe, bool>>>(filter),
                    RecipeIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Recipe>, IOrderedQueryable<CostControlBusinessEntity.Recipe>>,
                    Func<IQueryable<CostControlEntity.Recipe>, IOrderedQueryable<CostControlEntity.Recipe>>>(orderBy),
                    RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Recipe GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null)
        => id == null ? null : RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>
            (Repository.GetById(id, RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Recipe> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Recipe>, IIncludableQueryable<CostControlBusinessEntity.Recipe, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(RecipeIMapper.Map<Task<Entity.Models.Recipe>, Task<Recipe>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>
            (await Repository.GetByIdAsync(id, RecipeIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Recipe>, IIncludableQueryable<CostControlEntity.Recipe, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Recipe> GetWithRawSql(string query, params object[] parameters)
        => RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>, IEnumerable<CostControlBusinessEntity.Recipe>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>, IEnumerable<CostControlBusinessEntity.Recipe>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Recipe Add(CostControlBusinessEntity.Recipe entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null)
            {
                return null;
            }

            try
            {
                var result = RecipeIMapper
                    .Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(
                        Repository.Add(RecipeIMapper.Map<CostControlBusinessEntity.Recipe, CostControlEntity.Recipe>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Recipe> AddAsync(CostControlBusinessEntity.Recipe entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                var Recipe = RecipeIMapper.Map<CostControlBusinessEntity.Recipe, CostControlEntity.Recipe>(entity);

                var result = RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(Repository.Add(Recipe));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Recipe Update(CostControlBusinessEntity.Recipe entity)
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.Recipe Recipe = RecipeIMapper.Map<CostControlBusinessEntity.Recipe, CostControlEntity.Recipe>(entity);

                var result = RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(Repository.Update(Recipe));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Recipe> UpdateAsync(CostControlBusinessEntity.Recipe entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                var Recipe = RecipeIMapper.Map<CostControlBusinessEntity.Recipe, CostControlEntity.Recipe>(entity);

                var result = RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(Repository.Update(Recipe));

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

        public CostControlBusinessEntity.Recipe SingleOrDefault(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
        => RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(
            Repository.SingleOrDefault(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Recipe> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RecipeIMapper.Map<Task<CostControlEntity.Recipe>, Task<CostControlBusinessEntity.Recipe>>(
                Repository.SingleOrDefaultAsync(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                    Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Recipe FirstOrDefault(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
        => RecipeIMapper.Map<CostControlEntity.Recipe, CostControlBusinessEntity.Recipe>(
                Repository.SingleOrDefault(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                    Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Recipe> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RecipeIMapper.Map<Task<CostControlEntity.Recipe>, Task<CostControlBusinessEntity.Recipe>>(
                Repository.SingleOrDefaultAsync(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                    Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Recipe> AddRange(IEnumerable<CostControlBusinessEntity.Recipe> entities)
        {
            try
            {
                var result =
                RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
                      Repository.AddRange(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Recipe> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await RecipeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
                      Repository
                      .AddRange(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Recipe> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter)
        {
            try
            {
                var result = RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
                    Repository.RemoveFiltered(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                        Expression<Func<CostControlEntity.Recipe, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await RecipeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
                    Repository.RemoveFilteredAsync(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                    Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Recipe> RemoveRange(IEnumerable<CostControlBusinessEntity.Recipe> entities)
        {
            try
            {
                var result = RecipeIMapper.Map<IEnumerable<CostControlBusinessEntity.Recipe>>(
                    Repository.RemoveRange(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Recipe>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Recipe> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await RecipeIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Recipe>>>(
                        Repository
                        .Remove(RecipeIMapper.Map<IEnumerable<CostControlEntity.Recipe>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Recipe Exists(params object[] primaryKey)
        => RecipeIMapper.Map<CostControlBusinessEntity.Recipe>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Recipe> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await RecipeIMapper.Map<Task<CostControlBusinessEntity.Recipe>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
        => Repository.Exists(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(RecipeIMapper.Map<Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
            Expression<Func<CostControlEntity.Recipe, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
            => Repository.Count(RecipeIMapper.Map<Expression<Func<CostControlBusinessEntity.Recipe, bool>>,
                Expression<Func<CostControlEntity.Recipe, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    RecipeMapperConfig = null;
                    RecipeIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Recipe, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Recipe item, Expression<Func<CostControlBusinessEntity.Recipe, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        ~RecipeLogic()
        {
            Dispose(false);
        }
    }
}