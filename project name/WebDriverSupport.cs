using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace NSS_ChildHealth
{
    [Binding]
    public class WebDriverSupport
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private readonly IObjectContainer objectContainer;
        private IWebDriver webDriver;
        public static string Url;

        public WebDriverSupport(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        public static void LoadConfigValues()
        {            
            var configReader = new AppSettingsReader();
            //BrowserName = (string)configReader.GetValue("BrowserName", typeof(string));
            Url = (string)configReader.GetValue("Url", typeof(string));
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            switch (Hooks.browserName)
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("start-maximized");
                    webDriver = new ChromeDriver(chromeOptions);
                    break;

                case "chromeheadless":
                    ChromeOptions chromeHLOptions = new ChromeOptions();
                    chromeHLOptions.AddArguments("headless");
                    webDriver = new ChromeDriver(chromeHLOptions);
                    break;

                case "firefox":
                    webDriver = new FirefoxDriver();
                    webDriver.Manage().Window.Maximize();
                    break;

                case "ie":
                    //All Zones must have same'Enable Protected Mode' check/unchecked (Internet Options >> Security) 
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions
                    {
                        IgnoreZoomLevel = true,
                        EnableNativeEvents = false
                    };
                    //ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;                 
                    webDriver = new InternetExplorerDriver(ieOptions);
                    //webDriver = new InternetExplorerDriver(Hooks1.workingDirectory, ieOptions);
                    webDriver.Manage().Window.Maximize();
                    break;

                case "edge":                    
                    webDriver = new EdgeDriver();
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    break;
            }
            objectContainer.RegisterInstanceAs<IWebDriver>(webDriver);
        }

        [AfterScenario]
        public void AfterScenario()
        {            
            webDriver.Quit();
        }
    }
}
