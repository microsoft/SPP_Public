// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using TPP.Core.Data;
using TPP.Api.Configuration;

namespace TPP.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                builder.AddUserSecrets<Startup>();
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.RollingFile(GetLogPath())
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.RollingFile(GetLogPath())
                    .CreateLogger();
            }

            ConnectionStringKey = env.IsEnvironment("Local") ?
                    "ConnectionStrings:TppLocalDbConnection" : 
                    "ConnectionStrings:TppDbConnection";

            Configuration = builder.Build();

        }


        public IConfigurationRoot Configuration { get; }

        public string ConnectionStringKey;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            var xmlPath = GetXmlCommentsPath();

            services.AddMvc(c =>
                c.Conventions.Add(new ApiExplorerGroupPerVersionConvention())
            );


            //Add Options Configuration
            services.AddOptions();
            services.Configure<AADSettings>(Configuration.GetSection("AzureAd"));


            //Add Authentication services.
            services.AddAuthentication();

            //Add CORS to support client-side API calls
            services.AddCors();

            services.AddLogging();


            services.AddDbContext<TPPContext>(options =>
                    options.UseSqlServer(
                        Configuration[ConnectionStringKey]));


            //Add xUnit Test DB Context
            //services.AddDbContext<BaseDbContext>(options =>
            //        options.UseInMemoryDatabase());
            services.AddDbContext<BaseDbContext>(options =>
                    options.UseSqlServer(Configuration[ConnectionStringKey]));

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(c =>
            {
                //Add the detail information for the API.
                c.SwaggerDoc("v2", new Info
                {
                    Version = "v2",
                    Title = "Team & Players Performance API",
                    Description = "Microsoft TPP Web API",
                    TermsOfService = "Contract",
                    Contact = new Contact { Name = "Copyright (c) Microsoft Corporation. All rights reserved.", Email = "", Url = "" },
                    License = new License { Name = "Licensed under the MIT license", Url = "https://tppapp.blob.core.windows.net/content/static/License.txt" },
                    Extensions = { }
                });
                c.SwaggerDoc("v3", new Info
                {
                    Version = "v3",
                    Title = "Team & Players Performance API",
                    Description = "Microsoft TPP Web API",
                    TermsOfService = "Contract",
                    Contact = new Contact {Name = "Copyright (c) Microsoft Corporation. All rights reserved.", Email = "", Url = ""},
                    License = new License {Name = "Licensed under the MIT license", Url = "https://tppapp.blob.core.windows.net/content/static/License.txt" },
                    Extensions = {}
                });
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            loggerFactory.AddDebug(LogLevel.Trace);

            // Configure the app to use Jwt Bearer Authentication
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Authority = string.Format(Configuration["AzureAd:AadInstance"], Configuration["AzureAD:Tenant"]),
                Audience = Configuration["AzureAd:Audience"],
            });


            app.UseCors(builder => builder
                .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/v3/{controller}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "TPP API v3");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "TPP API v2");
            });
        }


        private string GetXmlCommentsPath()
        {
            return System.IO.Path.Combine("wwwroot", @"api.xml");
        }

        private string GetLogPath()
        {
            return System.IO.Path.Combine("wwwroot", "Api.log-{Date}.txt");
        }

    }
}
