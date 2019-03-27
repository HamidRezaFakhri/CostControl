namespace CostControl.BusinessLogic.Logics.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;
    using Microsoft.EntityFrameworkCore.Query;
    using SecurityBusinessEntity = BusinessEntity.Models.Security;
    using SecurityEntity = Entity.Models.Security;

    public class RoleLogic : Base.IGenericLogic<SecurityBusinessEntity.Role>, IDisposable
    {
        private MapperConfiguration RoleMapperConfig { get; set; }

        private IMapper RoleIMapper { get; set; }

        private readonly Data.UnitOfWork.UnitOfWork _unitOfWork;

        protected Data.Repository.IRepository<SecurityEntity.Role> Repository;

        public RoleLogic()
        {
            RoleMapperConfig = new BusinessLogic.Mapper.AutoMapperConfiguration().Configure();
            RoleIMapper = RoleMapperConfig.CreateMapper();
            _unitOfWork = new Data.UnitOfWork.UnitOfWork(new Data.DAL.CostControlDbContext());
            Repository = _unitOfWork.GetRepository<SecurityEntity.Role>();
        }

        public SecurityBusinessEntity.Role Remove(object id)
        {
            if (id == null)
            {
                return null;
            }

            if (Repository.GetById(id) != null)
            {
                var result = RoleIMapper
                                .Map<SecurityEntity.Role, SecurityBusinessEntity.Role>(Repository.Remove(id));
                Commit();

                return result;
            }

            return null;
        }

        public IEnumerable<SecurityBusinessEntity.Role> Remove(Expression<Func<SecurityBusinessEntity.Role, bool>> filter)
        {
            if (filter == null)
            {
                return null;
            }

            List<SecurityBusinessEntity.Role> result = null;

            var deleteLst = Repository.Get(RoleIMapper
                                .Map<Expression<Func<SecurityBusinessEntity.Role, bool>>,
                                    Expression<Func<SecurityEntity.Role, bool>>>(filter));

            if (deleteLst != null)
            {
                (deleteLst as List<SecurityBusinessEntity.Role>)
                    .ForEach(s => result.Add(RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Remove(s))));

                Commit();

                return result;
            }

            return null;
        }

        public async Task<SecurityBusinessEntity.Role> RemoveAsync(object id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
            {
                return null;
            }

            var entity = await Repository.GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity != null)
            {
                var result = RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Remove(id));
                await CommitAsync(cancellationToken);

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<SecurityBusinessEntity.Role>> RemoveAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(
              Repository
              .Remove(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public SecurityBusinessEntity.Role Exists(object primaryKey)
        => RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Exists(primaryKey));

        public async Task<SecurityBusinessEntity.Role> ExistsAsync(object primaryKey,
            CancellationToken cancellationToken = default(CancellationToken))
        => RoleIMapper.Map<SecurityBusinessEntity.Role>(await Repository.ExistsAsync(cancellationToken, primaryKey));

        public IEnumerable<SecurityBusinessEntity.Role> Get(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
                Func<IQueryable<SecurityBusinessEntity.Role>, IOrderedQueryable<SecurityBusinessEntity.Role>> orderBy = null,
                ICollection<Expression<Func<IQueryable<SecurityBusinessEntity.Role>, IIncludableQueryable<SecurityBusinessEntity.Role, object>>>> includeProperties = null,
                int? pageNumber = null,
                int? pageSize = null)
        => RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(
                           Repository.Get(
                               RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter),
                               RoleIMapper.Map<Func<IQueryable<SecurityEntity.Role>, IOrderedQueryable<SecurityEntity.Role>>>(orderBy),
                               RoleIMapper.MapIncludesList<Expression<Func<IQueryable<SecurityEntity.Role>, IIncludableQueryable<SecurityEntity.Role, object>>>>(includeProperties),
                               pageNumber,
                               pageSize));

        public async Task<IEnumerable<SecurityBusinessEntity.Role>> GetAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            Func<IQueryable<SecurityBusinessEntity.Role>, IOrderedQueryable<SecurityBusinessEntity.Role>> orderBy = null,
            ICollection<Expression<Func<IQueryable<SecurityBusinessEntity.Role>, IIncludableQueryable<SecurityBusinessEntity.Role, object>>>> includeProperties = null,
            int? pageNumber = null, int? pageSize = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RoleIMapper.Map<Task<IEnumerable<SecurityBusinessEntity.Role>>>(
                           Repository.GetAsync(
                               RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter),
                               RoleIMapper.Map<Func<IQueryable<SecurityBusinessEntity.Role>, IOrderedQueryable<SecurityBusinessEntity.Role>>,
                               Func<IQueryable<SecurityEntity.Role>, IOrderedQueryable<SecurityEntity.Role>>>(orderBy),
                               RoleIMapper.MapIncludesList<Expression<Func<IQueryable<SecurityEntity.Role>, IIncludableQueryable<SecurityEntity.Role, object>>>>(includeProperties),
                               pageNumber, pageSize, cancellationToken));

        public SecurityBusinessEntity.Role GetById<TKey>(TKey id)
        => id == null ? null : RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.GetById(id));

        public SecurityBusinessEntity.Role GetById<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<SecurityBusinessEntity.Role>, IIncludableQueryable<SecurityBusinessEntity.Role, object>>>> includeProperties = null)
        => id == null ? null : RoleIMapper.Map<SecurityBusinessEntity.Role>
                       (Repository.GetById(id, RoleIMapper.MapIncludesList<Expression<Func<IQueryable<SecurityEntity.Role>, IIncludableQueryable<SecurityEntity.Role, object>>>>(includeProperties)));

        public async Task<SecurityBusinessEntity.Role> GetByIdAsync<TKey>(TKey id,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : RoleIMapper.Map<SecurityBusinessEntity.Role>(await Repository.GetByIdAsync(id, cancellationToken));

        public async Task<SecurityBusinessEntity.Role> GetByIdAsync<TKey>(TKey id,
            ICollection<Expression<Func<IQueryable<SecurityBusinessEntity.Role>, IIncludableQueryable<SecurityBusinessEntity.Role, object>>>> includeProperties = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => id == null ? null : RoleIMapper.Map<SecurityBusinessEntity.Role>
                       (await Repository.GetByIdAsync(id, RoleIMapper.MapIncludesList<Expression<Func<IQueryable<SecurityEntity.Role>, IIncludableQueryable<SecurityEntity.Role, object>>>>(includeProperties), cancellationToken));

        public IEnumerable<SecurityBusinessEntity.Role> GetWithRawSql(string query, params object[] parameters)
        => RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(Repository.GetWithRawSql(query, parameters));

        public async Task<IEnumerable<SecurityBusinessEntity.Role>> GetWithRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(await Repository.GetWithRawSqlAsync(query, cancellationToken, parameters));

        public SecurityBusinessEntity.Role Add(SecurityBusinessEntity.Role entity)
        {
            //using (var transaction = objectContext.Connection.BeginTransaction())

            if (entity == null)
            {
                return null;
            }

            var result = RoleIMapper
                .Map<SecurityBusinessEntity.Role>(
                    Repository.Add(RoleIMapper.Map<SecurityEntity.Role>(entity)));
            Commit();

            return result;
        }

        public async Task<SecurityBusinessEntity.Role> AddAsync(SecurityBusinessEntity.Role entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Role = RoleIMapper.Map<SecurityEntity.Role>(entity);

            var result = RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Add(Role));
            await CommitAsync(cancellationToken);

            return result;
        }

        public SecurityBusinessEntity.Role Update(SecurityBusinessEntity.Role entity)
        {
            if (entity == null)
            {
                return null;
            }

            var Role = RoleIMapper.Map<SecurityEntity.Role>(entity);

            var result = RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Update(Role));
            Commit();

            return result;
        }

        public async Task<SecurityBusinessEntity.Role> UpdateAsync(SecurityBusinessEntity.Role entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null)
            {
                return null;
            }

            var Role = RoleIMapper.Map<SecurityEntity.Role>(entity);

            var result = RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Update(Role));

            await CommitAsync(cancellationToken);

            return result;
        }

        public int RunRawSql(string query,
            params object[] parameters)
        => Repository.RunRawSql(query, parameters);

        public async Task<int> RunRawSqlAsync(string query,
            CancellationToken cancellationToken = default(CancellationToken),
            params object[] parameters)
        => await Repository.RunRawSqlAsync(query, cancellationToken, parameters);

        public SecurityBusinessEntity.Role SingleOrDefault(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null)
        => RoleIMapper.Map<SecurityBusinessEntity.Role>(
                       Repository.SingleOrDefault(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter)));

        public async Task<SecurityBusinessEntity.Role> SingleOrDefaultAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RoleIMapper.Map<Task<SecurityBusinessEntity.Role>>(
                           Repository.SingleOrDefaultAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken));

        public SecurityBusinessEntity.Role FirstOrDefault(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null)
        => RoleIMapper.Map<SecurityBusinessEntity.Role>(
                           Repository.FirstOrDefault(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter)));

        public async Task<SecurityBusinessEntity.Role> FirstOrDefaultAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RoleIMapper.Map<Task<SecurityBusinessEntity.Role>>(
                           Repository.FirstOrDefaultAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken));

        public SecurityBusinessEntity.Role LastOrDefault(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null)
        => RoleIMapper.Map<SecurityBusinessEntity.Role>(
                           Repository.LastOrDefault(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter)));

        public async Task<SecurityBusinessEntity.Role> LastOrDefaultAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await RoleIMapper.Map<Task<SecurityBusinessEntity.Role>>(
                           Repository.LastOrDefaultAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken));

        public IEnumerable<SecurityBusinessEntity.Role> AddRange(IEnumerable<SecurityBusinessEntity.Role> entities)
        {
            var result =
                RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(
                      Repository.AddRange(RoleIMapper.Map<IEnumerable<SecurityEntity.Role>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<SecurityBusinessEntity.Role>> AddRangeAsync(IEnumerable<SecurityBusinessEntity.Role> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await RoleIMapper.Map<Task<IEnumerable<SecurityBusinessEntity.Role>>>(
                      Repository
                      .AddRange(RoleIMapper.Map<IEnumerable<SecurityEntity.Role>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<SecurityBusinessEntity.Role> RemoveFiltered(Expression<Func<SecurityBusinessEntity.Role, bool>> filter)
        {
            var result = RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(
                    Repository.RemoveFiltered(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<SecurityBusinessEntity.Role>> RemoveFilteredAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await RoleIMapper.Map<Task<IEnumerable<SecurityBusinessEntity.Role>>>(
                    Repository.RemoveFilteredAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken));

            await CommitAsync(cancellationToken);

            return result;
        }

        public IEnumerable<SecurityBusinessEntity.Role> RemoveRange(IEnumerable<SecurityBusinessEntity.Role> entities)
        {
            var result = RoleIMapper.Map<IEnumerable<SecurityBusinessEntity.Role>>(
                    Repository.RemoveRange(RoleIMapper.Map<IEnumerable<SecurityEntity.Role>>(entities)));

            Commit();

            return result;
        }

        public async Task<IEnumerable<SecurityBusinessEntity.Role>> RemoveRangeAsync(IEnumerable<SecurityBusinessEntity.Role> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await RoleIMapper.Map<Task<IEnumerable<SecurityBusinessEntity.Role>>>(
                        Repository
                        .Remove(RoleIMapper.Map<IEnumerable<SecurityEntity.Role>>(entities)));

            await CommitAsync(cancellationToken);

            return result;
        }

        public SecurityBusinessEntity.Role Exists(params object[] primaryKey)
        => RoleIMapper.Map<SecurityBusinessEntity.Role>(Repository.Exists(primaryKey));

        public async Task<SecurityBusinessEntity.Role> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken),
            params object[] primaryKey)
        => await RoleIMapper.Map<Task<SecurityBusinessEntity.Role>>(Repository.ExistsAsync(cancellationToken, primaryKey));

        public bool Exists(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null)
        => Repository.Exists(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter));

        public async Task<bool> ExistsAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.ExistsAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken);

        public async Task<int> GetCountAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.CountAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken);

        public int GetCount(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null)
        => Repository.Count(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter));

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Context?.Dispose();
                    RoleMapperConfig = null;
                    RoleIMapper = null;
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

        public bool Any(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null)
        => Repository.Any(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter));

        public async Task<bool> AnyAsync(Expression<Func<SecurityBusinessEntity.Role, bool>> filter = null,
            CancellationToken cancellationToken = default(CancellationToken))
        => await Repository.AnyAsync(RoleIMapper.Map<Expression<Func<SecurityEntity.Role, bool>>>(filter), cancellationToken);

        public Task LoadPropertyAsync(SecurityBusinessEntity.Role item, Expression<Func<SecurityBusinessEntity.Role, object>> property,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SecurityBusinessEntity.Role> GetByParentId(long parentId, Func<IQueryable<SecurityBusinessEntity.Role>, IOrderedQueryable<SecurityBusinessEntity.Role>> orderBy = null,
            ICollection<Expression<Func<IQueryable<SecurityBusinessEntity.Role>, IIncludableQueryable<SecurityBusinessEntity.Role, object>>>> includeProperties = null,
            int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            var commit = _unitOfWork.Commit();

            if (commit < 0)
            {
                throw new Exception("Commit failed!");
            }

            return commit;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var commit = CommitAsync(cancellationToken);

            if (commit.Result < 0)
            {
                throw new Exception("Commit failed!");
            }

            return await commit;
        }

        ~RoleLogic()
        {
            Dispose(false);
        }
    }
}