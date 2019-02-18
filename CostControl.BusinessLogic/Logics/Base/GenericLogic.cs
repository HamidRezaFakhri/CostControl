using AutoMapper;
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

namespace CostControl.BusinessLogic.Logics.Base
{
    //public abstract class GenericLogic<TEntity> : IDisposable, IGenericLogic<TEntity> where TEntity : class, new()
    //{
    //    protected MapperConfiguration GenericMapperConfig { get; set; }

    //    protected IMapper GenericIMapper { get; set; }

    //    protected readonly UnitOfWork UnitOfWork;

    //    protected IRepository<TEntity> Repository;

    //    public GenericLogic()
    //    {
    //        GenericMapperConfig = new AutoMapperConfiguration().Configure();
    //        GenericIMapper = GenericMapperConfig.CreateMapper();
    //        UnitOfWork = new UnitOfWork(new CostControlDbContext());
    //        //Repository = UnitOfWork.GetRepository<TEntity>();
    //    }

    //    public virtual TEntity Add(TEntity entity)
    //    {
    //        //using (var transaction = objectContext.Connection.BeginTransaction())

    //        if (entity == null)
    //        {
    //            return null;
    //        }

    //        try
    //        {
    //            var result = GenericIMapper.Map<TEntity>(
    //                    Repository.Add(GenericIMapper.Map<TEntity>(entity)));
    //            UnitOfWork.Commit();

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
    //    {
    //        try
    //        {
    //            var result = GenericIMapper.Map<IEnumerable<TEntity>>(
    //                  Repository.AddRange(GenericIMapper.Map<IEnumerable<TEntity>>(entities)));

    //            UnitOfWork.Commit();

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
    //    => Repository.Any(GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter));

    //    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await Repository.AnyAsync(GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter), cancellationToken);
        
    //    public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
    //    => Repository.Count(GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter));
        
    //    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await Repository.CountAsync(GenericIMapper.Map<Expression<Func<TEntity, bool>>,
    //        Expression<Func<TEntity, bool>>>(filter), cancellationToken);

    //    public virtual TEntity Exists(params object[] primaryKey)
    //    => GenericIMapper.Map<TEntity>(Repository.Exists(primaryKey));

    //    public virtual bool Exists(Expression<Func<TEntity, bool>> filter = null)
    //    => Repository.Exists(GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter));

    //    public virtual async Task<TEntity> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
    //        params object[] primaryKey)
    //    => GenericIMapper.Map<TEntity>(await Repository.ExistsAsync(cancellationToken, primaryKey));

    //    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await Repository.ExistsAsync(GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter), cancellationToken);

    //    public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
    //    => GenericIMapper.Map<TEntity>(
    //            Repository.SingleOrDefault(GenericIMapper.Map<Expression<Func<TEntity, bool>>,
    //                Expression<Func<TEntity, bool>>>(filter)));

    //    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await GenericIMapper.Map<Task<TEntity>>(
    //            Repository.SingleOrDefaultAsync(GenericIMapper.Map<Expression<Func<TEntity, bool>>,
    //                Expression<Func<TEntity, bool>>>(filter), cancellationToken));

    //    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        List<Expression<Func<TEntity, object>>> includeProperties = null,
    //        int? page = null,
    //        int? pageSize = null)
    //    => GenericIMapper.Map<IEnumerable<TEntity>>(
    //            Repository.Get(
    //                GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter),
    //                GenericIMapper.Map<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>,
    //                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(orderBy),
    //                GenericIMapper.Map<List<Expression<Func<TEntity, object>>>>(includeProperties),
    //                page,
    //                pageSize));

    //    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        List<Expression<Func<TEntity, object>>> includeProperties = null,
    //        int? page = null,
    //        int? pageSize = null,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await GenericIMapper.Map<Task<IEnumerable<TEntity>>>(
    //            Repository.GetAsync(
    //                GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter),
    //                GenericIMapper.Map<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>,
    //                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(orderBy),
    //                GenericIMapper.Map<List<Expression<Func<TEntity, object>>>>(includeProperties),
    //                page, pageSize, cancellationToken));

    //    public virtual TEntity GetById(object id,
    //        List<Expression<Func<TEntity, object>>> includeProperties = null)
    //    => id == null ? null : GenericIMapper.Map<TEntity>
    //        (Repository.GetById(id, GenericIMapper.Map<List<Expression<Func<TEntity, object>>>>(includeProperties)));

    //    public virtual async Task<TEntity> GetByIdAsync(object id,
    //        List<Expression<Func<TEntity, object>>> includeProperties = null,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => id == null ? null : GenericIMapper.Map<TEntity>
    //        (await Repository
    //                        .GetByIdAsync(id, GenericIMapper.Map<List<Expression<Func<TEntity, object>>>>(includeProperties), cancellationToken));

    //    public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
    //    => GenericIMapper.Map<IEnumerable<TEntity>>(Repository.GetWithRawSql(query, parameters));

    //    public virtual async Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query,
    //        CancellationToken cancellationToken = default(CancellationToken),
    //        params object[] parameters)
    //    => GenericIMapper.Map<IEnumerable<TEntity>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

    //    public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await Repository.LoadPropertyAsync(GenericIMapper.Map<TEntity>(item),
    //        GenericIMapper.Map<Expression<Func<TEntity, object>>>(property), cancellationToken);

    //    public virtual TEntity Remove(object id)
    //    {
    //        if (id == null)
    //        {
    //            return null;
    //        }

    //        if (Repository.GetById(id) != null)
    //        {
    //            var result = GenericIMapper
    //                .Map<TEntity>(Repository.Remove(id));
    //            UnitOfWork.Commit();

    //            return result;
    //        }

    //        return null;
    //    }

    //    public virtual TEntity Remove(TEntity entity)
    //    => Repository.Remove(GenericIMapper.Map<TEntity>(entity));

    //    public virtual IEnumerable<TEntity> RemoveFiltered(Expression<Func<TEntity, bool>> filter)
    //    {
    //        if (filter == null)
    //        {
    //            return null;
    //        }

    //        List<TEntity> result = null;

    //        var deleteLst = Repository.Get(GenericIMapper
    //                            .Map<Expression<Func<TEntity, bool>>>(filter));

    //        if (deleteLst != null)
    //        {
    //            (deleteLst as List<TEntity>)
    //                .ForEach(s => result.Add(GenericIMapper.Map<TEntity>(Repository.Remove(s))));

    //            UnitOfWork.Commit();

    //            return result;
    //        }

    //        return null;
    //    }

    //    public virtual async Task<IEnumerable<TEntity>> RemoveFilteredAsync(Expression<Func<TEntity, bool>> filter,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        try
    //        {
    //            var result = await GenericIMapper.Map<Task<IEnumerable<TEntity>>>(
    //                Repository.RemoveFilteredAsync(GenericIMapper.Map<Expression<Func<TEntity, bool>>,
    //                Expression<Func<TEntity, bool>>>(filter), cancellationToken));

    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
    //    {
    //        try
    //        {
    //            var result = GenericIMapper.Map<IEnumerable<TEntity>>(
    //                Repository.RemoveRange(GenericIMapper.Map<IEnumerable<TEntity>>(entities)));

    //            UnitOfWork.Commit();

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<object> ids)
    //    => GenericIMapper.Map<IEnumerable<TEntity>>(Repository.RemoveRange(ids));

    //    public virtual int RunRawSql(string query, params object[] parameters)
    //    => Repository.RunRawSql(query, parameters);

    //    public virtual async Task<int> RunRawSqlAsync(string query,
    //        CancellationToken cancellationToken = default(CancellationToken),
    //        params object[] parameters)
    //    => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

    //    public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null)
    //    => GenericIMapper.Map<TEntity>(
    //        Repository.SingleOrDefault(GenericIMapper.Map<Expression<Func<TEntity, bool>>,
    //            Expression<Func<TEntity, bool>>>(filter)));

    //    public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    => await GenericIMapper.Map<Task<TEntity>>(
    //            Repository.SingleOrDefaultAsync(GenericIMapper.Map<Expression<Func<TEntity, bool>>,
    //                Expression<Func<TEntity, bool>>>(filter), cancellationToken));

    //    public virtual TEntity Update(TEntity entity)
    //    {
    //        if (entity == null)
    //        {
    //            return null;
    //        }

    //        try
    //        {
    //            //SecurityEntity.Role role = RoleIMapper.Map<SecurityBusinessEntity.Role, SecurityEntity.Role>(entity);

    //            var result = GenericIMapper.Map<TEntity>(Repository.Update(entity));
    //            UnitOfWork.Commit();

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual IEnumerable<TEntity> UpdateFiltered(Expression<Func<TEntity, bool>> filter)
    //    => Repository.UpdateFiltered(filter);

    //    public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
    //    => Repository.UpdateRange(entities);

    //    private bool disposedValue = false; // To detect redundant calls

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!disposedValue)
    //        {
    //            if (disposing)
    //            {
    //                // TODO: dispose managed state (managed objects).
    //            }

    //            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
    //            // TODO: set large fields to null.

    //            disposedValue = true;
    //        }
    //    }

    //    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    //    // ~ResturantLogic() {
    //    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //    //   Dispose(false);
    //    // }

    //    // This code added to correctly implement the disposable pattern.
    //    public void Dispose()
    //    {
    //        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //        Dispose(true);
    //        // TODO: uncomment the following line if the finalizer is overridden above.
    //        // GC.SuppressFinalize(this);
    //    }

    //    //--------------------------------------------------------------------
    //    public virtual async Task<TEntity> RemoveAsync(object id,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        if (id == null)
    //        {
    //            return null;
    //        }

    //        var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

    //        if (entity != null)
    //        {
    //            var result = GenericIMapper.Map<TEntity>(Repository.Remove(id));
    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }

    //        return null;
    //    }

    //    public virtual async Task<IEnumerable<TEntity>> RemoveAsync(
    //        Expression<Func<TEntity, bool>> filter,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        try
    //        {
    //            var result = GenericIMapper.Map<IEnumerable<TEntity>>(
    //              Repository
    //              .Remove(GenericIMapper.Map<Expression<Func<TEntity, bool>>>(filter)));

    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual async Task<TEntity> AddAsync(TEntity entity,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        if (entity == null)
    //        {
    //            return null;
    //        }

    //        try
    //        {
    //            //var role = GenericIMapper.Map<TEntity>(entity);

    //            var result = GenericIMapper.Map<TEntity>(Repository.Add(entity));
    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public virtual async Task<TEntity> UpdateAsync(TEntity entity,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        if (entity == null)
    //        {
    //            return null;
    //        }

    //        try
    //        {
    //            //var role = RoleIMapper.Map<SecurityBusinessEntity.Role, SecurityEntity.Role>(entity);

    //            var result = GenericIMapper.Map<TEntity>(Repository.Update(entity));

    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        try
    //        {
    //            var result = await GenericIMapper.Map<Task<IEnumerable<TEntity>>>(
    //                  Repository.AddRange(GenericIMapper.Map<IEnumerable<TEntity>>(entities)));

    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public async Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        try
    //        {
    //            var result = await GenericIMapper.Map<Task<IEnumerable<TEntity>>>(
    //                    Repository.Remove(GenericIMapper.Map<IEnumerable<TEntity>>(entities)));

    //            await UnitOfWork.CommitAsync(cancellationToken);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
}