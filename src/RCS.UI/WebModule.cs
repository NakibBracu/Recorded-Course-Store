using Autofac;
using RCS.UI.Areas.Admin.Models;
using RCS.UI.Models;
using RCS.UI.Services;

namespace RCS.UI
{
    public class WebModule : Module
    {


        public WebModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseCreateModel>().AsSelf();
            builder.RegisterType<CourseUpdateModel>().AsSelf();
            builder.RegisterType<CourseListModel>().AsSelf();
            builder.RegisterType<PlaceOrderModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<OrderListModel>().AsSelf();
            builder.RegisterType<UserListModel>().AsSelf();
            builder.RegisterType<FileService>().As<IFileService>()
               .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
