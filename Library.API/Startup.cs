using Entities;
using Library.API.Seeding;
using Library.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration _config, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            Configuration = _config;

            env = hostingEnvironment;
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("config.json");

            config = builder.Build();
            this.logger = logger;
            logger.LogDebug("Startup");

        }
        
        public IConfiguration Configuration { get; }

        private IHostingEnvironment env;
        public IConfigurationRoot config;
        private ILogger logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
           {
               opt.SslPort = 44364;
               opt.Filters.Add(new RequireHttpsAttribute());
           })
                 .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(
                   new XmlDataContractSerializerOutputFormatter());

                }).AddJsonOptions(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ReCircleDbContext>();




            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = config["Auth:Token:Issuer"],
                    ValidAudience = config["Auth:Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth:GUID"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true

                };
            });
            

            /// TODO: Impliment and finalize this model of authentication to match the front-end
            /// TODO: Authorize only requests from my front-end & postman
            
            var connectionString = config["connectionStrings:AzureDb"];
            var testDbCS = config["connectionStrings:LocalDb"];

            if (env.IsProduction())
                services.AddDbContext<ReCircleDbContext>(options =>
                        options.UseSqlServer(connectionString));
            else
                services.AddDbContext<ReCircleDbContext>(options =>
                        options.UseSqlServer(testDbCS));

            services.AddTransient<IdentityInitializer>();
            services.AddSingleton(config);
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemRequestRepository, ItemRequestRepository>();
            services.AddScoped<IItemRecordRepository, ItemRecordRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ReCircleDbContext context, IdentityInitializer identity)
        {

            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //identity.Seed().Wait();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Item, Models.ItemDto>();
                cfg.CreateMap<Models.ItemForCreationDto, Entities.Item>();
            });

            logger.LogDebug("Configuration Complete");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
