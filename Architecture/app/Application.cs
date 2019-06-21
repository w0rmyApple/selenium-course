using System;
using System.Collections.Generic;
using Architecture.pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Architecture.app
{
    
    public class Application
    {
        const int n = 3;
        private IWebDriver driver;

        private CartPage cartPage;
        private ProductListPage productListPage;
        private ProductPage productPage;

        public Application()
        {
            driver = new ChromeDriver();
            cartPage = new CartPage(driver);
            productListPage = new ProductListPage(driver);
            productPage = new ProductPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }
        internal void AddAndDeleteProducts(string size)
        {
            for (int i = 1; i <= n; i++)
            {
                productListPage.Open();
                productListPage.SelectFirstProduct();
                productPage.AddToCart(size);
                productPage.CheckCountOfProductsInCart(i);
            }

            cartPage.Open();
            
            int k = cartPage.GetCountOfUniqueProducts();
            while (k > 0)
            {
                cartPage.DeleteProduct();
                int countAfter = cartPage.GetCountOfUniqueProducts();
                Assert.AreEqual(--k, countAfter);
            }
            Quit();
        }
    }
}
