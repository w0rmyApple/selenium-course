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
    class ClickAndCheck : TestBase
    {
        
        private IWebDriver driver;
        private WebDriverWait wait;


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


            
            for(int i=1; i <= GetCountOfMenuElements(driver);i++)
            {   
                GetMainItem(driver, i).Click();
                for (int j=1; j<= GetCountOfElements(driver);j++)
                {
                   GetChildItem(driver, j).Click();
                   Assert.True(IsElementPresent(driver, GetTitleLocator()), "Ожидалось, что заголовок будет видимым ");
                }
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
