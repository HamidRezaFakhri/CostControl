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
    public class MenuLogic : IGenericLogic<CostControlBusinessEntity.Menu>, IDisposable
    {
        private MapperConfiguration MenuMapperConfig { get; set; }

        private IMapper MenuIMapper { get; set; }

        private readonly UnitOfWork _unitOfWork;

        protected IRepository<CostControlEntity.Menu> Repository;

        public MenuLogic()
        {
            MenuMapperConfig = new AutoMapperConfiguration().Configure();
            MenuIMapper = MenuMapperConfig.CreateMapper();
            _unitOfWork = new UnitOfWork(new CostControlDbContext());
            Repository = _unitOfWork.GetRepository<CostControlEntity.Menu>();
        }

        public CostControlBusinessEntity.Menu Remove(object id)
        {
            if (id == null) return null;

            if (Repository.GetById(id) != null)
            {
                var result = MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(Repository.Remove(id));
                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<CostControlBusinessEntity.Menu> Remove(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter)
        {
            if (filter == null) return null;

            List<CostControlBusinessEntity.Menu> result = null;

            var deleteLst = Repository.Get(MenuIMapper
                                .Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                                    Expression<Func<CostControlEntity.Menu, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<CostControlBusinessEntity.Menu>)
                    .ForEach(s => result.Add(MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(Repository.Remove(s))));

                _unitOfWork.Commit();

                return result;
            }

            return null;
        }

        public async Task<CostControlBusinessEntity.Menu> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null) return null;

            var entity = await Repository.GetByIdAsync(id, null, cancellationToken);

            if (entity != null)
            {
                var result = MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(Repository.Remove(id));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Menu>> RemoveAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
                  Repository
                  .Remove(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Menu Exists(object primaryKey)
            => MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Menu> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => MenuIMapper.Map<CostControlBusinessEntity.Menu>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<CostControlBusinessEntity.Menu> Get(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
            int? pageNumber = null,
            int? pageSize = null)
        => MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>, IEnumerable<CostControlBusinessEntity.Menu>>(
                Repository.Get(
                    MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>, Expression<Func<CostControlEntity.Menu, bool>>>(filter),
                    MenuIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>>,
                    Func<IQueryable<CostControlEntity.Menu>, IOrderedQueryable<CostControlEntity.Menu>>>(orderBy),
                    MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties),
                    pageNumber, pageSize));

        public async Task<IEnumerable<CostControlBusinessEntity.Menu>> GetAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
            Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>> orderBy = null,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await MenuIMapper.Map<Task<IEnumerable<CostControlEntity.Menu>>, Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
                Repository.GetAsync(
                    MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>, Expression<Func<CostControlEntity.Menu, bool>>>(filter),
                    MenuIMapper.Map<Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>>,
                    Func<IQueryable<CostControlEntity.Menu>, IOrderedQueryable<CostControlEntity.Menu>>>(orderBy),
                    MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties),
                    pageNumber, pageSize, cancellationToken));

        public CostControlBusinessEntity.Menu GetById(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null)
        => id == null ? null : MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>
            (Repository.GetById(id, MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties)));

        public async Task<CostControlBusinessEntity.Menu> GetByIdAsync(object id,
            ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        //=> await await Task.FromResult(MenuIMapper.Map<Task<Entity.Models.Menu>, Task<Menu>>(Repository.GetByIdAsync(id, cancellationToken)));
        => id == null ? null : MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>
            (await Repository.GetByIdAsync(id, MenuIMapper.MapIncludesList<Expression<Func<IQueryable<CostControlEntity.Menu>, IIncludableQueryable<CostControlEntity.Menu, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Menu> GetWithRawSql(string query, params object[] parameters)
        => MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>, IEnumerable<CostControlBusinessEntity.Menu>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<CostControlBusinessEntity.Menu>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>, IEnumerable<CostControlBusinessEntity.Menu>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public CostControlBusinessEntity.Menu Add(CostControlBusinessEntity.Menu entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null) return null;

            try
            {
                var result = MenuIMapper
                    .Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(
                        Repository.Add(MenuIMapper.Map<CostControlBusinessEntity.Menu, CostControlEntity.Menu>(entity)));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Menu> AddAsync(CostControlBusinessEntity.Menu entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Menu = MenuIMapper.Map<CostControlBusinessEntity.Menu, CostControlEntity.Menu>(entity);

                var result = MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(Repository.Add(Menu));
                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Menu Update(CostControlBusinessEntity.Menu entity)
        {
            if (entity == null) return null;

            try
            {
                CostControlEntity.Menu Menu = MenuIMapper.Map<CostControlBusinessEntity.Menu, CostControlEntity.Menu>(entity);

                var result = MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(Repository.Update(Menu));
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CostControlBusinessEntity.Menu> UpdateAsync(CostControlBusinessEntity.Menu entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) return null;

            try
            {
                var Menu = MenuIMapper.Map<CostControlBusinessEntity.Menu, CostControlEntity.Menu>(entity);

                var result = MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(Repository.Update(Menu));

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

        public CostControlBusinessEntity.Menu SingleOrDefault(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
        => MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(
            Repository.SingleOrDefault(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Menu> SingleOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await MenuIMapper.Map<Task<CostControlEntity.Menu>, Task<CostControlBusinessEntity.Menu>>(
                Repository.SingleOrDefaultAsync(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                    Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken));

        public CostControlBusinessEntity.Menu FirstOrDefault(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
        => MenuIMapper.Map<CostControlEntity.Menu, CostControlBusinessEntity.Menu>(
                Repository.SingleOrDefault(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                    Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

        public async Task<CostControlBusinessEntity.Menu> FirstOrDefaultAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await MenuIMapper.Map<Task<CostControlEntity.Menu>, Task<CostControlBusinessEntity.Menu>>(
                Repository.SingleOrDefaultAsync(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                    Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken));

        public IEnumerable<CostControlBusinessEntity.Menu> AddRange(IEnumerable<CostControlBusinessEntity.Menu> entities)
        {
            try
            {
                var result =
                MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
                      Repository.AddRange(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Menu>> AddRangeAsync(IEnumerable<CostControlBusinessEntity.Menu> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await MenuIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
                      Repository
                      .AddRange(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Menu> RemoveFiltered(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter)
        {
            try
            {
                var result = MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
                    Repository.RemoveFiltered(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                        Expression<Func<CostControlEntity.Menu, bool>>>(filter)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Menu>> RemoveFilteredAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await MenuIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
                    Repository.RemoveFilteredAsync(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                    Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CostControlBusinessEntity.Menu> RemoveRange(IEnumerable<CostControlBusinessEntity.Menu> entities)
        {
            try
            {
                var result = MenuIMapper.Map<IEnumerable<CostControlBusinessEntity.Menu>>(
                    Repository.RemoveRange(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CostControlBusinessEntity.Menu>> RemoveRangeAsync(IEnumerable<CostControlBusinessEntity.Menu> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await MenuIMapper.Map<Task<IEnumerable<CostControlBusinessEntity.Menu>>>(
                        Repository
                        .Remove(MenuIMapper.Map<IEnumerable<CostControlEntity.Menu>>(entities)));

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CostControlBusinessEntity.Menu Exists(params object[] primaryKey)
        => MenuIMapper.Map<CostControlBusinessEntity.Menu>(Repository.Exists(primaryKey));

        public async Task<CostControlBusinessEntity.Menu> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await MenuIMapper.Map<Task<CostControlBusinessEntity.Menu>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
        => Repository.Exists(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(MenuIMapper.Map<Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
            Expression<Func<CostControlEntity.Menu, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
            => Repository.Count(MenuIMapper.Map<Expression<Func<CostControlBusinessEntity.Menu, bool>>,
                Expression<Func<CostControlEntity.Menu, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    MenuMapperConfig = null;
                    MenuIMapper = null;
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

        public bool Any(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<CostControlBusinessEntity.Menu, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadPropertyAsync(CostControlBusinessEntity.Menu item, Expression<Func<CostControlBusinessEntity.Menu, object>> property, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CostControlBusinessEntity.Menu> GetByParentId(long parentId, Func<IQueryable<CostControlBusinessEntity.Menu>, IOrderedQueryable<CostControlBusinessEntity.Menu>> orderBy = null, ICollection<Expression<Func<IQueryable<CostControlBusinessEntity.Menu>, IIncludableQueryable<CostControlBusinessEntity.Menu, object>>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        ~MenuLogic()
        {
            Dispose(false);
        }
    }
}