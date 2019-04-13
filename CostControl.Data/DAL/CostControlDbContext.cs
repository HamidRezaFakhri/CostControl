namespace CostControl.Data.DAL
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CostControl.Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;

    //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize_identity_model?view=aspnetcore-2.1
    //Identity

    public class CostControlDbContext : DbContext
    {
        //public CostControlDbContext(DbContextOptions options) : base(options)
        //{
        //    Database.SetCommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
        //}

        //private static ILoggerFactory LoggerFactory => new LoggerFactory().AddConsole(LogLevel.Trace);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.EnableDetailedErrors;
                //optionsBuilder.EnableSensitiveDataLogging;
                //await db.Database.EnsureDeletedAsync();
                //await db.Database.EnsureCreatedAsync();

                //var log = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
                //optionsBuilder.UseLoggerFactory(log);

                optionsBuilder
                    //.UseSqlServer(@"Data Source=79.175.155.6;Initial Catalog=CostControl;Trusted_Connection=false;user id = sa;password=Cost#Point@98;Connection Timeout=300;MultipleActiveResultSets=true;",
                    .UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=CostControl;Trusted_Connection=false;user id = sa;password=2129;Connection Timeout=300;MultipleActiveResultSets=true;",
                                //opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds))
                                opts => opts.CommandTimeout(120))
                    //.UseLazyLoadingProxies()
                    //.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                    .EnableSensitiveDataLogging();

                //_context.ChangeTracker.LazyLoadingEnabled = false;

                //.UseLoggerFactory(LoggerFactory);
                ;
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.RemovePluralizingTableNameConvention();

            var implementedConfigTypes =
                Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace != null && !t.IsAbstract && !t.IsGenericTypeDefinition
                                          && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                                            i.GetTypeInfo().IsGenericType
                                            && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in implementedConfigTypes)
            {
                dynamic config = Activator.CreateInstance(configType);
                modelBuilder.ApplyConfiguration(config);
            }

            //modelBuilder.ApplyConfiguration(new RoleConfigure());
            //modelBuilder.ApplyConfiguration(new UserConfigure());
            //modelBuilder.ApplyConfiguration(new InventoryConfigure());
            //modelBuilder.ApplyConfiguration(new OverCostTypeConfigure());
            //modelBuilder.ApplyConfiguration(new OverCostConfigure());
            //modelBuilder.ApplyConfiguration(new SettingConfigure());
            //modelBuilder.ApplyConfiguration(new RecipeItemConfigure());
            //modelBuilder.ApplyConfiguration(new RecipeConfigure());
            //modelBuilder.ApplyConfiguration(new ResturantConfigure());
            //modelBuilder.ApplyConfiguration(new MenuConfigure());
            //modelBuilder.ApplyConfiguration(new ConsumptionUnitConfigure());
            //modelBuilder.ApplyConfiguration(new BuffetConfigure());
            //modelBuilder.ApplyConfiguration(new IngredientConfigure());
        }

        public virtual DbSet<IncommingUser> Users { get; set; }

        public virtual DbSet<Inventory> Inventories { get; set; }

        public virtual DbSet<OverCostType> OverCostTypes { get; set; }

        public virtual DbSet<OverCost> OverCosts { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Recipe> RecipeItems { get; set; }

        public virtual DbSet<Food> Recipes { get; set; }

        public virtual DbSet<SalePoint> SalePoints { get; set; }

        public virtual DbSet<ConsumptionUnit> ConsumptionUnits { get; set; }

        public virtual DbSet<Buffet> Buffets { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<DraftItem> DraftItems { get; set; }

        public virtual DbSet<Draft> Drafts { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<CostPoint> CostPoints { get; set; }

        public virtual DbSet<IntakeRemittance> IntakeRemittances { get; set; }

        public virtual DbSet<DataImport> DataImports { get; set; }

        public virtual DbSet<SaleCostPoint> SaleCostPoints { get; set; }

    }
}