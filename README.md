
# das-alpha-hmrc-api-mock

The App is a .Net Core 2.2 web application

# Authorization

Authorization is by Bearer token and requires a header to be present on each Http request:

<code>Authorization: Bearer 123456789</code>

# Database Setup

Publish the database project by right clicking the project and selecting "Publish"

Server Name  : (localdb)\ProjectsV13
Database Name: SFA-DAS-HMRC-API-Stub-Database

There is a post deployment script that will seed the database with test data