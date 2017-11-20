using Autofac;
using Autofac.Integration.Mvc;
using Chicadresse.Business.DependencyModule;
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

            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new DataModule());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}