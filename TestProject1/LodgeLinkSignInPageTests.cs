using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;

namespace TestProject1
{
    [TestFixture]
    public class LodgeLinkSignInPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://app.lodgelink.com/en/sign-in/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void VerifyPageTitle()
        {
            Assert.AreEqual("Sign In - LodgeLink", driver.Title);
        }

        [Test]
        public void VerifyHeadingText()
        {
            IWebElement heading = driver.FindElement(By.XPath("//h2[text()='Sign into your account']"));
            Assert.AreEqual("Sign into your account", heading.Text);
        }

        [Test]
        public void VerifyInvalidEmail()
        {
            IWebElement emailField = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            emailField.SendKeys("invalidemail");
            submitButton.Click();
            IWebElement errorText = driver.FindElement(By.CssSelector("[class='jss117 jss118 jss123 jss124']"));
            Assert.AreEqual("Email Address is not a valid email address or is formatted incorrectly.", errorText.Text);
        }

        [Test]
        public void UnknownUser()
        {
            IWebElement emailField = driver.FindElement(By.XPath("//input[@type='email']"));
            emailField.SendKeys("uknown@user.com");
            IWebElement passwordField = driver.FindElement(By.XPath("//input[@type='password']"));
            passwordField.SendKeys("123456");
            IWebElement submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            submitButton.Click();
            Thread.Sleep(4000);
            IWebElement errorText = driver.FindElement(By.XPath("//li[text()='Unknown authentication error.']"));
            Assert.AreEqual("Unknown authentication error.", errorText.Text);
        }
    }
}
