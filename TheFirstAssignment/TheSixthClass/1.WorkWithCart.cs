using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheFirstAssignment.TheSixthClass
{
    [TestFixture(Description = "1. Сценарий работы с корзиной"), Order(1)]

    class WorkWithCart
    {
        bool IsElementPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }
        const int n = 3;
        private IWebDriver driver;
        private WebDriverWait wait;
        By locator;
        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://litecart.stqa.ru/en/");
            driver.Manage().Window.Maximize();
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            for (int i = 1; i <= n; i++)
            {
                string name = driver.FindElement(By.XPath("//div[contains(@class,'name')]")).Text;
                locator = By.XPath("//li[contains(@class,'product')]");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy
                    (locator));
                driver.FindElement(locator).Click();
                Assert.AreEqual(name, driver.FindElement(By.XPath("//h1")).Text, "Ожидалось, что осуществится переход на 1 товар");

                string key = "Small";

                if (IsElementPresent(driver, By.XPath("//select[contains(@name,'options[Size]')]")))
                {
                    driver.FindElement(By.XPath("//select[contains(@name,'options[Size]')]")).SendKeys(key);
                }
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Name("add_cart_product")));
                element.Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath($"//span[contains(@class,'quantity') and contains(text(),'{i.ToString()}')]")));
                driver.Navigate().Back();

            }
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(@href,'checkout') and contains(@class,'link')]"))).Click();
            wait.Until(ExpectedConditions.TitleIs("Checkout | My Store"));
            int k = driver.FindElements(By.XPath("//li[contains(@class,'shortcut')]//a")).Count;
            while (k > 1)
            {
                int count = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[contains(@id,'box-checkout-summary')]//td[contains(@class, 'item')]"))).Count;
                locator = By.XPath($"(//li[contains(@class,'shortcut')]//a)[1]");
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
                driver.FindElement(locator).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500);
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//li[contains(@class, 'item')][1]//button[contains(@name, 'remove_cart_item')]"))).Click();
                driver.FindElement(By.XPath("//li[contains(@class, 'item')][1]//button[contains(@name,'remove_cart_item')]")).Click();
                Thread.Sleep(500);
                Assert.AreEqual(--count, driver.FindElements(By.XPath("//div[contains(@id,'box-checkout-summary')]//td[contains(@class, 'item')]")).Count, "Ожидалось, что количество наименований товара уменьшится");
                k--;
            }
            driver.FindElement(By.XPath("//li[contains(@class, 'item')]//button[contains(@name,'remove_cart_item')]")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//em[contains(text(),'There are no items in your cart.')]")));
            
        }
        [Test(Description = "2. Закрытие браузера"), Order(2)]
        public void KillBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}

