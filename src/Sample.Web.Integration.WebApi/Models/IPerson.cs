namespace Sample.Web.Integration.WebApi.Models
{
    public interface IPerson
    {
        int Id { get; }
        EnvironmentType Environment { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}