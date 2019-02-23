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
    public class InventoryLogic : IGenericLogic<CostControlBusinessEntity.Inventory>, IDisposable
    {
        private MapperConfiguration InventoryMapperConfig { get; set; }

        private IMapper InventoryIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Inventory> Repository;

        public InventoryLogic()
        {
            InventoryMapperConfig = new AutoMapperConfiguration().Configure();
            InventoryIMapper = InventoryMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Inventory>();
        }

        public CostControlBusinessEntity.Inventory Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> Remove(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Inventory> result = null;

            var deleteLst = Repository.Get(InventoryIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                                    Expression<Func<CostControlEntity.Inventory, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Inventory>)
                    .ForEach(s => result.Add(InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Inventory> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                  Repository
                  .Remove(InventoryIMapper.Map<Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Inventory Exists(object primaryKey)
            => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Inventory> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => InventoryIMapper.Map<CostControlBusinessEntity.Inventory>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Inventory> Get(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>, IEnumerable<CostControlBusinessEntity.Inventory>>(
                Repository.Get(
                    InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>, Expression<Func<CostControlEntity.Inventory, bool>>>(filter),
                    InventoryIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>>,
                    Func<IQueryable<CostControlEntity.Inventory>, IOrderedQueryable<CostControlEntity.Inventory>>>(orderBy),
                    InventoryIMapper.Map<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> GetAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<IEnumerable<CostControlEntity.Inventory>>, Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                Repository.GetAsync(
                    InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>, Expression<Func<CostControlEntity.Inventory, bool>>>(filter),
                    InventoryIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>>,
                    Func<IQueryable<CostControlEntity.Inventory>, IOrderedQueryable<CostControlEntity.Inventory>>>(orderBy),
                    InventoryIMapper.Map<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Inventory GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>> includeProperties = null)
        => id == null ? null : InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>
            (Repository.GetById(id, InventoryIMapper.Map<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Inventory> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(InventoryIMapper.Map<Task<Entity.Models.Inventory>, Task<Inventory>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>
            (await Repository.GetByIdAsync(id, InventoryIMapper.Map<Func<IQueryable<CostControlEntity.Inventory>, IIncludableQueryable<CostControlEntity.Inventory, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Inventory> GetWithRawSql(string query, params object[] parameters)
        => InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>, IEnumerable<CostControlBusinessEntity.Inventory>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>, IEnumerable<CostControlBusinessEntity.Inventory>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Inventory Add(CostControlBusinessEntity.Inventory entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = InventoryIMapper
                    .Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(
                        Repository.Add(InventoryIMapper.Map<CostControlBusinessEntity.Inventory, CostControlEntity.Inventory>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Inventory> AddAsync(CostControlBusinessEntity.Inventory entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Inventory = InventoryIMapper.Map<CostControlBusinessEntity.Inventory, CostControlEntity.Inventory>(entity);

                var result = InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(Repository.Add(Inventory));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Inventory Update(CostControlBusinessEntity.Inventory entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Inventory Inventory = InventoryIMapper.Map<CostControlBusinessEntity.Inventory, CostControlEntity.Inventory>(entity);

                var result = InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(Repository.Update(Inventory));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Inventory> UpdateAsync(CostControlBusinessEntity.Inventory entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Inventory = InventoryIMapper.Map<CostControlBusinessEntity.Inventory, CostControlEntity.Inventory>(entity);

                var result = InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(Repository.Update(Inventory));

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

        public CostControlBusinessEntity.Inventory SingleOrDefault(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(
            Repository.SingleOrDefault(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Inventory> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<CostControlEntity.Inventory>, Task<CostControlBusinessEntity.Inventory>>(
                Repository.SingleOrDefaultAsync(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                    Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Inventory FirstOrDefault(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
        => InventoryIMapper.Map<CostControlEntity.Inventory, CostControlBusinessEntity.Inventory>(
                Repository.SingleOrDefault(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                    Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Inventory> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await InventoryIMapper.Map<Task<CostControlEntity.Inventory>, Task<CostControlBusinessEntity.Inventory>>(
                Repository.SingleOrDefaultAsync(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                    Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Inventory> AddRange(IEnumerable<CostControlBusinessEntity.Inventory> entities)
        {
            try
            {
                var result =
                InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                      Repository.AddRange(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Inventory> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await InventoryIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                      Repository
                      .AddRange(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter)
        {
            try
            {
                var result = InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                    Repository.RemoveFiltered(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                        Expression<Func<CostControlEntity.Inventory, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await InventoryIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                    Repository.RemoveFilteredAsync(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                    Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> RemoveRange(IEnumerable<CostControlBusinessEntity.Inventory> entities)
        {
            try
            {
                var result = InventoryIMapper.Map<IEnumerable<CostControlBusinessEntity.Inventory>>(
                    Repository.RemoveRange(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Inventory>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Inventory> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await InventoryIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Inventory>>>(
                        Repository
                        .Remove(InventoryIMapper.Map<IEnumerable<CostControlEntity.Inventory>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        => await Repository.CountAsync(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
            Expression<Func<CostControlEntity.Inventory, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null)
            => Repository.Count(InventoryIMapper.Map<Expression<Func<CostControlBusinessEntity.Inventory, bool>>,
                Expression<Func<CostControlEntity.Inventory, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
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
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Inventory, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Inventory item, Expression<Func<CostControlBusinessEntity.Inventory, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Inventory> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Inventory>, IOrderedQueryable<CostControlBusinessEntity.Inventory>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.Inventory>, IIncludableQueryable<CostControlBusinessEntity.Inventory, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~InventoryLogic()
        {
            Dispose(false);
        }
    }
}