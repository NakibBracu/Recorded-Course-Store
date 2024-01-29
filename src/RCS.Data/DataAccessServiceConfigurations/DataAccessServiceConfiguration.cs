using Microsoft.Extensions.DependencyInjection;
using RCS.Data.Identity.Entities;
using RCS.Data.Identity.Extensions;
using RCS.Data.NHibernateConfig;
using RCS.Data.Repositories;
using RCS.Data.UnitOfWorks;

namespace RCS.Data.DataAccessServiceConfigurations
{
    public static class DataAccessServiceConfiguration
    {
        public static IServiceCollection ConfigureDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddNHibernate(connectionString);

            
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddHibernateStores();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICartLineRepository, CartLineRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }

}
