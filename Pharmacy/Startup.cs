using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Repositories;
using Pharmacy.Models.Database.Repositories.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                options
                .UseSqlServer(
                    Configuration.GetConnectionString("DBConnection"), 
                    p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });

            services.AddControllers();

            services.AddScoped<IClientsRepo, SqlClientsRepo>();
            services.AddScoped<IProductsRepo, SqlProductsRepo>();
            services.AddScoped<ICartRepo, SqlCartRepo>();
            services.AddScoped<IActiveSubstancesRepo, SqlActiveSubstancesRepo>();
            services.AddScoped<IPassiveSubstancesRepo, SqlPassiveSubstancesRepo>();
            services.AddScoped<ICategoryRepo, SqlCategoryRepo>();

            services.AddScoped<CartService>();
            services.AddScoped<SubstancesService>();
            services.AddScoped<ProductsService>();

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

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
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
