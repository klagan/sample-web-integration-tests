namespace Sample.Web.Integration.Test
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Sample.Web.Integration.Test.Models;
    using Sample.Web.Integration.Test.Services;
    using Sample.Web.Integration.WebApi;
    using Sample.Web.Integration.WebApi.Models;
    using Xunit;

    public class VanillaTests 
        : IClassFixture<MyWebApplicationFactory<Startup>>
    {
        private readonly MyWebApplicationFactory<Startup> _factory;

        public VanillaTests(MyWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            
            var configuration = _factory.Services.GetService<IConfiguration>();

            var testSettings = new TestConfiguration();
            configuration.GetSection(TestConstants.TestConfigurationSection).Bind(testSettings);

            _factory.ClientOptions.BaseAddress = new Uri(testSettings.BaseAddress);
            _factory.ClientOptions.AllowAutoRedirect = false; // set to false for authn/authn tests
        }

        [Fact]
        public async Task TestPersonRepository()
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("/Person");
            var person = (IPerson) JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            
            response.EnsureSuccessStatusCode(); 
            
            Assert.Equal(EnvironmentType.Test, person.Environment);
        }
        
        /// <summary>
        /// Test that an unauthenticated user is not allowed to secured endpoints
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task When_attempt_to_access_secure_endpoint_by_unauthorised_user_Then_returned_401()
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("/Person/Secured");
            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact]
        public async Task When_attempt_to_access_secure_endpoint_by_authorised_user_Then_returned_200()
        {
            var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddAuthentication(TestConstants.TestAuthenticationSchemeName)
                            .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                                TestConstants.TestAuthenticationSchemeName, options => { });
                    });
                })
                .CreateClient();

            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(TestConstants.TestAuthenticationSchemeName);

            var response = await client.GetAsync("/Person/Secured");
            var result = await response.Content.ReadAsStringAsync();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Secured!", result);
        }
    }
}