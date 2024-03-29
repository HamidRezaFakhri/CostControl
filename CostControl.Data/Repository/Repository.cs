﻿using CostControl.Entity.Models.Base.Enums;
using CostControl.Entity.Models.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CostControl.Data.Repository
{
    /// <summary>
    /// Generic repository class for entity operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class, IBaseEntity, new()
        //public class Repository<TEntity> where TEntity : class
    {
        //private readonly OrderBy<TEntity> DefaultOrderBy = new OrderBy<TEntity>(qry => qry.OrderBy(e => e.Id));

        internal protected DbContext Context = null;

        internal protected DbSet<TEntity> DbSet;
        //internal IDbSet<TEntity> dbSet;
        private string _errorMessage = string.Empty;

        //internal IObjectSet<TEntity> dbSet;

        public Repository() { }

        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context?.Set<TEntity>();
        }

        //protected IDbFactory DbFactory
        //{
        //    get;
        //    private set;
        //}

        //protected PdkGdsAppContext DbContext => Context ?? (Context = DbFactory.Init());

        //public Repository(IDbFactory dbFactory)
        //{
        //    DbFactory = dbFactory;
        //    this.DbSet = Context.Set<TEntity>();
        //}

        /// <summary>
        /// generic method for Entities by raw sql
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters)
            => DbSet.FromSql(query, parameters);

        /// <summary>
        /// generic method for Entities by raw sql Async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
            => await DbSet.FromSql(query, parameters).ToListAsync(cancellationToken);

        public virtual int RunRawSql(string query,
            params object[] parameters)
            => Context.Database.ExecuteSqlCommand(query, parameters);

        public virtual async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken), params object[] parameters)
            => await Context.Database.ExecuteSqlCommandAsync(query, cancellationToken, parameters);
        //{
        //    using (var dbContextTransaction = Context.Database.BeginTransaction(/*System.Data.IsolationLevel.Snapshot*/))
        //    {
        //        try
        //        {
        //            var result = await Context.Database.ExecuteSqlCommandAsync(query, cancellationToken, parameters);

        //            dbContextTransaction.Commit();
        //            return result;
        //        }
        //        catch (Exception e)
        //        {
        //            dbContextTransaction.Rollback();
        //            throw e;
        //        }
        //    }
        //}

        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <param name="filter">Filter expression for filtering the entities.</param>
        /// <param name="orderBy">Sorting the entities.</param>
        /// <param name="includeProperties">Include for eager-loading.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            //Action<IFetchOptions<TEntity>> fetchOptions = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            else
            {
                query = query.Where(e => e.State == ObjectState.Active);
            }

            //foreach (Expression<Func<TEntity, object>> include in includeProperties)
            //    query = query.Include(include);

            if (includeProperties != null)
            {
                query = includeProperties
                    .Aggregate(query, (current, include) => current.Include(include));
            }

            query = orderBy != null ? orderBy(query) : query.OrderBy(q => q.State);

            if (page.HasValue && page != null && pageSize.HasValue && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
            //return query.AsNoTracking();
        }

        /// <summary>
        /// generic Get method for Entities as async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
            //=> await Task.FromResult(Get(filter, orderBy, includeProperties, page, pageSize));
            => await Get(filter, orderBy, includeProperties, page, pageSize).AsQueryable().ToListAsync(cancellationToken);

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null)
            => filter == null ? DbSet.SingleOrDefault() : DbSet.SingleOrDefault(filter);

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => filter == null
                ? await DbSet.SingleOrDefaultAsync(cancellationToken)
                : await DbSet.SingleOrDefaultAsync(filter, cancellationToken);

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
            => filter == null ? DbSet.FirstOrDefault() : DbSet.FirstOrDefault(filter);

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => filter == null
                ? await DbSet.FirstOrDefaultAsync(cancellationToken)
                : await DbSet.FirstOrDefaultAsync(filter, cancellationToken);

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(object id, List<Expression<Func<TEntity, object>>> includeProperties = null)
            => id == null ? null : DbSet.Find(id);

        /// <summary>
        /// Generic get method on the basis of id for Entities as async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(object id,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => id == null ? null : await DbSet.FindAsync(cancellationToken, id);
        
        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                //if (ModelState.IsValid)
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Context.Entry(entity).State = EntityState.Added;

                //Context.Add(entity);

                return entity;
            }
            catch (Exception dbEx)
            {
                throw GetDbEntityValidationExceptionDetails(dbEx);
            }
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException(nameof(entities));
                }

                DbSet.AddRange(entities);
                return entities;
            }
            catch (Exception dbEx)
            {
                throw GetDbEntityValidationExceptionDetails(dbEx);
            }
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual TEntity Remove(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            return Remove(DbSet.Find(id));
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual TEntity Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                //DbEntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);

                //if (Context.Entry<TEntity>(entity).State == EntityState.Detached)
                //{
                //    DbSet.Attach(entity);
                //}
                //DbSet.Remove(entity);

                Context.Entry(entity).State = EntityState.Deleted;
                //Context.Remove(entity);
                return entity;
            }
            catch (Exception dbEx)
            {
                throw GetDbEntityValidationExceptionDetails(dbEx);
            }
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> RemoveFiltered(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }
            //var deleteLst = DbSet.Where<TEntity>(filter).AsQueryable();

            var delList = new List<TEntity>();

            DbSet
                .Where<TEntity>(filter)
                .ToList()
                .ForEach(entity =>
                    {
                        //DbSet.Remove(obj);
                        Context.Entry(entity).State = EntityState.Deleted;
                        delList.Add(entity);
                    }
                );

            return delList.AsEnumerable();
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition async.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> RemoveFilteredAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
            => await RemoveFiltered(filter).AsQueryable().ToListAsync(cancellationToken);

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
            //if (entities == null) throw new ArgumentNullException(nameof(entities));

            //return Context.RemoveRange(entities);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<object> ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                //if (HasChenges())
                //{

                //}

                Context.Entry(entity).State = EntityState.Modified;
                //Context.Update(entity);

                //DbSet.Attach(entity);
                //Context.Entry(entity).State = EntityState.Modified;

                return entity;
            }
            catch (Exception dbEx)
            {
                throw GetDbEntityValidationExceptionDetails(dbEx);
            }
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }

            return entities;
        }

        public IEnumerable<TEntity> UpdateFiltered(Expression<Func<TEntity, bool>> filter)
        {
            var entities = Get(filter);

            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }

            return entities;
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual TEntity Exists(params object[] primaryKey)
            => DbSet.Find(primaryKey);

        /// <summary>
        /// Generic method to check if entity exists async
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
            => await DbSet.FindAsync(cancellationToken, primaryKey);

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
            => filter == null ? DbSet.Count() : DbSet.Count(filter);

        /// <summary>
        /// Generic count for entities async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => await DbSet.CountAsync(filter, cancellationToken);

        public virtual bool Exists(Expression<Func<TEntity, bool>> filter = null)
            => DbSet.Any(filter);

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => await DbSet.AnyAsync(filter, cancellationToken);

        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        => DbSet.Any(filter);

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await DbSet.AnyAsync(filter, cancellationToken);

        public virtual async Task LoadPropertyAsync(TEntity item,
            Expression<Func<TEntity, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Context.Entry(item).Reference(property).LoadAsync(cancellationToken);

        public void SetUnchanged(TEntity entity)
        => Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

        ///// <summary>
        ///// method to delete is there any changes or not
        ///// </summary>
        ///// <returns></returns>
        //public virtual bool HasChenges()
        //{
        //    return context.ChangeTracker.HasChanges() || (context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
        //                                      || e.State == EntityState.Modified
        //                                      || e.State == EntityState.Deleted));
        //}

        public bool HasPendingChenges => false;

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }

        private Exception GetDbEntityValidationExceptionDetails(Exception exception)
        {
            //foreach (var validationErrors in exception.EntityValidationErrors)
            //{
            //    foreach (var validationError in validationErrors.ValidationErrors)
            //    {
            //        _errorMessage += Environment.NewLine +
            //                         $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
            //    }
            //}
            return new Exception(_errorMessage, exception);
        }

        //Attach, Detach
    }
}