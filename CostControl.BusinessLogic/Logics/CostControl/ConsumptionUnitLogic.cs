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
    public class ConsumptionUnitLogic : IGenericLogic<CostControlBusinessEntity.ConsumptionUnit>, IDisposable
    {
        private MapperConfiguration ConsumptionUnitMapperConfig { get; set; }

        private IMapper ConsumptionUnitIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.ConsumptionUnit> Repository;

        public ConsumptionUnitLogic()
        {
            ConsumptionUnitMapperConfig = new AutoMapperConfiguration().Configure();
            ConsumptionUnitIMapper = ConsumptionUnitMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.ConsumptionUnit>();
        }

        public CostControlBusinessEntity.ConsumptionUnit Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                CostControlBusinessEntity.ConsumptionUnit result = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> Remove(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.ConsumptionUnit> result = null;

            IEnumerable<CostControlEntity.ConsumptionUnit> deleteLst = Repository.Get(ConsumptionUnitIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                                    Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.ConsumptionUnit>)
                    .ForEach(s => result.Add(ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            CostControlEntity.ConsumptionUnit entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                CostControlBusinessEntity.ConsumptionUnit result = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> RemoveAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result = ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
                  Repository
                  .Remove(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.ConsumptionUnit Exists(object primaryKey)
        {
            return ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Exists(primaryKey));
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(await Repository.ExistsAsync(cancellationToken, primaryKey));
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> Get(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            return ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>, IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
                           Repository.Get(
                               ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>, Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter),
                               ConsumptionUnitIMapper.Map<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>>,
                               Func<IQueryable<CostControlEntity.ConsumptionUnit>, IOrderedQueryable<CostControlEntity.ConsumptionUnit>>>(orderBy),
                               ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties), pageNumber, pageSize));
        }

        public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> GetAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlEntity.ConsumptionUnit>>, Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
                           Repository.GetAsync(
                               ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>, Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter),
                               ConsumptionUnitIMapper.Map<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>>,
                               Func<IQueryable<CostControlEntity.ConsumptionUnit>, IOrderedQueryable<CostControlEntity.ConsumptionUnit>>>(orderBy),
                               ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));
        }

        public CostControlBusinessEntity.ConsumptionUnit GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null)
        {
            return id == null ? null : ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>
                       (Repository.GetById(id, ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties)));
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return id == null ? null : ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>
                       (await Repository.GetByIdAsync(id, ConsumptionUnitIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.ConsumptionUnit>, IIncludableQueryable<CostControlEntity.ConsumptionUnit, object>>>>(includeProperties), cancellationToken));
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> GetWithRawSql(string query, params object[] parameters)
        {
            return ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>, IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(Repository.GetWithRawSql(query, parameters));
        }

        public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        {
            return ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>, IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));
        }

        public CostControlBusinessEntity.ConsumptionUnit Add(CostControlBusinessEntity.ConsumptionUnit entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlBusinessEntity.ConsumptionUnit result = ConsumptionUnitIMapper
                    .Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(
                        Repository.Add(ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit, CostControlEntity.ConsumptionUnit>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> AddAsync(CostControlBusinessEntity.ConsumptionUnit entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.ConsumptionUnit ConsumptionUnit = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit, CostControlEntity.ConsumptionUnit>(entity);

                CostControlBusinessEntity.ConsumptionUnit result = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(Repository.Add(ConsumptionUnit));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.ConsumptionUnit Update(CostControlBusinessEntity.ConsumptionUnit entity)
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.ConsumptionUnit ConsumptionUnit = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit, CostControlEntity.ConsumptionUnit>(entity);

                CostControlBusinessEntity.ConsumptionUnit result = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(Repository.Update(ConsumptionUnit));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> UpdateAsync(CostControlBusinessEntity.ConsumptionUnit entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.ConsumptionUnit ConsumptionUnit = ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit, CostControlEntity.ConsumptionUnit>(entity);

                CostControlBusinessEntity.ConsumptionUnit result = ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(Repository.Update(ConsumptionUnit));

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
        {
            return Repository.RunRawSql(query, parameters);
        }

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        {
            return await Repository.RunRawSqlAsync(query, cancellationToken, parameters);
        }

        public CostControlBusinessEntity.ConsumptionUnit SingleOrDefault(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
        {
            return ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(
                       Repository.SingleOrDefault(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                           Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ConsumptionUnitIMapper.Map<Task<CostControlEntity.ConsumptionUnit>, Task<CostControlBusinessEntity.ConsumptionUnit>>(
                           Repository.SingleOrDefaultAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                               Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken));
        }

        public CostControlBusinessEntity.ConsumptionUnit FirstOrDefault(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
        {
            return ConsumptionUnitIMapper.Map<CostControlEntity.ConsumptionUnit, CostControlBusinessEntity.ConsumptionUnit>(
                           Repository.SingleOrDefault(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                               Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ConsumptionUnitIMapper.Map<Task<CostControlEntity.ConsumptionUnit>, Task<CostControlBusinessEntity.ConsumptionUnit>>(
                           Repository.SingleOrDefaultAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                               Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken));
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> AddRange(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result =
                ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
                      Repository.AddRange(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result = await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
                      Repository
                      .AddRange(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> RemoveFiltered(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result = ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
                    Repository.RemoveFiltered(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                        Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result = await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
                    Repository.RemoveFilteredAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                    Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> RemoveRange(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result = ConsumptionUnitIMapper.Map<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>(
                    Repository.RemoveRange(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.ConsumptionUnit> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.ConsumptionUnit> result = await ConsumptionUnitIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.ConsumptionUnit>>>(
                        Repository
                        .Remove(ConsumptionUnitIMapper.Map<IEnumerable<CostControlEntity.ConsumptionUnit>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.ConsumptionUnit Exists(params object[] primaryKey)
        {
            return ConsumptionUnitIMapper.Map<CostControlBusinessEntity.ConsumptionUnit>(Repository.Exists(primaryKey));
        }

        public async Task<CostControlBusinessEntity.ConsumptionUnit> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        {
            return await ConsumptionUnitIMapper.Map<Task<CostControlBusinessEntity.ConsumptionUnit>>(Repository.ExistsAsync(cancellationToken, primaryKey));
        }

        public bool Exists(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
        {
            return Repository.Exists(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));
        }

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.ExistsAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken);
        }

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.CountAsync(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                       Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter), cancellationToken);
        }

        public int GetCount(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
        {
            return Repository.Count(ConsumptionUnitIMapper.Map<Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>>,
                           Expression<Func<CostControlEntity.ConsumptionUnit, bool>>>(filter));
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    ConsumptionUnitMapperConfig = null;
                    ConsumptionUnitIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.ConsumptionUnit, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.ConsumptionUnit item, Expression<Func<CostControlBusinessEntity.ConsumptionUnit, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.ConsumptionUnit> GetByParentId(long parentId,
            Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IOrderedQueryable<CostControlBusinessEntity.ConsumptionUnit>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.ConsumptionUnit>, IIncludableQueryable<CostControlBusinessEntity.ConsumptionUnit, object>>>> includeProperties = null,
            int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~ConsumptionUnitLogic()
        {
            Dispose(false);
        }
    }
}