## SenseFramework.Services Samples

To implement SenseFramework.Services features, only implement IServiceApplication interface to your referenece class.

This will guide you to register your endpoint with fluent configuration.

### Sample

---

>```csharp
> public class BookModuleRegisterar : IServiceApplication
>    {
>        private ILogger _logger;
>
>        public BookModuleRegisterar(ILogger logger)
>        {
>            _logger = logger;
>        }
>
>        public void RegisterServices(IWindsorContainer container)
>        {
>           // string baseAddress = ConfigurationManager.AppSettings["BookServiceHost"];
>            var ipaddress = IpFinder.GetLocalIpAddress();
>
>            string baseAddress = $"http://{ipaddress}:8097/services/";
>
>
>            container.Register(
>                Component.For<IBookService>().ImplementedBy<BookServiceApplication>()
>                    .AsWcfService(new DefaultServiceModel()
>                    .AddEndpoints(WcfEndpoint.ForContract(typeof(IBookService)).BoundTo(new WSHttpBinding
>                    {
>                        MaxReceivedMessageSize = int.MaxValue,
>                        ReceiveTimeout = new TimeSpan(0, 0, 2, 0, 0),
>                        CloseTimeout = new TimeSpan(0, 0, 0, 60, 0),
>                        Security = new WSHttpSecurity { Mode = SecurityMode.None }
>
>                    }))
>                    .PublishMetadata(c => c.EnableHttpGet())
>                        .AddBaseAddresses(new Uri(baseAddress + "books"))).LifestylePerWcfOperation());
>
>            _logger.Info($"Server endpoint on : {baseAddress}books");
>
>        }
>    }
>
>
>```

After implementation of **_IServiceApplication_** interface, SenseFramework will scan and register all components at startup of the application.