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
        [TestInitialize]
        public void NavigateToRegistrationPage()
        {
            Common.chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Account/Register");
        }

        private void CheckTopError(IWebDriver driver, string expectedError)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement validationErrorsList = mainContent.FindElement(By.XPath("//div[1]/form[1]/div[1]"));
            IWebElement errorMessage = validationErrorsList.FindElement(By.XPath("//ul[1]/li[1]"));
            StringAssert.Contains("validation-summary-errors", errorMessage.GetAttribute("class"));
            StringAssert.Contains(expectedError, errorMessage.Text);
            StringAssert.Contains("field-validation-error", errorMessage.GetAttribute("class"));
            IWebElement passwordInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[4]/input[1]"));
            StringAssert.Contains("", passwordInput.Text);
        }

        private void PerformRegistration(IWebDriver driver, string username, string password, string email)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement userNameInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[2]/input[1]"));
            IWebElement passwordInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[4]/input[1]"));
            IWebElement emailAddressInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[6]/input[1]"));
            IWebElement createButton = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/p[2]/input[1]"));

            userNameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            emailAddressInput.SendKeys(email);
            createButton.Click();
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
            IWebElement userNameInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[2]/input[1]"));
            IWebElement passwordInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[4]/input[1]"));
            IWebElement emailAddressInput = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/div[6]/input[1]"));
            IWebElement createButton = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/p[2]/input[1]"));
            StringAssert.Contains("create", createButton.Text);
            IWebElement homeLink = mainContent.FindElement(By.XPath("//div[1]/a"));
            Common.CheckLink(driver, homeLink, Common.HOME_URL);
        }

        private void RegisterEmptyFields(IWebDriver driver)
        {
            string username = "";
            string password = "";
            string email = "";

            PerformRegistration(driver, username, password, email);

            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));

            IWebElement usernameErrorSpan = mainContent.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[2]/span/span"));
            IWebElement usernameError = usernameErrorSpan.FindElement(By.XPath("//span[1]"));
            StringAssert.Contains("field-validation-error", usernameErrorSpan.GetAttribute("class"));
            StringAssert.Contains("Username must have at least 3 characters and can only contain letters, numbers, - and _.", usernameError.Text);

            IWebElement passwordError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[4]/span/span"));
            StringAssert.Contains("Password must be at least 6 characters long and contain at least one number and one special character.", passwordError.Text);

            IWebElement emailError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[6]/span/span"));
            StringAssert.Contains("The e-mail address provided is invalid. Please check the value and try again.", emailError.Text);
        }

        private void RegisterExistingUser(IWebDriver driver)
        {
            string username = "randomperson007";
            string password = "v3ry_str0ng_password!";
            string email = "random@person.com";

            PerformRegistration(driver, username, password, email);
            CheckTopError(driver, "Username already exists. Please enter a different user name.");
        }

        private void RegisterInvalidPassword(IWebDriver driver)
        {
            string username = "aaa";
            string password = "v";
            string email = "some@email.com";

            PerformRegistration(driver, username, password, email);
            CheckTopError(driver, "Password must be at least 6 characters long and contain at least one number and one special character.");
        }

        private void RegisterInvalidUsername(IWebDriver driver)
        {
            string username = "a";
            string password = "v3ry_str0ng_password!";
            string email = "some@email.com";

            PerformRegistration(driver, username, password, email);
            CheckTopError(driver, "The user name provided is invalid. Please check the value and try again.");
        }

        private void RegisterValidUser(IWebDriver driver)
        {
            int randomNumber = new Random().Next(1000000);
            string username = "dude_" + randomNumber;
            string password = "an0ther#strongPassword";
            string email = "some_" + randomNumber + "@email.com";

            PerformRegistration(driver, username, password, email);
            StringAssert.Contains(Common.HOME_URL + "Account/LogIn", driver.Url);
        }

        [TestMethod]
        public void TestRegisterExistingUser()
        {
            RegisterExistingUser(Common.chromeDriver);
        }

        [TestMethod]
        public void TestRegisterContent()
        {
            RegisterContent(Common.chromeDriver);
        }

        [TestMethod]
        public void TestRegisterEmptyFields()
        {
            RegisterEmptyFields(Common.chromeDriver);
        }

        [TestMethod]
        public void TestRegisterInvalidPassword()
        {
            RegisterInvalidPassword(Common.chromeDriver);
        }

        [TestMethod]
        public void TestRegisterInvalidUsername()
        {
            RegisterInvalidUsername(Common.chromeDriver);
        }

        [TestMethod]
        public void TestRegisterValidUser()
        {
            RegisterValidUser(Common.chromeDriver);
        }
    }
}
