using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICourseRepository, CourseRepository>();

            return services;
        }
    }

}
