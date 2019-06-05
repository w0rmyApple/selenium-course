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
using System.Text.RegularExpressions;
using System.Threading;

namespace TheFirstAssignment.TheThirdClass
{
    [TestFixture(Description = "1. Проверка создания пользователя"), Order(1)]

    class CreateUser
    {
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

            //a
            locator = By.XPath("//div[contains(@id,'box-account-login')]//a");
            driver.FindElement(locator).Click();
            wait.Until(ExpectedConditions.TitleIs("Create Account | My Store"));
            string postcode = "34523";
            string firstName = "Maria";
            string lastName = "Kuznetsova";
            string adress = "Black street";
            string city = "Los Angeles";
            string country = "United States";
            string email = "marymarymap@mail.ru";
            string phone = "89002100999";
            string password = "Secret123$";
            string zone = "NY";

            driver.FindElement(By.XPath("//input[contains(@name,'firstname')]")).SendKeys(firstName);
            driver.FindElement(By.XPath("//input[contains(@name,'lastname')]")).SendKeys(lastName);
            driver.FindElement(By.XPath("//input[contains(@name,'address1')]")).SendKeys(adress);
            driver.FindElement(By.XPath("//input[contains(@name,'postcode')]")).SendKeys(postcode);
            driver.FindElement(By.XPath("//input[contains(@name,'city')]")).SendKeys(city);
            driver.FindElement(By.XPath("//select[contains(@name,'country_code')]")).SendKeys(country);
            wait.Until(ExpectedConditions.ElementExists(By.XPath($"//select[contains(@name,'zone_code')]//option[contains(@value, {zone})]")));
            driver.FindElement(By.XPath("//select[contains(@name,'zone_code')]")).SendKeys(zone);
            driver.FindElement(By.XPath("//input[contains(@name,'email')]")).SendKeys(email);
            driver.FindElement(By.XPath("//input[contains(@name,'phone')]")).SendKeys(phone);
            driver.FindElement(By.XPath("//input[contains(@name,'password')]")).SendKeys(password);
            driver.FindElement(By.XPath("//input[contains(@name,'confirmed_password')]")).SendKeys(password);

            driver.FindElement(By.XPath("//div[contains(@id,'create-account')]//button[contains(@name,'create_account')]")).Click();
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
            driver.FindElement(By.XPath("//div[contains(@id,'box-account')]//a[contains(text(),'Logout')]")).Click();

            driver.FindElement(By.XPath("//form[contains(@name,'login_form')]//input[contains(@name,'email')]")).SendKeys(email);
            driver.FindElement(By.XPath("//form[contains(@name,'login_form')]//input[contains(@name,'password')]")).SendKeys(password);
            driver.FindElement(By.XPath("//form[contains(@name,'login_form')]//button[contains(@name,'login')]")).Click();

            driver.FindElement(By.XPath("//div[contains(@id,'box-account')]//a[contains(text(),'Logout')]")).Click();

        }
        [Test(Description = "2. Закрытие браузера"), Order(4)]
        public void KillBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
