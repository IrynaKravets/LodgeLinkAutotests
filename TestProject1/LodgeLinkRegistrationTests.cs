using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;

namespace TestProject1
{
    [TestFixture]
    public class LodgeLinkRegistrationTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://app.lodgelink.com/en/sign-up/verify-email/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void VerifyPageTitle()
        {
            Assert.AreEqual("Join Now - LodgeLink", driver.Title);
        }

        [Test]
        public void VerifyHeadingText()
        {
            IWebElement heading = driver.FindElement(By.XPath("//h2[@class]"));
            Assert.AreEqual("Let’s get you started", heading.Text);
        }

        [Test]
        public void VerifySignInLink()
        {
            IWebElement signinLink = driver.FindElement(By.XPath("//a[@href='/en/sign-in']"));
            Assert.IsTrue(signinLink.Displayed && signinLink.Enabled);
        }

        [Test]
        public void VerifyInvalidEmail()
        {
            IWebElement emailField = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            emailField.SendKeys("invalidemail");
            submitButton.Click();
            IWebElement errorText = driver.FindElement(By.CssSelector("[class='jss49 jss50 jss55 jss56']"));
            Assert.AreEqual("Email Address is not a valid email address or is formatted incorrectly.", errorText.Text);
        }

        [Test]
        public void EnterValidEmail()
        {
            
            IWebElement emailField = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            emailField.SendKeys("test@lodgelink.com");
            submitButton.Click();
            Thread.Sleep(4000);
            IWebElement succesfulltext = driver.FindElement(By.XPath("//h2[text()='You’ve got mail!']"));
            Assert.AreEqual("You’ve got mail!", succesfulltext.Text);
        }

        
    }
}
