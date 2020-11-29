using OpenQA.Selenium;
//using SeleniumExtras.PageObjects;
using System;

namespace NUnitWillhaben1
{
    public class WillhabenLoginPage
    {
        private readonly IWebDriver driver;

        public WillhabenLoginPage(IWebDriver driver) => this.driver = driver;

        private IWebElement InputFieldEMail => this.driver.FindElement(By.Id("email"));

        private IWebElement InputFieldPassword => this.driver.FindElement(By.Id("password"));
               
        private IWebElement SubmitButton => this.driver.FindElement(By.XPath("//span[contains(text(),'Einloggen')]"));

        //alert message: Bitte gib eine gültige E-Mail-Adresse an
        private IWebElement AlertIncorrectEMailFormat => this.driver.FindElement(By
            .XPath("//small[contains(text(),'Bitte gib eine gültige E-Mail-Adresse an.')]"));

        private IWebElement AlertIncorrectCredentials => this.driver.FindElement(By
            .XPath("//p[contains(text(),'Der Benutzername oder das Passwort konnten nicht e')]"));

        //alert message: Bitte gib dein Passwort ein
        private IWebElement AlertNoPasswordAdded => this.driver.FindElement(By.Id("password-message"));

       
        public void SetEMail(String eMail) => this.InputFieldEMail.SendKeys(eMail);
        

        public void SetPassword(String inputFieldPassword) => this.InputFieldPassword.SendKeys(inputFieldPassword);
        

        public void SetSubmitButtonClick() => this.SubmitButton.Click();
        
        public IWebElement GetAlertIncorrectCredentials() => this.AlertIncorrectCredentials;
        

        //public IWebElement GetAlertIncorrectEMailFormat() => this.AlertIncorrectEMailFormat;
        
        //public IWebElement GetSubmitButton() => this.SubmitButton;
        

        public IWebElement GetAlertNoPasswordAdded() => this.AlertNoPasswordAdded;   
       
    }
}
