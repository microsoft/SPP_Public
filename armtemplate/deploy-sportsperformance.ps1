<#
 .SYNOPSIS
    Deploys SPP Api template to Azure

 .DESCRIPTION
    Deploys SPP Api template to Azure

 .PARAMETER subscriptionId
    The subscription id where the template will be deployed.

 .PARAMETER resourceGroupLocation
    Optional, a resource group location. If specified, will try to create a new resource group in this location.

 .PARAMETER deploymentName
    The deployment name.

 .PARAMETER templateFilePath
    Optional, path to the template file. Defaults to sportsperformance.template.json.

 .PARAMETER parametersFilePath
    Optional, path to the parameters file. Defaults to sportsperformance.parameters.json. If file is not found, will prompt for parameter values based on template.
#>

param(
 [Parameter(Mandatory=$True)]
 [string]
 $subscriptionId,

 [string]
 $resourceGroupLocation,

 [Parameter(Mandatory=$True)]
 [string]
 $deploymentName,

 [string]
 $templateFilePath = "sportsperformance.template.json",

 [string]
 $parametersFilePath = "sportsperformance.parameters.json"
)

<#
.SYNOPSIS
    Registers RPs
#>
Function RegisterRP {
    Param(
        [string]$ResourceProviderNamespace
    )

    Write-Host "Registering resource provider '$ResourceProviderNamespace'";
    Register-AzureRmResourceProvider -ProviderNamespace $ResourceProviderNamespace;
}


#******************************************************************************
# Script body
# Execution begins here
#******************************************************************************
Write-Host "";
Write-Host "";
Write-Host "###########################################################################";
Write-Host "#";
Write-Host "# Azure Resource Manager Template Deployment --- Api with database ";
Write-Host "#";
Write-Host "###########################################################################";
Write-Host "";
Write-Host "";

# Test if there's an active session
$currentActionPreference = $ErrorActionPreference;
$ErrorActionPreference = 'silentlycontinue';
$context = Get-AzureRmContext;
$ErrorActionPreference = $currentActionPreference;

$ErrorActionPreference = "Stop"

if($context.Account -eq $null)
{
	Write-Host "Not logged in...";
	Login-AzureRmAccount;
}
else
{
	Write-Host "Already logged in";
}

# Select subscription
Write-Host "";
Write-Host "Selecting subscription '$subscriptionId'";;
Write-Host "";
Select-AzureRmSubscription -SubscriptionID $subscriptionId;;

# Register RPs
$resourceProviders = @("microsoft.web");
if($resourceProviders.length) {
    Write-Host "Registering resource providers"
    foreach($resourceProvider in $resourceProviders) {
        RegisterRP($resourceProvider);
    }
}

# Get the list of Resource Groups for this subscription
$groups = Get-AzureRmResourceGroup;
if($groups.Count -eq 0)
{
	Write-Host "";
	Write-Host "No Resource Groups returned. Ensure you are using the correct subscription.";
	Write-Host "";
	exit;
}
$groupNames = $groups | Select-Object ResourceGroupName;

# Display list of Resource Groups
Write-Host "";
Write-Host "";
Write-Host "The following Resource Groups are available for your subscription:";
$index = 1;
foreach($item in $groupNames)
{
	Write-Host $index'.'  $item.ResourceGroupName;
	$index++;
}

# Prompt for Resource Group
Write-Host "";
Write-Host "";
$resourceGroupName = Read-Host -Prompt 'Type the name to use from above or enter a new one';
if([string]::IsNullOrEmpty($resourceGroupName))
{
	Write-Host "No Resource Group specified";
	exit;
}

# Create or check for existing resource group
$resourceGroup = Get-AzureRmResourceGroup -Name $resourceGroupName -ErrorAction SilentlyContinue;
if(!$resourceGroup)
{
    Write-Host "Resource group '$resourceGroupName' does not exist, it will be created.";
    if(!$resourceGroupLocation) {
		# Get the list of Resource Locations for this subscription
		$locations = Get-AzureRmLocation;
		$locationNames = $locations | Select-Object Location;

		# Display list of Locations
		Write-Host "";
		$index = 1;
		foreach($item in $locationNames)
		{
			Write-Host $index'.'  $item.Location;
			$index++;
		}
        Write-Host "Please enter a location from the list above...";
		$resourceGroupLocation = Read-Host "resourceGroupLocation";
    }
	else{
		Write-Host "Using location $resourceGroupLocation";
	}
    Write-Host "Creating resource group '$resourceGroupName' in location '$resourceGroupLocation'";
    New-AzureRmResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation;
}
else{
    Write-Host "Using existing resource group '$resourceGroupName'";
}

Write-Host "Starting deployment...";

# Execute the deployment
$output = New-AzureRmResourceGroupDeployment -Name $deploymentName -ResourceGroupName $resourceGroupName -TemplateFile $templateFilePath -TemplateParameterFile $parametersFilePath;

Write-Host "";
Write-Host "Finished Api.";
Write-Host "";

Write-Host "";
Write-Host "Done.";
Write-Host "";
 