namespace Sample.Web.Integration.WebApi.Models
{
    public class Person : IPerson
    {
        public Person(
            int id,
            string firstName,
            string lastName,
            EnvironmentType environment
        )
        {
            Id = id;
            Environment = environment;
            FirstName = firstName;
            LastName = lastName;
        }
        
        public int Id { get; set; }
        
        public EnvironmentType Environment { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}