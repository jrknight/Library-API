﻿using Library.API.Services;
using Library.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration _config, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            Configuration = _config;

            env = hostingEnvironment;
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("config.json");
            _env = env;
            config = builder.Build();
            this.logger = logger;
            logger.LogDebug("Startup");

        }

        private IHostingEnvironment _env;
        public IConfiguration Configuration { get; }
        public IHostingEnvironment env;
        public IConfigurationRoot config;
        private ILogger logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
           {
               if (!_env.IsProduction())
               {
                   
               }
               opt.Filters.Add(new RequireHttpsAttribute());
           })
                 .AddMvcOptions( o =>
                 {
                     o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter());

                 }).AddJsonOptions( o =>
                 {
                     o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                     o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 });

            services.AddIdentity<Admin, IdentityRole>().AddEntityFrameworkStores<LibraryDbContext>();

            services.Configure<IdentityOptions>(config =>
            {
                config.Cookies
            });

            /// TODO: Impliment and finalize this model of authentication to match the front-end
            /// TODO: Authorize only requests from my front-end & postman

            var connectionString = config["connectionStrings:LocalDb"];
            services.AddDbContext<LibraryDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookGenreRepository, BookGenreRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LibraryDbContext context)
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



            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Book, Models.BookDto>();
                cfg.CreateMap<Models.BookForCreationDto, Entities.Book>();
                cfg.CreateMap<Entities.Author, Models.AuthorDto>();
                cfg.CreateMap<Models.AuthorForCreationDto, Entities.Author>();
                cfg.CreateMap<Entities.Genre, Models.GenreDto>();
                cfg.CreateMap<Models.GenreForCreationDto, Entities.Genre>();
                cfg.CreateMap<Models.BookRequestForCreationDto, BookRequest>();
            });

            logger.LogDebug("Configuration Complete");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
