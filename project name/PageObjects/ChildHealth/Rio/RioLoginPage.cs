using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using NSS_ChildHealth.Common;
using NSS_ChildHealth.Common.Helpers;
using NSS_ChildHealth.TestData.ChildHealth;


namespace NSS_ChildHealth.PageObjects.ChildHealth.Rio
{
    class RioLoginPage : BasePage
    {
        #region Page Objects
        By envSelection = By.XPath("//li[contains(.,'TEST')]");
        By selectRoleDropdown = By.Id("selectedRole");
        By roleSelectButton = By.ClassName("BtEnabled");
        By usernameTextBox = By.Id("username");
        By passwordTextBox = By.Id("password");
        By logInButton = By.Id("Submit");

        By loggedInAlreadyText = By.XPath("//strong[contains(.,'You are already logged into the Rio system elsewhere')]");

        #endregion

        DropDownLists DropDownList;
        Tables Table;
        RadioButtons RadioButton;

        

        public RioLoginPage(IWebDriver driver) : base(driver)
        {
            DropDownList = new DropDownLists(driver);
            Table = new Tables(driver);
            RadioButton = new RadioButtons(driver);
        }

        public void GoToSite()
        {
            Visit("https://test.scphws.scot.nhs.uk/rio/default.asp?");
            IsDisplayed(envSelection, 10);
            Click(envSelection);            
        }  
        
        public void EnterUsername(string username)
        {
            Type(usernameTextBox, username);
        }

        public void EnterPassword(string password)
        {           
            Type(passwordTextBox, password);
        }

        public void ClickLogInButton(string password)
        {
            Click(logInButton);
            if (IsDisplayed(loggedInAlreadyText, 5))
            {
                EnterPassword(password);
                Click(logInButton);
            }             
        }

        public void ValidLogin(string username, string password, string role)
        {            
            GoToSite();
            EnterUsername(username);
            EnterPassword(password);
            ClickLogInButton(password);
            SelectRole(role);
        }

        public void SelectRole(string roleText)
        {
            IsDisplayed(selectRoleDropdown, 10);            
            DropDownList.SelectFromDropDown(selectRoleDropdown, roleText);
            Wait(1000);
            ClickWhenClickable(roleSelectButton, 10);
        }

        public string GetPageTitle()
        {
            return getPageTitle();
        }


    }
}
