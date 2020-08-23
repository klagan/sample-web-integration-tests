namespace Sample.Web.Integration.Test.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Sample.Web.Integration.WebApi.Services;

    public class MyWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        
        protected override void ConfigureWebHost(
            IWebHostBuilder builder
        )
        {
            builder
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