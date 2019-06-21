using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.pages
{
    internal class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        internal CartPage Open()
        {
            driver.Url = "http://litecart.stqa.ru/en/checkout";
            return this;
        }
        internal void DeleteProduct()
        {
            By locator = By.XPath("(//li[contains(@class,'shortcut')]//a)[1]");
            if (IsElementPresent(driver, locator))
            {
                driver.FindElement(locator).Click();
            }
            driver.FindElement(By.XPath("//li[contains(@class, 'item')][1]//button[contains(@name, 'remove_cart_item')]")).Click();
            Thread.Sleep(500);
        }
        internal int GetCountOfUniqueProducts()
        {
            By locator = By.XPath("//div[contains(@id,'box-checkout-summary')]//td[contains(@class, 'item')]");

            return IsElementPresent(driver,locator) ?
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).Count :
            0;
            
        }
        
    }
}
