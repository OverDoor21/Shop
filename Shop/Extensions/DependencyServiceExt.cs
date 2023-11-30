using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Services;

namespace Shop.Extensions
{
    public static class DependencyServiceExt
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
            services.AddScoped<ProductServices>();
            services.AddScoped<BargainService>();
            return services;
        }

    }
}
