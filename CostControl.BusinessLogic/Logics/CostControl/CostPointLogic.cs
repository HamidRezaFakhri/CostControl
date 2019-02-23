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
    public class CostPointLogic : IGenericLogic<CostControlBusinessEntity.CostPoint>, IDisposable
    {
        private MapperConfiguration CostPointMapperConfig { get; set; }

        private IMapper CostPointIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.CostPoint> Repository;

        public CostPointLogic()
        {
            CostPointMapperConfig = new AutoMapperConfiguration().Configure();
            CostPointIMapper = CostPointMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.CostPoint>();
        }

        public CostControlBusinessEntity.CostPoint Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.CostPoint> Remove(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.CostPoint> result = null;

            var deleteLst = Repository.Get(CostPointIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                                    Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.CostPoint>)
                    .ForEach(s => result.Add(CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.CostPoint> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> RemoveAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
                  Repository
                  .Remove(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.CostPoint Exists(object primaryKey)
            => CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.CostPoint> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.CostPoint> Get(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            try
            {
                IEnumerable<CostControlEntity.CostPoint> a = Repository.Get(
                          CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>, Expression<Func<CostControlEntity.CostPoint, bool>>>(filter),
                          CostPointIMapper.Map<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>>,
                          Func<IQueryable<CostControlEntity.CostPoint>, IOrderedQueryable<CostControlEntity.CostPoint>>>(orderBy),
                          CostPointIMapper.Map<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>(includeProperties),
                          pageNumber, pageSize);
                
                    var b = a.ToList();

                return CostPointIMapper
                    .Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(a);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> GetAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await CostPointIMapper.Map<Task<IEnumerable<CostControlEntity.CostPoint>>, Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
                Repository.GetAsync(
                    CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>, Expression<Func<CostControlEntity.CostPoint, bool>>>(filter),
                    CostPointIMapper.Map<Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>>,
                    Func<IQueryable<CostControlEntity.CostPoint>, IOrderedQueryable<CostControlEntity.CostPoint>>>(orderBy),
                    CostPointIMapper.Map<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.CostPoint GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>> includeProperties = null)
        => id == null ? null : CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>
            (Repository.GetById(id, CostPointIMapper.Map<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.CostPoint> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(CostPointIMapper.Map<Task<Entity.Models.CostPoint>, Task<CostPoint>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>
            (await Repository.GetByIdAsync(id, CostPointIMapper.Map<Func<IQueryable<CostControlEntity.CostPoint>, IIncludableQueryable<CostControlEntity.CostPoint, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.CostPoint> GetWithRawSql(string query, params object[] parameters)
        => CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>, IEnumerable<CostControlBusinessEntity.CostPoint>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>, IEnumerable<CostControlBusinessEntity.CostPoint>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.CostPoint Add(CostControlBusinessEntity.CostPoint entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = CostPointIMapper
                    .Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(
                        Repository.Add(CostPointIMapper.Map<CostControlBusinessEntity.CostPoint, CostControlEntity.CostPoint>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.CostPoint> AddAsync(CostControlBusinessEntity.CostPoint entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var CostPoint = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint, CostControlEntity.CostPoint>(entity);

                var result = CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(Repository.Add(CostPoint));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.CostPoint Update(CostControlBusinessEntity.CostPoint entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.CostPoint CostPoint = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint, CostControlEntity.CostPoint>(entity);

                var result = CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(Repository.Update(CostPoint));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.CostPoint> UpdateAsync(CostControlBusinessEntity.CostPoint entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var CostPoint = CostPointIMapper.Map<CostControlBusinessEntity.CostPoint, CostControlEntity.CostPoint>(entity);

                var result = CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(Repository.Update(CostPoint));

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

        public CostControlBusinessEntity.CostPoint SingleOrDefault(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
        => CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(
            Repository.SingleOrDefault(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.CostPoint> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await CostPointIMapper.Map<Task<CostControlEntity.CostPoint>, Task<CostControlBusinessEntity.CostPoint>>(
                Repository.SingleOrDefaultAsync(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                    Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.CostPoint FirstOrDefault(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
        => CostPointIMapper.Map<CostControlEntity.CostPoint, CostControlBusinessEntity.CostPoint>(
                Repository.SingleOrDefault(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                    Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.CostPoint> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await CostPointIMapper.Map<Task<CostControlEntity.CostPoint>, Task<CostControlBusinessEntity.CostPoint>>(
                Repository.SingleOrDefaultAsync(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                    Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.CostPoint> AddRange(IEnumerable<CostControlBusinessEntity.CostPoint> entities)
        {
            try
            {
                var result =
                CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
                      Repository.AddRange(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.CostPoint> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await CostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
                      Repository
                      .AddRange(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.CostPoint> RemoveFiltered(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter)
        {
            try
            {
                var result = CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
                    Repository.RemoveFiltered(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                        Expression<Func<CostControlEntity.CostPoint, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await CostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
                    Repository.RemoveFilteredAsync(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                    Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.CostPoint> RemoveRange(IEnumerable<CostControlBusinessEntity.CostPoint> entities)
        {
            try
            {
                var result = CostPointIMapper.Map<IEnumerable<CostControlBusinessEntity.CostPoint>>(
                    Repository.RemoveRange(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.CostPoint>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.CostPoint> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await CostPointIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.CostPoint>>>(
                        Repository
                        .Remove(CostPointIMapper.Map<IEnumerable<CostControlEntity.CostPoint>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.CostPoint Exists(params object[] primaryKey)
        => CostPointIMapper.Map<CostControlBusinessEntity.CostPoint>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.CostPoint> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await CostPointIMapper.Map<Task<CostControlBusinessEntity.CostPoint>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
        => Repository.Exists(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(CostPointIMapper.Map<Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
            Expression<Func<CostControlEntity.CostPoint, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
            => Repository.Count(CostPointIMapper.Map<Expression<Func<CostControlBusinessEntity.CostPoint, bool>>,
                Expression<Func<CostControlEntity.CostPoint, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    CostPointMapperConfig = null;
                    CostPointIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.CostPoint, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.CostPoint item, Expression<Func<CostControlBusinessEntity.CostPoint, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.CostPoint> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.CostPoint>, IOrderedQueryable<CostControlBusinessEntity.CostPoint>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.CostPoint>, IIncludableQueryable<CostControlBusinessEntity.CostPoint, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~CostPointLogic()
        {
            Dispose(false);
        }
    }
}