
# SFA-DAS-HMRC-API-Stub

The App is a .Net Core 2.2 web application 

This is a port of an existing stub application that can be found here: https://github.com/UKGovernmentBEIS/das-alpha-taxservice-mock
To maitain backwards compatibility the same mechanism of token generation has been used

# SFA-DAS-TaxService-Stub

The App is a .Net Core 2.2 web application

This is a port of an existing stub application that can be found here: https://github.com/UKGovernmentBEIS/das-alpha-hmrc-api-mock
To maitain backwards compatibility the same mechanism of token generation has been used

# API Documentation

The API is documented with Swagger and the OAuth via a .wellknown endpoint

* https://localhost:44360/index.html
* https://localhost:44360/.well-known/openid-configuration

# Authorization

Authorization is by custom generated tokens that follow the OAuth2 Code flow.
Swagger has been added to the SFA-DAS-HMRC-API-Stub project and the OAuth configuration can also be seen via the app using the /.well-known/openid-configuration URI

## Example flow

Create a browser request to generate an access code:

<code>
GET: https://localhost:44360/oauth/authorize?client_id=28fHGKpDXKZwtQwSmaO3W9FwV0Ia&redirect_uri=http%3a%2f%2flocalhost%2fauth&response_type=code&scope=read:apprenticeship-levy
</code>

Once an access code is generated by successfully logging in via the SFA-DAS-TaxService-Stub website a request for an access token can be sent using a POST to:

<code>
https://localhost:44360/oauth/token
</code>

(the body is required to be x-www-form-urlencoded)

<code>
code:xxxxxxxxxxxxxxx
client_id:xxxxxxxxxxxxxxxxxxx
client_secret:xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
redirect_uri:http%3A%2F%2Flocalhost%2Fauth
grant_type:authorization_code
</code>

The call to this endpoint will return an access token that can then be used to authenticate against the SFA-DAS-HMRC-API-Stub project endpoints

Refresh token can be obtained by making POST request to 
<code>
https://localhost:44360/oauth/token
</code>

<code>
refresh_token:xxxxxxxxxxxxxxxx
client_id:xxxxxxxxxxxxxxxxxxx
client_secret:xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
grant_type:refresh_token
</code>

# Database Setup

The current implementation uses the existing mlab mongodb database

# Config Setup (TBD)

N.B The project uses the configuration mechanism, however the das-employer-config project has not yet been updated

The configuration in use is the standard das AzureStorage setup.
To install the config you will need to: 

0. Install the AzureStorage Emulator
1. Clone the [das-employer-config](https://github.com/SkillsFundingAgency/das-employer-config) repo from GitHub
2. Clone the [das-employer-config-updater](https://github.com/SkillsFundingAgency/das-employer-config-updater) repo from GitHub
3. Run the das-employer-config-updater project following the prompts (this will install the config into the AzureStorage Emulator)