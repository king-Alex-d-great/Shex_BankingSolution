using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineBanking.Domain.Entities;

namespace WebUI.domain
{
    public class Program
    {
        public async static Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<AppDbContext>();
                    var userManager = service.GetRequiredService<UserManager<User>>();
                    var roleManager = service.GetRequiredService<RoleManager<AppRole>>();
                    await DataInitializer.SeedRolesAsync(userManager, roleManager);
                    await DataInitializer.SeedSuperAdminAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred! seeding Failed.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public void GeneratePassword(char firstname, char lastname)
        {
            var midFourCharacters = (RandomNumberGenerator.GetInt32(10, 99)).ToString() + (RandomNumberGenerator.GetInt32(10, 99)).ToString();
            Console.WriteLine((char)92);
        }
    }
}
