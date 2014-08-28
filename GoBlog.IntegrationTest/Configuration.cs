using GoBlog.Areas.Admin;
using NUnit.Framework;
using SpecsFor.Mvc;

namespace GoBlog.IntegrationTest
{
    [SetUpFixture]
    public class Configuration
    {
        private SpecsForIntegrationHost host;

        [SetUp]
        public void SetUp()
        {
            var configuration = new SpecsForMvcConfig();

            configuration
                .UseIISExpress()
                .With(Project.Named("GoBlog"))
                .ApplyWebConfigTransformForConfig("Debug");

            configuration.RegisterArea<AdminAreaRegistration>();
            configuration.BuildRoutesUsing(RouteConfig.RegisterRoutes);
            configuration.UseBrowser(BrowserDriver.InternetExplorer);
        
            host = new SpecsForIntegrationHost(configuration);
            host.Start();
        }

        [TearDown]
        public void TearDown()
        {
            host.Shutdown();
        }
    }
}