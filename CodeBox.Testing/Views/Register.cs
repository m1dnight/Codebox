using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class Register
    {
        private const string REGISTRATION_PAGE = Common.HOME_URL + "Account/Register";

        [TestInitialize]
        public void NavigateToLogInPage()
        {
            Common.chromeDriver.Navigate().GoToUrl(REGISTRATION_PAGE);
        }

        /// <summary>
        /// Test whether the link to the registration page works
        /// </summary>
        /// <param name="driver"></param>
        private void RegisterContent(IWebDriver driver)
        {
            // Check web elements
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            StringAssert.Contains("Registration", mainContent.FindElement(By.XPath("//h1")).Text);

            mainContent.FindElement(By.Name("Username"));
            IWebElement userNameInput = mainContent.FindElement(By.XPath("//div/form/fieldset/div[2]/input[1]"));
            IWebElement passwordInput = mainContent.FindElement(By.XPath("//div/form/fieldset/div[4]/input[1]"));
            IWebElement emailAddressInput = mainContent.FindElement(By.XPath("//div/form/fieldset/div[6]/input[1]"));
        }

        [TestMethod]
        public void TestRegisterContent()
        {
            RegisterContent(Common.chromeDriver);
        }
    }
}
