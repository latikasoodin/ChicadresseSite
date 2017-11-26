using System.Linq;
using Autofac;

namespace Chicadresse.Business.DependencyModule
{
    public class DataModule : Module
    {
        #region ctor
        public DataModule()
        {

        }
        #endregion

        #region methods

        /// <summary>
        /// overrided method to load the configuration for datamodule/repositorymodule.
        /// </summary>
        /// <param name="builder">ContainerBuilder(Used to build an Autofac.IContainer from component registrations)</param>
        protected override void Load(ContainerBuilder builder)
        {
            //Register types using reflection
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("Chicadresse.Data"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            //Or Resgister types manually
            //builder.RegisterType(typeof(GenericRepository)).As(typeof(IRepository)).InstancePerRequest() ;
        }

        #endregion
    }
}
