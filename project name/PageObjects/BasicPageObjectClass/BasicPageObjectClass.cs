using OpenQA.Selenium;
using NSS_ChildHealth.Common;
using NSS_ChildHealth.Common.Helpers;

namespace NSS_ChildHealth.PageObjects.BasicPageObjectClass
{
    class BasicPageObjectClass : BasePage
    {
        #region Page Objects
        

        #endregion

        DropDownLists dropDownList;
        Tables table;
        RadioButtons radioButton;

        public BasicPageObjectClass(IWebDriver driver) : base(driver)
        {
            dropDownList = new DropDownLists(driver);
            table = new Tables(driver);
            radioButton = new RadioButtons(driver);
        }


        public void GoToSite()
        {
            Visit("https://www.bbc.co.uk/news");
        }     

   

    }
}
