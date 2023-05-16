using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using NSS_ChildHealth.Common;
using NSS_ChildHealth.Common.Helpers;


namespace NSS_ChildHealth.PageObjects.ChildHealth.Rio
{
    class RioHomePage : BasePage
    {
        string frameName = "MainScreen";
        #region Page Objects
        By tltleHeader = By.Id("HeaderTitle");
        By secQuestionsDialogTextArea = By.Id("modalDialogDiv");
        By secQuestionDialogNoButton = By.XPath("//span[@class='ui-button-text'][contains(.,'No')]");
        By secQuestionDialogYesButton = By.XPath("//span[@class='ui-button-text'][contains(.,'Yes')]");    
        By configureSecurityQuestionsTitle = By.XPath("//h1[contains(.,'Configure Security Questions')]");
        By menuButton = By.Id("mainMenuOpenButton");
        By clientDetailsMainMenuOption = By.XPath("(//div[contains(.,'Client Details')])[3]");
        By reverseFunctionsMainMenuOption = By.XPath("(//div[contains(.,'Reverse Functions')])[3]");
        By reportsMainMenuOption = By.XPath("(//div[contains(.,'Reports')])[5]");
        By demographicDetailsSubMenuOption = By.XPath("(//li[@title='Demographic Details'])[2]");
        By registerDeathSubMenuOption = By.XPath("(//li[@title='Register Death'])[2]");
        By reverseDeathSubMenuOption = By.XPath("(//li[@title='Reverse Death'])[2]");
        By newBirthRegistrationsReportsSubMenuOption = By.XPath("(//li[@title='New Birth Registrations'])[2]");
        By searchMainMenuOption = By.Id("liMenu1");
        By searchTextArea = By.XPath("//*[@id='currentMenu']/input");
        By noSummaryDialog = By.XPath("(//div[contains(.,'No summary tabs found')])[3]");
        By NoSummaryDialogOkButton = By.ClassName("ui-button-text");
        By newBirthRegistrationsTable = By.CssSelector("table[data-maximise-report-id = '18049']");
        By newBirthRegistrationTableNameHrefIdentifier = By.CssSelector("a[href*='DemBirthDetails.asp']");
        By newBirthRegistrationsTab = By.CssSelector("a[title='New Birth Registrations Details & Movers In/Out Allocation']");
        By newbornHearingExaminationResultsTab = By.CssSelector("a[title='Newborn Hearing Examination Results']");
        By childrenOverdueNewbornHearingExaminationResultsTable = By.CssSelector("table[data-maximise-report-id = '18045']");
        By childrenOverdueNewbornHearingExaminationResultsPanel = By.CssSelector("div[data-json-panel-properties*='18045']");
        By expandTableIcon = By.CssSelector("span[class='ImageMaximise case-record-panel-icon']");
        By printButton = By.XPath("//span[@class='ui-button-text'][contains(.,'Print')]");

        By tab1 = By.Id("6194d639-17b6-4ba7-adcd-951823e85d66_tab");
        By tab2 = By.Id("685f925e-7d20-4915-9cf5-534db8ee35f76194d639-17b6-4ba7-adcd-951823e85d66");

        By birthDetailsSearchOption = By.XPath("(//div[@class='menuitemText'][contains(.,'Birth Details')])[2]");

        #endregion

        DropDownLists dropDownList;
        Tables table;
        RadioButtons radioButton;

        public RioHomePage(IWebDriver driver) : base(driver)
        {
            dropDownList = new DropDownLists(driver);
            table = new Tables(driver);
            radioButton = new RadioButtons(driver);
        }


        public string GetSecurityQuestionsDialogText()
        {
            IsDisplayed(secQuestionsDialogTextArea, 5);
            return ElementText(secQuestionsDialogTextArea);
        }

        public void ClickYesOnSecurityQuestionsDialog()
        {
            Click(secQuestionDialogYesButton);
        }

        public void ClickNoOnSecurityQuestionsDialog()
        {
            //Dialog was removed on build received on 10/09/2020
            //Click(secQuestionDialogNoButton);
        }

        public bool IsConfigureSecurityQuestionsTitleDispalyed()
        {
            return IsDisplayed(configureSecurityQuestionsTitle, 10);
        }

        public void SelectClientDetailsFromMainMenu()
        {
            Wait(1000);
            ClickWhenClickable(menuButton, 10);
            ClickWhenClickable(clientDetailsMainMenuOption, 10);
        }

        public void SelectSearchFromMainMenu()
        {
            Wait(1000);
            ClickWhenClickable(menuButton, 10);
            ClickWhenClickable(searchMainMenuOption, 10);
        }

        public void SelectReverseFunctionsFromMainMenu()
        {
            Wait(1000);
            ClickWhenClickable(menuButton, 10);
            ClickWhenClickable(reverseFunctionsMainMenuOption, 10);
        }

        public void SelectReportsMainMenuOption()
        {
            Wait(1000);
            ClickWhenClickable(menuButton, 10);
            //MoveToElement(reportsMainMenuOption);
            ClickWhenClickable(reportsMainMenuOption, 10);
        }

        public void SelectDemographicDetailsFromClientDetailsSubMenu()
        {
            ClickWhenClickable(demographicDetailsSubMenuOption, 10);
        }

        public void SelectReverseDeathFromReverseFunctionsSubMenu()
        {
            ClickWhenClickable(reverseDeathSubMenuOption, 10);
        }

        public void SelectRegisterDeathFromClientDetailsSubMenu()
        {
            ClickWhenClickable(registerDeathSubMenuOption, 10);
        }
        
        public void SelectNewBirthRegistrationsFromReportsSubMenu(int priorWindowCount)
        {
            ClickWhenClickable(newBirthRegistrationsReportsSubMenuOption, 10);
            int windowsCount;
            int count = 0;
            do
            {
                Wait(1000);
                windowsCount = GetNumberOfWindows();
                count++;
            } while ((windowsCount == priorWindowCount) | (count == 10));
        }

        public void NavigateToDemographicsDetailsSearchViaMenus()
        {
            SelectClientDetailsFromMainMenu();
            SelectDemographicDetailsFromClientDetailsSubMenu();
        }

        public void NavigateToRegisterDeathSearchViaMenu()
        {
            SelectClientDetailsFromMainMenu();
            SelectRegisterDeathFromClientDetailsSubMenu();
        }

        public void NavigateToReverseDeathViaMenus()
        {
            SelectReverseFunctionsFromMainMenu();
            SelectReverseDeathFromReverseFunctionsSubMenu();
        }

        public void NavigateToNewBirthRegistrations()
        {
            SelectReportsMainMenuOption();
            int priorWindowCount = GetNumberOfWindows();
            SelectNewBirthRegistrationsFromReportsSubMenu(priorWindowCount);
        }

        public void EnterTextInSearch(string text)
        {
            TypeWhenClickable(searchTextArea, text, 5);
        }

        public void SelectFromSearchResults(string option)
        {
            By locator = By.XPath("(//div[@class='menuitemText'][contains(.,'" + option + "')])[2]");
            Click(locator);
        }

        public string SelectFirstClientInNewBirthRegistrationsTableAndReturnName()
        {
            Wait(1000);
            ElementContainingTextIsDisplayed(tltleHeader, "User Portal", 10);
            SwitchToFrame(frameName);
            IsDisplayed(newBirthRegistrationsTable, 10);
            string nameLinkText = table.GetTextFromTableCell(newBirthRegistrationsTable, 2, 2);
            By nameLink = By.XPath("(//b[contains(.,'" + nameLinkText + "')])[1]");
            Click(nameLink);
            SwitchToDefaultContent();
            return nameLinkText.Substring(0, nameLinkText.IndexOf(','));
        }

        public void SelectClientByIdentifyingChiInNewBirthRegistrationsTable(string chiNumber)
        {
            Wait(1000);
            ElementContainingTextIsDisplayed(tltleHeader, "User Portal", 10);
            SwitchToFrame(frameName);
            IsDisplayed(newBirthRegistrationsTable, 10);
            int clientRow = table.SearchForStringInTableColumnAndReturnRowNumber(newBirthRegistrationsTable, 1, chiNumber);
            table.ClickElementInTableCell(newBirthRegistrationsTable, newBirthRegistrationTableNameHrefIdentifier, clientRow, 2);
            SwitchToDefaultContent();
        }

        public bool IsChiListedInChildrenOverdueNewbornHearingExaminationResultsTable(string chi)
        {
            Wait(1000);
            ElementContainingTextIsDisplayed(tltleHeader, "User Portal", 10);
            SwitchToFrame(frameName);
            IsDisplayed(childrenOverdueNewbornHearingExaminationResultsTable, 10);
            int headerColumn = table.SearchForStringInTableHeaderfAndReturnColumnNumber(childrenOverdueNewbornHearingExaminationResultsTable, "CHI");
            int row = table.SearchForStringInTableColumnAndReturnRowNumber(childrenOverdueNewbornHearingExaminationResultsTable, headerColumn, chi);
            SwitchToDefaultContent();
            if (row == 0)
                return false;
            else
                return true;
        }

        public void SelectNewBirthRegistrationsTab()
        {
            Wait(1000);            
            ElementContainingTextIsDisplayed(tltleHeader, "User Portal", 10);
            SwitchToFrame(frameName);
            Click(newBirthRegistrationsTab);
            SwitchToDefaultContent();
        }

        public void SelectNewbornHearingExaminationResultsTab()
        {
            Wait(1000);
            ElementContainingTextIsDisplayed(tltleHeader, "User Portal", 10);
            SwitchToFrame(frameName);
            ClickWhenClickable(newbornHearingExaminationResultsTab, 10);
            SwitchToDefaultContent();
        }

        public string ReturnColumnsNotDisplayedInChildrenOverdueNewbornHearingExaminationResultsTable()
        {
            string[] expectedHeaders = { "CHI", "Name", "D.o.B", "Gender", "Postcode", "Place of Birth", "Days Outstanding" };
            string columnsNotMatched;
            SwitchToFrame(frameName);
            columnsNotMatched = table.ReturnColumnHeadersNotInExpectedOrder(childrenOverdueNewbornHearingExaminationResultsTable, expectedHeaders);
            SwitchToDefaultContent();
            return columnsNotMatched;
        }

        public void ExpandChildrenOverdueNewbornHearingExaminationResultsTable()
        {
            SwitchToFrame(frameName);
            Find(childrenOverdueNewbornHearingExaminationResultsPanel).FindElement(expandTableIcon).Click();
            SwitchToDefaultContent();
        }

        public bool IsPrintButtonDisplayed()
        {
            bool printButtonExists;
            SwitchToFrame(frameName);
            if (IsDisplayed(printButton, 10))
                printButtonExists = true;
            else
                printButtonExists = false;
            SwitchToDefaultContent();
            return printButtonExists;
        }

        public string GetPageTitle()
        {
            return getPageTitle();
        }



        


    }
}
