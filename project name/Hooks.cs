using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.IO;
using System.Configuration;
using System.Reflection;
using NUnit.Framework;

namespace NSS_ChildHealth
{
    [Binding]
    public class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private IWebDriver Driver;
        //public static string BrowserName;
        //public static string Url;
        private static string reportPath;
        public static string workingDirectory;
        public static string browserName;
        private static ExtentTest featureName;
        private static string[] arrayFeatureTags;
        private static ExtentTest scenario;
        public static ExtentReports extent;

        public ScenarioContext _scenarioContext;
        public static FeatureContext _featureContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        [BeforeTestRun]
        public static void IntialiseReport()
        {
            WebDriverSupport.LoadConfigValues();
            // Get Path of Solution Directory and set Test Report destination Folder

            browserName = TestContext.Parameters.Get("Browser", "chrome");

            string filenameTimeStampPart = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string projectName = Assembly.GetExecutingAssembly().GetName().Name;
            workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            workingDirectory = workingDirectory.Replace("\\" + projectName + "\\bin\\Debug\\", "");
            reportPath = workingDirectory + "\\TestResults\\Run_" + browserName.ToUpper() + "_" + filenameTimeStampPart + "\\";
            // start reporter
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.Theme = Theme.Dark;
            // create ExtentReports and attach reporter
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }       

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);            

                arrayFeatureTags = _featureContext.FeatureInfo.Tags;
                if (arrayFeatureTags.Length == 0)
                {
                    List<string> arrNoTag = new List<string>();
                    arrNoTag.Add("Tag-lessFeatureTests");
                    arrayFeatureTags = arrNoTag.ToArray();
                }         
        }

        [Before]
        public void BeforeScenario()
        {
            //LoadConfigValues();            
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title).AssignCategory(arrayFeatureTags);
        }

        [AfterStep]
        public void InsertReportingSteps(IWebDriver driver)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            string stepText = ScenarioStepContext.Current.StepInfo.Text;
            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepText);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepText);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepText);
            }
            else
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepText).Fail(_scenarioContext.TestError.InnerException, 
                        MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot(driver, stepText)).Build());
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepText).Fail(_scenarioContext.TestError.InnerException, 
                        MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot(driver, stepText)).Build());
                else if (stepType == "Then")                    
                    scenario.CreateNode<Then>(stepText).Fail(_scenarioContext.TestError.Message, 
                        MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot(driver, stepText)).Build());
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            // add Environment info
            string os = Environment.OSVersion.ToString();
            extent.AddSystemInfo("OS", os);
            extent.AddSystemInfo("Browser", browserName.ToUpper());
            string machineName = Environment.MachineName;
            extent.AddSystemInfo("Machine Name", machineName);
            string userName = Environment.UserName;
            extent.AddSystemInfo("Run by User", userName);
            extent.Flush();
        }

        public string CaptureScreenshot(IWebDriver driver, string step)
        {
            Driver = driver;
            string filenameStepPart = step.Replace(" ", "_");
            filenameStepPart = filenameStepPart.Replace("'", "");
            string filenameTimeStampPart = DateTime.Now.ToString("HH-mm-ss");
            string screenshotFilename = filenameStepPart + "_" + filenameTimeStampPart + ".jpg";
            Screenshot SS = ((ITakesScreenshot)Driver).GetScreenshot();
            SS.SaveAsFile(reportPath + screenshotFilename, ScreenshotImageFormat.Jpeg);
            return screenshotFilename;
        }

    }
}
