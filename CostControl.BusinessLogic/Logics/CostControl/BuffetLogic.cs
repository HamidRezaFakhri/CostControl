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
    public class BuffetLogic : IGenericLogic<CostControlBusinessEntity.Buffet>, IDisposable
    {
        private MapperConfiguration BuffetMapperConfig { get; set; }

        private IMapper BuffetIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Buffet> Repository;

        public BuffetLogic()
        {
            BuffetMapperConfig = new AutoMapperConfiguration().Configure();
            BuffetIMapper = BuffetMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Buffet>();
        }

        public CostControlBusinessEntity.Buffet Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> Remove(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Buffet> result = null;

            var deleteLst = Repository.Get(BuffetIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                                    Expression<Func<CostControlEntity.Buffet, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Buffet>)
                    .ForEach(s => result.Add(BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Buffet> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                  Repository
                  .Remove(BuffetIMapper.Map<Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Buffet Exists(object primaryKey)
            => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Buffet> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => BuffetIMapper.Map<CostControlBusinessEntity.Buffet>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Buffet> Get(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>, IEnumerable<CostControlBusinessEntity.Buffet>>(
                Repository.Get(
                    BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>, Expression<Func<CostControlEntity.Buffet, bool>>>(filter),
                    BuffetIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>>,
                    Func<IQueryable<CostControlEntity.Buffet>, IOrderedQueryable<CostControlEntity.Buffet>>>(orderBy),
                    BuffetIMapper.Map<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>(includeProperties), pageNumber, pageSize));


        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> GetAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<IEnumerable<CostControlEntity.Buffet>>, Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                Repository.GetAsync(
                    BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>, Expression<Func<CostControlEntity.Buffet, bool>>>(filter),
                    BuffetIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>>,
                    Func<IQueryable<CostControlEntity.Buffet>, IOrderedQueryable<CostControlEntity.Buffet>>>(orderBy),
                    BuffetIMapper.Map<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Buffet GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>> includeProperties = null)
        => id == null ? null : BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>
            (Repository.GetById(id, BuffetIMapper.Map<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Buffet> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(BuffetIMapper.Map<Task<Entity.Models.Buffet>, Task<Buffet>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>
            (await Repository.GetByIdAsync(id, BuffetIMapper.Map<Func<IQueryable<CostControlEntity.Buffet>, IIncludableQueryable<CostControlEntity.Buffet, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Buffet> GetWithRawSql(string query, params object[] parameters)
        => BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>, IEnumerable<CostControlBusinessEntity.Buffet>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>, IEnumerable<CostControlBusinessEntity.Buffet>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Buffet Add(CostControlBusinessEntity.Buffet entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = BuffetIMapper
                    .Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(
                        Repository.Add(BuffetIMapper.Map<CostControlBusinessEntity.Buffet, CostControlEntity.Buffet>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Buffet> AddAsync(CostControlBusinessEntity.Buffet entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Buffet = BuffetIMapper.Map<CostControlBusinessEntity.Buffet, CostControlEntity.Buffet>(entity);

                var result = BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(Repository.Add(Buffet));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Buffet Update(CostControlBusinessEntity.Buffet entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Buffet Buffet = BuffetIMapper.Map<CostControlBusinessEntity.Buffet, CostControlEntity.Buffet>(entity);

                var result = BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(Repository.Update(Buffet));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Buffet> UpdateAsync(CostControlBusinessEntity.Buffet entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Buffet = BuffetIMapper.Map<CostControlBusinessEntity.Buffet, CostControlEntity.Buffet>(entity);

                var result = BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(Repository.Update(Buffet));

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

        public CostControlBusinessEntity.Buffet SingleOrDefault(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(
            Repository.SingleOrDefault(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Buffet> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<CostControlEntity.Buffet>, Task<CostControlBusinessEntity.Buffet>>(
                Repository.SingleOrDefaultAsync(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                    Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Buffet FirstOrDefault(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
        => BuffetIMapper.Map<CostControlEntity.Buffet, CostControlBusinessEntity.Buffet>(
                Repository.SingleOrDefault(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                    Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Buffet> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await BuffetIMapper.Map<Task<CostControlEntity.Buffet>, Task<CostControlBusinessEntity.Buffet>>(
                Repository.SingleOrDefaultAsync(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                    Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Buffet> AddRange(IEnumerable<CostControlBusinessEntity.Buffet> entities)
        {
            try
            {
                var result =
                BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                      Repository.AddRange(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Buffet> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await BuffetIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                      Repository
                      .AddRange(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter)
        {
            try
            {
                var result = BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                    Repository.RemoveFiltered(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                        Expression<Func<CostControlEntity.Buffet, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await BuffetIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                    Repository.RemoveFilteredAsync(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                    Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> RemoveRange(IEnumerable<CostControlBusinessEntity.Buffet> entities)
        {
            try
            {
                var result = BuffetIMapper.Map<IEnumerable<CostControlBusinessEntity.Buffet>>(
                    Repository.RemoveRange(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Buffet>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Buffet> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await BuffetIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Buffet>>>(
                        Repository
                        .Remove(BuffetIMapper.Map<IEnumerable<CostControlEntity.Buffet>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        => await Repository.CountAsync(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
            Expression<Func<CostControlEntity.Buffet, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null)
            => Repository.Count(BuffetIMapper.Map<Expression<Func<CostControlBusinessEntity.Buffet, bool>>,
                Expression<Func<CostControlEntity.Buffet, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
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
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Buffet, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Buffet item, Expression<Func<CostControlBusinessEntity.Buffet, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Buffet> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Buffet>, IOrderedQueryable<CostControlBusinessEntity.Buffet>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.Buffet>, IIncludableQueryable<CostControlBusinessEntity.Buffet, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~BuffetLogic()
        {
            Dispose(false);
        }
    }
}