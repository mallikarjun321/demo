using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSS_ChildHealth.Common.Helpers
{
    class RadioButtons : BasePage
    {
        IWebDriver Driver;
        public RadioButtons(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        //Select Radio Button based on its value
        public void SelectRadioButton(By radio, String value)
        {
            IList<IWebElement> RadioGroup = Driver.FindElements(radio);

            for (int i = 0; i < RadioGroup.Count; i++)
            {
                if (RadioGroup[i].GetAttribute("value").ToString().Equals(value))
                {
                    RadioGroup[i].Click();
                    break;
                }
            }
        }
        //
        public string ReturnRadioButtonValuesNotAvailable(By RadioGroupLocator, String[] ExpectedValues)
        {
            int count = 0;
            List<string> notMatched = new List<string>();
            IList<IWebElement> RadioGroup = Driver.FindElements(RadioGroupLocator);
            for (int i = 0; i < ExpectedValues.Length; i++)
            {
                int missed = 0;
                for (int x = 0; x < RadioGroup.Count; x++)
                {
                    if (RadioGroup[x].GetAttribute("value").Equals(ExpectedValues[i]))
                    {
                        count++;
                    }
                    else
                    {
                        missed++;
                    }
                }
                if (missed > (RadioGroup.Count - 1))
                {
                    notMatched.Add(ExpectedValues[i].ToString());
                }
            }
            if (count != ExpectedValues.Length)
            {
                String allUnMatched = "";
                for (int p = 0; p < notMatched.Count; p++)
                {
                    allUnMatched = allUnMatched + ", " + notMatched[p];
                }
                char[] arr = new char[] { ',' };
                return allUnMatched.TrimStart(arr);
            }
            return "";
        }

    }
}
