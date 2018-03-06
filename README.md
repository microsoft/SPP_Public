
# Getting Started with the Sports Performance Platform

## Overview

We are a virtual Engineering team within Microsoft. Over the past few years, we've been working on a variety of sports projects. Two years ago we started working in a more concerted effort on the Sports Performance Platform (SPP)--a platform that enables professional and amateur teams to collect and analyze sports data in an aggregated way and build predictive analytical reports from this data. 

Recently, at the Hashtag Sports conference we launched the Sports Performance Platform--as a partner-led effort. You can see some of thecoverage from the event in the following articles: 

* https://www.microsoft.com/en-us/garage/blog/2017/06/new-garage-project-brings-predictive-analytics-sports-performance-data/
* https://www.geekwire.com/2017/microsoft-debuts-sports-performance-platform-help-teams-make-data-driven-decisions/
* https://www.engadget.com/2017/06/27/microsoft-sports-performance-platform/
* https://blogs.microsoft.com/blog/2017/06/27/sports-performance-platform-puts-data-play-action-athletes-teams/
* https://mspoweruser.com/microsoft-launches-sports-performance-platform-new-analytics-solution-help-athletes-sports-teams-make-better-decisions/
* https://chyronhego.com/chyronhegos-tracab-delivers-player-tracking-component-microsoft-sports-performance-platform/

As a part of this launch, we mentioned that we would open source a portion of the code-base to the community. The goal of this was tohelp inspire and grow a community of like-minded sports geeks like us to play around in this new space, learn how to use the Sports Performance Platform and build some models that help evolve sports as a whole. 

To help you get started, we've included a Getting Started tutorial, so you can get the code deployed and start playing around with thedata. We will publish future blog-posts, in which we will explore different technical sides of the platform. 

## What is in the Open-Source SPP Repo?

In this GitHub repo, you'll find a core set of resources that you can use to get started using SPP. There are three major folders with some additional files with which you'll want to familiarize yourself. 

The folder structure and contents are as follows: 

* armtemplate
* how-to-videos
* src/api
* templates

In the *armtemplate* folder, you'll find a PowerShell script and associated ARM template files. There is some configuration required for the ARM template, so be sure to read the readme.md file to understand these configuration options. These files enable you to automatically deploy the open-source SPP code into your own Azure subscription. 

In the *how-to-videos*, you'll find some introductory how-to videos that walk you through some of the basics, such as a walkthrough of the GitHub repo, getting started with Power BI, etc. 

In the *src/api* folder, you'll find a set of SPP APIs--these are a sub-set of our broader set of APIs but will get you started developing on SPP. 

In the *templates* folder, you will find sample data, a BACPAC file to restore sample data to an Azure SQL Database. You will also find a Power BI template, which you can use to explore different sports dashboard views. For more information, jump to *Step 3* below. 

## How should you use the Open-Source SPP code? 

If you're not a developer, then you may want to first explore the Power BI template that is located in the *templates* folder. To explore this, you need to have Power BI Desktop installed (or upload the template to a powerbi.com account). This is  by far the quickest and easiest way to get started with the assets we've published. 

If you're more dev- or data-savvy, then you'll want to explore restoring the BACPAC file to an Azure SQL Datanbase or even jumping into coding.  

So, you have three different ways in which you can explore and interact with the open-source elements: 

* Just open and explore PowerBi reports. Low effort and friction to do this, and you can get up and running in a few minutes. 
* Restore a hydrated DB to Azure, and begin to migrate the CSVs and explore the data. Medium effort with some database knowledge, and you can get up and running within an hour of work. You will need to understand databases and data concepts. (Review the steps below in the Getting Started Tutorial.)
* Use the PowerShell script and ARM Template to deploy the code to your Azure subscription (or manually deploy it). Higher effort and requires some technical knowledge to do this. (Review the readme.md in the *armtemplate* folder.) 

We will add additional elements to the open-source code over time, post blog posts and update this repo on a regular basis. 

## Getting Started Tutorial
In this tutorial, we will explore how to fully utilize a range of Microsoft Azure services to standup a database using Azure SQL, create visuals using PowerBI, and perform simple machine learning experiments using Azure Machine Learning. This tutorial will help you get familiar with the Sports Performance Platform (SPP), which is a Microsoft Garage Project. 

The data that we will be using will explore concepts of external load, internal load, and readiness—we refer to this as athlete wellness and readiness. 

We start with the database that SPP uses for its APIs. Our APIs serve multiple purposes—authentication, application development, andanalytics. The database that we restore will give you an opportunity to see what tables we use for our APIs. However, we will be usingexternal tables to showcase our tutorial. For more information about our core tables and APIs, look out for more blog posts and documentation in the future. 

### Pre-requisite Software

#### Step 1 - Set up an Azure Subscription

To use the Sports Performance Platform, you need to first have an Azure subscription. It's easy to create a subscription. You just needto go to the Azure portal and using your Microsoft Account (e.g. youremailname@hotmail.com, youremailname@outlook.com, etc.) and sign up for an account.  You can create a free account by starting from here: https://azure.microsoft.com/en-us/free/. 

The open-source deployment of the Sports Performance Platform requires that you deploy the code into your Azure subscription, so you need to do this first.

You can check out the Azure services and offerings by going to www.microsoft.com/azure. 

#### Step 2 - Install SQL Server Management Studio

With your Azure subscription up and running, you also need to download and install SQL Server Management Studio (SSMS). This is a freetool that allows you to interact directly with your SQL Database. To install SSMS, go here:https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms. Click the Download SQL Server Management Studio 17.01 link, and select Run when prompted. 

#### Step 3 - Install Power BI

Because the Sports Performance Platform relies on Power BI as the reporting front-end, you also need to install Power BI Desktop. To dothis, go here: https://powerbi.microsoft.com/en-us/. Under Products, click Power BI Desktop and then Download. Click Run when prompted. 

Currently, you can only install the desktop version on your Windows machine. However, there is a very rich browser experience, so you can sign in and create reports from powerbi.com from both your Windows and Mac machines. 

For more information, check out the how-to video here: https://github.com/Microsoft/SPP_Public/tree/master/how-to-videos/SPP_Video_Two_Overview_of_SPP_Power_BI_Template. 

#### Step 4 - Restoring Azure SQL Database

While this is for more advanced users, you will at some point want to begin to explore machine learning. You can do this through theAzure Machine Learning (ML) Studio: https://studio.azureml.net/. Click the Sign Up button to get started and follow the instructions. 

### Restoring the Sports Data using a BACPAC File

Using a BACPAC file—which is a SQL database file data-type standard—we can restore a database that already stores the necessary tables and data for SPP. 

Note that you'll need to first set up an Azure storage with a container and then upload the BACPAC file to that storage container. 

Here are the steps to get your database restored:

* Login to the Azure portal and go to your SQL Server. 
* After selecting your server, you will see the option to “Import Database.” 
* Upon clicking “Import Database”, fill out the following information:  
* Subscription – Name of the subscription you are using  
* Storage – Select the storage account that you have put your BACPAC file in. You will need to navigate to the specific BACPAC file in your storage container.    
  * For more information, use a tool like Azure Storage Explorer to upload the BACPAC file from GitHub to blob storage.   
* Pricing Tier – Basic w/ Storage of 100 MBServer
* Admin and password as denoted by your SQL Server
* Click done and it will proceed to restore the database to Azure SQL. 

Check to see if it is complete by connecting to the server with SQL Server Management Studio. Assuming the database was successfully imported and restored, you can then connect directly to the SQL Database from Power BI and begin to create your Performance Analytics dashboards. 


### Building Power BI Reports

Now that we have the 7 tables that we will use in our PowerBI report, we can build the PowerBI report to understand the Seattle Reign’s wellness and readiness. To build the reports from scratch will require extensive knowledge in Power Query—PowerBI’s tool for reading-in data from data stores like Azure SQL databases. 

Note that there is a great Power BI course on edx.org. To find the online course, navigate to www.edx.org and then search for "Power BI."

If you would like to build the PowerBI report from scratch, you can always reverse engineer the PowerBI file that was provided to you in GitHub.

For now, we take the following steps:

* Open “SPP_Template.pbix” once you have PowerBI Desktop installed on your machine. 
* You will click on Edit Queries.
* You will open up the Power Query editor window.

Here, there are few things to understand from this window:

* Click on View to find the Advanced Editor tab
* Advanced Editor tab that we will use in the next step. This contains all the loaded data that we have imported from SQL or built from scratch
* The Query Settings pane contains all the modifications we have made to the base table found within the SQL database
* The main window is a preview of the data. 

In this case, we are looking at Catapult data.  Clicking on Advanced Editor will bring up a textbox editor where you find the raw code behind the steps that are outlined in the Query Settings pane. The first line of the code will say Source = Sql.Database("tpptemplates.database.windows.net","TPPTemplate"). Change the highlighted to the name of your database. Click Done, and the table will refresh against the table that is in your database. 

Note: You may get an error in the columns “TotalDuration” and “Time90HRMax”. If you do, follow these steps to resolve it:

* Click on the “Duplicated Column” step in the Query Settings. 
* Then click on Home -> Data Type -> “Date/Time/Timezone”* Do the same for the column “Time90HRMax.” You can change them both at the same time by selecting both columns at once. By making this change, you will add a step named “Changed Type”* When you scroll down to the last step in Query Settings and click on it, you will see the table updated with 0 errors.  
* Repeat the step of changing the database name for the 7 tables you see under the Queries pane. 
* Leave “Query1” and “Calendar” alone as those are tables built from scratch using Power Query. Our blog post won’t cover the creation of these tables, but feel free to reverse engineer it and learn more about Power Query functions. You will now have a PowerBI file thatis customized for the tables that are from your database.  

### Unpacking a PowerBI Visual

Now that you have your customized PowerBI file outfitted with tables from your database, the last thing to learn is how to reverse engineer a visual. Let’s take the visual below that we find from the Training page of our report:

We find some core elements to this visual. It contains data points from one of our tables.

The first thing we point our attention to is the Visualization pane. This lets us choose which visualizationwe want to use to represent the data. Second, we select the specific metrics wewant from the Fields pane and we drag and drop it to each of the meta-data about the visual.

In our case, Details == Player, Legend == Position, and the X/Y axes are governed by Max Velocity and Total Distance.  It contains color formatting for each data point as well as a data label. Forwards are labeled as red, full backs are yellow, and so on. This is defined by the Formatting pane, shown by the red box in the image below. Here, you can set any formatting guidelines. The downward triangleslet you expand upon the formatting feature to dive deeper into the specific format. In our case, we would click on “Data colors” to change the colors of each position. Another example is “Category la..”, short for “Category Labels”, is how we get the player name to show up underneath the scatter plot data point. 

It contains 2 dashed, light-blue lines. Here, our red boxsignifies the Advanced Analytics pane. This is only available for certainvisuals, so you will have to reverse engineer other visuals in this report tolearn which visuals support this functionality. In our case, it lets us set benchmarks that allow us to divide the scatter plot into quadrants. 

If you check for these elements as you review the PowerBI file, you will get a sense of how to start creating your own. The 2nd page of the report—Explanation of Reports—outlines the motivation behind each page. Understanding how to read a visual for its components and formatting will bring you further along the journey of building your own robust reports. 

## Conclusion

We hope that this readme serves as a way to get started with understanding how to import data into cloud storage and use the data with PowerBI to build reports and find visual signals. The Sports Performance Platform, incubated from the Microsoft Garage, is the first step in building a scalable solution to your sports analytics. To learn more, please visit this survey. 
 
 
 
 
