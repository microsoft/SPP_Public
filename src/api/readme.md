# Sports Performance Platform Deployment Guide

Note: You will see "TPP" in the notes and in the code namespaces/code-files. This is because the code-name for our project (before we went through official branding) was team and player performance--so TPP. 

This guide will walk you the process of deploying the TPP infrastructure into an Azure subscription.

> __Note__: At this moment only database deployment is covered. Web Api deployment guide will come later.

## Database deployment

### Prerequisites

* An Azure subscription
* Visual Studio 2015
* SQL Server data tools

### Clone the tpp-backend repository
* If you have not done so already, clone the __tpp-backend__ repository to your local machine.
* Check out the __master__ branch.

### Create the SQL Server Resource Group
* Create resource group using the Azure Portal or PowerShell/CLI (https://azure.microsoft.com/en-us/documentation/articles/powershell-azure-resource-manager).

### Deploy SQL Server database
* Navigate to the __scripts/sql-server/arm-templates__ folder.
* Use __sql-server-database.json__ and __sql-server-database.parameters.json__ template files to make a new deployment into the resource group created earlier.

### Publish the database schema
> __Note__: Use Visual Studio Developer Command Prompt

> __Note__: Instruction on how to create publish profile could be found here: https://msdn.microsoft.com/en-us/library/hh272687(v=vs.103).aspx

* Navigate to the __src/TPP/Data/TPP.Data.Sql__ folder.
* Run the following command: __msbuild TPP.Data.Sql.sqlproj /t:Build;Publish /p:SqlPublishProfilePath=your_file_name.publish.xml__
