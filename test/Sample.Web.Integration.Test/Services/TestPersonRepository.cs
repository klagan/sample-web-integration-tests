namespace Sample.Web.Integration.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Sample.Web.Integration.WebApi.Models;
    using Sample.Web.Integration.WebApi.Services;

    public class TestPersonRepository : IPersonRepository
    {
        private static readonly IList<IPerson> People;
        
        static TestPersonRepository()
        {
            People = new List<IPerson> {new Person(1, "kam", "lagan", EnvironmentType.Test)};

        }
        
        public IPerson Get(
            int id
        )
        {
            return People.SingleOrDefault(o => o.Id == id);
        }
    }
}