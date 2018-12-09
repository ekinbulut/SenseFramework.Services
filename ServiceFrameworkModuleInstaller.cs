/*
 * Project : SenseFramework - Services
 * Author : Ekin Bulut 
 * Date : 25.02.2017
 * 
 * Desc : This class is a Windsor Installer for SenseFramework.Services.dll. 
 *        The main objective is to build a base structure for WCF services. 
 *        This class registers the wcf facilities and configures the initial behavior of the endpoint services.
 *        
 */

using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;


namespace SenseFramework.Services
{
    using Core.Configuration;
    using Integrations;

    public class ServiceFrameworkModuleInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            container.AddFacility<WcfFacility>(f =>
            {
                f.Services.AspNetCompatibility = AspNetCompatibilityRequirementsMode.Allowed;
            });

            //Adding service behavior for service exceptions.
            ServiceDebugBehavior returnFaults = new ServiceDebugBehavior
            {
                IncludeExceptionDetailInFaults = true,
                HttpHelpPageEnabled = true,
            };

            container.Register(
                Component.For<IServiceBehavior>().Instance(returnFaults));

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AssemblyInstaller.AssemblyDirectory))
                .BasedOn<IServiceApplication>()
                .WithServiceAllInterfaces()
                .LifestyleTransient());

            var services = container.ResolveAll<IServiceApplication>();

            foreach (IServiceApplication serviceApplication in services)
            {
                serviceApplication.RegisterServices(container);
            }


        }
    }
}