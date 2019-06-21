using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.pages
{
    internal class ProductListPage : Page
    {
        public ProductListPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        internal void Open()
        {
            driver.Url = "http://litecart.stqa.ru/en/";
        }
        
        internal void SelectFirstProduct()
        {
          driver.FindElement(By.XPath($"(//li[contains(@class,'product')])[1]")).Click();
        }

    }
}
