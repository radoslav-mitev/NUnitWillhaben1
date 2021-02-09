using OpenQA.Selenium;
//using SeleniumExtras.PageObjects;
//using System;

namespace NUnitWillhaben1
{
    public class WillhabenHomepage
    {
        private readonly IWebDriver driver;          

        public WillhabenHomepage(IWebDriver driver) => this.driver = driver;
        
        public IWebElement CookiesAlert => this.driver.FindElement(By.Id("didomi-notice-agree-button"));
          
        public IWebElement LoginLink => this.driver.FindElement(By.Id("login-logout"));        
    }
}
