namespace Sample.Web.Integration.WebApi.Controllers
{
    using Sample.Web.Integration.WebApi.Models;
    using Sample.Web.Integration.WebApi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _personRepository;

        public PersonController(
            IPersonRepository personRepository,
            ILogger<PersonController> logger
        )
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        [HttpGet]
        public IPerson Get()
        {
            return _personRepository.Get(1);
        }

        [Authorize]
        [HttpGet("Secured")]
        public string Secured()
        {
            return "Secured!";
        }
    }
}