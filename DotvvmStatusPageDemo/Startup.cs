using System;
using DotVVM.Framework.Compilation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;

namespace DotvvmStatusPageDemo
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddDotVVM<DotvvmStartup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath,modifyConfiguration:
                configuration =>
                {
                    configuration.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;
                    

                });

            dotvvmConfiguration.AssertConfigurationIsValid();
        }
    }
}
