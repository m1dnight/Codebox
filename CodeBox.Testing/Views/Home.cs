using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using CodeBox.Testing.Views;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class Home
    {

        /// <summary>
        /// Test whether all web-elements are correctly present on
        /// the home-screen visible to a user that is not logged in.
        /// </summary>
        /// <param name="driver"></param>
        private void homeContents(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(Common.HOME_URL);
            IWebElement topBar = driver.FindElement(By.Id("TopBar"));
            IWebElement menu = topBar.FindElement(By.Id("menu"));
            IWebElement logInLink = driver.FindElement(By.LinkText("Log In"));
            IWebElement signUpLink = driver.FindElement(By.LinkText("Sign Up"));
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            StringAssert.Contains(Common.HOME_URL + "Account/LogIn", logInLink.GetAttribute("href"));
            StringAssert.Contains(Common.HOME_URL + "Account/Register", signUpLink.GetAttribute("href"));
            StringAssert.Contains("CodeBox v1.0s", mainContent.FindElement(By.XPath("//h1")).Text);
        }

        /// <summary>
        /// Test whether the link to the login page works
        /// </summary>
        /// <param name="driver"></param>
        private void loginLink(IWebDriver driver)
        {
            //Navigate to login page
            driver.Navigate().GoToUrl(Common.HOME_URL);
            IWebElement logInLink = driver.FindElement(By.LinkText("Log In"));
            Common.CheckLink(driver, logInLink, Common.HOME_URL + "Account/LogIn");
        }

        /// <summary>
        /// Test whether the link to the registration page works
        /// </summary>
        /// <param name="driver"></param>
        private void SignUpLink(IWebDriver driver)
        {
            //Navigate to login page
            driver.Navigate().GoToUrl(Common.HOME_URL);
            IWebElement signUpLink = driver.FindElement(By.LinkText("Sign Up"));
            signUpLink.Click();
            StringAssert.Contains(Common.HOME_URL + "Account/Register", driver.Url);
        }

        [TestMethod]
        public void testHomeContents()
        {
            homeContents(Common.chromeDriver);
        }

        [TestMethod]
        public void testLoginlink()
        {
            loginLink(Common.chromeDriver);
        }

        [TestMethod]
        public void testSignUplink()
        {
            SignUpLink(Common.chromeDriver);
        }

    }
}
