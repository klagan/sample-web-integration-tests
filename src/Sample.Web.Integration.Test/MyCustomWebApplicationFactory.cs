namespace Sample.Web.Integration.Test
{
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Sample.Web.Integration.WebApi.Services;

    public class MyCustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(
            IWebHostBuilder builder
        )
        {
            builder.ConfigureServices(services =>
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