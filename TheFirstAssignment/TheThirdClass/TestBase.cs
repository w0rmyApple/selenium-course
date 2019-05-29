using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TheFirstAssignment.TheThirdClass
{
    public class TestBase
    {
        private By locator;
        protected int GetCountOfElements(IWebDriver driver)
        {
            locator = By.XPath("//td[contains(@id,'sidebar')] //li[contains(@id, 'app-') and contains(@class,'selected')]//li[contains(@id, 'doc')]");
            int count = driver.FindElements(locator).Count;
            return count;
        }
        protected int GetCountOfMenuElements(IWebDriver driver)
        {
            locator = By.XPath("//td[contains(@id,'sidebar')] //li[contains(@id, 'app-')]");
            int count=  driver.FindElements(locator).Count;
            return count;
        }


        protected IWebElement GetMainItem(IWebDriver driver, int num)
        {
            locator = By.XPath(string.Format("//td[contains(@id,'sidebar')]//li[contains(@id, 'app-')][{0}]", num));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementExists(locator));
        }
        protected IWebElement GetChildItem(IWebDriver driver, int num)
        {
            locator = By.XPath(string.Format("//td[contains(@id,'sidebar')] //li[contains(@id, 'app-') and contains(@class,'selected')]//li[contains(@id, 'doc')][{0}]",num));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementExists(locator));

        }

        protected By GetTitleLocator()
        {
            return locator = By.XPath($"//td[contains(@id,'content')]//h1");
        }

        protected bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
