# Integration test samples for ASP.NET Core MVC applications

Different branches will illustrate different maturities of the solution

## 1. develop/custom-webfactory

This sample uses a custom web factory to replcae the production services with test services.  This allows us to test with mock or test dependencies.


## 2. develop/add-authentication

This sample introduces authentication and authorisation.  It tests the endpoint using a test authentication scheme and tests denial of access if the user is not authenticated.