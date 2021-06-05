using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Repositories;
using Pharmacy.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using Pharmacy.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Pharmacy.Models.Mappings;
using Pharmacy.Services.Interfaces;
using Pharmacy.Services;
using Pharmacy.Helpers;

namespace Pharmacy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PharmacyDBContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
            });

            services.AddControllers();

            services.AddScoped<IProductsRepo, SqlProductsRepo>();
            services.AddScoped<ICartRepo, SqlCartRepo>();

            services.AddScoped<ICartService, CartService>();

            services.Configure<Contact>(Configuration.GetSection("Contact"));

            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true);
                });
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/pharmacy-313010";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/pharmacy-313010",
                        ValidateAudience = true,
                        ValidAudience = "pharmacy-313010",
                        ValidateLifetime = true
                    };
                });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CartItemMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PharmacyDBContext dBContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            dBContext.Database.EnsureCreated();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
