using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace TheFirstAssignment.TheThirdClass
{
    [TestFixture(Description = "1. Проверка открытия правильной страницы товара"), Order(1)]

    class CheckRightPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        By locator, locator1, locator2,locator3;
        [Test(Description = "1. Открыть браузер"), Order(1)]
        public void OpenBrowser()
        {
            // driver = new FirefoxDriver();
            //driver = new ChromeDriver();
            driver = new InternetExplorerDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            driver.Manage().Window.Maximize();
            // locator = By.XPath("(//li[contains(@class,'product')])");
            locator = By.XPath("//div[contains(@id,'box-campaigns')]//li[contains(@class,'product')]"); // гл стр
            //берем первый
            IWebElement product = driver.FindElement(locator);
            locator1 = By.XPath("//div[contains(@id,'box-campaigns')]//li[contains(@class,'product')]//a[contains(@class,'link') and contains(@title,'Duck')]");
            string name = driver.FindElement(locator1).GetAttribute("Title");

            IWebElement element = driver.FindElement(locator);
            locator1 = By.XPath($"//div[contains(@id,'box-campaigns')]//li[contains(@class,'product')]//a[contains(@class,'link') and contains(@title,'{name}')]");
            IWebElement link = driver.FindElement(locator1);
            string text = link.GetAttribute("Title");
            element.Click();
            // a
            Assert.True(text == name, "Ожидалось, что заголовки совпадут");
            driver.Navigate().Back();
            //b
            locator = By.XPath("//div[contains(@id,'box-campaigns')]//s[@class='regular-price']"); // гл стр старая цена
            locator1 = By.XPath("//div[contains(@id,'box-campaigns')]//strong[@class='campaign-price']"); // гл стр новая цена
            locator2 = By.XPath("//strong[@class='campaign-price']"); // стр товара новая цена
            locator3 = By.XPath("//s[@class='regular-price']"); // стр товара старая цена
            string oldPrice = driver.FindElement(locator).Text;
            string newPrice = driver.FindElement(locator1).Text;
            driver.FindElement(locator).Click();
            Assert.True(driver.FindElement(locator2).Text == newPrice &&
                       (driver.FindElement(locator3).Text == oldPrice));

            //c
            char[] delimiterChars = { '(', ')', ',', ' ' };
            string[] nums = driver.FindElement(locator3).GetCssValue("color").Split(delimiterChars);
            Assert.AreEqual(nums[1], nums[3] ,nums[5],"Ожидалось, что старая цена будет серой");
            Assert.True(driver.FindElement(locator3).GetCssValue("text-decoration").Contains("line-through"),"Ожидалось, что старая цена будет зачеркнута");
            

            driver.Navigate().Back();
            nums = driver.FindElement(locator).GetCssValue("color").Split(delimiterChars);
            Assert.AreEqual(nums[1], nums[3], nums[5], "Ожидалось, что старая цена будет серой");
            Assert.True(driver.FindElement(locator).GetCssValue("text-decoration").Contains("line-through"), "Ожидалось, что старая цена будет зачеркнута");
            driver.FindElement(locator).Click();

            //d
            nums = driver.FindElement(locator2).GetCssValue("color").Split(delimiterChars);
            string zero = "0";
            Assert.AreEqual(nums[3], nums[5], zero, "Ожидалось, что новая цена будет красной");

            string weight = driver.FindElement(locator2).GetCssValue("font-weight");
            int.TryParse(weight, out int res);
            Assert.True(res>=700, "Ожидалось, что новая цена будет жирной");

            driver.Navigate().Back();
            nums = driver.FindElement(locator1).GetCssValue("color").Split(delimiterChars);
            Assert.AreEqual(nums[3], nums[5], zero, "Ожидалось, что новая цена будет красной");

            weight = driver.FindElement(locator1).GetCssValue("font-weight");
            int.TryParse(weight, out res);
            Assert.True(res >= 700, "Ожидалось, что новая цена будет жирной");

            //e
            string OldSize = driver.FindElement(locator).GetCssValue("font-size");
            OldSize = OldSize.Substring(0, OldSize.Length - 2);
            double res2 =double.Parse(OldSize.Substring(0,OldSize.Length-2), new NumberFormatInfo
            {
                NumberDecimalSeparator = ","
            });
            string NewSize = driver.FindElement(locator1).GetCssValue("font-size");

            int.TryParse(NewSize.Substring(0, OldSize.Length - 2), out int res1);
            Assert.True(res1 > res2, "Ожидалось, что новая цена будет крупнее");
        }
        [Test(Description = "4. Закрытие браузера"), Order(4)]
        public void KillBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
