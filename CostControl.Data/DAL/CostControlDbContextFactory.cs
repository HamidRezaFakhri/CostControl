using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CostControl.Data.DAL
{
    //public class CostControlDbContextFactory : IDesignTimeDbContextFactory<CostControlDbContext>
    //{
    //    public CostControlDbContext CreateDbContext(string[] args)
    //    {
    //        //var basePath = Directory.GetCurrentDirectory();
    //        //Console.WriteLine($"Using `{basePath}` as the BasePath");
    //        //var configuration = new ConfigurationBuilder()
    //        //                        .SetBasePath(basePath)
    //        //                        .AddJsonFile("appsettings.json")
    //        //                        .Build();

    //        //var connectionString = configuration.GetConnectionString("DefaultConnection")
    //        //                                    .Replace("|DataDirectory|", Path.Combine(basePath, "wwwroot", "app_data"));
    //        //optionsBuilder.UseSqlServer(connectionString);

    //        var optionsBuilder = new DbContextOptionsBuilder<CostControlDbContext>();

    //        optionsBuilder
    //            .UseSqlServer(@"Data Source=.;Initial Catalog=CostControl;Trusted_Connection=false;
    //                                            User ID=sa; Password=2129;MultipleActiveResultSets=true;");

    //        return new CostControlDbContext(optionsBuilder.Options);
    //    }
    //}
}