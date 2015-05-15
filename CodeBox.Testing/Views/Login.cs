using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeBox.Testing.Views;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class LogIn
    {

        private const string LOG_IN_PAGE = Common.HOME_URL + "Account/LogIn";

        [TestInitialize]
        public void NavigateToLogInPage()
        {
            Common.chromeDriver.Navigate().GoToUrl(LOG_IN_PAGE);
        }

        /// <summary>
        /// Test whether the link to the login page works
        /// </summary>
        /// <param name="driver"></param>
        [TestMethod]
        private void LogInContent(IWebDriver driver)
        {
            // Check web elements
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            StringAssert.Contains("Authorization", mainContent.FindElement(By.XPath("//h1")).Text);

            mainContent.FindElement(By.Name("Username"));
            IWebElement userNameBox = mainContent.FindElement(By.Id("Username"));
            IWebElement passwordBox = mainContent.FindElement(By.Id("Password"));
            IWebElement submitButton = mainContent.FindElement(By.XPath("//div/form/fieldset/p/input"));
        }

        private void PerformValidLogIn(IWebDriver driver, string userName, string passWord)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement userNameBox = mainContent.FindElement(By.Id("Username"));
            IWebElement passwordBox = mainContent.FindElement(By.Id("Password"));
            IWebElement submitButton = mainContent.FindElement(By.XPath("//div/form/fieldset/p/input"));
            userNameBox.SendKeys(userName);
            passwordBox.SendKeys(passWord);
            submitButton.Click();
        }

        private void CheckMenuLinks(IWebDriver driver, IWebElement menu)
        {
            IWebElement newLink = menu.FindElement(By.XPath("//li[1]/a[2]"));
            StringAssert.Contains("New", newLink.Text);
            StringAssert.Contains("a", newLink.TagName);
            StringAssert.Contains(Common.HOME_URL + "Snippet/Create", newLink.GetAttribute("href"));

            IWebElement codeBoxLink = menu.FindElement(By.XPath("//li[2]/a[2]"));
            StringAssert.Contains("Codebox", codeBoxLink.Text);
            StringAssert.Contains("a", codeBoxLink.TagName);
            StringAssert.Contains(Common.HOME_URL + "Snippet/List", codeBoxLink.GetAttribute("href"));

            IWebElement groupsLink = menu.FindElement(By.XPath("//li[3]/a[2]"));
            StringAssert.Contains("Groups", groupsLink.Text);
            StringAssert.Contains("a", groupsLink.TagName);
            StringAssert.Contains(Common.HOME_URL + "Group", groupsLink.GetAttribute("href"));

            IWebElement managementLink = menu.FindElement(By.XPath("//li[4]/a[2]"));
            StringAssert.Contains("Management", managementLink.Text);
            StringAssert.Contains("a", managementLink.TagName);
            StringAssert.Contains(Common.HOME_URL + "Admin", managementLink.GetAttribute("href"));
        }

        private void ValidLogInAdministrator(IWebDriver driver)
        {
            string userName = "administrator";
            string password = "abc123";
            PerformValidLogIn(driver, userName, password);
            StringAssert.Contains(Common.HOME_URL + "Snippet/List", driver.Url);
            IWebElement topBar = driver.FindElement(By.Id("TopBar"));
            IWebElement menu = topBar.FindElement(By.Id("TopBar"));

            // Check menu bar
            CheckMenuLinks(driver, menu);

            // Check user information bar
            topBar = driver.FindElement(By.Id("TopBar"));
            IWebElement userInformation = topBar.FindElement(By.Id("TopBar"));
            IWebElement accountDetailsLink = userInformation.FindElement(By.XPath("//p/a"));
            IWebElement logOutDetailsLink = userInformation.FindElement(By.XPath("//p/span/a"));
            StringAssert.Contains("[" + userName + "]", accountDetailsLink.Text);
            StringAssert.Contains("Log Out", logOutDetailsLink.Text);
        }

        [TestMethod]
        public void TestLogInContent()
        {
            LogInContent(Common.chromeDriver);
        }

        [TestMethod]
        public void TestValidLogInAdministrator()
        {
            ValidLogInAdministrator(Common.chromeDriver);
        }
    }
}
