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
    public class DataImportLogic : IGenericLogic<CostControlBusinessEntity.DataImport>, IDisposable
    {
        private MapperConfiguration DataImportMapperConfig { get; set; }

        private IMapper DataImportIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.DataImport> Repository;

        public DataImportLogic()
        {
            DataImportMapperConfig = new AutoMapperConfiguration().Configure();
            DataImportIMapper = DataImportMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.DataImport>();
        }

        public CostControlBusinessEntity.DataImport Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                CostControlBusinessEntity.DataImport result = DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> Remove(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<CostControlBusinessEntity.DataImport> result = null;

            IEnumerable<CostControlEntity.DataImport> deleteLst = Repository.Get(DataImportIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                                    Expression<Func<CostControlEntity.DataImport, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.DataImport>)
                    .ForEach(s => result.Add(DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.DataImport> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            CostControlEntity.DataImport entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                CostControlBusinessEntity.DataImport result = DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> RemoveAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result = DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                  Repository
                  .Remove(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.DataImport Exists(object primaryKey)
        {
            return DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Exists(primaryKey));
        }

        public async Task<CostControlBusinessEntity.DataImport> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(await Repository.ExistsAsync(cancellationToken, primaryKey));
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> Get(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            return DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>, IEnumerable<CostControlBusinessEntity.DataImport>>(
                           Repository.Get(
                               DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>, Expression<Func<CostControlEntity.DataImport, bool>>>(filter),
                               DataImportIMapper.Map<Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>>,
                               Func<IQueryable<CostControlEntity.DataImport>, IOrderedQueryable<CostControlEntity.DataImport>>>(orderBy),
                               DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties), pageNumber, pageSize));
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> GetAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DataImportIMapper.Map<Task<IEnumerable<CostControlEntity.DataImport>>, Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                           Repository.GetAsync(
                               DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>, Expression<Func<CostControlEntity.DataImport, bool>>>(filter),
                               DataImportIMapper.Map<Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>>,
                               Func<IQueryable<CostControlEntity.DataImport>, IOrderedQueryable<CostControlEntity.DataImport>>>(orderBy),
                               DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));
        }

        public CostControlBusinessEntity.DataImport GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null)
        {
            return id == null ? null : DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>
                       (Repository.GetById(id, DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties)));
        }

        public async Task<CostControlBusinessEntity.DataImport> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return id == null ? null : DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>
                       (await Repository.GetByIdAsync(id, DataImportIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.DataImport>, IIncludableQueryable<CostControlEntity.DataImport, object>>>>(includeProperties), cancellationToken));
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> GetWithRawSql(string query, params object[] parameters)
        {
            return DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>, IEnumerable<CostControlBusinessEntity.DataImport>>(Repository.GetWithRawSql(query, parameters));
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        {
            return DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>, IEnumerable<CostControlBusinessEntity.DataImport>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));
        }

        public CostControlBusinessEntity.DataImport Add(CostControlBusinessEntity.DataImport entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlBusinessEntity.DataImport result = DataImportIMapper
                    .Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(
                        Repository.Add(DataImportIMapper.Map<CostControlBusinessEntity.DataImport, CostControlEntity.DataImport>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.DataImport> AddAsync(CostControlBusinessEntity.DataImport entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.DataImport DataImport = DataImportIMapper.Map<CostControlBusinessEntity.DataImport, CostControlEntity.DataImport>(entity);

                CostControlBusinessEntity.DataImport result = DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(Repository.Add(DataImport));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.DataImport Update(CostControlBusinessEntity.DataImport entity)
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.DataImport DataImport = DataImportIMapper.Map<CostControlBusinessEntity.DataImport, CostControlEntity.DataImport>(entity);

                CostControlBusinessEntity.DataImport result = DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(Repository.Update(DataImport));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.DataImport> UpdateAsync(CostControlBusinessEntity.DataImport entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            try
            {
                CostControlEntity.DataImport DataImport = DataImportIMapper.Map<CostControlBusinessEntity.DataImport, CostControlEntity.DataImport>(entity);

                CostControlBusinessEntity.DataImport result = DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(Repository.Update(DataImport));

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

        public CostControlBusinessEntity.DataImport SingleOrDefault(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        {
            return DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(
                       Repository.SingleOrDefault(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                           Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));
        }

        public async Task<CostControlBusinessEntity.DataImport> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DataImportIMapper.Map<Task<CostControlEntity.DataImport>, Task<CostControlBusinessEntity.DataImport>>(
                           Repository.SingleOrDefaultAsync(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                               Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));
        }

        public CostControlBusinessEntity.DataImport FirstOrDefault(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        {
            return DataImportIMapper.Map<CostControlEntity.DataImport, CostControlBusinessEntity.DataImport>(
                           Repository.SingleOrDefault(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                               Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));
        }

        public async Task<CostControlBusinessEntity.DataImport> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DataImportIMapper.Map<Task<CostControlEntity.DataImport>, Task<CostControlBusinessEntity.DataImport>>(
                           Repository.SingleOrDefaultAsync(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                               Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> AddRange(IEnumerable<CostControlBusinessEntity.DataImport> entities)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result =
                DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                      Repository.AddRange(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.DataImport> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result = await DataImportIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                      Repository
                      .AddRange(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> RemoveFiltered(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result = DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                    Repository.RemoveFiltered(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                        Expression<Func<CostControlEntity.DataImport, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result = await DataImportIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                    Repository.RemoveFilteredAsync(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                    Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> RemoveRange(IEnumerable<CostControlBusinessEntity.DataImport> entities)
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result = DataImportIMapper.Map<IEnumerable<CostControlBusinessEntity.DataImport>>(
                    Repository.RemoveRange(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.DataImport>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.DataImport> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                IEnumerable<CostControlBusinessEntity.DataImport> result = await DataImportIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.DataImport>>>(
                        Repository
                        .Remove(DataImportIMapper.Map<IEnumerable<CostControlEntity.DataImport>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.DataImport Exists(params object[] primaryKey)
        {
            return DataImportIMapper.Map<CostControlBusinessEntity.DataImport>(Repository.Exists(primaryKey));
        }

        public async Task<CostControlBusinessEntity.DataImport> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        {
            return await DataImportIMapper.Map<Task<CostControlBusinessEntity.DataImport>>(Repository.ExistsAsync(cancellationToken, primaryKey));
        }

        public bool Exists(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        {
            return Repository.Exists(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter));
        }

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.ExistsAsync(DataImportIMapper.Map<Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken);
        }

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Repository.CountAsync(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                       Expression<Func<CostControlEntity.DataImport, bool>>>(filter), cancellationToken);
        }

        public int GetCount(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        {
            return Repository.Count(DataImportIMapper.Map<Expression<Func<CostControlBusinessEntity.DataImport, bool>>,
                           Expression<Func<CostControlEntity.DataImport, bool>>>(filter));
        }

        public int GetData()
        {
            return Repository.RunRawSql("EXEC dbo.Sp_GetData");
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    DataImportMapperConfig = null;
                    DataImportIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.DataImport, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.DataImport item, Expression<Func<CostControlBusinessEntity.DataImport, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.DataImport> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.DataImport>, IOrderedQueryable<CostControlBusinessEntity.DataImport>> orderBy = null, ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.DataImport>, IIncludableQueryable<CostControlBusinessEntity.DataImport, object>>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~DataImportLogic()
        {
            Dispose(false);
        }
    }
}