using Castle.Windsor;

namespace SenseFramework.Services.Integrations
{
    /// <summary>
    /// Implemantation interface for WCF service applications.
    /// </summary>
    public interface IServiceApplication
    {
        /// <summary>
        /// Registers the wcf services.
        /// </summary>
        /// <param name="container">The IoC_Container.</param>
        void RegisterServices(IWindsorContainer container);
    }
}
