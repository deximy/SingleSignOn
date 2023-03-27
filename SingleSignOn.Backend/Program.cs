using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SingleSignOn.Backend.Services;
using System.ComponentModel;

namespace SingleSignOn.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<JwtGeneratorService>();

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(
                    options => {
                    }
                )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services
                .AddIdentityServer()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var db_context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                db_context?.Database.Migrate();
            }

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run();
        }
    }
}
