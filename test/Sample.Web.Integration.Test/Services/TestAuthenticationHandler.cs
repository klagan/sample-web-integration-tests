namespace Sample.Web.Integration.Test.Services
{
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Sample.Web.Integration.Test.Models;

    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Test user"), 
                new Claim(ClaimTypes.Role, "Admin")
            };
            
            var identity = new ClaimsIdentity(claims, TestConstants.TestAuthenticationSchemeName);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, TestConstants.TestAuthenticationSchemeName);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}