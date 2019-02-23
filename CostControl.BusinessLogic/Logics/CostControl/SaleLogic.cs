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
    public class SaleLogic : IGenericLogic<CostControlBusinessEntity.Sale>, IDisposable
    {
        private MapperConfiguration SaleMapperConfig { get; set; }

        private IMapper SaleIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Sale> Repository;

        public SaleLogic()
        {
            SaleMapperConfig = new AutoMapperConfiguration().Configure();
            SaleIMapper = SaleMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Sale>();
        }

        public CostControlBusinessEntity.Sale Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Sale> Remove(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Sale> result = null;

            var deleteLst = Repository.Get(SaleIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                                    Expression<Func<CostControlEntity.Sale, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Sale>)
                    .ForEach(s => result.Add(SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Sale> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Sale>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = SaleIMapper.Map<IEnumerable<CostControlBusinessEntity.Sale>>(
                  Repository
                  .Remove(SaleIMapper.Map<Expression<Func<CostControlEntity.Sale, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Sale Exists(object primaryKey)
            => SaleIMapper.Map<CostControlBusinessEntity.Sale>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Sale> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => SaleIMapper.Map<CostControlBusinessEntity.Sale>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Sale> Get(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Sale>, IOrderedQueryable<CostControlBusinessEntity.Sale>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Sale>, IIncludableQueryable<CostControlBusinessEntity.Sale, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>, IEnumerable<CostControlBusinessEntity.Sale>>(
                Repository.Get(
                    SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>, Expression<Func<CostControlEntity.Sale, bool>>>(filter),
                    SaleIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Sale>, IOrderedQueryable<CostControlBusinessEntity.Sale>>,
                    Func<IQueryable<CostControlEntity.Sale>, IOrderedQueryable<CostControlEntity.Sale>>>(orderBy),
                    SaleIMapper.Map<Func<IQueryable<CostControlEntity.Sale>, IIncludableQueryable<CostControlEntity.Sale, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Sale>> GetAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Sale>, IOrderedQueryable<CostControlBusinessEntity.Sale>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Sale>, IIncludableQueryable<CostControlBusinessEntity.Sale, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleIMapper.Map<Task<IEnumerable<CostControlEntity.Sale>>, Task<IEnumerable<CostControlBusinessEntity.Sale>>>(
                Repository.GetAsync(
                    SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>, Expression<Func<CostControlEntity.Sale, bool>>>(filter),
                    SaleIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Sale>, IOrderedQueryable<CostControlBusinessEntity.Sale>>,
                    Func<IQueryable<CostControlEntity.Sale>, IOrderedQueryable<CostControlEntity.Sale>>>(orderBy),
                    SaleIMapper.Map<Func<IQueryable<CostControlEntity.Sale>, IIncludableQueryable<CostControlEntity.Sale, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Sale GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.Sale>, IIncludableQueryable<CostControlBusinessEntity.Sale, object>> includeProperties = null)
        => id == null ? null : SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>
            (Repository.GetById(id, SaleIMapper.Map<Func<IQueryable<CostControlEntity.Sale>, IIncludableQueryable<CostControlEntity.Sale, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Sale> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.Sale>, IIncludableQueryable<CostControlBusinessEntity.Sale, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(SaleIMapper.Map<Task<Entity.Models.Sale>, Task<Sale>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>
            (await Repository.GetByIdAsync(id, SaleIMapper.Map<Func<IQueryable<CostControlEntity.Sale>, IIncludableQueryable<CostControlEntity.Sale, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Sale> GetWithRawSql(string query, params object[] parameters)
        => SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>, IEnumerable<CostControlBusinessEntity.Sale>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Sale>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>, IEnumerable<CostControlBusinessEntity.Sale>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Sale Add(CostControlBusinessEntity.Sale entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = SaleIMapper
                    .Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(
                        Repository.Add(SaleIMapper.Map<CostControlBusinessEntity.Sale, CostControlEntity.Sale>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Sale> AddAsync(CostControlBusinessEntity.Sale entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Sale = SaleIMapper.Map<CostControlBusinessEntity.Sale, CostControlEntity.Sale>(entity);

                var result = SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(Repository.Add(Sale));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Sale Update(CostControlBusinessEntity.Sale entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Sale Sale = SaleIMapper.Map<CostControlBusinessEntity.Sale, CostControlEntity.Sale>(entity);

                var result = SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(Repository.Update(Sale));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Sale> UpdateAsync(CostControlBusinessEntity.Sale entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Sale = SaleIMapper.Map<CostControlBusinessEntity.Sale, CostControlEntity.Sale>(entity);

                var result = SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(Repository.Update(Sale));

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

        public CostControlBusinessEntity.Sale SingleOrDefault(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null)
        => SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(
            Repository.SingleOrDefault(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                Expression<Func<CostControlEntity.Sale, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Sale> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleIMapper.Map<Task<CostControlEntity.Sale>, Task<CostControlBusinessEntity.Sale>>(
                Repository.SingleOrDefaultAsync(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                    Expression<Func<CostControlEntity.Sale, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Sale FirstOrDefault(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null)
        => SaleIMapper.Map<CostControlEntity.Sale, CostControlBusinessEntity.Sale>(
                Repository.SingleOrDefault(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                    Expression<Func<CostControlEntity.Sale, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Sale> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SaleIMapper.Map<Task<CostControlEntity.Sale>, Task<CostControlBusinessEntity.Sale>>(
                Repository.SingleOrDefaultAsync(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                    Expression<Func<CostControlEntity.Sale, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Sale> AddRange(IEnumerable<CostControlBusinessEntity.Sale> entities)
        {
            try
            {
                var result =
                SaleIMapper.Map<IEnumerable<CostControlBusinessEntity.Sale>>(
                      Repository.AddRange(SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Sale>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Sale> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SaleIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Sale>>>(
                      Repository
                      .AddRange(SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Sale> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter)
        {
            try
            {
                var result = SaleIMapper.Map<IEnumerable<CostControlBusinessEntity.Sale>>(
                    Repository.RemoveFiltered(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                        Expression<Func<CostControlEntity.Sale, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Sale>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SaleIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Sale>>>(
                    Repository.RemoveFilteredAsync(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                    Expression<Func<CostControlEntity.Sale, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Sale> RemoveRange(IEnumerable<CostControlBusinessEntity.Sale> entities)
        {
            try
            {
                var result = SaleIMapper.Map<IEnumerable<CostControlBusinessEntity.Sale>>(
                    Repository.RemoveRange(SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Sale>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Sale> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SaleIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Sale>>>(
                        Repository
                        .Remove(SaleIMapper.Map<IEnumerable<CostControlEntity.Sale>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Sale Exists(params object[] primaryKey)
        => SaleIMapper.Map<CostControlBusinessEntity.Sale>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Sale> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await SaleIMapper.Map<Task<CostControlBusinessEntity.Sale>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null)
        => Repository.Exists(SaleIMapper.Map<Expression<Func<CostControlEntity.Sale, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(SaleIMapper.Map<Expression<Func<CostControlEntity.Sale, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
            Expression<Func<CostControlEntity.Sale, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null)
            => Repository.Count(SaleIMapper.Map<Expression<Func<CostControlBusinessEntity.Sale, bool>>,
                Expression<Func<CostControlEntity.Sale, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    SaleMapperConfig = null;
                    SaleIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Sale, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Sale item, Expression<Func<CostControlBusinessEntity.Sale, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Sale> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Sale>, IOrderedQueryable<CostControlBusinessEntity.Sale>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.Sale>, IIncludableQueryable<CostControlBusinessEntity.Sale, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~SaleLogic()
        {
            Dispose(false);
        }
    }
}