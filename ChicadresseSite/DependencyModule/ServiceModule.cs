using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicadresseSite.DependencyModule
{
    public class ServiceModule : Module
    {
        #region ctor
        public ServiceModule()
        {

        }
        #endregion

        #region methods

        /// <summary>
        /// overrided method to load the configuration for servicemodule/businessmodule.
        /// </summary>
        /// <param name="builder">ContainerBuilder(Used to build an Autofac.IContainer from component registrations)</param>
        protected override void Load(ContainerBuilder builder)
        {
            //Register types dynamically using reflection.
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("Chicadresse.Business"))
                      .Where(t => t.Name.EndsWith("Service"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();

            //or Register types manually.
            //builder.RegisterType(typeof(BaseService)).As(typeof(IService)).InstancePerRequest() ;

        }
        #endregion
    }
}