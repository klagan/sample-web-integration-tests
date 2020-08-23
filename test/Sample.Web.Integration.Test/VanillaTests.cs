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
    using Xunit.Abstractions;

    public class VanillaTests 
        : IClassFixture<MyWebApplicationFactory<Startup>>
    {
        private readonly MyWebApplicationFactory<Startup> _factory;
        private ITestOutputHelper _outputHelper;

        public VanillaTests(MyWebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper)
        {
            _factory = factory;
            _outputHelper = outputHelper;
            
            var configuration = _factory.Services.GetService<IConfiguration>();

            var testSettings = new TestConfiguration();
            configuration.GetSection(TestConstants.TestConfigurationSection).Bind(testSettings);

            _factory.ClientOptions.BaseAddress = new Uri(testSettings.BaseAddress);
            _factory.ClientOptions.AllowAutoRedirect = false; // set to false for authn/authn tests
        }

        [Fact]
        public async Task Test_that_we_have_replaced_the_dependent_service_with_our_own_fake()
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("/Person");
            var person = (IPerson) JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            
            response.EnsureSuccessStatusCode(); 
            
            Assert.Equal(EnvironmentType.Test, person.Environment);
            
            _outputHelper.WriteLine("example of writing to test output");
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
            
            _outputHelper.WriteLine("another example of writing to test output");
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
            
            _outputHelper.WriteLine("and another example of writing to test output");
        }
    }
}