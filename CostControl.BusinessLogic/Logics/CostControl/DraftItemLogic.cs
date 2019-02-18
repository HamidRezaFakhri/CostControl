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
    public class DraftItemLogic : IGenericLogic<CostControlBusinessEntity.DraftItem>, IDisposable
    {
        private MapperConfiguration DraftItemMapperConfig { get; set; }

        private IMapper DraftItemIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.DraftItem> Repository;

        public DraftItemLogic()
        {
            DraftItemMapperConfig = new AutoMapperConfiguration().Configure();
            DraftItemIMapper = DraftItemMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.DraftItem>();
        }

        public CostControlBusinessEntity.DraftItem Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.DraftItem> Remove(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.DraftItem> result = null;

            var deleteLst = Repository.Get(DraftItemIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                                    Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.DraftItem>)
                    .ForEach(s => result.Add(DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.DraftItem> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> RemoveAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
                  Repository
                  .Remove(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.DraftItem Exists(object primaryKey)
            => DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.DraftItem> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.DraftItem> Get(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.DraftItem, object>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>, IEnumerable<CostControlBusinessEntity.DraftItem>>(
                Repository.Get(
                    DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>, Expression<Func<CostControlEntity.DraftItem, bool>>>(filter),
                    DraftItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>>,
                    Func<IQueryable<CostControlEntity.DraftItem>, IOrderedQueryable<CostControlEntity.DraftItem>>>(orderBy),
                    DraftItemIMapper.Map<List<Expression<Func<CostControlEntity.DraftItem, object>>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> GetAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.DraftItem, object>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DraftItemIMapper.Map<Task<IEnumerable<CostControlEntity.DraftItem>>, Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
                Repository.GetAsync(
                    DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>, Expression<Func<CostControlEntity.DraftItem, bool>>>(filter),
                    DraftItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>>,
                    Func<IQueryable<CostControlEntity.DraftItem>, IOrderedQueryable<CostControlEntity.DraftItem>>>(orderBy),
                    DraftItemIMapper.Map<List<Expression<Func<CostControlEntity.DraftItem, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.DraftItem GetById(object id,
            List<Expression<Func<CostControlBusinessEntity.DraftItem, object>>> includeProperties = null)
        => id == null ? null : DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>
            (Repository.GetById(id, DraftItemIMapper.Map<List<Expression<Func<CostControlEntity.DraftItem, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.DraftItem> GetByIdAsync(object id,
            List<Expression<Func<CostControlBusinessEntity.DraftItem, object>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(DraftItemIMapper.Map<Task<Entity.Models.DraftItem>, Task<DraftItem>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>
            (await Repository.GetByIdAsync(id, DraftItemIMapper.Map<List<Expression<Func<CostControlEntity.DraftItem, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.DraftItem> GetWithRawSql(string query, params object[] parameters)
        => DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>, IEnumerable<CostControlBusinessEntity.DraftItem>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>, IEnumerable<CostControlBusinessEntity.DraftItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.DraftItem Add(CostControlBusinessEntity.DraftItem entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = DraftItemIMapper
                    .Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(
                        Repository.Add(DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem, CostControlEntity.DraftItem>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.DraftItem> AddAsync(CostControlBusinessEntity.DraftItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var DraftItem = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem, CostControlEntity.DraftItem>(entity);

                var result = DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(Repository.Add(DraftItem));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.DraftItem Update(CostControlBusinessEntity.DraftItem entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.DraftItem DraftItem = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem, CostControlEntity.DraftItem>(entity);

                var result = DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(Repository.Update(DraftItem));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.DraftItem> UpdateAsync(CostControlBusinessEntity.DraftItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var DraftItem = DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem, CostControlEntity.DraftItem>(entity);

                var result = DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(Repository.Update(DraftItem));

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

        public CostControlBusinessEntity.DraftItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
        => DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(
            Repository.SingleOrDefault(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.DraftItem> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DraftItemIMapper.Map<Task<CostControlEntity.DraftItem>, Task<CostControlBusinessEntity.DraftItem>>(
                Repository.SingleOrDefaultAsync(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                    Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.DraftItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
        => DraftItemIMapper.Map<CostControlEntity.DraftItem, CostControlBusinessEntity.DraftItem>(
                Repository.SingleOrDefault(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                    Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.DraftItem> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DraftItemIMapper.Map<Task<CostControlEntity.DraftItem>, Task<CostControlBusinessEntity.DraftItem>>(
                Repository.SingleOrDefaultAsync(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                    Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.DraftItem> AddRange(IEnumerable<CostControlBusinessEntity.DraftItem> entities)
        {
            try
            {
                var result =
                DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
                      Repository.AddRange(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.DraftItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await DraftItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
                      Repository
                      .AddRange(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.DraftItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter)
        {
            try
            {
                var result = DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
                    Repository.RemoveFiltered(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                        Expression<Func<CostControlEntity.DraftItem, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await DraftItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
                    Repository.RemoveFilteredAsync(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                    Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.DraftItem> RemoveRange(IEnumerable<CostControlBusinessEntity.DraftItem> entities)
        {
            try
            {
                var result = DraftItemIMapper.Map<IEnumerable<CostControlBusinessEntity.DraftItem>>(
                    Repository.RemoveRange(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DraftItem>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.DraftItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await DraftItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DraftItem>>>(
                        Repository
                        .Remove(DraftItemIMapper.Map<IEnumerable<CostControlEntity.DraftItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.DraftItem Exists(params object[] primaryKey)
        => DraftItemIMapper.Map<CostControlBusinessEntity.DraftItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.DraftItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await DraftItemIMapper.Map<Task<CostControlBusinessEntity.DraftItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
        => Repository.Exists(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(DraftItemIMapper.Map<Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
            Expression<Func<CostControlEntity.DraftItem, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
            => Repository.Count(DraftItemIMapper.Map<Expression<Func<CostControlBusinessEntity.DraftItem, bool>>,
                Expression<Func<CostControlEntity.DraftItem, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    DraftItemMapperConfig = null;
                    DraftItemIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.DraftItem, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.DraftItem item, Expression<Func<CostControlBusinessEntity.DraftItem, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.DraftItem> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.DraftItem>, IOrderedQueryable<CostControlBusinessEntity.DraftItem>> orderBy = null, List<Expression<Func<CostControlBusinessEntity.DraftItem, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~DraftItemLogic()
        {
            Dispose(false);
        }
    }
}