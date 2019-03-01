using CostControl.Entity.Models.Base.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CostControl.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters);

        int RunRawSql(string query, params object[] parameters);

        Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            ICollection<Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>> includeProperties = null,
            int? page = null,
            int? pageSize = null,
            bool disableTracking = false,
            bool eagerLoaging =  false);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            ICollection<Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>> includeProperties = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken),
            bool disableTracking = false,
            bool eagerLoaging = false);

        TEntity GetById(object id,
            ICollection<Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>> includeProperties = null
            );
        
        Task<TEntity> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        //TODO:
        //Min, Max, Sum, Average, Contains, ToList, ToArray, ToDictionary, Load, ForEach

        TEntity Add(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        TEntity Remove(object id);

        TEntity Remove(TEntity entity);

        IEnumerable<TEntity> RemoveFiltered(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> RemoveFilteredAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken));

        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> RemoveRange(IEnumerable<object> ids);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> UpdateFiltered(Expression<Func<TEntity, bool>> filter);

        int Count(Expression<Func<TEntity, bool>> filter = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        TEntity Exists(params object[] primaryKey);

        Task<TEntity> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey);

        bool Exists(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task LoadPropertyAsync(TEntity item,
            Expression<Func<TEntity, object>> property,
            CancellationToken cancellationToken = default(CancellationToken));

        void SetUnchanged(TEntity entity);
    }
}