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
    public class CostPointGroupLogic : IGenericLogic<CostControlBusinessEntity.CostPointGroup>, IDisposable
    {
        private MapperConfiguration CostPointGroupMapperConfig { get; set; }

        private IMapper CostPointGroupIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.CostPointGroup> Repository;

        public CostPointGroupLogic()
        {
            CostPointGroupMapperConfig = new AutoMapperConfiguration().Configure();
            CostPointGroupIMapper = CostPointGroupMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.CostPointGroup>();
        }

        public CostControlBusinessEntity.CostPointGroup Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> Remove(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.CostPointGroup> result = null;

            var deleteLst = Repository.Get(CostPointGroupIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                                    Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.CostPointGroup>)
                    .ForEach(s => result.Add(CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.CostPointGroup> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> RemoveAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
                  Repository
                  .Remove(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.CostPointGroup Exists(object primaryKey)
            => CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.CostPointGroup> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> Get(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>, IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
                Repository.Get(
                    CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>, Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter),
                    CostPointGroupIMapper.Map<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>>,
                    Func<IQueryable<CostControlEntity.CostPointGroup>, IOrderedQueryable<CostControlEntity.CostPointGroup>>>(orderBy),
                    CostPointGroupIMapper.Map<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> GetAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlEntity.CostPointGroup>>, Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
                Repository.GetAsync(
                    CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>, Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter),
                    CostPointGroupIMapper.Map<Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>>,
                    Func<IQueryable<CostControlEntity.CostPointGroup>, IOrderedQueryable<CostControlEntity.CostPointGroup>>>(orderBy),
                    CostPointGroupIMapper.Map<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.CostPointGroup GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>> includeProperties = null)
        => id == null ? null : CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>
            (Repository.GetById(id, CostPointGroupIMapper.Map<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.CostPointGroup> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(CostPointGroupIMapper.Map<Task<Entity.Models.CostPointGroup>, Task<CostPointGroup>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>
            (await Repository.GetByIdAsync(id, CostPointGroupIMapper.Map<Func<IQueryable<CostControlEntity.CostPointGroup>, IIncludableQueryable<CostControlEntity.CostPointGroup, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> GetWithRawSql(string query, params object[] parameters)
        => CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>, IEnumerable<CostControlBusinessEntity.CostPointGroup>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>, IEnumerable<CostControlBusinessEntity.CostPointGroup>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.CostPointGroup Add(CostControlBusinessEntity.CostPointGroup entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = CostPointGroupIMapper
                    .Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(
                        Repository.Add(CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup, CostControlEntity.CostPointGroup>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.CostPointGroup> AddAsync(CostControlBusinessEntity.CostPointGroup entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var CostPointGroup = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup, CostControlEntity.CostPointGroup>(entity);

                var result = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(Repository.Add(CostPointGroup));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.CostPointGroup Update(CostControlBusinessEntity.CostPointGroup entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.CostPointGroup CostPointGroup = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup, CostControlEntity.CostPointGroup>(entity);

                var result = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(Repository.Update(CostPointGroup));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.CostPointGroup> UpdateAsync(CostControlBusinessEntity.CostPointGroup entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var CostPointGroup = CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup, CostControlEntity.CostPointGroup>(entity);

                var result = CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(Repository.Update(CostPointGroup));

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

        public CostControlBusinessEntity.CostPointGroup SingleOrDefault(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
        => CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(
            Repository.SingleOrDefault(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.CostPointGroup> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await CostPointGroupIMapper.Map<Task<CostControlEntity.CostPointGroup>, Task<CostControlBusinessEntity.CostPointGroup>>(
                Repository.SingleOrDefaultAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                    Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.CostPointGroup FirstOrDefault(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
        => CostPointGroupIMapper.Map<CostControlEntity.CostPointGroup, CostControlBusinessEntity.CostPointGroup>(
                Repository.SingleOrDefault(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                    Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.CostPointGroup> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await CostPointGroupIMapper.Map<Task<CostControlEntity.CostPointGroup>, Task<CostControlBusinessEntity.CostPointGroup>>(
                Repository.SingleOrDefaultAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                    Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> AddRange(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities)
        {
            try
            {
                var result =
                CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
                      Repository.AddRange(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
                      Repository
                      .AddRange(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> RemoveFiltered(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter)
        {
            try
            {
                var result = CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
                    Repository.RemoveFiltered(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                        Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
                    Repository.RemoveFilteredAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                    Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> RemoveRange(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities)
        {
            try
            {
                var result = CostPointGroupIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPointGroup>>(
                    Repository.RemoveRange(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.CostPointGroup> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await CostPointGroupIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPointGroup>>>(
                        Repository
                        .Remove(CostPointGroupIMapper.Map<IEnumerable<CostControlEntity.CostPointGroup>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.CostPointGroup Exists(params object[] primaryKey)
        => CostPointGroupIMapper.Map<CostControlBusinessEntity.CostPointGroup>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.CostPointGroup> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await CostPointGroupIMapper.Map<Task<CostControlBusinessEntity.CostPointGroup>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
        => Repository.Exists(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
            Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
            => Repository.Count(CostPointGroupIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>>,
                Expression<Func<CostControlEntity.CostPointGroup, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    CostPointGroupMapperConfig = null;
                    CostPointGroupIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.CostPointGroup, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.CostPointGroup item, Expression<Func<CostControlBusinessEntity.CostPointGroup, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.CostPointGroup> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IOrderedQueryable<CostControlBusinessEntity.CostPointGroup>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.CostPointGroup>, IIncludableQueryable<CostControlBusinessEntity.CostPointGroup, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~CostPointGroupLogic()
        {
            Dispose(false);
        }
    }
}