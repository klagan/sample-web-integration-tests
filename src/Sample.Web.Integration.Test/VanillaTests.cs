namespace Sample.Web.Integration.Test
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Sample.Web.Integration.WebApi;
    using Sample.Web.Integration.WebApi.Models;
    using Xunit;

    public class VanillaTests 
        : IClassFixture<MyCustomWebApplicationFactory<Startup>>
    {
        private readonly MyCustomWebApplicationFactory<Startup> _factory;

        public VanillaTests(MyCustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TestPersonRepository()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Person");
            var person = (IPerson) JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            
            response.EnsureSuccessStatusCode(); 
            
            Assert.Equal(EnvironmentType.Test, person.Environment);
        }
    }
}