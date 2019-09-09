namespace CostControl.Data.UnitOfWork
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using CostControl.Data.Repository;
	using CostControl.Entity.Models.Base.Interfaces;
	using CostControl.Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Storage;

	public class UnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; }

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
			Context = context;
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
			var temp = new Repository<TEntity>(this);
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

		private Repository<IncommingUser> _incommingUser;

		public Repository<IncommingUser> IncommingUser => _incommingUser ?? (_incommingUser = new Repository<IncommingUser>(this));

		private Repository<SalePoint> _salePoint;

		public Repository<SalePoint> SalePoint => _salePoint ?? (_salePoint = new Repository<SalePoint>(this));

		private Repository<SaleCostPoint> _saleCostPoint;

		public Repository<SaleCostPoint> SaleCostPoint => _saleCostPoint ?? (_saleCostPoint = new Repository<SaleCostPoint>(this));

		private Repository<CostPointGroup> _costPointGroup;

		public Repository<CostPointGroup> CostPointGroup => _costPointGroup ?? (_costPointGroup = new Repository<CostPointGroup>(this));

		private Repository<OverCostType> _overCostType;

		public Repository<OverCostType> OverCostType => _overCostType ?? (_overCostType = new Repository<OverCostType>(this));

		private Repository<OverCost> _overCost;

		public Repository<OverCost> OverCost => _overCost ?? (_overCost = new Repository<OverCost>(this));

		private Repository<ConsumptionUnit> _consumptionUnit;

		public Repository<ConsumptionUnit> ConsumptionUnit => _consumptionUnit ?? (_consumptionUnit = new Repository<ConsumptionUnit>(this));

		private Repository<Inventory> _inventory;

		public Repository<Inventory> Inventory => _inventory ?? (_inventory = new Repository<Inventory>(this));

		private Repository<Ingredient> _ingredient;

		public Repository<Ingredient> Ingredient => _ingredient ?? (_ingredient = new Repository<Ingredient>(this));

		private Repository<Food> _recipe;

		public Repository<Food> Recipe => _recipe ?? (_recipe = new Repository<Food>(this));

		private Repository<Recipe> _recipeItem;

		public Repository<Recipe> RecipeItem => _recipeItem ?? (_recipeItem = new Repository<Recipe>(this));

		private Repository<Menu> _menu;

		public Repository<Menu> Menu => _menu ?? (_menu = new Repository<Menu>(this));

		private Repository<MenuItem> _menuItem;

		public Repository<MenuItem> MenuItem => _menuItem ?? (_menuItem = new Repository<MenuItem>(this));

		private Repository<Draft> _draft;

		public Repository<Draft> Draft => _draft ?? (_draft = new Repository<Draft>(this));

		private Repository<DraftItem> _draftItem;

		public Repository<DraftItem> DraftItem => _draftItem ?? (_draftItem = new Repository<DraftItem>(this));

		private Repository<CostPoint> _costPoint;

		public Repository<CostPoint> CostPoint => _costPoint ?? (_costPoint = new Repository<CostPoint>(this));

		private Repository<Buffet> _buffet;

		public Repository<Buffet> Buffet => _buffet ?? (_buffet = new Repository<Buffet>(this));

		private Repository<Setting> _setting;

		public Repository<Setting> Setting => _setting ?? (_setting = new Repository<Setting>(this));

		private Repository<IntakeRemittance> _intakeRemittance;

		public Repository<IntakeRemittance> IntakeRemittance => _intakeRemittance ?? (_intakeRemittance = new Repository<IntakeRemittance>(this));

		private Repository<IntakeRemittanceItem> _intakeRemittanceItem;

		public Repository<IntakeRemittanceItem> IntakeRemittanceItem => _intakeRemittanceItem ?? (_intakeRemittanceItem = new Repository<IntakeRemittanceItem>(this));


		private Repository<IntakeRemittanceItemLog> _intakeRemittanceItemLog;

		public Repository<IntakeRemittanceItemLog> IntakeRemittanceItemLog => _intakeRemittanceItemLog ?? (_intakeRemittanceItemLog = new Repository<IntakeRemittanceItemLog>(this));

		private Repository<DataImport> _dataImport;

		public Repository<DataImport> DataImport => _dataImport ?? (_dataImport = new Repository<DataImport>(this));

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
				return Context.SaveChanges();
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
			finally
			{
				if (Context.Database.CurrentTransaction != null)
					Context.Database.CommitTransaction();
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

			return await Context.SaveChangesAsync(cancellationToken);

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
			Context
				.ChangeTracker
				.Entries()
				.ToList()
				.ForEach(x => x.Reload());

			if (Context.Database.CurrentTransaction != null)
				Context.Database.RollbackTransaction();

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
			cancellationToken.ThrowIfCancellationRequested();

			throw await new Task<Exception>(() => new NotImplementedException(), cancellationToken);

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
			cancellationToken.ThrowIfCancellationRequested();

			return await Context.Database.ExecuteSqlCommandAsync(commandText, parameters, cancellationToken);
		}

		public virtual int ExecuteSqlCommand(string commandText, params object[] parameters)
		{
			return Context.Database.ExecuteSqlCommand(commandText, parameters);
		}

		public virtual void BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Unspecified)
		{
			if (_dbContextTransaction == null)
				_dbContextTransaction = Context.Database.BeginTransaction(isolationLevel);

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

		~UnitOfWork()
		{
			Dispose(false);
		}
	}
}