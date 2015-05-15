using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeBox.Testing.Views;
using CodeBox.WebUI.Models.Account;

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

        private void LogOut()
        {
            IWebElement logOutButton = Common.chromeDriver.FindElement(By.XPath("//*[@id=\"logout\"]/a"));
            logOutButton.Click();
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
            StringAssert.Contains("Authorization", mainContent.FindElement(By.XPath("//h1[1]")).Text);

            mainContent.FindElement(By.Name("Username"));
            IWebElement userNameBox = mainContent.FindElement(By.Id("Username"));
            IWebElement passwordBox = mainContent.FindElement(By.Id("Password"));
            IWebElement submitButton = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/p[1]/input[1]"));
            IWebElement homeLink = mainContent.FindElement(By.XPath("//div[1]/a"));
            Common.CheckLink(driver, homeLink, Common.HOME_URL);
        }

        private void PerformLogIn(IWebDriver driver, string userName, string passWord)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement userNameBox = mainContent.FindElement(By.Id("Username"));
            IWebElement passwordBox = mainContent.FindElement(By.Id("Password"));
            IWebElement submitButton = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/p[1]/input[1]"));
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
        }

        private void ValidLogInAdministrator(IWebDriver driver)
        {
            string userName = "administrator";
            string password = "abc123";
            PerformLogIn(driver, userName, password);
            StringAssert.Contains(Common.HOME_URL + "Snippet/List", driver.Url);
            IWebElement topBar = driver.FindElement(By.Id("TopBar"));
            IWebElement menu = topBar.FindElement(By.Id("TopBar"));

            // Check menu bar
            CheckMenuLinks(driver, menu);

            IWebElement managementLink = menu.FindElement(By.XPath("//li[4]/a[2]"));
            StringAssert.Contains("Management", managementLink.Text);
            StringAssert.Contains("a", managementLink.TagName);
            StringAssert.Contains(Common.HOME_URL + "Admin", managementLink.GetAttribute("href"));

            // Check user information bar
            topBar = driver.FindElement(By.Id("TopBar"));
            IWebElement userInformation = topBar.FindElement(By.Id("TopBar"));
            IWebElement accountDetailsLink = userInformation.FindElement(By.XPath("//p[1]/a[1]"));
            IWebElement logOutDetailsLink = userInformation.FindElement(By.XPath("//p[1]/span[1]/a[1]"));
            StringAssert.Contains("[" + userName + "]", accountDetailsLink.Text);
            StringAssert.Contains("Log Out", logOutDetailsLink.Text);

            LogOut();
        }

        private void ValidLogInRegular(IWebDriver driver)
        {
            string userName = "randomperson007";
            string password = "v3ry_str0ng_password!";
            PerformLogIn(driver, userName, password);

            StringAssert.Contains(Common.HOME_URL + "Snippet/List", driver.Url);
            IWebElement topBar = driver.FindElement(By.Id("TopBar"));
            IWebElement menu = topBar.FindElement(By.Id("TopBar"));

            // Check menu bar
            CheckMenuLinks(driver, menu);

            LogOut();
        }

        private void InvalidLogInPassword(IWebDriver driver)
        {
            string userName = "randomperson007";
            string password = "wrong";
            PerformLogIn(driver, userName, password);

            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement errorMessage = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[4]/span[1]"));
            StringAssert.Contains("The password is not correct!", errorMessage.Text);
            StringAssert.Contains("field-validation-error", errorMessage.GetAttribute("class"));
        }

        private void InvalidLogInUsername(IWebDriver driver)
        {
            string userName = "doesnotexist";
            string password = "whatever";
            PerformLogIn(driver, userName, password);
            
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement errorMessage = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[2]/span[1]"));
            StringAssert.Contains("User does not exist!", errorMessage.Text);
            StringAssert.Contains("field-validation-error", errorMessage.GetAttribute("class"));
        }

        [TestMethod]
        public void TestInvalidLogInPassword()
        {
            InvalidLogInPassword(Common.chromeDriver);
        }

        [TestMethod]
        public void TestInvalidLogInUsername()
        {
            InvalidLogInUsername(Common.chromeDriver);
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

        [TestMethod]
        public void TestValidLogInRegular()
        {
           ValidLogInRegular(Common.chromeDriver);
        }
    }
}
