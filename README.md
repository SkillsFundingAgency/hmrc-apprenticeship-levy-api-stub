
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

# Config Setup

The configuration in use is the standard das AzureStorage setup.
To install the config you will need to: 

0. Install the AzureStorage Emulator
1. Clone the [das-employer-config](https://github.com/SkillsFundingAgency/das-employer-config) repo from GitHub
2. Clone the [das-employer-config-updater](https://github.com/SkillsFundingAgency/das-employer-config-updater) repo from GitHub
3. Run the das-employer-config-updater project following the prompts (this will install the config into the AzureStorage Emulator)