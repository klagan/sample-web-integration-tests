# Integration test samples for ASP.NET Core MVC applications

Different branches will illustrate different maturities of the solution

## 1. develop/custom-webfactory

This sample uses a custom web factory to replcae the production services with test services.  This allows us to test with mock or test dependencies.


## 2. develop/add-authentication

This sample introduces authentication and authorisation.  It tests the endpoint using a test authentication scheme and tests denial of access if the user is not authenticated.

// TODO: write terraform scripts to create application in azure and automate user secrets

Run these user secret commands to configure the web api application registration.  The details will be found in the cloud application registration.

```
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:TenantId <tenantid>
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:ClientId <clientid>
```

Run these user secret commands to configure the test client application registration.  The details will be found in the cloud application registration.

```
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:TenantId <tenantid>
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:ClientId <clientid>
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:ClientSecret <clientsecret>
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:UserName <username>
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:Password <password>
dotnet user-secrets set --id aspnet-Sample.Web.Integration.WebApi-EC6B85CD-844D-4C0D-B02C-F421B05181BF Test:Scopes <scopes>
```

The test client application secrets can be pushed into the environment variables in production and then removed after tests are complete.