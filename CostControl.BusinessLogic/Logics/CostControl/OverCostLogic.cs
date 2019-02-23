using AutoMapper;
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
    public class OverCostLogic : IGenericLogic<CostControlBusinessEntity.OverCost>, IDisposable
    {
        private MapperConfiguration OverCostMapperConfig { get; set; }

        private IMapper OverCostIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.OverCost> Repository;

        public OverCostLogic()
        {
            OverCostMapperConfig = new AutoMapperConfiguration().Configure();
            OverCostIMapper = OverCostMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.OverCost>();
        }

        public CostControlBusinessEntity.OverCost Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.OverCost> Remove(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.OverCost> result = null;

            var deleteLst = Repository.Get(OverCostIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                                    Expression<Func<CostControlEntity.OverCost, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.OverCost>)
                    .ForEach(s => result.Add(OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.OverCost> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCost>> RemoveAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = OverCostIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCost>>(
                  Repository
                  .Remove(OverCostIMapper.Map<Expression<Func<CostControlEntity.OverCost, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.OverCost Exists(object primaryKey)
            => OverCostIMapper.Map<CostControlBusinessEntity.OverCost>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.OverCost> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => OverCostIMapper.Map<CostControlBusinessEntity.OverCost>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.OverCost> Get(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.OverCost>, IOrderedQueryable<CostControlBusinessEntity.OverCost>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.OverCost>, IIncludableQueryable<CostControlBusinessEntity.OverCost, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>, IEnumerable<CostControlBusinessEntity.OverCost>>(
                Repository.Get(
                    OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>, Expression<Func<CostControlEntity.OverCost, bool>>>(filter),
                    OverCostIMapper.Map<Func<IQueryable<CostControlBusinessEntity.OverCost>, IOrderedQueryable<CostControlBusinessEntity.OverCost>>,
                    Func<IQueryable<CostControlEntity.OverCost>, IOrderedQueryable<CostControlEntity.OverCost>>>(orderBy),
                    OverCostIMapper.Map<Func<IQueryable<CostControlEntity.OverCost>, IIncludableQueryable<CostControlEntity.OverCost, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.OverCost>> GetAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.OverCost>, IOrderedQueryable<CostControlBusinessEntity.OverCost>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.OverCost>, IIncludableQueryable<CostControlBusinessEntity.OverCost, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostIMapper.Map<Task<IEnumerable<CostControlEntity.OverCost>>, Task<IEnumerable<CostControlBusinessEntity.OverCost>>>(
                Repository.GetAsync(
                    OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>, Expression<Func<CostControlEntity.OverCost, bool>>>(filter),
                    OverCostIMapper.Map<Func<IQueryable<CostControlBusinessEntity.OverCost>, IOrderedQueryable<CostControlBusinessEntity.OverCost>>,
                    Func<IQueryable<CostControlEntity.OverCost>, IOrderedQueryable<CostControlEntity.OverCost>>>(orderBy),
                    OverCostIMapper.Map<Func<IQueryable<CostControlEntity.OverCost>, IIncludableQueryable<CostControlEntity.OverCost, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.OverCost GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.OverCost>, IIncludableQueryable<CostControlBusinessEntity.OverCost, object>> includeProperties = null)
        => id == null ? null : OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>
            (Repository.GetById(id, OverCostIMapper.Map<Func<IQueryable<CostControlEntity.OverCost>, IIncludableQueryable<CostControlEntity.OverCost, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.OverCost> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.OverCost>, IIncludableQueryable<CostControlBusinessEntity.OverCost, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(OverCostIMapper.Map<Task<Entity.Models.OverCost>, Task<OverCost>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>
            (await Repository.GetByIdAsync(id, OverCostIMapper.Map<Func<IQueryable<CostControlEntity.OverCost>, IIncludableQueryable<CostControlEntity.OverCost, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.OverCost> GetWithRawSql(string query, params object[] parameters)
        => OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>, IEnumerable<CostControlBusinessEntity.OverCost>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.OverCost>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>, IEnumerable<CostControlBusinessEntity.OverCost>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.OverCost Add(CostControlBusinessEntity.OverCost entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = OverCostIMapper
                    .Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(
                        Repository.Add(OverCostIMapper.Map<CostControlBusinessEntity.OverCost, CostControlEntity.OverCost>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.OverCost> AddAsync(CostControlBusinessEntity.OverCost entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var OverCost = OverCostIMapper.Map<CostControlBusinessEntity.OverCost, CostControlEntity.OverCost>(entity);

                var result = OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(Repository.Add(OverCost));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.OverCost Update(CostControlBusinessEntity.OverCost entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.OverCost OverCost = OverCostIMapper.Map<CostControlBusinessEntity.OverCost, CostControlEntity.OverCost>(entity);

                var result = OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(Repository.Update(OverCost));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.OverCost> UpdateAsync(CostControlBusinessEntity.OverCost entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var OverCost = OverCostIMapper.Map<CostControlBusinessEntity.OverCost, CostControlEntity.OverCost>(entity);

                var result = OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(Repository.Update(OverCost));

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

        public CostControlBusinessEntity.OverCost SingleOrDefault(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null)
        => OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(
            Repository.SingleOrDefault(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                Expression<Func<CostControlEntity.OverCost, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCost> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostIMapper.Map<Task<CostControlEntity.OverCost>, Task<CostControlBusinessEntity.OverCost>>(
                Repository.SingleOrDefaultAsync(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                    Expression<Func<CostControlEntity.OverCost, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.OverCost FirstOrDefault(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null)
        => OverCostIMapper.Map<CostControlEntity.OverCost, CostControlBusinessEntity.OverCost>(
                Repository.SingleOrDefault(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                    Expression<Func<CostControlEntity.OverCost, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.OverCost> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await OverCostIMapper.Map<Task<CostControlEntity.OverCost>, Task<CostControlBusinessEntity.OverCost>>(
                Repository.SingleOrDefaultAsync(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                    Expression<Func<CostControlEntity.OverCost, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.OverCost> AddRange(IEnumerable<CostControlBusinessEntity.OverCost> entities)
        {
            try
            {
                var result =
                OverCostIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCost>>(
                      Repository.AddRange(OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCost>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.OverCost> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await OverCostIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCost>>>(
                      Repository
                      .AddRange(OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.OverCost> RemoveFiltered(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter)
        {
            try
            {
                var result = OverCostIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCost>>(
                    Repository.RemoveFiltered(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                        Expression<Func<CostControlEntity.OverCost, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCost>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await OverCostIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCost>>>(
                    Repository.RemoveFilteredAsync(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                    Expression<Func<CostControlEntity.OverCost, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.OverCost> RemoveRange(IEnumerable<CostControlBusinessEntity.OverCost> entities)
        {
            try
            {
                var result = OverCostIMapper.Map<IEnumerable<CostControlBusinessEntity.OverCost>>(
                    Repository.RemoveRange(OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.OverCost>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.OverCost> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await OverCostIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.OverCost>>>(
                        Repository
                        .Remove(OverCostIMapper.Map<IEnumerable<CostControlEntity.OverCost>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.OverCost Exists(params object[] primaryKey)
        => OverCostIMapper.Map<CostControlBusinessEntity.OverCost>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.OverCost> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await OverCostIMapper.Map<Task<CostControlBusinessEntity.OverCost>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null)
        => Repository.Exists(OverCostIMapper.Map<Expression<Func<CostControlEntity.OverCost, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(OverCostIMapper.Map<Expression<Func<CostControlEntity.OverCost, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
            Expression<Func<CostControlEntity.OverCost, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null)
            => Repository.Count(OverCostIMapper.Map<Expression<Func<CostControlBusinessEntity.OverCost, bool>>,
                Expression<Func<CostControlEntity.OverCost, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    OverCostMapperConfig = null;
                    OverCostIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.OverCost, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.OverCost item, Expression<Func<CostControlBusinessEntity.OverCost, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.OverCost> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.OverCost>, IOrderedQueryable<CostControlBusinessEntity.OverCost>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.OverCost>, IIncludableQueryable<CostControlBusinessEntity.OverCost, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~OverCostLogic()
        {
            Dispose(false);
        }
    }
}