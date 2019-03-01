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
    public class IntakeRemittanceItemLogic : IGenericLogic<CostControlBusinessEntity.IntakeRemittanceItem>, IDisposable
    {
        private MapperConfiguration IntakeRemittanceItemMapperConfig { get; set; }

        private IMapper IntakeRemittanceItemIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.IntakeRemittanceItem> Repository;

        public IntakeRemittanceItemLogic()
        {
            IntakeRemittanceItemMapperConfig = new AutoMapperConfiguration().Configure();
            IntakeRemittanceItemIMapper = IntakeRemittanceItemMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.IntakeRemittanceItem>();
        }

        public CostControlBusinessEntity.IntakeRemittanceItem Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> Remove(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.IntakeRemittanceItem> result = null;

            var deleteLst = Repository.Get(IntakeRemittanceItemIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                                    Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.IntakeRemittanceItem>)
                    .ForEach(s => result.Add(IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> RemoveAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
                  Repository
                  .Remove(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IntakeRemittanceItem Exists(object primaryKey)
            => IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> Get(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>, IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
                Repository.Get(
                    IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>, Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter),
                    IntakeRemittanceItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>>,
                    Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlEntity.IntakeRemittanceItem>>>(orderBy),
                    IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties),
                    pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> GetAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlEntity.IntakeRemittanceItem>>, Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
                Repository.GetAsync(
                    IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>, Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter),
                    IntakeRemittanceItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>>,
                    Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlEntity.IntakeRemittanceItem>>>(orderBy),
                    IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.IntakeRemittanceItem GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null)
        => id == null ? null : IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>
            (Repository.GetById(id, IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(IntakeRemittanceItemIMapper.Map<Task<Entity.Models.IntakeRemittanceItem>, Task<IntakeRemittanceItem>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>
            (await Repository.GetByIdAsync(id, IntakeRemittanceItemIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlEntity.IntakeRemittanceItem, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> GetWithRawSql(string query, params object[] parameters)
        => IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>, IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>, IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.IntakeRemittanceItem Add(CostControlBusinessEntity.IntakeRemittanceItem entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = IntakeRemittanceItemIMapper
                    .Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(
                        Repository.Add(IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem, CostControlEntity.IntakeRemittanceItem>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> AddAsync(CostControlBusinessEntity.IntakeRemittanceItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var IntakeRemittanceItem = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem, CostControlEntity.IntakeRemittanceItem>(entity);

                var result = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Add(IntakeRemittanceItem));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IntakeRemittanceItem Update(CostControlBusinessEntity.IntakeRemittanceItem entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.IntakeRemittanceItem IntakeRemittanceItem = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem, CostControlEntity.IntakeRemittanceItem>(entity);

                var result = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Update(IntakeRemittanceItem));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> UpdateAsync(CostControlBusinessEntity.IntakeRemittanceItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var IntakeRemittanceItem = IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem, CostControlEntity.IntakeRemittanceItem>(entity);

                var result = IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Update(IntakeRemittanceItem));

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

        public CostControlBusinessEntity.IntakeRemittanceItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
        => IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(
            Repository.SingleOrDefault(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemIMapper.Map<Task<CostControlEntity.IntakeRemittanceItem>, Task<CostControlBusinessEntity.IntakeRemittanceItem>>(
                Repository.SingleOrDefaultAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.IntakeRemittanceItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
        => IntakeRemittanceItemIMapper.Map<CostControlEntity.IntakeRemittanceItem, CostControlBusinessEntity.IntakeRemittanceItem>(
                Repository.SingleOrDefault(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceItemIMapper.Map<Task<CostControlEntity.IntakeRemittanceItem>, Task<CostControlBusinessEntity.IntakeRemittanceItem>>(
                Repository.SingleOrDefaultAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> AddRange(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities)
        {
            try
            {
                var result =
                IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
                      Repository.AddRange(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
                      Repository
                      .AddRange(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter)
        {
            try
            {
                var result = IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
                    Repository.RemoveFiltered(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                        Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
                    Repository.RemoveFilteredAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> RemoveRange(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities)
        {
            try
            {
                var result = IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>(
                    Repository.RemoveRange(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IntakeRemittanceItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem>>>(
                        Repository
                        .Remove(IntakeRemittanceItemIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittanceItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IntakeRemittanceItem Exists(params object[] primaryKey)
        => IntakeRemittanceItemIMapper.Map<CostControlBusinessEntity.IntakeRemittanceItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IntakeRemittanceItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await IntakeRemittanceItemIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittanceItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
        => Repository.Exists(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
            Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
            => Repository.Count(IntakeRemittanceItemIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>>,
                Expression<Func<CostControlEntity.IntakeRemittanceItem, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    IntakeRemittanceItemMapperConfig = null;
                    IntakeRemittanceItemIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.IntakeRemittanceItem item, Expression<Func<CostControlBusinessEntity.IntakeRemittanceItem, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittanceItem> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittanceItem>> orderBy = null, ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittanceItem>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittanceItem, object>>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~IntakeRemittanceItemLogic()
        {
            Dispose(false);
        }
    }
}