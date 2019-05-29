using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace HomeTasks
{
    [TestFixture(Description = "3. Авторизация в системе через Firefox "), Order(3)]
    public class UseFirefoxNightly
    {
        private WebDriverWait wait;
        
        [Test(Description = "1. Регистрация в системе"), Order(1)]
        public void OpenBrowser()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            IWebDriver driver = new FirefoxDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.Manage().Window.Maximize();
            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));
            driver.Quit();
            driver = null;

        }      
        
    }
}