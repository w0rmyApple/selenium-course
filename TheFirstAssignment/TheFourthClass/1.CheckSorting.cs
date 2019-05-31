using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace TheFirstAssignment.TheThirdClass
{
    [TestFixture(Description = "1. Проверка сортировки стран и зон"), Order(1)]
    class CheckSorting
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        By locator;
       
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
        bool CheckSort(By locator)
        {
            List<string> names = driver.FindElements(locator).Select(l => l.Text).ToList();
            List<string> sorted = names;
            sorted.Sort();
            bool flag = true;
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] != sorted[i])
                    flag = false;
            }
            return flag;
        }

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

            Assert.True(IsElementPresent(driver, By.XPath("//h1[contains(text(),'Countries')]")), "Ожидался переход на страницу '/Countries/'");

            //a

            locator = By.XPath("//form[contains(@name,'countries_form')]//tr[contains(@class,'row')]//td[5]");
            //сортируем взятый список стран

            Assert.True(CheckSort(locator), "Ожидалось, что список стран будет отсортирован");
            //b
            locator = By.XPath("//form[contains(@name,'countries_form')]//tr[contains(@class,'row')]//td[6]");
            List<string> countOfZones = driver.FindElements(locator).Select(l => l.Text).ToList();
            int count;
            for (int i=0;i<countOfZones.Count;i++)
            {
                int.TryParse(countOfZones[i], out count);
                if (count > 0)
                { 
                    locator = By.XPath($"(//form[contains(@name,'countries_form')]//tr[contains(@class,'row')]//td[5]//a)[{++i}]");
                    IWebElement element = driver.FindElement(locator);
                    string href = element.GetAttribute("href");
                    IWebElement link= driver.FindElement(By.CssSelector($"[href='{href}']"));
                    link.Click();

                    locator = By.XPath("//form[contains(@method,'post')]//td[3]//input[contains(@type,'hidden')]");
                    Assert.True(CheckSort(locator), "Ожидалось, что список зон будет отсортирован");

                    driver.Navigate().Back();
                }
            }
        }

        [Test(Description = "2. Закрытие браузера"), Order(2)]
        public void CloseBrowser()
        {
            driver.Quit();
            driver = null;
        }
        [Test(Description = "3. Проверка отсортированности зон"), Order(3)]
        public void CheckSOrtZones()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            driver.Manage().Window.Maximize();


            string login = "admin";
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(login);
            driver.FindElement(By.Name("login")).Click();

            Assert.True(IsElementPresent(driver, By.XPath("//h1[contains(text(),'Geo Zones')]")), "Ожидался переход на страницу '/Geo Zones/'");


            //2
            locator = By.XPath("//form[contains(@name,'geo_zones_form')]//tr[contains(@class,'row')]//td[3]");
            IList <IWebElement> elements = driver.FindElements(locator);
            for (int i=0;i<elements.Count;i++)
            {
                locator = By.XPath($"(//form[contains(@name,'geo_zones_form')]//tr[contains(@class,'row')]//td[3]//a)[{i+1}]");
                IWebElement element = driver.FindElement(locator);
                string href = element.GetAttribute("href");
                IWebElement link = driver.FindElement(By.CssSelector($"[href='{href}']"));
                link.Click();
                locator = By.XPath("//select[contains(@name,'[zone_code]')]");
                Assert.True(CheckSort(locator), "Ожидалось, что список зон будет отсортирован");
                driver.Navigate().Back();
            }
        }

        [Test(Description = "4. Закрытие браузера"), Order(4)]
        public void KillBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}