
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Resturant_Api.Helper;
using Resturant_Api.MiddleWare;
using Resturant_Api_Core.Entites.User;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Reposatries;
using Resturant_Api_Core.Services.AuthServices;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Services.OrderServises;
using Resturant_Api_Reposatry.Data.AppContext;
using Resturant_Api_Reposatry.identity;
using Resturant_Api_Reposatry.Reposatries;
using Resturant_Api_Reposatry.UnitOfWork;
using Resturant_Api_Services.AuthService;
using Resturant_Api_Services.EntitesSERVICES;
using Resturant_Api_Services.EntitesSERVICES.OrderServ;
using StackExchange.Redis;

namespace Resturant_Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            #region Connections
            builder.Services.AddDbContext<ResturantContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentitycontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);

            });
            #endregion
            #region ScopedServices
            builder.Services.AddScoped<IAuthServices, AuthentcationJwt>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<AppIdentitycontext>();
            builder.Services.AddScoped<IMealsServices,MealsService>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<ITableServices, TableServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<IBasketReposatry, BasketRepo>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            #endregion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            #region SeedDB
            var Scoped = app.Services.CreateScope();
            var services = Scoped.ServiceProvider;
            var context = services.GetRequiredService<ResturantContext>();
            var IdentityContext = services.GetRequiredService<AppIdentitycontext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await IdentityContext.Database.MigrateAsync();
                //----------
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await Seed.SeedData(userManager, roleManager);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }
            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
