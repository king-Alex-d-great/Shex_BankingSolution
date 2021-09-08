using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;


namespace WebUI.domain.Middlewares
{
    public static class DbsExtension
    {
        public static IServiceCollection AddDBConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OBConnection");
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("OBConnection")));            

            return services;
        }
    }
}
