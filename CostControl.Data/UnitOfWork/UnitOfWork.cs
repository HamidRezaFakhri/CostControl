using CostControl.Data.DAL;
using CostControl.Data.Repository;
using CostControl.Entity.Models.Base.Interfaces;
using CostControl.Entity.Models.CostControl;
using CostControl.Entity.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CostControl.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context = null;

        private IDbContextTransaction _dbContextTransaction;

        //private readonly IDbFactory dbFactory;

        //private static readonly ISessionFactory _sessionFactory;
        //private ITransaction _transaction;

        //public ISession Session { get; private set; }

        //static UnitOfWork()
        //{
        //    // Initialise singleton instance of ISessionFactory, static constructors are only executed once during the
        //    // application lifetime - the first time the UnitOfWork class is used
        //    _sessionFactory = Fluently.Configure()
        //        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("UnitOfWorkExample")))
        //        .Mappings(x => x.AutoMappings.Add(
        //            AutoMap.AssemblyOf<Product>(new AutomappingConfiguration()).UseOverridesFromAssemblyOf<ProductOverrides>()))
        //        .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
        //        .BuildSessionFactory();
        //}

        //public void Commit()
        //{
        //    try
        //    {
        //        // commit transaction if there is one active
        //        if (_transaction != null && _transaction.IsActive)
        //            _transaction.Commit();
        //    }
        //    catch
        //    {
        //        // rollback if there was an exception
        //        if (_transaction != null && _transaction.IsActive)
        //            _transaction.Rollback();

        //        throw;
        //    }
        //    finally
        //    {
        //        Session.Dispose();
        //    }
        //}

        //public void Rollback()
        //{
        //    try
        //    {
        //        if (_transaction != null && _transaction.IsActive)
        //            _transaction.Rollback();
        //    }
        //    finally
        //    {
        //        Session.Dispose();
        //    }
        //}

        //public UnitOfWork()
        //{
        //    _context = _context ?? new CostControlDbContext();
        //    //    Session = _sessionFactory.OpenSession();
        //}

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        //private Dictionary<string, object> repositories;

        //public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity, new()
        //{
        //    if (repositories == null)
        //    {
        //        repositories = new Dictionary<string, object>();
        //    }

        //    var type = typeof(TEntity).Name;

        //    if (!repositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(Repository<>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);
        //        repositories.Add(type, repositoryInstance);
        //    }
        //    return (Repository<TEntity>)repositories[type];
        //}

        //private Dictionary<Type, object> repositories;

        //public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity, new()
        //{
        //    if (repositories == null)
        //    {
        //        repositories = new Dictionary<Type, object>();
        //    }

        //    var type = typeof(TEntity);

        //    if (!repositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(Repository<>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
        //        repositories.Add(type, repositoryInstance);
        //    }
        //    return (Repository<TEntity>)repositories[type];
        //}

        public virtual Repository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            var temp = new Repository<TEntity>(_context);
            return (from property in GetType().GetProperties()
                    where property.PropertyType == temp.GetType()
                    select property.GetValue(this) as Repository<TEntity>)
                    .FirstOrDefault();

            //using (var kernel = new StandardKernel())
            //{
            //    kernel.Load(Assembly.GetExecutingAssembly());
            //    var result = kernel.Get<TEntity>(new ConstructorArgument("context", context));
            //    // Requirements
            //    //   - Must be in this assembly
            //    //   - Must implement a specific interface (i.e. IBlogModule)
            //    if (result != null && result.GetType().GetInterfaces().Contains(typeof(IBaseEntity)))
            //    {
            //        return result;
            //    }
            //}
            //// Optional: return an error instead of a null?
            ////var msg = typeof (T).FullName + " doesn't implement the IBlogModule.";
            ////throw new Exception(msg);
            //return null;
        }

        //public UnitOfWork(IDbFactory dbFactory)
        //{
        //    this.dbFactory = dbFactory;
        //}

        //public PdkGdsAppContext Context
        //{
        //    get { return context ?? (context = dbFactory.Init()); }
        //}

        #region Properties

        //private Repository<Role> _roleRepository;

        //public Repository<Role> RoleRepository => _roleRepository ?? (_roleRepository = new Repository<Role>(_context));

        //private Repository<User> _userRepository;

        //public Repository<User> UserRepository => _userRepository ?? (_userRepository = new Repository<User>(_context));
        
        private Repository<IncommingUser> _incommingUser;

        public Repository<IncommingUser> IncommingUser => _incommingUser ?? (_incommingUser = new Repository<IncommingUser>(_context));

        private Repository<SalePoint> _salePoint;

        public Repository<SalePoint> SalePoint => _salePoint ?? (_salePoint = new Repository<SalePoint>(_context));

        private Repository<SaleCostPoint> _saleCostPoint;

        public Repository<SaleCostPoint> SaleCostPoint => _saleCostPoint ?? (_saleCostPoint = new Repository<SaleCostPoint>(_context));

        private Repository<CostPointGroup> _costPointGroup;

        public Repository<CostPointGroup> CostPointGroup => _costPointGroup ?? (_costPointGroup = new Repository<CostPointGroup>(_context));

        private Repository<OverCostType> _overCostType;

        public Repository<OverCostType> OverCostType => _overCostType ?? (_overCostType = new Repository<OverCostType>(_context));

        private Repository<OverCost> _overCost;

        public Repository<OverCost> OverCost => _overCost ?? (_overCost = new Repository<OverCost>(_context));

        private Repository<ConsumptionUnit> _consumptionUnit;

        public Repository<ConsumptionUnit> ConsumptionUnit => _consumptionUnit ?? (_consumptionUnit = new Repository<ConsumptionUnit>(_context));

        private Repository<Inventory> _inventory;

        public Repository<Inventory> Inventory => _inventory ?? (_inventory = new Repository<Inventory>(_context));

        private Repository<Ingredient> _ingredient;

        public Repository<Ingredient> Ingredient => _ingredient ?? (_ingredient = new Repository<Ingredient>(_context));

        private Repository<Food> _recipe;

        public Repository<Food> Recipe => _recipe ?? (_recipe = new Repository<Food>(_context));

        private Repository<Recipe> _recipeItem;

        public Repository<Recipe> RecipeItem => _recipeItem ?? (_recipeItem = new Repository<Recipe>(_context));

        private Repository<Menu> _menu;

        public Repository<Menu> Menu => _menu ?? (_menu = new Repository<Menu>(_context));

        private Repository<MenuItem> _menuItem;

        public Repository<MenuItem> MenuItem => _menuItem ?? (_menuItem = new Repository<MenuItem>(_context));

        private Repository<Draft> _draft;

        public Repository<Draft> Draft => _draft ?? (_draft = new Repository<Draft>(_context));

        private Repository<DraftItem> _draftItem;

        public Repository<DraftItem> DraftItem => _draftItem ?? (_draftItem = new Repository<DraftItem>(_context));

        private Repository<CostPoint> _costPoint;

        public Repository<CostPoint> CostPoint => _costPoint ?? (_costPoint = new Repository<CostPoint>(_context));

        private Repository<Sale> _sale;

        public Repository<Sale> Sale => _sale ?? (_sale = new Repository<Sale>(_context));

        private Repository<SaleItem> _saleItem;

        public Repository<SaleItem> SaleItem => _saleItem ?? (_saleItem = new Repository<SaleItem>(_context));

        private Repository<Buffet> _buffet;

        public Repository<Buffet> Buffet => _buffet ?? (_buffet = new Repository<Buffet>(_context));

        private Repository<Setting> _setting;

        public Repository<Setting> Setting => _setting ?? (_setting = new Repository<Setting>(_context));

        private Repository<IntakeRemittance> _intakeRemittance;

        public Repository<IntakeRemittance> IntakeRemittance => _intakeRemittance ?? (_intakeRemittance = new Repository<IntakeRemittance>(_context));

        private Repository<DataImport> _dataImport;

        public Repository<DataImport> DataImport => _dataImport ?? (_dataImport = new Repository<DataImport>(_context));
        #endregion

        //private Repository<TEntity> entities;

        //public Repository<TEntity> Entities
        //{
        //    get
        //    {
        //        return this.entities ?? new Repository<TEntity>(context);
        //    }
        //}

        public virtual int Commit()
        {
            //using (var dbContextTransaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        _context.SaveChanges();
            //        dbContextTransaction.Commit();
            //    }
            //    catch (Exception)
            //    {
            //        dbContextTransaction.Rollback();
            //    }
            //}

            try
            {
                return _context.SaveChanges();
            }
            /* EF core
            catch (DbEntityValidationException e)
            {
                //foreach (var eve in (e as DbEntityValidationException).EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                //throw;

                // Retrieve the error messages as a list of strings.
                var errorMessages = e.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        //.Select(x => x.ErrorMessage);
                        .Select(x => x.PropertyName + ": " + x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
            } */
            catch (Exception e)
            {
                RollBack();
                throw e.InnerException ?? e;
            }
        }

        //public async Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    await context.SaveChangesAsync(cancellationToken);
        //}

        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //params TEntity[] entities;
            //_context.ApplyChanges(entities);
            //_context.LoadRelatedEntitiesAsync(entities);
            //_context.AcceptChanges(entities);
            //_context.DetachEntities(entities);

            cancellationToken.ThrowIfCancellationRequested();

            return await _context.SaveChangesAsync(cancellationToken);

            //int result = default(int);
            //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    result = await _context.SaveChangesAsync(cancellationToken);
            //    scope.Complete();
            //}
            //return await Task.FromResult(result);
        }

        public virtual void RollBack()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());

            //foreach (var entry in context.ChangeTracker.Entries()
            //  .Where(e => e.State != EntityState.Unchanged))
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.State = EntityState.Detached;
            //            break;
            //        case EntityState.Modified:
            //        case EntityState.Deleted:
            //            entry.Reload();
            //            break;
            //    }
            //}
        }

        public virtual async Task RollBackAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();

            ////Code Analysis doesn’t flag the following method
            //await Task.Run(() => {
            //    context
            //        .ChangeTracker
            //        .Entries()
            //        .ToList()
            //        .ForEach(x => x.ReloadAsync());
            //});

            //context
            //        .ChangeTracker
            //        .Entries()
            //        .ToList()
            //        .ForEach(x => x.ReloadAsync());

            //return Task.FromResult(0);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string commandText,
            CancellationToken cancellationToken = default(CancellationToken), params object[] parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(commandText, parameters, cancellationToken);
        }

        public virtual int ExecuteSqlCommand(string commandText, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(commandText, parameters);
        }

        public virtual void BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Unspecified)
        {
            if (_dbContextTransaction == null)
                _dbContextTransaction = _context.Database.BeginTransaction(isolationLevel);
            
            //if (_dbContextTransaction != null)
            //    _dbContextTransaction.Commit();
        }

        //void Clear();
        //bool HasChanges();
        //bool HasEntity(object entity);
        //event EventHandler<EntityChangedEventArgs> EntityChanged;

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}