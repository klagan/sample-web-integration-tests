namespace Sample.MyAuthentication.Helpers
{
    using System.Linq;
    using System.Security;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Identity.Client;

    public class ResourceOwnerPasswordAuthentication
    {
        public static string GetToken(IConfiguration configuration, string configurationSectionName = "Test")
        {
            var testOptions = new TestConfiguration();
            configuration.Bind(configurationSectionName, testOptions);
            
            var securePassword = new SecureString();
            testOptions.Password.ToCharArray().ToList().ForEach(securePassword.AppendChar);
            
            var scopes = new [] {testOptions.Scopes};

            var clientOptions = new MyPublicClientApplicationOptions();
            configuration.Bind(configurationSectionName, clientOptions);

            var authClient = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(clientOptions)
                .Build();

            var result = authClient
                .AcquireTokenByUsernamePassword(scopes, testOptions.UserName, securePassword)
                .ExecuteAsync()
                .Result;

            return result.AccessToken;
        }
    }
}