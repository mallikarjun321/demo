# NSS_ChildHealth
Automation Framework in C# using SpecFlow and Extent Report

## Install Visual Studio Community Edition
You can find the free Community Edition of Visual Studio here: https://visualstudio.microsoft.com/downloads/.

Kicking off the download will download Visual Studio Installer. From there you can install Visual Studio

During installation you will be asked if you want to install any Workloads. You only have to select '.NET desktop development' which is under 'Desktop & Mobile'.

Note: Your computer will have to be rebooted before starting Visual Studio.

## Add Extensions to Visual Studio
In Visual Studio, from the top menu bar, select Extensions > Manage Extensions.
The 'Manage Extensions' window should be displayed. 
On left menu select 'Online' and search for and install:
* GitHub Extension for Visual Studio (Not required if using Azure DevOps as a repo)
* SpecFlow for Visual Studio

Note: You will have to restart Visual Studio.

## Pull the Framework from a Remote Repository
Open PowerShell in the directory you want the Framework solution to be pulled to or open a command prompt and change directory.
Type the git command 'git clone' and then append on the url of the Framework's repository, e.g. 'git clone https://github.com/jemerson-dxc/SpecFlowExtent.git'

When the command is executed the Framework Solution will be pulled from the remote site and once finished will be available on your machine.

## Install Required Nuget Packages
Start Visual Studio and open the Framework Solution in the directory you cloned it to. The name of the solution is SpecFlowUnitTest4.6.1.sln.

You may see a lot of errors in the code that are shown as certain words being underlined. Right-click the solution name in the 'Solution Explorer' and select 'Build Solution' from the menu. This should result in a successful build and the removal of all errors.

Navigate to 'Manage Packages for Solution' via 'Tools > Nuget Package Manager > Manage Nuget Packages for Solution'.

The Nuget packages should be listed in the 'Installed' section and above that a banner with the following message: 'Some Nuget packages are missing from the solution. Click to restore from your online packages source.'

Select the 'Restore' button next to the message and the Nuget packages will be downloaded.

Restart Visual Studio.

## Browsers
The following Browsers can be controlled by the Framework:
* Chrome
* Headless Chrome
* Firefox
* IE11
* Edge

In order for the framework to create sessions for the above Browsers, it has to be able to run their driver executables.
Information on what to download can be found in the following locations:
* https://sites.google.com/a/chromium.org/chromedriver/ (Chrome & Headless Chrome)
* https://firefox-source-docs.mozilla.org/testing/geckodriver/Support.html (Firefox)
* https://github.com/SeleniumHQ/selenium/wiki/InternetExplorerDriver#required-configuration (IE)
* https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/#downloads (Edge)

Once downloaded, create a folder off of C: and give it a meaningful name, e.g. 'BrowserDrivers' and place the downloaded drivers in to it.
Add the path of the folder (e.g. C:/BrowserDrivers) as a System Variable by editing the 'Path' variable in 'Environment Variables'.

If you download the Edge driver, you must rename it to 'MicrosoftWebDriver'

When added, you can test they are accessible by opening a command prompt and attempt to run a driver by entering the driver's filename, e.g. 'chromedriver'. You should see a message about the driver starting or it has started and some other information such as 'Only local connects are allowed'. Select ctrl + c to stop the driver running.
 
Adding the folder containing the Browser drivers to the 'Path' Environment Variable means that the Framework doesn't have to know where the driver is stored to run it as the OS will do that for it.
