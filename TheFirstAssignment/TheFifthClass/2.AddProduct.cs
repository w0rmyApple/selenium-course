using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TheFirstAssignment
{
    [TestFixture(Description = "1. Проверка добавление товара"), Order(1)]

    class AddProduct
    {
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
            driver.FindElement(By.XPath("//a[contains(@href,'catalog')]")).Click();
            wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
            driver.FindElement(By.XPath("//a[contains(@class,'button') and contains(text(),' Add New Product')]")).Click();

            //fill the fields in general
            string date1 = "01/01/2001";
            string date2 = "01/01/2009";
            string name = "real kitty";
            string code = "13524";
            string category = "Subcategory";
            string category1 = "Root";
            string quantity = "5";
            string soldOut = "Temporary sold out";
            string imagePath = AppDomain.CurrentDomain.BaseDi‌rectory + "Content\\images\\kitty.jpg";
            string manufacturer_id = "ACME Corp.";
            string keywords = "real kitty soft wow";
            string shortDescription = "the best present for every child! soft and warm! you can touch it, you cat play with it, you can stroke it!";
            string description = "The domestic cat, or house cat, is a small mammal that has lived among people for thousands of years. " +
                " Domestic cats belong to the same animal family as the lion, tiger, jaguar, leopard, puma, and cheetah. " ;
            string price = "10";
            string priceCode = "US Dollars";

            driver.FindElement(By.XPath("//tr//strong[contains(text(),'Status')]//..//label[contains(text(),' Enabled')]")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys(name);
            driver.FindElement(By.Name("code")).SendKeys(code);
            driver.FindElement(By.XPath($"//*[contains(@name,'categories[]') and contains(@data-name,'{category}')]")).Click();
            driver.FindElement(By.XPath($"//*[contains(@name,'categories[]') and contains(@data-name,'{category1}')]")).Click();
            driver.FindElement(By.XPath("//td[contains(text(),'Unisex')]//..//input")).Click();
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys(quantity);
            driver.FindElement(By.Name("sold_out_status_id")).SendKeys(soldOut);
            driver.FindElement(By.XPath("//input[contains(@name,'new_images[]')]")).SendKeys(imagePath);

            driver.FindElement(By.XPath("//input[contains(@name,'date_valid_from')]")).SendKeys(Keys.Home +date1);
            driver.FindElement(By.XPath("//input[contains(@name,'date_valid_to')]")).SendKeys(Keys.Home + date2);

            // in information
            driver.FindElement(By.XPath("//a[contains(@href,'information')]")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.Name("manufacturer_id")).SendKeys(manufacturer_id);
            driver.FindElement(By.Name("keywords")).SendKeys(keywords);
            driver.FindElement(By.Name("short_description[en]")).SendKeys(shortDescription);
            driver.FindElement(By.XPath(("//div[@class='trumbowyg-editor']"))).SendKeys(description);
            driver.FindElement(By.Name("head_title[en]")).SendKeys(name);
            driver.FindElement(By.Name("meta_description[en]")).SendKeys(keywords);
            // driver.FindElement(By.Name("quantity")).Clear();

            //in prices
            driver.FindElement(By.XPath("//a[contains(@href,'prices')]")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys(price );
            driver.FindElement(By.Name("purchase_price_currency_code")).SendKeys(priceCode);
            driver.FindElement(By.Name("prices[USD]")).Clear();
            driver.FindElement(By.Name("prices[USD]")).SendKeys(price);
            driver.FindElement(By.Name("prices[EUR]")).Clear();
            driver.FindElement(By.Name("prices[EUR]")).SendKeys(price);

            driver.FindElement(By.Name("save")).Click();

            Assert.True(IsElementPresent(driver, By.XPath($"//a[contains(text(),'{name}')]")), "Ожидалось, что созданный товар будет в списке элементов");

        }
        [Test(Description = "2. Закрытие браузера"), Order(4)]
        public void KillBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}