using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TheFirstAssignment
{
    [TestFixture(Description = "1. Поиск по запросу \"News\" "), Order(1)]
    public class FindNews
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Manage().Window.Maximize();
            

        }
        [Test(Description = "2. Поиск по ключу"), Order(2)]
        public void FindNewsInBrowser()
        {
            driver.FindElement(By.Name("q")).SendKeys("News");
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(ExpectedConditions.TitleIs("News - Поиск в Google"));

        }
        [Test(Description = "3. Закрытие браузера"), Order(3)]
        public void CloseBrowser()
        {
            driver.Quit();
            driver = null;
        }

    }
}
