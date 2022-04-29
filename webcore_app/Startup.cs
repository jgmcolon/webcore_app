using AppService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using webcore_app.Core;
using webcore_app.Core.Common;
using webcore_app.Core.Database;
using webcore_app.Core.Interfaces;
using ILogger = webcore_app.Core.Interfaces.ILogger;

namespace webcore_app
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
            services.AddMvc().AddNewtonsoftJson();

            services.AddCors();
            
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "HUMAN RESOURSE MANAGER SERVICE",
                    TermsOfService = new Uri("http://lab.dev/"),
                    Contact = new OpenApiContact() { Name = "Jose Mata", Email = "jose.mata@logaritmos.com.do" }
                });
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.Configure<AppSettings>(appSettingsSection);

            #region DB Configurations
            
            ConnectionStringProvider connectionStringProvider = new(Configuration, appSettings.ConnectionStrings);

            services.AddDbContext<Core.Database.AppContext>(options => options.UseSqlServer(
                connectionStringProvider.ConnectionString));

            services.AddScoped<IMyDbContext, Core.Database.AppContext>();

            services.AddScoped<IUnitOfWork<Core.Database.AppContext>, UnitOfWork<Core.Database.AppContext>>();

            #endregion

            #region LogConfiguration
            services.AddScoped<ILogger, Logger>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
