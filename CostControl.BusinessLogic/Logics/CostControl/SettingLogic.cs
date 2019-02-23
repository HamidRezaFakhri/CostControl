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
    public class SettingLogic : IGenericLogic<CostControlBusinessEntity.Setting>, IDisposable
    {
        private MapperConfiguration SettingMapperConfig { get; set; }

        private IMapper SettingIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Setting> Repository;

        public SettingLogic()
        {
            SettingMapperConfig = new AutoMapperConfiguration().Configure();
            SettingIMapper = SettingMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Setting>();
        }

        public CostControlBusinessEntity.Setting Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Setting> Remove(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Setting> result = null;

            var deleteLst = Repository.Get(SettingIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                                    Expression<Func<CostControlEntity.Setting, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Setting>)
                    .ForEach(s => result.Add(SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Setting> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Setting>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
                  Repository
                  .Remove(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Setting Exists(object primaryKey)
            => SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Setting> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => SettingIMapper.Map<CostControlBusinessEntity.Setting>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Setting> Get(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>, IEnumerable<CostControlBusinessEntity.Setting>>(
                Repository.Get(
                    SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>, Expression<Func<CostControlEntity.Setting, bool>>>(filter),
                    SettingIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>>,
                    Func<IQueryable<CostControlEntity.Setting>, IOrderedQueryable<CostControlEntity.Setting>>>(orderBy),
                    SettingIMapper.Map<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Setting>> GetAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SettingIMapper.Map<Task<IEnumerable<CostControlEntity.Setting>>, Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
                Repository.GetAsync(
                    SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>, Expression<Func<CostControlEntity.Setting, bool>>>(filter),
                    SettingIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>>,
                    Func<IQueryable<CostControlEntity.Setting>, IOrderedQueryable<CostControlEntity.Setting>>>(orderBy),
                    SettingIMapper.Map<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Setting GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>> includeProperties = null)
        => id == null ? null : SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>
            (Repository.GetById(id, SettingIMapper.Map<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Setting> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(SettingIMapper.Map<Task<Entity.Models.Setting>, Task<Setting>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>
            (await Repository.GetByIdAsync(id, SettingIMapper.Map<Func<IQueryable<CostControlEntity.Setting>, IIncludableQueryable<CostControlEntity.Setting, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Setting> GetWithRawSql(string query, params object[] parameters)
        => SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>, IEnumerable<CostControlBusinessEntity.Setting>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Setting>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>, IEnumerable<CostControlBusinessEntity.Setting>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Setting Add(CostControlBusinessEntity.Setting entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = SettingIMapper
                    .Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(
                        Repository.Add(SettingIMapper.Map<CostControlBusinessEntity.Setting, CostControlEntity.Setting>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Setting> AddAsync(CostControlBusinessEntity.Setting entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Setting = SettingIMapper.Map<CostControlBusinessEntity.Setting, CostControlEntity.Setting>(entity);

                var result = SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(Repository.Add(Setting));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Setting Update(CostControlBusinessEntity.Setting entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Setting Setting = SettingIMapper.Map<CostControlBusinessEntity.Setting, CostControlEntity.Setting>(entity);

                var result = SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(Repository.Update(Setting));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Setting> UpdateAsync(CostControlBusinessEntity.Setting entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Setting = SettingIMapper.Map<CostControlBusinessEntity.Setting, CostControlEntity.Setting>(entity);

                var result = SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(Repository.Update(Setting));

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

        public CostControlBusinessEntity.Setting SingleOrDefault(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
        => SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(
            Repository.SingleOrDefault(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Setting> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SettingIMapper.Map<Task<CostControlEntity.Setting>, Task<CostControlBusinessEntity.Setting>>(
                Repository.SingleOrDefaultAsync(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                    Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Setting FirstOrDefault(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
        => SettingIMapper.Map<CostControlEntity.Setting, CostControlBusinessEntity.Setting>(
                Repository.SingleOrDefault(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                    Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Setting> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await SettingIMapper.Map<Task<CostControlEntity.Setting>, Task<CostControlBusinessEntity.Setting>>(
                Repository.SingleOrDefaultAsync(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                    Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Setting> AddRange(IEnumerable<CostControlBusinessEntity.Setting> entities)
        {
            try
            {
                var result =
                SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
                      Repository.AddRange(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Setting>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Setting> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SettingIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
                      Repository
                      .AddRange(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Setting> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter)
        {
            try
            {
                var result = SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
                    Repository.RemoveFiltered(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                        Expression<Func<CostControlEntity.Setting, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Setting>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SettingIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
                    Repository.RemoveFilteredAsync(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                    Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Setting> RemoveRange(IEnumerable<CostControlBusinessEntity.Setting> entities)
        {
            try
            {
                var result = SettingIMapper.Map<IEnumerable<CostControlBusinessEntity.Setting>>(
                    Repository.RemoveRange(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Setting>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Setting> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await SettingIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Setting>>>(
                        Repository
                        .Remove(SettingIMapper.Map<IEnumerable<CostControlEntity.Setting>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Setting Exists(params object[] primaryKey)
        => SettingIMapper.Map<CostControlBusinessEntity.Setting>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Setting> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await SettingIMapper.Map<Task<CostControlBusinessEntity.Setting>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
        => Repository.Exists(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(SettingIMapper.Map<Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
            Expression<Func<CostControlEntity.Setting, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
            => Repository.Count(SettingIMapper.Map<Expression<Func<CostControlBusinessEntity.Setting, bool>>,
                Expression<Func<CostControlEntity.Setting, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    SettingMapperConfig = null;
                    SettingIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Setting, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Setting item, Expression<Func<CostControlBusinessEntity.Setting, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Setting> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Setting>, IOrderedQueryable<CostControlBusinessEntity.Setting>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.Setting>, IIncludableQueryable<CostControlBusinessEntity.Setting, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~SettingLogic()
        {
            Dispose(false);
        }
    }
}