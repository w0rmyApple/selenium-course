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
    [TestFixture(Description = "2. Проверка наличия стикеров у товаров "), Order(2)]
    class CheckStickers 
    { 
        private IWebDriver driver;
        private WebDriverWait wait;
        private By locator;

        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            driver.Manage().Window.Maximize();

            locator = By.XPath("(//li[contains(@class,'product')])");
            IList<IWebElement> products = driver.FindElements(locator);
            int count = driver.FindElements(locator).Count;
            
            for (int i=1;i<=count;i++)
            {
                locator = By.XPath(string.Format(("(//div[contains(@class,'sticker')])[{0}]"),i));
                int countOfStickers = products[i-1].FindElements(locator).Count();
                Assert.AreEqual(countOfStickers, 1, "Ожидалось, что на каждом товаре будет 1 стикер");
            }

        }
        [Test(Description = "3. Закрытие браузера"), Order(3)]
        public void CloseBrowser()
        {
            driver.Quit();
            driver = null;

        }
    }
}
