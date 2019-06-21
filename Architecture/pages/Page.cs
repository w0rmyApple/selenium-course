using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Architecture
{
    internal class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }
        public bool IsElementPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }
    }
}
