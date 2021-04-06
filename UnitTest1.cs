using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace NUnitWillhaben1
//Simple NUnit test with hard coded test data, additional to the SpecFlow Tests
{
    [TestFixture]
    public class TestWillhaben
    {
        private IWebDriver _driver;
        private WillhabenHomepage _willhabenHomepage;
        private WillhabenLoginPage _willhabenLoginpage;
        private WebDriverWait _wait;


        [OneTimeSetUp]
        public void Setup()
        {

            //this._driver = new ChromeDriver();//without docker

            var options = new ChromeOptions();//in case of docker implementation
            this._driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub/"), options);//in case of docker implementation

            this._driver.Navigate().GoToUrl("https://www.willhaben.at/iad");//LT
            //System.Threading.Thread.Sleep(10000);

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

        [Test]
        public void Test()
        {
            //test data are hard coded
            this._willhabenLoginpage.SetEMail("test00000@test.at");
            this._willhabenLoginpage.SetPassword("password");
            this._willhabenLoginpage.SetSubmitButtonClick();

            //Assert.IsTrue(this.willhabenLoginpage.GetAlertIncorrectCredentials().Displayed);
            IWebElement element = this._wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(this._willhabenLoginpage.GetAlertIncorrectCredentials()));

            Assert.IsTrue(element.Displayed);
        }

        [OneTimeTearDown]
        public void Teardown() => this._driver.Close();
    }
}