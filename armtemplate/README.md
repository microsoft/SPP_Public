# SPP Deployment Process
+ Created by
  - Michael Hill Technical Architect, POP, Inc.
  - Kevin Moses, Sr Software Engineer, POP, Inc.

These instructions are created to guide in the setup of the Microsoft Sports Performance Platform (SPP). Setup requires an active Azure Subscription and Azure PowerShell. To see if PowerShell is intalled open Windows PowerShell and type *Get-Module AzureRm -list*. If not installed follow the instructions here: [Install and Configure Azure PowerShell](https://docs.microsoft.com/en-us/powershell/azure/install-azurerm-ps?view=azurermps-4.1.0&viewFallbackFrom=azurermps-4.0.0) Once installed you are ready to proceed. NOTE: when creating names for the items below change *YOURORG* to the name of your organization. Abbreviations might be needed due to length constraints.

## Set up Active Directory
  * Open the Azure Portal and Create new Active Directory for SPP Admin
    * Naming convention is SPP*YOURORG*-Admin for domain and org name
  * Add users to Active Directory

## Create a Resource Group
  * In the desired subscription add a resource group with the name SPP-*YourORG*

## Execute the ARM templates
  * Upload the SPPTemplateDBexport.bacpac to an Azure Storeage location
    * Upate the SqlDataImportStorageKey and SqlDataImportStorageUri in the sportsperformance.parameters.json for the location of the bacpac file


  * Change names in the parameters files to match your organization in sportsperformance.parameters.json:

    NOTE: that some of the services have limitations on length of the names. These are called out in comments in the parameters files. Adjust names for anything starting with sportsperf so the services are still recognized for their intended purpose. Naming convention used should be similar to ssp*YOURORG*api as an example.

    NOTE: You will need to supply a SQL Admin user name and password during ARM template execution.

  	NOTE: Uncomment the SQL Firewall section of the ARM Template to enable the "Allow access to Azure services". This option configures the firewall to allow all connections from Azure including connections from the subscriptions of other customers. When selecting this option, make sure your login and user permissions limit access to only authorized users. For more information read the following: [Azure SQL Database server-level and database-level firewall rules](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-firewall-configure)

    This firewall rule is required for the ARM template to create the DB tables and seed with sample data. If you chose not to do this you will have to manually restore the SPPTemplateDBexport.bacpac from SQL Server Management Studio.

  * Run the deploy-sportsperformance.ps1

    This powershell script will provision all services needed for the SPP API and Database with seeded content. You will need your Azure Subscription ID for the deployment. Follow all prompts. Once the SPP API items are finished you will be able to see them in the Azure Resource group specified. If the code deploy failed please follow the Manual deployment instructions below.


## Manually deploy the API code
  * Deploy SPP API
    * Open the SPP API Solution
      * The solution can be found here: sportsperformance\src\api\TPP.v3.sln
    * Right-click TPP.API in the solution explorer and select publish
    * Select or create a new publish profile for the App service created by the ARM template deployment
    * Click publish to publish the API


## Register Applications with SPP*YOURORG*-admin Active Directory
  * Register SPP API
    * Use SPP API as display name
    * Use SPPYOURORGADMIN.onmicrosoft.com for sign in URL
    * Click Create
    * Update the manifest
      * Change oauth2AllowImplicitFlow to True
    * Open Setting and change reply URLs
      * Add https://sppYOURORGadmin.onmicrosoft.com/sppapi
	     * Copy this into Defines in APP project
    * Add https://sppYOURORGapi.azurewebsites.com
    * Add Localhost:44333
        * Make sure this matches project debug URL
