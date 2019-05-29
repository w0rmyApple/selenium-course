using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TheFirstAssignment.TheThirdClass
{
    [TestFixture(Description = "1. Авторизоваться, прокликать все пункты меню и проверить наличие заголовка "), Order(1)]
    class ClickAndCheck 
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        By locator, locator1;

        bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.Manage().Window.Maximize();


            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            locator = By.XPath("//li[contains(@id,'app-')]");
            IList<IWebElement> listOfBarTitles = driver.FindElements(locator);
            locator1 = By.XPath("//li[contains(@id,'doc-')]");
            int countOfBarTitles = listOfBarTitles.Count;
            if (countOfBarTitles > 0)
            {
                for (int i = 0; i < countOfBarTitles; i++)
                {
                    listOfBarTitles = driver.FindElements(locator);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    listOfBarTitles[i].Click();
                    IList<IWebElement> listOfNestedTitles = driver.FindElements(locator1);
                    int countOfNestedTitles = listOfNestedTitles.Count;
                    if (countOfNestedTitles > 0)
                    {
                        for (int j = 0; j < countOfNestedTitles; j++)
                        {
                            listOfNestedTitles = driver.FindElements(locator1);
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                            listOfNestedTitles[j].Click();
                            Assert.True(IsElementPresent(driver,By.XPath("//h1")), "Ожидалось, что заголовок будет видимым ");
                        }
                    }
                }
            }

        }

        [Test(Description = "2. Закрытие браузера"), Order(2)]
        public void CloseBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
