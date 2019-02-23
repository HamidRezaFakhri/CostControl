using CostControl.BusinessEntity.Models.Base.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CostControl.BusinessLogic.Logics.Base
{
    //public interface IGenericLogic<TEntity> where TEntity : class, IBaseEntity, new()
    //{
    //    Task<TEntity> RemoveAsync(object id,
    //        CancellationToken cancellationToken = default(CancellationToken));

    //    Task<IEnumerable<TEntity>> RemoveAsync(
    //        Expression<Func<TEntity, bool>> filter,
    //        CancellationToken cancellationToken = default(CancellationToken));

    //    Task<TEntity> AddAsync(TEntity entity,
    //        CancellationToken cancellationToken = default(CancellationToken));

    //    Task<TEntity> UpdateAsync(TEntity entity,
    //        CancellationToken cancellationToken = default(CancellationToken));

    //    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities,
    //        CancellationToken cancellationToken = default(CancellationToken));

    //    Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities,
    //        CancellationToken cancellationToken = default(CancellationToken));
    //}

    public interface IGenericLogic<TEntity> : IBaseLogic where TEntity : class, IBaseEntity, new()
    {
        IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters);

        Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters);

        int RunRawSql(string query,
            params object[] parameters);

        Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

        IEnumerable<TEntity> GetByParentId(
            long parentId,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity GetById(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null);

        Task<TEntity> GetByIdAsync(object id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity FirstOrDefault(Expression<Func<TEntity,
            bool>> filter = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken));

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity Remove(object id);

        Task<TEntity> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken));

        IEnumerable<TEntity> Remove(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> RemoveAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken));

        IEnumerable<TEntity> RemoveFiltered(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> RemoveFilteredAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken));

        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken));
        
        TEntity Exists(params object[] primaryKey);

        Task<TEntity> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey);

        bool Exists(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task LoadPropertyAsync(TEntity item,
            Expression<Func<TEntity, object>> property,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}