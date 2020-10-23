using DotVVM.Diagnostics.StatusPage;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmStatusPageDemo
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);

            var aggregateMarkupFileLoader = config.ServiceProvider.GetService<IMarkupFileLoader>() as AggregateMarkupFileLoader;
            
            aggregateMarkupFileLoader.Loaders.RemoveAt(0);
            aggregateMarkupFileLoader.Loaders.Add(new EvilMarkupFileLoader());
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("TheGood", "good", "Views/TheGood.dothtml");
            config.RouteTable.Add("TheBad", "bad", "Views/TheBad.dothtml");
            config.RouteTable.Add("TheUgly", "ugly", "Views/TheUgly.dothtml");

            config.RouteTable.Add("Default", "", "Views/Default.dothtml");

        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("Styles", new StylesheetResource()
            {
                Location = new UrlResourceLocation("~/Resources/style.css")
            });
        }

		public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddStatusPage(pageOptions => pageOptions.CompileAfterPageLoads=false);
            //options.AddStatusPageApi();
            options.AddDefaultTempStorages("temp");

		}
    }
}
