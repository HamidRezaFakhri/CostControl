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
    public class DepoLogic : IGenericLogic<CostControlBusinessEntity.Depo>, IDisposable
    {
        private MapperConfiguration DepoMapperConfig { get; set; }

        private IMapper DepoIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Depo> Repository;

        public DepoLogic()
        {
            DepoMapperConfig = new AutoMapperConfiguration().Configure();
            DepoIMapper = DepoMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Depo>();
        }

        public CostControlBusinessEntity.Depo Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Depo> Remove(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Depo> result = null;

            var deleteLst = Repository.Get(DepoIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                                    Expression<Func<CostControlEntity.Depo, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Depo>)
                    .ForEach(s => result.Add(DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Depo> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                  Repository
                  .Remove(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Depo Exists(object primaryKey)
            => DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Depo> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Depo> Get(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.Depo, object>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>, IEnumerable<CostControlBusinessEntity.Depo>>(
                Repository.Get(
                    DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>, Expression<Func<CostControlEntity.Depo, bool>>>(filter),
                    DepoIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>>,
                    Func<IQueryable<CostControlEntity.Depo>, IOrderedQueryable<CostControlEntity.Depo>>>(orderBy),
                    DepoIMapper.Map<List<Expression<Func<CostControlEntity.Depo, object>>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> GetAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>> orderBy = null,
            List<Expression<Func<CostControlBusinessEntity.Depo, object>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<IEnumerable<CostControlEntity.Depo>>, Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                Repository.GetAsync(
                    DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>, Expression<Func<CostControlEntity.Depo, bool>>>(filter),
                    DepoIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>>,
                    Func<IQueryable<CostControlEntity.Depo>, IOrderedQueryable<CostControlEntity.Depo>>>(orderBy),
                    DepoIMapper.Map<List<Expression<Func<CostControlEntity.Depo, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Depo GetById(object id,
            List<Expression<Func<CostControlBusinessEntity.Depo, object>>> includeProperties = null)
        => id == null ? null : DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>
            (Repository.GetById(id, DepoIMapper.Map<List<Expression<Func<CostControlEntity.Depo, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Depo> GetByIdAsync(object id,
            List<Expression<Func<CostControlBusinessEntity.Depo, object>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(DepoIMapper.Map<Task<Entity.Models.Depo>, Task<Depo>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>
            (await Repository.GetByIdAsync(id, DepoIMapper.Map<List<Expression<Func<CostControlEntity.Depo, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Depo> GetWithRawSql(string query, params object[] parameters)
        => DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>, IEnumerable<CostControlBusinessEntity.Depo>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>, IEnumerable<CostControlBusinessEntity.Depo>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Depo Add(CostControlBusinessEntity.Depo entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = DepoIMapper
                    .Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(
                        Repository.Add(DepoIMapper.Map<CostControlBusinessEntity.Depo, CostControlEntity.Depo>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Depo> AddAsync(CostControlBusinessEntity.Depo entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Depo = DepoIMapper.Map<CostControlBusinessEntity.Depo, CostControlEntity.Depo>(entity);

                var result = DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(Repository.Add(Depo));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Depo Update(CostControlBusinessEntity.Depo entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Depo Depo = DepoIMapper.Map<CostControlBusinessEntity.Depo, CostControlEntity.Depo>(entity);

                var result = DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(Repository.Update(Depo));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Depo> UpdateAsync(CostControlBusinessEntity.Depo entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Depo = DepoIMapper.Map<CostControlBusinessEntity.Depo, CostControlEntity.Depo>(entity);

                var result = DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(Repository.Update(Depo));

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

        public CostControlBusinessEntity.Depo SingleOrDefault(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(
            Repository.SingleOrDefault(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Depo> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<CostControlEntity.Depo>, Task<CostControlBusinessEntity.Depo>>(
                Repository.SingleOrDefaultAsync(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                    Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Depo FirstOrDefault(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => DepoIMapper.Map<CostControlEntity.Depo, CostControlBusinessEntity.Depo>(
                Repository.SingleOrDefault(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                    Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Depo> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DepoIMapper.Map<Task<CostControlEntity.Depo>, Task<CostControlBusinessEntity.Depo>>(
                Repository.SingleOrDefaultAsync(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                    Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Depo> AddRange(IEnumerable<CostControlBusinessEntity.Depo> entities)
        {
            try
            {
                var result =
                DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                      Repository.AddRange(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Depo> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await DepoIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                      Repository
                      .AddRange(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Depo> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter)
        {
            try
            {
                var result = DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                    Repository.RemoveFiltered(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                        Expression<Func<CostControlEntity.Depo, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await DepoIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                    Repository.RemoveFilteredAsync(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                    Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Depo> RemoveRange(IEnumerable<CostControlBusinessEntity.Depo> entities)
        {
            try
            {
                var result = DepoIMapper.Map<IEnumerable<CostControlBusinessEntity.Depo>>(
                    Repository.RemoveRange(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Depo>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Depo> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await DepoIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Depo>>>(
                        Repository
                        .Remove(DepoIMapper.Map<IEnumerable<CostControlEntity.Depo>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Depo Exists(params object[] primaryKey)
        => DepoIMapper.Map<CostControlBusinessEntity.Depo>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Depo> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await DepoIMapper.Map<Task<CostControlBusinessEntity.Depo>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        => Repository.Exists(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(DepoIMapper.Map<Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
            Expression<Func<CostControlEntity.Depo, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
            => Repository.Count(DepoIMapper.Map<Expression<Func<CostControlBusinessEntity.Depo, bool>>,
                Expression<Func<CostControlEntity.Depo, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    DepoMapperConfig = null;
                    DepoIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Depo, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Depo item, Expression<Func<CostControlBusinessEntity.Depo, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Depo> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Depo>, IOrderedQueryable<CostControlBusinessEntity.Depo>> orderBy = null, List<Expression<Func<CostControlBusinessEntity.Depo, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~DepoLogic()
        {
            Dispose(false);
        }
    }
}