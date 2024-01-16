using Microsoft.Extensions.DependencyInjection;
using RCS.Data.NHibernateConfig;
using RCS.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCS.Services
{
    public static class ServicesConfig
    {
        public static IServiceCollection RegisterServiceLayers(this IServiceCollection services)
        {

            services.AddScoped<ICourseService, CourseService>();
            return services;
        }
    }
}
