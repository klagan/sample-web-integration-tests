namespace Sample.Web.Integration.WebApi.Services
{
    using Sample.Web.Integration.WebApi.Models;

    public interface IPersonRepository
    {
        IPerson Get(
            int id
        );
    }
}