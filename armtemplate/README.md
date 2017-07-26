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

    Note that some of the services have limitations on length of the names. These are called out in comments in the parameters files. Adjust names for anything starting with sportsperf so the services are still recognized for their intended purpose. Naming convention used should be similar to ssp*YOURORG*api as an example.

  * Run the deploy-sportsperformance.ps1

    This powershell script will provision all services needed for the SPP API and Database with seeded content. You will need your Azure Subscription ID for the deployment. Follow all prompts. Once the SPP API items are finished you will be able to see them in the Azure Resource group specified. If the code deploy failed please follow the Manual deployment instructions below.

  * Update SQL Administrator password
    * In the Azure Portal select the Database Server that was created during ARM template execution
    * Click Reset Password
    * Enter new password ensuring proper password strength
    * Go back to the Resource Group where everything was deployed
    * Open the API App Service that was created by the ARM template
    * Select Application Settings
    * Scroll down to Connection Strings and click Show connection string values
    * Update the password for the TppDbConnection connection string
    * Click Save


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
