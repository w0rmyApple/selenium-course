using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace HomeTasks
{
    [TestFixture(Description = "1. Авторизация в системе через Firefox "), Order(1)]
    public class UseFirefox
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.Manage().Window.Maximize();
        }
        [Test(Description = "2. Авторизоваться в системе"), Order(2)]
        public void AutorizationAsAnAdmin()
        {
            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

        }
        [Test(Description = "3. Закрытие браузера"), Order(3)]
        public void CloseBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
