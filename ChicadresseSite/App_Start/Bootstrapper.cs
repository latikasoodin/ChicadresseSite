using Autofac;
using Autofac.Integration.Mvc;
using Chicadresse.Business.DependencyModule;
using Chicadresse.Core.Caching;
using ChicadresseSite.DependencyModule;
using ChicadresseSite.Mappings;
using System.Reflection;
using System.Web.Mvc;

namespace ChicadresseSite.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutomapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterFilterProvider();
            builder.RegisterType(typeof(MemoryCacheManager)).As(typeof(ICacheManager)).InstancePerLifetimeScope();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new DataModule());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}