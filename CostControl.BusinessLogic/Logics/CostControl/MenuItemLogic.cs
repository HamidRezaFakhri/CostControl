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
    public class MenuItemLogic : IGenericLogic<CostControlBusinessEntity.MenuItem>, IDisposable
    {
        private MapperConfiguration MenuItemMapperConfig { get; set; }

        private IMapper MenuItemIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.MenuItem> Repository;

        public MenuItemLogic()
        {
            MenuItemMapperConfig = new AutoMapperConfiguration().Configure();
            MenuItemIMapper = MenuItemMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.MenuItem>();
        }

        public CostControlBusinessEntity.MenuItem Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.MenuItem> Remove(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.MenuItem> result = null;

            var deleteLst = Repository.Get(MenuItemIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                                    Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.MenuItem>)
                    .ForEach(s => result.Add(MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.MenuItem> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> RemoveAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
                  Repository
                  .Remove(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.MenuItem Exists(object primaryKey)
            => MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.MenuItem> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.MenuItem> Get(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>, IEnumerable<CostControlBusinessEntity.MenuItem>>(
                Repository.Get(
                    MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>, Expression<Func<CostControlEntity.MenuItem, bool>>>(filter),
                    MenuItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>>,
                    Func<IQueryable<CostControlEntity.MenuItem>, IOrderedQueryable<CostControlEntity.MenuItem>>>(orderBy),
                    MenuItemIMapper.Map<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>(includeProperties), pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> GetAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>> orderBy = null,
            Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await MenuItemIMapper.Map<Task<IEnumerable<CostControlEntity.MenuItem>>, Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
                Repository.GetAsync(
                    MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>, Expression<Func<CostControlEntity.MenuItem, bool>>>(filter),
                    MenuItemIMapper.Map<Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>>,
                    Func<IQueryable<CostControlEntity.MenuItem>, IOrderedQueryable<CostControlEntity.MenuItem>>>(orderBy),
                    MenuItemIMapper.Map<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.MenuItem GetById(object id,
            Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>> includeProperties = null)
        => id == null ? null : MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>
            (Repository.GetById(id, MenuItemIMapper.Map<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.MenuItem> GetByIdAsync(object id,
            Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(MenuItemIMapper.Map<Task<Entity.Models.MenuItem>, Task<MenuItem>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>
            (await Repository.GetByIdAsync(id, MenuItemIMapper.Map<Func<IQueryable<CostControlEntity.MenuItem>, IIncludableQueryable<CostControlEntity.MenuItem, object>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.MenuItem> GetWithRawSql(string query, params object[] parameters)
        => MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>, IEnumerable<CostControlBusinessEntity.MenuItem>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>, IEnumerable<CostControlBusinessEntity.MenuItem>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.MenuItem Add(CostControlBusinessEntity.MenuItem entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = MenuItemIMapper
                    .Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(
                        Repository.Add(MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem, CostControlEntity.MenuItem>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.MenuItem> AddAsync(CostControlBusinessEntity.MenuItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var MenuItem = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem, CostControlEntity.MenuItem>(entity);

                var result = MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(Repository.Add(MenuItem));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.MenuItem Update(CostControlBusinessEntity.MenuItem entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.MenuItem MenuItem = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem, CostControlEntity.MenuItem>(entity);

                var result = MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(Repository.Update(MenuItem));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.MenuItem> UpdateAsync(CostControlBusinessEntity.MenuItem entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var MenuItem = MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem, CostControlEntity.MenuItem>(entity);

                var result = MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(Repository.Update(MenuItem));

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

        public CostControlBusinessEntity.MenuItem SingleOrDefault(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
        => MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(
            Repository.SingleOrDefault(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.MenuItem> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await MenuItemIMapper.Map<Task<CostControlEntity.MenuItem>, Task<CostControlBusinessEntity.MenuItem>>(
                Repository.SingleOrDefaultAsync(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                    Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.MenuItem FirstOrDefault(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
        => MenuItemIMapper.Map<CostControlEntity.MenuItem, CostControlBusinessEntity.MenuItem>(
                Repository.SingleOrDefault(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                    Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.MenuItem> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await MenuItemIMapper.Map<Task<CostControlEntity.MenuItem>, Task<CostControlBusinessEntity.MenuItem>>(
                Repository.SingleOrDefaultAsync(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                    Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.MenuItem> AddRange(IEnumerable<CostControlBusinessEntity.MenuItem> entities)
        {
            try
            {
                var result =
                MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
                      Repository.AddRange(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.MenuItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await MenuItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
                      Repository
                      .AddRange(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.MenuItem> RemoveFiltered(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter)
        {
            try
            {
                var result = MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
                    Repository.RemoveFiltered(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                        Expression<Func<CostControlEntity.MenuItem, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await MenuItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
                    Repository.RemoveFilteredAsync(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                    Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.MenuItem> RemoveRange(IEnumerable<CostControlBusinessEntity.MenuItem> entities)
        {
            try
            {
                var result = MenuItemIMapper.Map<IEnumerable<CostControlBusinessEntity.MenuItem>>(
                    Repository.RemoveRange(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.MenuItem>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.MenuItem> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await MenuItemIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.MenuItem>>>(
                        Repository
                        .Remove(MenuItemIMapper.Map<IEnumerable<CostControlEntity.MenuItem>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.MenuItem Exists(params object[] primaryKey)
        => MenuItemIMapper.Map<CostControlBusinessEntity.MenuItem>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.MenuItem> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await MenuItemIMapper.Map<Task<CostControlBusinessEntity.MenuItem>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
        => Repository.Exists(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(MenuItemIMapper.Map<Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
            Expression<Func<CostControlEntity.MenuItem, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
            => Repository.Count(MenuItemIMapper.Map<Expression<Func<CostControlBusinessEntity.MenuItem, bool>>,
                Expression<Func<CostControlEntity.MenuItem, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    MenuItemMapperConfig = null;
                    MenuItemIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.MenuItem, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.MenuItem item, Expression<Func<CostControlBusinessEntity.MenuItem, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.MenuItem> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.MenuItem>, IOrderedQueryable<CostControlBusinessEntity.MenuItem>> orderBy = null, Func<IQueryable<CostControlBusinessEntity.MenuItem>, IIncludableQueryable<CostControlBusinessEntity.MenuItem, object>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~MenuItemLogic()
        {
            Dispose(false);
        }
    }
}