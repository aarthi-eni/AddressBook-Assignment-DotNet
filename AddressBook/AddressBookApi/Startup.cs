using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Entities;
using Microsoft.EntityFrameworkCore;
using Contracts.IRepositories;
using Contracts.IServices;
using Services;
using Repositories;
using AutoMapper;
using AddressBookApi.Controllers;
namespace AddressBook
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["JwtSecret:Issuer"],
                       ValidAudience = Configuration["JwtSecret:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecret:Key"]))
                   };
               });
            services.AddControllers(setupAction =>
             {
                setupAction.ReturnHttpNotAcceptable = true;
             }).AddNewtonsoftJson(setupAction =>
             {
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             });


            ServiceProvider serviceProvider= services.BuildServiceProvider();
            ILogger<UserController> loggerAddressBook = serviceProvider.GetService<ILogger<UserController>>();
            //ILogger<AssetServices> loggerAsset = serviceProvider.GetService<ILogger<AssetServices>>();

            services.AddSingleton(typeof(ILogger), loggerAddressBook);
            //services.AddSingleton(typeof(ILogger), loggerAsset);

            services.AddScoped<IAuthServices,AuthServices>();
            services.AddScoped<IAddressBookService, AddressBookService>();
            //services.AddScoped<IAssetService, AssetServices>();
             services.AddScoped<IAuthenticationRepositories,AuthenticationRepositories>();
            services.AddScoped<IAddressBookRepositories, AddressBookRepositories>();
            //services.AddScoped<IAssetRepositories, AssetRepositories>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
             
            services.AddDbContext<AddressBookContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            }); 
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}