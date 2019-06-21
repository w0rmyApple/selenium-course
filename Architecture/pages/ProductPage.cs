using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.pages
{
    class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Name, Using = "postcode")]
        internal IWebElement PostcodeInput;
        internal void AddToCart(string size)
        {
            if (IsElementPresent(driver, By.XPath("//select[contains(@name,'options[Size]')]")))
            {
                driver.FindElement(By.XPath("//select[contains(@name,'options[Size]')]")).SendKeys(size);
            }
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Name("add_cart_product")));
            element.Click();

        }
        internal void CheckCountOfProductsInCart(int count)
        {
            wait.Until(ExpectedConditions.ElementExists(By.XPath($"//span[contains(@class,'quantity') and contains(text()," +
                $"'{count.ToString()}')]")));
        }
    }
}
