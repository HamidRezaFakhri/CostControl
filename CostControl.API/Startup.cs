using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostControl.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddFormatterMappings()
                .AddAuthorization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin", builder =>
                    {
                        builder.AllowAnyOrigin();
                        //.WithOrigins("https://localhost:44395/");

                        builder.AllowCredentials();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
                });

            //// Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new ClientMappingProfile());
            //});

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            //OR 
            services.AddAutoMapper();
            
            //services.AddMvc();

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.ApiName = "api1";
            //    });

            //services.AddDbContext<CostControlDbContext>(options =>
            //{
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        serverDbContextOptionsBuilder =>
            //        {
            //            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
            //            serverDbContextOptionsBuilder.CommandTimeout(minutes);
            //            serverDbContextOptionsBuilder.EnableRetryOnFailure();
            //        });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");

            //app.UseAuthentication();

            app.UseMvc();
        }
    }
}
