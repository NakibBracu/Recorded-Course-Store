using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RCS.Data.Identity.Store;


namespace RCS.Data.Identity.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddHibernateStores(this IdentityBuilder builder)
        {
            AddStores(builder.Services, builder.UserType, builder.RoleType!);
            return builder;
        }

        private static void AddStores(IServiceCollection services, Type userType, Type? roleType)
        {
            if (roleType != null)
            {
                // register user store type
                var userStoreServiceType = typeof(IUserStore<>).MakeGenericType(userType);
                var userStoreImplType = typeof(UserStore<,>).MakeGenericType(userType, roleType);
                services.AddScoped(userStoreServiceType, userStoreImplType);

                // add role store type
                var roleStoreSvcType = typeof(IRoleStore<>).MakeGenericType(roleType);
                var roleStoreImplType = typeof(RoleStore<>).MakeGenericType(roleType);
                services.AddScoped(roleStoreSvcType, roleStoreImplType);
            }
            else
            {
                // register user only store type
                var userStoreServiceType = typeof(IUserStore<>).MakeGenericType(userType);
                var userStoreImplType = typeof(UserOnlyStore<>).MakeGenericType(userType);
                services.AddScoped(userStoreServiceType, userStoreImplType);
            }
        }
    }
}
