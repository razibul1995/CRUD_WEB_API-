using CRUD_WEB_API.DbContexts;
using CRUD_WEB_API.Helper;
using CRUD_WEB_API.IRepository;
using CRUD_WEB_API.Repository;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_WEB_API
{
    public class Startup
    {

        
        
            private readonly IHostEnvironment _env;
            public Startup(IConfiguration configuration, IHostEnvironment env)
            {
                _env = env;
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                services.AddHttpContextAccessor();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "crud-api", Version = "v1" });
                });

                if (_env.IsProduction())
                {
                    string connection = Environment.GetEnvironmentVariable("ConnectionString");

                    services.AddDbContext<DbContextCom>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);

                    Connection.testAPI = connection;
                }
                else
                {
                    var data = Configuration.GetConnectionString("Development");

                    services.AddDbContext<DbContextCom>(options => options.UseSqlServer(data));

                    Connection.testAPI = Configuration.GetConnectionString("Development");
                }

                services.AddControllers(opts =>
                {
                    if (_env.IsDevelopment())
                    {
                        opts.Filters.Add<AllowAnonymousFilter>();
                    }
                    else
                    {
                        var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()
                              .Build();
                        opts.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
                    }

                });



                RegisterServices(services);

                //services.AddControllers().AddNewtonsoftJson(options =>
                //{
                //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //});

            }
            private void RegisterServices(IServiceCollection services)
            {
                DependencyContainer.RegisterServices(services);
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
            {
                app.UseCors(x => x
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());

                app.UseHttpsRedirection();

                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                //app.UseMiddleware<UserInfoMiddleware>();


                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "crud-api v1"));
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }



