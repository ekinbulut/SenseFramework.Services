/*
 * Project : SenseFramework - Services
 * Author : Ekin Bulut 
 * Date : 25.02.2017
 * 
 * Desc : Implementation of ITierApplicationModule. This module executes the registration of components into IOC Manager.
 *        
 */


namespace SenseFramework.Services
{
    using Core.Integrations;
    using Core.IoC;
    using Core.Messaging;
    public class ServiceFrameworkModule : ITierApplicationModule
    {
        public void RegisterModule()
        {
            Messanger.Logger.Info("Service Core Module Installing...");

            IoCManager.Container.Install(new ServiceFrameworkModuleInstaller());

            Messanger.Logger.Info("Service Core Module Installed...");

        }
    }
}
