using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSS_ChildHealth.Common.Helpers
{
    class DropDownLists : BasePage
    {
        IWebDriver Driver;
        public DropDownLists(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        //Select an option from the dropdown menu
        public void SelectFromDropDown(By DropDownLocator, string textToSelect)
        {
            //SelectElement sel = new SelectElement(Find(DropDownLocator));
            //sel.SelectByText(textToSelect);
            WaitForSelectToPolpulate(DropDownLocator).SelectByText(textToSelect);
        }
        //Select an option from the dropdown menu using partial text
        public void SelectFromDropDownUsingPartialText(By DropDownLocator, string textToSelect)
        {
            WaitForSelectToPolpulate(DropDownLocator).SelectByText(textToSelect, true);
        }
        //Select an option from the dropdown menu by Index
        public void SelectFromDropDownByIndex(By DropDownLocator, int index)
        {
            WaitForSelectToPolpulate(DropDownLocator).SelectByIndex(index);
        }

        public SelectElement WaitForSelectToPolpulate(By selectElement)
        {
            WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(5));
            return wait.Until<SelectElement>(drv =>
            {
                SelectElement element = new SelectElement(drv.FindElement(selectElement));
                if (element.Options.Count > 0)
                {
                    return element;
                }
                return null;
            }
            );
        }

        //Returns options not found in Drop down menu
        public string ReturnDropDownValuesThatAreNotListed(By DropDownLocator, string[] ExpectedOptions)
        {
            int count = 0;
            List<string> notMatched = new List<string>();
            SelectElement sel = new SelectElement(Find(DropDownLocator));
            IList<IWebElement> options = sel.Options;
            for (int i = 0; i < ExpectedOptions.Length; i++)
            {
                int missed = 0;
                for (int x = 0; x < options.Count; x++)
                {
                    if (options[x].Text.Trim().Equals(ExpectedOptions[i]))
                    {
                        count++;
                    }
                    else
                    {
                        missed++;
                    }
                }
                if (missed > (options.Count - 1))
                {
                    notMatched.Add(ExpectedOptions[i].ToString());
                }
            }
            if (count != ExpectedOptions.Length)
            {
                string allUnMatched = "";
                for (int p = 0; p < notMatched.Count; p++)
                {
                    allUnMatched = allUnMatched + ", " + notMatched[p];
                }
                char[] arr = new char[] { ',' };
                return allUnMatched.TrimStart(arr).Trim();
            }
            return "";
        }

        public bool IsOptionAvailableInDropDown(By dropdownLocator, string optionText)
        {
            IList<IWebElement> options = WaitForSelectToPolpulate(dropdownLocator).Options;
            List<string> strOptions = new List<string>();
            foreach (IWebElement option in options)
            {
                strOptions.Add(option.Text.Trim());
            };

            return strOptions.Contains(optionText);
        }

        public string GetCurrentDropdownValue(By DropDownLocator)
        {
            SelectElement sel = new SelectElement(Find(DropDownLocator));

            return sel.SelectedOption.Text.Trim();
        }

        public string GetDropdownOptionSelectedByIndex(By DropDownLocator, int index)
        {
            SelectFromDropDownByIndex(DropDownLocator, index);
            SelectElement sel = new SelectElement(Find(DropDownLocator));
            return sel.SelectedOption.Text.Trim();
        }
    }
}
