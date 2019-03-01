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
    public class IntakeRemittanceLogic : IGenericLogic<CostControlBusinessEntity.IntakeRemittance>, IDisposable
    {
        private MapperConfiguration IntakeRemittanceMapperConfig { get; set; }

        private IMapper IntakeRemittanceIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.IntakeRemittance> Repository;

        public IntakeRemittanceLogic()
        {
            IntakeRemittanceMapperConfig = new AutoMapperConfiguration().Configure();
            IntakeRemittanceIMapper = IntakeRemittanceMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.IntakeRemittance>();
        }

        public CostControlBusinessEntity.IntakeRemittance Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> Remove(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.IntakeRemittance> result = null;

            var deleteLst = Repository.Get(IntakeRemittanceIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                                    Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.IntakeRemittance>)
                    .ForEach(s => result.Add(IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.IntakeRemittance> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> RemoveAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
                  Repository
                  .Remove(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IntakeRemittance Exists(object primaryKey)
            => IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IntakeRemittance> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> Get(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>, IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
                Repository.Get(
                    IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>, Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter),
                    IntakeRemittanceIMapper.Map<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>>,
                    Func<IQueryable<CostControlEntity.IntakeRemittance>, IOrderedQueryable<CostControlEntity.IntakeRemittance>>>(orderBy),
                    IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties),
                    pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> GetAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlEntity.IntakeRemittance>>, Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
                Repository.GetAsync(
                    IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>, Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter),
                    IntakeRemittanceIMapper.Map<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>>,
                    Func<IQueryable<CostControlEntity.IntakeRemittance>, IOrderedQueryable<CostControlEntity.IntakeRemittance>>>(orderBy),
                    IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.IntakeRemittance GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null)
        => id == null ? null : IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>
            (Repository.GetById(id, IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.IntakeRemittance> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(IntakeRemittanceIMapper.Map<Task<Entity.Models.IntakeRemittance>, Task<IntakeRemittance>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>
            (await Repository.GetByIdAsync(id, IntakeRemittanceIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.IntakeRemittance>, IIncludableQueryable<CostControlEntity.IntakeRemittance, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> GetWithRawSql(string query, params object[] parameters)
        => IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>, IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>, IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.IntakeRemittance Add(CostControlBusinessEntity.IntakeRemittance entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = IntakeRemittanceIMapper
                    .Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(
                        Repository.Add(IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance, CostControlEntity.IntakeRemittance>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.IntakeRemittance> AddAsync(CostControlBusinessEntity.IntakeRemittance entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var IntakeRemittance = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance, CostControlEntity.IntakeRemittance>(entity);

                var result = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(Repository.Add(IntakeRemittance));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IntakeRemittance Update(CostControlBusinessEntity.IntakeRemittance entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.IntakeRemittance IntakeRemittance = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance, CostControlEntity.IntakeRemittance>(entity);

                var result = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(Repository.Update(IntakeRemittance));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.IntakeRemittance> UpdateAsync(CostControlBusinessEntity.IntakeRemittance entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var IntakeRemittance = IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance, CostControlEntity.IntakeRemittance>(entity);

                var result = IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(Repository.Update(IntakeRemittance));

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

        public CostControlBusinessEntity.IntakeRemittance SingleOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
        => IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(
            Repository.SingleOrDefault(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittance> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceIMapper.Map<Task<CostControlEntity.IntakeRemittance>, Task<CostControlBusinessEntity.IntakeRemittance>>(
                Repository.SingleOrDefaultAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.IntakeRemittance FirstOrDefault(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
        => IntakeRemittanceIMapper.Map<CostControlEntity.IntakeRemittance, CostControlBusinessEntity.IntakeRemittance>(
                Repository.SingleOrDefault(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.IntakeRemittance> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await IntakeRemittanceIMapper.Map<Task<CostControlEntity.IntakeRemittance>, Task<CostControlBusinessEntity.IntakeRemittance>>(
                Repository.SingleOrDefaultAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> AddRange(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities)
        {
            try
            {
                var result =
                IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
                      Repository.AddRange(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
                      Repository
                      .AddRange(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> RemoveFiltered(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter)
        {
            try
            {
                var result = IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
                    Repository.RemoveFiltered(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                        Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
                    Repository.RemoveFilteredAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                    Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> RemoveRange(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities)
        {
            try
            {
                var result = IntakeRemittanceIMapper.Map<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>(
                    Repository.RemoveRange(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.IntakeRemittance> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await IntakeRemittanceIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.IntakeRemittance>>>(
                        Repository
                        .Remove(IntakeRemittanceIMapper.Map<IEnumerable<CostControlEntity.IntakeRemittance>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.IntakeRemittance Exists(params object[] primaryKey)
        => IntakeRemittanceIMapper.Map<CostControlBusinessEntity.IntakeRemittance>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.IntakeRemittance> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await IntakeRemittanceIMapper.Map<Task<CostControlBusinessEntity.IntakeRemittance>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
        => Repository.Exists(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
            Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
            => Repository.Count(IntakeRemittanceIMapper.Map<Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>>,
                Expression<Func<CostControlEntity.IntakeRemittance, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    IntakeRemittanceMapperConfig = null;
                    IntakeRemittanceIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.IntakeRemittance, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.IntakeRemittance item, Expression<Func<CostControlBusinessEntity.IntakeRemittance, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.IntakeRemittance> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IOrderedQueryable<CostControlBusinessEntity.IntakeRemittance>> orderBy = null, ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.IntakeRemittance>, IIncludableQueryable<CostControlBusinessEntity.IntakeRemittance, object>>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~IntakeRemittanceLogic()
        {
            Dispose(false);
        }
    }
}