using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace NUnitWillhaben1
//Simple NUnit test with hard coded test data, additional to the SpecFlow Tests
{
    [TestFixture]
    public class TestWillhaben
    {
        private IWebDriver driver;
        private WillhabenHomepage willhabenHomepage;
        private WillhabenLoginPage willhabenLoginpage;
        private WebDriverWait wait;


        [OneTimeSetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            this.driver.Navigate().GoToUrl("https://www.willhaben.at/iad");
            
            this.wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(15));

            this.willhabenHomepage = new WillhabenHomepage(this.driver);
            this.willhabenLoginpage = new WillhabenLoginPage(this.driver);

            //accepting cookies in popup element
            this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenHomepage.CookiesAlert)).Click();

            //Click on "Einloggen" link
            this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenHomepage.LoginLink)).Click();

        }

        [Test]
        public void Test()
        {
            //test data are hard coded
            this.willhabenLoginpage.SetEMail("test00000@test.at");
            this.willhabenLoginpage.SetPassword("password");
            this.willhabenLoginpage.SetSubmitButtonClick();

            //Assert.IsTrue(this.willhabenLoginpage.GetAlertIncorrectCredentials().Displayed);
            IWebElement element = this.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this.willhabenLoginpage.GetAlertIncorrectCredentials()));

            Assert.IsTrue(element.Displayed);
        }

        [OneTimeTearDown]
        public void Teardown() => this.driver.Close();
    }
}