using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace NUnitWillhaben1.Steps
{
    [TestFixture]
    [Binding]

    public sealed class LoginSteps

    {
        private IWebDriver _driver;
        private WillhabenHomepage _willhabenHomepage;
        private WillhabenLoginPage _willhabenLoginpage;
        private WebDriverWait _wait;

        

        [Given(@"an user opens www\.willhaben\.at in a chrome browser")]
        public void GivenAnUserOpensWww_Willhaben_AtInAChromeBrowser()
        {
            //this._driver = new ChromeDriver();//without docker

            var options = new ChromeOptions();//in case of docker implementation
            this._driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub/"), options);//in case of docker implementation

            this._driver.Navigate().GoToUrl("https://www.willhaben.at/iad");

            this._wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(25));

            this._willhabenHomepage = new WillhabenHomepage(this._driver);
            this._willhabenLoginpage = new WillhabenLoginPage(this._driver);

            //accepting cookies in popup element
            this._wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this._willhabenHomepage.CookiesAlert)).Click();

            //Click on "Einloggen" link
            this._wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this._willhabenHomepage.LoginLink)).Click();
        }

        [When(@"trying to log into account using non-valid credentials (.*) and (.*)")]
        public void WhenTryingToLogIntoAccountUsingNon_ValidCredentialsAnd(string p0, string p1, Table table)
        {
            //ScenarioContext.Current.Pending();
            dynamic data = table.CreateDynamicInstance();

            this._willhabenLoginpage.SetEMail((String)data.eMail);
            this._willhabenLoginpage.SetPassword((String)data.password);
            this._willhabenLoginpage.SetSubmitButtonClick();

        }

        [Then(@"user should see \[Der Benutzername oder das Passwort konnten nicht erkannt werden\.]")]
        public void ThenUserShouldSeeDerBenutzernameOderDasPasswortKonntenNichtErkanntWerden_()
        {
            IWebElement element = this._wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this._willhabenLoginpage.GetAlertIncorrectCredentials()));

            Assert.IsTrue(element.Displayed);
            this._driver.Close();
        }

        [When(@"trying to log into accoung using correct ""(.*)"" format but without a ""(.*)""")]
        public void WhenTryingToLogIntoAccoungUsingCorrectFormatButWithoutA(string p0, string p1)
        {
            this._willhabenLoginpage.SetEMail(p0);
            this._willhabenLoginpage.SetPassword(p1);
            this._willhabenLoginpage.SetSubmitButtonClick();
        }

        [Then(@"user should see \[Bitte gib dein Passwort ein]")]
        public void ThenUserShouldSeeBitteGibDeinPasswortEin()
        {
            IWebElement element = this._wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this._willhabenLoginpage.GetAlertNoPasswordAdded()));

            Assert.IsTrue(element.Displayed);
            this._driver.Close();
        }


        [OneTimeTearDown]
        public void Teardown() => this._driver.Quit();
    }
}
