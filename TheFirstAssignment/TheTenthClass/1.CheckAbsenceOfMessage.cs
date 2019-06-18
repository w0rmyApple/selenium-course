using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFirstAssignment.TheTenthClass
{
    [TestFixture(Description = "1.Проверить отсутствие сообщений в логе браузера"), Order(1)]
    class CheckAbsenceOfMessage
    {
        bool IsElementPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }
        private IWebDriver driver;
        private WebDriverWait wait;
        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            driver.Manage().Window.Maximize();

            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));

            By locator = By.XPath("//a[contains(@href,'product_id') and contains(@title,'Edit')]");
            IList<IWebElement> listOfProducts = driver.FindElements(locator);
            for (int i = 1; i <= listOfProducts.Count(); i++)
            {
                locator = By.XPath($"(//a[contains(@href,'product_id') and contains(@title,'Edit')])['{i}']");
                IWebElement element = driver.FindElement(locator);
                element.Click();
                Assert.True(IsElementPresent(driver,By.XPath("//h1[contains(text(),'Edit Product')]")));
                int countOfLogs = driver.Manage().Logs.GetLog("browser").Count;
                Assert.AreEqual(countOfLogs, 0);
                driver.Navigate().Back();

            }
            driver.Quit();
            driver = null;

        }
    }
}
