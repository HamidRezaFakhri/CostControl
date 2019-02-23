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
    public class SaleItemLogic : IGenericLogic<CostControlBusinessEntity.SaleItem>, IDisposable
    {
        private MapperConfiguration SaleItemMapperConfig { get; set; }

        private IMapper SaleItemIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.SaleItem> Repository;

        public SaleItemLogic()
        {
            SaleItemMapperConfig = new AutoMapperConfiguration().Configure();
            SaleItemIMapper = SaleItemMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.SaleItem>();
        }

        public CostControlBusinessEntity.SaleItem Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.SaleItem> Remove(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.SaleItem> result = null;

            var deleteLst = Repository.Get(SaleItemIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                                    Expression<Func<CostControlEntity.SaleItem, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.SaleItem>)
                    .ForEach(s => result.Add(SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.SaleItem> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleItem>> RemoveAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = SaleItemIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleItem>>(
                  Repository
                  .Remove(SaleItemIMapper.Map<Expression<Func<CostControlEntity.SaleItem, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.SaleItem Exists(object primaryKey)
            => SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.SaleItem> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.SaleItem> Get(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.SaleItem>, IOrderedQueryable<CostControlBusinessEntity.SaleItem>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.SaleItem>, IIncludableQueryable<CostControlBusinessEntity.SaleItem, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>, IEnumerable<CostControlBusinessEntity.SaleItem>>(
                Repository.Get(
                    SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>, Expression<Func<CostControlEntity.SaleItem, bool>>>(filter),
                    SaleItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.SaleItem>, IOrderedQueryable<CostControlBusinessEntity.SaleItem>>,
                    Func<IQueryable<CostControlEntity.SaleItem>, IOrderedQueryable<CostControlEntity.SaleItem>>>(orderBy),
                    SaleItemIMapper.Map<Func<IQueryable<CostControlEntity.SaleItem>, IIncludableQueryable<CostControlEntity.SaleItem, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.SaleItem>> GetAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.SaleItem>, IOrderedQueryable<CostControlBusinessEntity.SaleItem>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.SaleItem>, IIncludableQueryable<CostControlBusinessEntity.SaleItem, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleItemIMapper.Map<Task<IEnumerable<CostControlEntity.SaleItem>>, Task<IEnumerable<CostControlBusinessEntity.SaleItem>>>(
                Repository.GetAsync(
                    SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>, Expression<Func<CostControlEntity.SaleItem, bool>>>(filter),
                    SaleItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.SaleItem>, IOrderedQueryable<CostControlBusinessEntity.SaleItem>>,
                    Func<IQueryable<CostControlEntity.SaleItem>, IOrderedQueryable<CostControlEntity.SaleItem>>>(orderBy),
                    SaleItemIMapper.Map<Func<IQueryable<CostControlEntity.SaleItem>, IIncludableQueryable<CostControlEntity.SaleItem, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.SaleItem GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.SaleItem>, IIncludableQueryable<CostControlBusinessEntity.SaleItem, object>> includeProperties = null)
        => id == null ? null : SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>
            (Repository.GetById(id, SaleItemIMapper.Map<Func<IQueryable<CostControlEntity.SaleItem>, IIncludableQueryable<CostControlEntity.SaleItem, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.SaleItem> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.SaleItem>, IIncludableQueryable<CostControlBusinessEntity.SaleItem, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(SaleItemIMapper.Map<Task<Entity.Models.SaleItem>, Task<SaleItem>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>
            (await Repository.GetByIdAsync(id, SaleItemIMapper.Map<Func<IQueryable<CostControlEntity.SaleItem>, IIncludableQueryable<CostControlEntity.SaleItem, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.SaleItem> GetWithRawSql(string query, params object[] parameters)
        => SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>, IEnumerable<CostControlBusinessEntity.SaleItem>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.SaleItem>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>, IEnumerable<CostControlBusinessEntity.SaleItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.SaleItem Add(CostControlBusinessEntity.SaleItem entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = SaleItemIMapper
                    .Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(
                        Repository.Add(SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem, CostControlEntity.SaleItem>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.SaleItem> AddAsync(CostControlBusinessEntity.SaleItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var SaleItem = SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem, CostControlEntity.SaleItem>(entity);

                var result = SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(Repository.Add(SaleItem));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.SaleItem Update(CostControlBusinessEntity.SaleItem entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.SaleItem SaleItem = SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem, CostControlEntity.SaleItem>(entity);

                var result = SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(Repository.Update(SaleItem));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.SaleItem> UpdateAsync(CostControlBusinessEntity.SaleItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var SaleItem = SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem, CostControlEntity.SaleItem>(entity);

                var result = SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(Repository.Update(SaleItem));

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

        public CostControlBusinessEntity.SaleItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null)
        => SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(
            Repository.SingleOrDefault(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                Expression<Func<CostControlEntity.SaleItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.SaleItem> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleItemIMapper.Map<Task<CostControlEntity.SaleItem>, Task<CostControlBusinessEntity.SaleItem>>(
                Repository.SingleOrDefaultAsync(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                    Expression<Func<CostControlEntity.SaleItem, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.SaleItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null)
        => SaleItemIMapper.Map<CostControlEntity.SaleItem, CostControlBusinessEntity.SaleItem>(
                Repository.SingleOrDefault(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                    Expression<Func<CostControlEntity.SaleItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.SaleItem> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleItemIMapper.Map<Task<CostControlEntity.SaleItem>, Task<CostControlBusinessEntity.SaleItem>>(
                Repository.SingleOrDefaultAsync(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                    Expression<Func<CostControlEntity.SaleItem, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.SaleItem> AddRange(IEnumerable<CostControlBusinessEntity.SaleItem> entities)
        {
            try
            {
                var result =
                SaleItemIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleItem>>(
                      Repository.AddRange(SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.SaleItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SaleItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleItem>>>(
                      Repository
                      .AddRange(SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.SaleItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter)
        {
            try
            {
                var result = SaleItemIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleItem>>(
                    Repository.RemoveFiltered(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                        Expression<Func<CostControlEntity.SaleItem, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleItem>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SaleItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleItem>>>(
                    Repository.RemoveFilteredAsync(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                    Expression<Func<CostControlEntity.SaleItem, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.SaleItem> RemoveRange(IEnumerable<CostControlBusinessEntity.SaleItem> entities)
        {
            try
            {
                var result = SaleItemIMapper.Map<IEnumerable<CostControlBusinessEntity.SaleItem>>(
                    Repository.RemoveRange(SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.SaleItem>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.SaleItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SaleItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.SaleItem>>>(
                        Repository
                        .Remove(SaleItemIMapper.Map<IEnumerable<CostControlEntity.SaleItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.SaleItem Exists(params object[] primaryKey)
        => SaleItemIMapper.Map<CostControlBusinessEntity.SaleItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.SaleItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await SaleItemIMapper.Map<Task<CostControlBusinessEntity.SaleItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null)
        => Repository.Exists(SaleItemIMapper.Map<Expression<Func<CostControlEntity.SaleItem, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(SaleItemIMapper.Map<Expression<Func<CostControlEntity.SaleItem, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
            Expression<Func<CostControlEntity.SaleItem, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null)
            => Repository.Count(SaleItemIMapper.Map<Expression<Func<CostControlBusinessEntity.SaleItem, bool>>,
                Expression<Func<CostControlEntity.SaleItem, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    SaleItemMapperConfig = null;
                    SaleItemIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.SaleItem, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.SaleItem item, Expression<Func<CostControlBusinessEntity.SaleItem, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.SaleItem> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.SaleItem>, IOrderedQueryable<CostControlBusinessEntity.SaleItem>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.SaleItem>, IIncludableQueryable<CostControlBusinessEntity.SaleItem, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~SaleItemLogic()
        {
            Dispose(false);
        }
    }
}