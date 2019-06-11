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
    [TestFixture(Description = "1.Проверить, что ссылки открываются в новом окне "), Order(1)]
    class CheckLinks
    {
        string ThereIsWindowOtherThan(ICollection<string> oldWindows)
        {
            bool flag;
            ICollection <string> newWindows = driver.WindowHandles;
            string res = newWindows.ToList().First();
            foreach (var nwindow in newWindows )
            {
                flag = false;
                foreach (var window in oldWindows)
                {
                    if (window == nwindow)
                        flag = true; 
                }
                if (flag == false)
                     res = nwindow;
            }
            return res;
        }
        private IWebDriver driver;
        private WebDriverWait wait;
        By locator;
        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {

            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            driver.Manage().Window.Maximize();


            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));

            string country = "Ukraine";
            driver.FindElement(By.XPath($"//a[contains(text(),'{country}')]")).Click();
            //IList<IWebElement> list = driver.FindElements(By.XPath("//form//a[contains(@target,'_blank')]"));
            int count = driver.FindElements(By.XPath("//form//a[contains(@target,'_blank')]")).Count;

            for (int i=0;i<count;i++)
            {
                locator = By.XPath("//form//a[contains(@target,'_blank')]");
                IWebElement element = driver.FindElements(locator)[i];
                string mainWindow = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;

                element.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                string newWindow =ThereIsWindowOtherThan(oldWindows);
                driver.SwitchTo().Window(newWindow);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
            driver.Quit();
            driver = null;
        }
    }
}
