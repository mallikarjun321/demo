using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSS_ChildHealth.Common.Helpers
{
    class Tables : BasePage
    {
        IWebDriver Driver;
        public Tables(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        //Returns the string of the Table Cell by feeding in Row and Column
        public string GetTextFromTableCell(By byTableIdentifier, int iRow, int iCol)
        {
            IWebElement engageTable = Driver.FindElement(byTableIdentifier);
            List<IWebElement> lstTrElem = new List<IWebElement>(engageTable.FindElements(By.TagName("tr")));
            List<IWebElement> lstTdElem = new List<IWebElement>(lstTrElem[iRow-1].FindElements(By.TagName("td")));
            if (lstTdElem.Count == 0)
            {
                List<IWebElement> lstThElem = new List<IWebElement>(engageTable.FindElements(By.TagName("th")));
                return lstThElem[iRow - 1].Text;
            }
            else
                return (lstTdElem[iCol - 1].Text);
            //Debug.WriteLine(lstTdElem[iCol].Text);

        }
        //Returns the string of the Table Header Cell by feeding in Column
        public string GetTextFromTableHeaderCell(By byTableIdentifier, int iCol)
        {
            IWebElement engageTable = Driver.FindElement(byTableIdentifier);
            List<IWebElement> lstThElem = new List<IWebElement>(engageTable.FindElements(By.TagName("th")));
            //Debug.WriteLine(lstThElem[iCol].Text);
            return (lstThElem[iCol-1].Text);
        }
        //Returns number of Rows belonging to table
        public int GetNumberOfRowsFromTable(By byTableIdentifier)
        {
            IWebElement engageTable = Driver.FindElement(byTableIdentifier);
            List<IWebElement> lstTrElem = new List<IWebElement>(engageTable.FindElements(By.TagName("tr")));
            return lstTrElem.Count;
        }
        //Returns number of Columns belonging to table by counting Column Headings
        public int GetNumberOfColumnsFromTable(By byTableIdentifier)
        {
            IWebElement engageTable = Driver.FindElement(byTableIdentifier);
            List<IWebElement> lstThElem = new List<IWebElement>(engageTable.FindElements(By.TagName("th")));
            List<IWebElement> lstTdElem;
            if (lstThElem.Count > 0)
                return lstThElem.Count;
            else
            {
                lstTdElem = new List<IWebElement>(engageTable.FindElements(By.TagName("td")));
                return lstTdElem.Count;
            }
        }
        //Click on element in Table Cell
        public void ClickElementInTableCell(By byTableIdentifier, By byElementIdentifier, int iRow, int iCol)
        {
            IWebElement engageTable = Find(byTableIdentifier);
            List<IWebElement> lstTrElem = new List<IWebElement>(engageTable.FindElements(By.TagName("tr")));
            List<IWebElement> lstTdElem = new List<IWebElement>(lstTrElem[iRow-1].FindElements(By.TagName("td")));
            lstTdElem[iCol-1].FindElement(byElementIdentifier).Click();
        }
        //Search for string in Table Column and return Row number
        public int SearchForStringInTableColumnAndReturnRowNumber(By byTableIdentifier, int iCol, string textToSearch)
        {
            //IWebElement searchToTable = Driver.FindElement(byTableIdentifier);
            int NumOfRows = GetNumberOfRowsFromTable(byTableIdentifier);
            int rowNum = 0;
            for (int i = 1; i <= NumOfRows; i++)
            {
                if (GetTextFromTableCell(byTableIdentifier, i, iCol).Equals(textToSearch))
                {
                    rowNum = i;
                    break;
                }
            }
            return rowNum;
        }
        //Search for string in Table Header and return Column number
        public int SearchForStringInTableHeaderfAndReturnColumnNumber(By byTableIdentifier, string textToSearch)
        {
            int NumOfCols = GetNumberOfColumnsFromTable(byTableIdentifier);
            int colNum = 0;
            for (int i = 1; i <= NumOfCols - 1; i++)
            {
                if (GetTextFromTableHeaderCell(byTableIdentifier, i).Equals(textToSearch))
                {
                    colNum = i;
                    break;
                }
            }
            return colNum;
        }

        public string GetTextFromTableCellInRowInNamedColumn(By byTableIdentifier, string columnName, int rowNumber)
        {
            int columnNumber = SearchForStringInTableHeaderfAndReturnColumnNumber(byTableIdentifier, columnName);
            return GetTextFromTableCell(byTableIdentifier, rowNumber, columnNumber);
        }

        public string ReturnColumnHeadersNotInExpectedOrder(By byTableIdentifier, string[] expectedHeaders)
        {
            string columnsNotMatched = "";

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                if (!GetTextFromTableHeaderCell(byTableIdentifier, i + 1).Equals(expectedHeaders[i]))
                    columnsNotMatched += ", " + expectedHeaders[i];
            }

            char[] arr = new char[] { ',' };
            columnsNotMatched = columnsNotMatched.Trim(arr);
            return columnsNotMatched.Trim();
        }

    }
}
