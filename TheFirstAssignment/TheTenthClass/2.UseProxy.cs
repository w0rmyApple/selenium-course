using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;


namespace TheFirstAssignment.TheTenthClass
{
    [TestFixture(Description = "2.Перенаправьте трафик в прокси-сервер0"), Order(2)]
    class UseProxy
    {
        private WebDriverWait wait;
        public Proxy proxy = new Proxy();
        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void Start()
        {
            proxy.Kind = ProxyKind.Manual;
            proxy.HttpProxy = "localhost:8888";
            ChromeOptions options = new ChromeOptions();
            options.Proxy = proxy;
            IWebDriver driver = new ChromeDriver(options);


            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));

            driver.Quit();
            driver = null;

        }
    }
}
