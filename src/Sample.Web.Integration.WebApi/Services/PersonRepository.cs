namespace Sample.Web.Integration.WebApi.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Sample.Web.Integration.WebApi.Models;

    public class PersonRepository : IPersonRepository
    {
        private static readonly IList<IPerson> People;
        
        static PersonRepository()
        {
            People = new List<IPerson> {new Person(1, "kam", "lagan", EnvironmentType.Production)};

        }
        
        public IPerson Get(
            int id
        )
        {
            return People.SingleOrDefault(o => o.Id == id);
        }
    }
}