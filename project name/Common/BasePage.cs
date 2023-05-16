
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NSS_ChildHealth.Common
{
    //Base page contains all the reusable methods for all pages
    public class BasePage
    {
        IWebDriver Driver;

        //public DropDownLists DropDownList { get; private set; }
        //public Tables Table { get; private set; }
        //public RadioButtons RadioButton { get; private set; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
        //Navigate to a URL
        protected void Visit(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
        //Find web element on page
        protected IWebElement Find(By locator)
        {
            return Driver.FindElement(locator);
        }
        //Finds an element based on text content
        protected bool FindByText(string text)
        {
            return Driver.PageSource.Contains(text);
        }
        //Find Elements by their locator i.e. tag name
        protected ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            ReadOnlyCollection<IWebElement> elList = Driver.FindElements(locator);
            return elList;
        }

        protected ReadOnlyCollection<IWebElement> FindElementsInAnElement(IWebElement webElement, By locator)
        {
            ReadOnlyCollection<IWebElement> elList = webElement.FindElements(locator);
            return elList;
        }
        //Hover over a menu item then click that item
        protected void HoverOver(By locator, By locator2)
        {
            IWebElement element = Find(locator);
            IWebElement element2 = Find(locator2);
            Actions action = new Actions(Driver);
            action.MoveToElement(element).MoveToElement(element2).Click().Build().Perform();
        }
        //Move to a particular element and click on it
        protected void MoveToAndClick(By locator)
        {
            IWebElement element = Find(locator);
            Actions action = new Actions(Driver);
            action.MoveToElement(element).Click().Build().Perform();
        }
        //Sends PageDown command
        protected void PageDown(By locator)
        {
            Find(locator).SendKeys(Keys.PageDown);
        }
        //Click on a web element
        protected void Click(By locator)
        {
            Find(locator).Click();
        }
        //Sends the Enter key command
        protected void PressEnter(By Locator)
        {
            Find(Locator).SendKeys(Keys.Enter);
        }
        //Sends the Tab key command
        protected void PressTab(By Locator)
        {
            Find(Locator).SendKeys(Keys.Tab);
        }
        //Sends the arrow down key command
        protected void PressArrowDown(By Locator)
        {
            Find(Locator).SendKeys(Keys.ArrowDown);
        }
        //Input text into a field
        protected void Type(By locator, string inputText)
        {
            Find(locator).SendKeys(inputText);
        }
        //Clear a field 
        protected void ClearField(By locator)
        {
            Find(locator).Clear();
        }
        ////Clear a field and type text into it
        //protected void ClearFieldAndType(By locator, string inputText)
        //{
        //    Find(locator).SendKeys(Keys.Control + "a");
        //    Find(locator).SendKeys(Keys.Delete);
        //    Find(locator).SendKeys(inputText);
        //}
        //Accept a browser alert window
        protected void AcceptAlert()
        {
            Driver.SwitchTo().Alert().Accept();
        }
       
        //Compares to strings and returns if they are equal
        protected bool IsEqualTo(string inputText1, string inputText2)
        {
            return (Equals(inputText1, inputText2));
        }
        //Wait for an element to be displayed
        protected bool IsDisplayed(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(maxWaitTime));
                //wait.Until(ExpectedConditions.ElementIsVisible(locator));
                wait.Until(drv => drv.FindElement(locator).Displayed);
                return true;
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                return false;
            }
        }
        //Webdriver wait to pause test
        protected void Wait(int maxWaitTime)
        {
            Task.Delay(maxWaitTime).Wait();
        }
        //Returns the text value of an element
        protected string ElementText(By locator)
        {
            return Find(locator).Text;
        }
        protected string ElementText(IWebElement element)
        {
            return element.Text;
        }
        //Checks if element class contains specific text
        protected bool elementHasClass(IWebElement element, String text)
        {
            return element.GetAttribute("class").Contains(text);
        }

        #region Other Generic Methods



        //Move to a particular element
        protected void MoveToElement(By locator)
        {
            IWebElement element = Find(locator);
            Actions action = new Actions(Driver);
            action.MoveToElement(element).Build().Perform();
        }

        //Move to a particular element and click on it using SendKeys. Helped with Chrome issue where it won't select a button properly
        protected void MoveToAndSendKeysEnter(By locator)
        {
            IWebElement element = Find(locator);
            Actions action = new Actions(Driver);
            action.MoveToElement(element).SendKeys(Keys.Enter).Build().Perform();
        }
        //Click on the object using Javascript (Helped with Chrome issue not selecting button properly)
        protected void ClickObjectUsingJavascript(By locator)
        {
            IWebElement element = Find(locator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("arguments[0].click()", element);
        }
        //Wait for an element to be clickable
        protected void IsClickable(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
                    FromSeconds(maxWaitTime));
                //wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                wait.Until(drv => drv.FindElement(locator).Displayed);
                wait.Until(drv => drv.FindElement(locator).Enabled);
            }
            catch (WebDriverTimeoutException)
            {
                
            }
        }

        //Wait for an element with text to be displayed
        protected bool ElementWithTextIsDisplayed(By locator, string elementText, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(maxWaitTime));
                wait.Until(drv => drv.FindElement(locator).Text.Equals(elementText));
                return true;
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                return false;
            }
        }

        //Wait for an element containing text to be displayed
        protected bool ElementContainingTextIsDisplayed(By locator, string elementText, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTime));
                wait.Until(drv => drv.FindElement(locator).Text.Contains(elementText));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        //Wait for an element to contain any text to be displayed
        protected bool ElementTextIsNotEmptyIsDisplayed(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTime));
                wait.Until(drv => drv.FindElement(locator).Text.Trim().Length > 0);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected void ClickWhenClickable(By locator, int maxWaitTime)
        {
            IsClickable(locator, maxWaitTime);
            Click(locator);
        }

        protected void TypeWhenClickable(By locator, string text, int maxWaitTime)
        {
            IsClickable(locator, maxWaitTime);
            Type(locator, text);
        }

        //Click on a web element
        protected void ClickUsingSpace(By locator)
        {
            Find(locator).SendKeys(Keys.Space);
        }


        //Click on Text
        protected void ClickOnText(string textToClick)
        {
            By ElementWithText = By.XPath("//*[contains(text(), '" + textToClick + "')]");
            Click(ElementWithText);
        }

        //Clear a field and type text into it
        protected void ClearFieldAndType(By locator, string inputText)
        {
            do
            {
                Find(locator).SendKeys(Keys.Control + "a");
                Find(locator).SendKeys(Keys.Delete);
                Find(locator).SendKeys(inputText);
            } while (Find(locator).GetAttribute("value").Equals(inputText) == false);

        }
        //Clear a field and type text into it
        protected void ClearFieldAndTypeDateString(By locator, string inputText)
        {
            do
            {
                Find(locator).SendKeys(Keys.Control + "a");
                Find(locator).SendKeys(Keys.Delete);
                Find(locator).SendKeys(inputText);
            } while (Find(locator).GetAttribute("value").Replace("/", "").Equals(inputText) == false);

        }

        ////Wait for an element to be displayed
        //protected bool IsDisplayed(By locator, int maxWaitTime)
        //{
        //    try
        //    {
        //        WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
        //            FromSeconds(maxWaitTime));
        //        wait.Until(drv => drv.FindElement(locator).Displayed);
        //        return true;
        //    }
        //    catch (WebDriverTimeoutException)
        //    {
        //        return false;
        //    }
        //}
        //Is Element enabled
        protected bool IsEnabled(By locator)
        {
            return Find(locator).Enabled;
        }


        //Checks if element class contains specific text
        protected bool ElementHasClass(IWebElement element, String text)
        {
            return element.GetAttribute("class").Contains(text);
        }
        //Gets Page Title
        protected string getPageTitle()
        {
            return Driver.Title.ToString();
        }
        //Wait for page title to be displayed
        protected bool TitleIsDisplayed(string title, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
                    FromSeconds(maxWaitTime));
                //wait.Until(ExpectedConditions.TitleIs(title));
                wait.Until(drv => drv.Title.Equals(title));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        //Wait for page title containing text to be displayed
        protected bool TitleContainingTextIsDisplayed(string titleText, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
                    FromSeconds(maxWaitTime));
                //wait.Until(ExpectedConditions.TitleContains(titleText));
                wait.Until(drv => drv.Title.Contains(titleText));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        protected string TextThatDoesNotMatchElementText(By Locator, String TextToMatch)
        {
            if (Find(Locator).Text.Equals(TextToMatch))
            {
                return "";
            }
            else
            {
                return Find(Locator).Text;
            }
        }

        public int RandomIntegerGenerate(int intFrom, int intTo)
        {
            Random rnd = new Random();
            return rnd.Next(intFrom, intTo);
        }

        //#################################################### Text ##############################################################

        //Determines whether text is visible on page or not 
        protected bool FindByVisibleText(string text)
        {
            string bodyText = Driver.FindElement(By.TagName("body")).Text;
            return bodyText.Contains(text);
        }
        //Returns string of text that does not exist on the page. Input is taken from Array parameter
        protected string ReturnTextThatisNotDisplayed(string[] stringArray)
        {
            List<string> notMatched = new List<string>();
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (FindByVisibleText(stringArray[x]) == false)
                {
                    notMatched.Add(stringArray[x]);
                }
            }
            string allNotMatched = "";
            if (notMatched.Count > 0)
            {
                for (int c = 0; c < notMatched.Count; c++)
                {
                    allNotMatched = allNotMatched + ", " + notMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allNotMatched.TrimStart(arr);
            }
            else
            {
                return allNotMatched;
            }
        }
        //Returns string of text that does exist on the page. Input is taken from Array parameter
        protected string ReturnTextThatisDisplayed(string[] stringArray)
        {
            List<string> arrMatched = new List<string>();
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (FindByVisibleText(stringArray[x]) == true)
                {
                    arrMatched.Add(stringArray[x]);
                }
            }
            string allMatched = "";
            if (arrMatched.Count > 0)
            {
                for (int c = 0; c < arrMatched.Count; c++)
                {
                    allMatched = allMatched + ", " + arrMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allMatched.TrimStart(arr);
            }
            else
            {
                return allMatched;
            }
        }

        protected string ReturnTextThatisNotDisplayedInAnElement(string[] stringArray, By locator)
        {            
            List<string> notMatched = new List<string>();
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (ElementText(locator).Contains(stringArray[x]) == false)
                {
                    notMatched.Add(stringArray[x]);
                }
            }
            string allNotMatched = "";
            if (notMatched.Count > 0)
            {
                for (int c = 0; c < notMatched.Count; c++)
                {
                    allNotMatched = allNotMatched + ", " + notMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allNotMatched.TrimStart(arr);
            }
            else
            {
                return allNotMatched;
            }
        }

        protected string ReturnTextThatisNotDisplayedInListOfElements(string[] stringArray, By locator)
        {
            ReadOnlyCollection<IWebElement> elList = FindElements(locator);

            List<string> elementsTextList = new List<string>();
            for (int i = 0; i < elList.Count; i++)
            {
                elementsTextList.Add(ElementText(elList[i]));
            }

            List<string> notMatched = new List<string>();
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (elementsTextList.Contains(stringArray[x]) == false)
                {
                    notMatched.Add(stringArray[x]);
                }
            }
            string allNotMatched = "";
            if (notMatched.Count > 0)
            {
                for (int c = 0; c < notMatched.Count; c++)
                {
                    allNotMatched = allNotMatched + ", " + notMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allNotMatched.TrimStart(arr);
            }
            else
            {
                return allNotMatched;
            }
        }

        protected string ReturnTextThatisNotDisplayedInListOfElements(string text, By locator)
        {
            ReadOnlyCollection<IWebElement> elList = FindElements(locator);

            List<string> elementsTextList = new List<string>();
            for (int i = 0; i < elList.Count; i++)
            {
                elementsTextList.Add(ElementText(elList[i]));
            }

            List<string> notMatched = new List<string>();
                if (elementsTextList.Contains(text) == false)
                {
                    notMatched.Add(text);
                }
            string allNotMatched = "";
            if (notMatched.Count > 0)
            {
                for (int c = 0; c < notMatched.Count; c++)
                {
                    allNotMatched = allNotMatched + ", " + notMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allNotMatched.TrimStart(arr);
            }
            else
            {
                return allNotMatched;
            }
        }

        protected string ReturnTextThatisNotDisplayedInListOfElements(string[] stringArray, By locator, IWebElement webElement)
        {
            ReadOnlyCollection<IWebElement> elList = FindElementsInAnElement(webElement, locator);

            List<string> elementsTextList = new List<string>();
            for (int i = 0; i < elList.Count; i++)
            {
                elementsTextList.Add(ElementText(elList[i]));
            }

            List<string> notMatched = new List<string>();
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (elementsTextList.Contains(stringArray[x]) == false)
                {
                    notMatched.Add(stringArray[x]);
                }
            }
            string allNotMatched = "";
            if (notMatched.Count > 0)
            {
                for (int c = 0; c < notMatched.Count; c++)
                {
                    allNotMatched = allNotMatched + ", " + notMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allNotMatched.TrimStart(arr);
            }
            else
            {
                return allNotMatched;
            }
        }

        protected string ReturnTextThatisDisplayedInListOfElements(string[] stringArray, By locator)
        {
            ReadOnlyCollection<IWebElement> elList = FindElements(locator);

            List<string> elementsTextList = new List<string>();
            for (int i = 0; i < elList.Count; i++)
            {
                elementsTextList.Add(ElementText(elList[i]));
            }

            List<string> arrMatched = new List<string>();
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (elementsTextList.Contains(stringArray[x]) == true)
                {
                    arrMatched.Add(stringArray[x]);
                }
            }
            string allMatched = "";
            if (arrMatched.Count > 0)
            {
                for (int c = 0; c < arrMatched.Count; c++)
                {
                    allMatched = allMatched + ", " + arrMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allMatched.TrimStart(arr);
            }
            else
            {
                return allMatched;
            }
        }

        protected string ReturnArrayTextThatisNotInAnotherArray(string[] arrayToBeVerified, string[] arrayToCompare)
        {
            List<string> notMatched = new List<string>();
            for (int x = 0; x < arrayToCompare.Length; x++)
            {
                if (arrayToBeVerified.Contains(arrayToCompare[x]) == false)
                {
                    notMatched.Add(arrayToCompare[x]);
                }
            }
            string allNotMatched = "";
            if (notMatched.Count > 0)
            {
                for (int c = 0; c < notMatched.Count; c++)
                {
                    allNotMatched = allNotMatched + ", " + notMatched[c];
                }
                char[] arr = new char[] { ',' };
                return allNotMatched.TrimStart(arr);
            }
            else
            {
                return allNotMatched;
            }
        }

        //#################################################### Text ##############################################################
        //#################################################### Dates #############################################################

        //Returns tomorrows date as a string with time portion stripped out
        protected string GetAddDaysToTodaysDate(double numberOfDays)
        {
            return DateTime.Today.AddDays(numberOfDays).ToString().Remove(10);
        }
        //################################################ Dates #########################################################
        //#################################################### URL #############################################################

        //Return the FNP URL from AppSettings
        protected string GetURLFromAppSettingsReader()
        {
            var configReader = new AppSettingsReader();
            string FNPUrl = (string)configReader.GetValue("FNPUrl", typeof(string));
            return FNPUrl;
        }

        protected string GetCurrentUrl()
        {
            return Driver.Url;
        }

        protected string ReturnNothing()
        {
            return "";
        }

        //public void Page()
        //{
        //    DropDownList = new DropDownLists(Driver);
        //    Table = new Tables(Driver);
        //    RadioButton = new RadioButtons(Driver);
        //}


        //#################################################### URL #############################################################
        //#################################################### Frames #############################################################

        protected void SwitchToFrame(string frameNameOrId)
        {
            Driver.SwitchTo().Frame(frameNameOrId);
        }

        protected void SwitchToFrame(int frameIndex)
        {
            Driver.SwitchTo().Frame(frameIndex);
        }

        protected void SwitchToFrame(IWebElement frameElement)
        {
            Driver.SwitchTo().Frame(frameElement);
        }

        protected void SwitchToDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
        }

        protected void SwitchToParentFrame()
        {
            Driver.SwitchTo().ParentFrame();
        }

        //#################################################### Frames #############################################################

        //################################################### Windows #############################################################

        protected void SwitchToLastOpenedWindow()
        {
            string newWindowHandle = Driver.WindowHandles.Last();
            Driver.SwitchTo().Window(newWindowHandle);
        }

        protected int GetNumberOfWindows()
        {
            return Driver.WindowHandles.Count();
        }

        //################################################### Windows #############################################################


        #endregion
    }
}




