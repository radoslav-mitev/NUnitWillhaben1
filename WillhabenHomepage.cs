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

        //{
        //    get
        //    {
        //        return this.driver.FindElement(By.Id("login-logout"));
        //    }
        //}




        //FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cookies akzeptieren')]")
        //[FindsBy(How = How.Id, Using = "didomi-notice-agree-button")]
        //IWebElement cookiesAlert;

        //public IWebElement GetCookiesAlert()
        //{
        //    return this.cookiesAlert;
        //}

        // @FindBy(xpath = "//a[@id='login-logout']")
        //[FindsBy(How = How.Id, Using = "login-logout")]
        //IWebElement loginLink;

        //public IWebElement GetLoginLink()
        //{
        //    return this.loginLink;
        //}


        //public WillhabenHomepage(IWebDriver driver)
        //{
        //    this.driver = driver;
        //    PageFactory.InitElements(driver, this);
        //}

        //Click on accept cookies button
        //public void AcceptCookies()
        //{
        //    this.cookiesAlert.Click();
        //}


        //Click on "Einloggen" link
        //public void ClickLogin()
        //{
        //    this.loginLink.Click();
        //}

    }
}
