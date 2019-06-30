﻿using System;
using CostControl.Presentation.ModelBinding.PersianDate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostControl.Presentation
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(20);//You can set Time   
			});

			services.AddAntiforgery();

			services
				.AddMvcCore()
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

			services.AddMvc(
				config =>
				{
					config.ModelBinderProviders.Insert(0, new PersianDateModelBinderProvider());
					//config.ModelBinderProviders.Add(new PersianDateModelBinderProvider());
				})
				.AddSessionStateTempDataProvider();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			//app.UseCookiePolicy();

			app.UseSession();

			app.UseCors("AllowSpecificOrigin");

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=IncommingUser}/{action=Login}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});
		}
	}
}
