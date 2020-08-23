namespace Sample.Web.Integration.Test.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Sample.Web.Integration.WebApi.Services;

    public class MyWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        
        protected override void ConfigureWebHost(
            IWebHostBuilder builder
        )
        {
            builder
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    var environmentName =
                        Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT") ??
                        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                        "Development";

                    configurationBuilder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(services =>
                {
                    // find the production service in the service container (personrepository)
                    var personRepoDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(IPersonRepository));

                    // remove the production service
                    services.Remove(personRepoDescriptor);

                    // replace it with our test repository
                    services.AddTransient<IPersonRepository, TestPersonRepository>();
                });
        }
    }
}