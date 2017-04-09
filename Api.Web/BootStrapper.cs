
using Api.Services;
using Api.Web.Extensions;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace Api.Web
{
    public static class BootStrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            // builder.RegisterWebApiFilterProvider(config);

            builder.RegisterModule(new AppLoggerModule());

            //builder.RegisterWebApiFilterProvider(config);
            // Set the dependency resolver to be Autofac.
            IContainer container = RegisterServices(builder);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterType<ContactsService>().As<IContacts>().InstancePerRequest();
            builder.RegisterType<TokenValidationService>().As<IToken>().InstancePerRequest();
            return builder.Build();
        }
    }
}