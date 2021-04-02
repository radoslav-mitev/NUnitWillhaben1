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
        private IWebDriver driver;
        private WillhabenHomepage willhabenHomepage;
        private WillhabenLoginPage willhabenLoginpage;
        private WebDriverWait wait;

        

        [Given(@"an user opens www\.willhaben\.at in a chrome browser")]
        public void GivenAnUserOpensWww_Willhaben_AtInAChromeBrowser()
        {
            //this.driver = new ChromeDriver();//without docker

            var options = new ChromeOptions();//in case of docker implementation
            this.driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub/"), options);//in case of docker implementation

            this.driver.Navigate().GoToUrl("https://www.willhaben.at/iad");

            this.wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(25));

            this.willhabenHomepage = new WillhabenHomepage(this.driver);
            this.willhabenLoginpage = new WillhabenLoginPage(this.driver);

            //accepting cookies in popup element
            this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenHomepage.CookiesAlert)).Click();

            //Click on "Einloggen" link
            this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenHomepage.LoginLink)).Click();
        }

        [When(@"trying to log into account using non-valid credentials (.*) and (.*)")]
        public void WhenTryingToLogIntoAccountUsingNon_ValidCredentialsAnd(string p0, string p1, Table table)
        {
            //ScenarioContext.Current.Pending();
            dynamic data = table.CreateDynamicInstance();

            this.willhabenLoginpage.SetEMail((String)data.eMail);
            this.willhabenLoginpage.SetPassword((String)data.password);
            this.willhabenLoginpage.SetSubmitButtonClick();

        }

        [Then(@"user should see \[Der Benutzername oder das Passwort konnten nicht erkannt werden\.]")]
        public void ThenUserShouldSeeDerBenutzernameOderDasPasswortKonntenNichtErkanntWerden_()
        {
            IWebElement element = this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenLoginpage.GetAlertIncorrectCredentials()));

            Assert.IsTrue(element.Displayed);
            this.driver.Close();
        }

        [When(@"trying to log into accoung using correct ""(.*)"" format but without a ""(.*)""")]
        public void WhenTryingToLogIntoAccoungUsingCorrectFormatButWithoutA(string p0, string p1)
        {
            this.willhabenLoginpage.SetEMail(p0);
            this.willhabenLoginpage.SetPassword(p1);
            this.willhabenLoginpage.SetSubmitButtonClick();
        }

        [Then(@"user should see \[Bitte gib dein Passwort ein]")]
        public void ThenUserShouldSeeBitteGibDeinPasswortEin()
        {
            IWebElement element = this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenLoginpage.GetAlertNoPasswordAdded()));

            Assert.IsTrue(element.Displayed);
            this.driver.Close();
        }


        [OneTimeTearDown]
        public void Teardown() => this.driver.Quit();
    }
}
