using Autofac;
using Chicadresse.Data.Base;
using Chicadresse.Data.UnitOfWork;

namespace Chicadresse.Business.DependencyModule
{
    public class EFModule : Module
    {
        #region ctor
        public EFModule()
        {

        }
        #endregion

        #region methods

        /// <summary>
        /// overrided method to load the configuration for entityframwork.
        /// </summary>
        /// <param name="builder">ContainerBuilder(Used to build an Autofac.IContainer from component registrations)</param>
        protected override void Load(ContainerBuilder builder)
        {
            // Register the types manually
            builder.RegisterType(typeof(DbFactory)).As(typeof(IDbFactory)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

        }
        #endregion
    }
}
