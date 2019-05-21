using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TheFirstAssignment
{
    [TestFixture(Description = "1. Поиск по запросу \"Новости\" "), Order(1)]
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
            //wait.Until(ExpectedConditions)
        }
    }
}
