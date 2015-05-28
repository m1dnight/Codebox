using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class UserEdit
    {

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        private void CheckUserAccount(IWebDriver driver, SimpleUser expectedUser)
        {
            IWebElement firstNameBox = driver.FindElement(By.Id("Name"));
            IWebElement lastNameBox = driver.FindElement(By.Id("Surname"));
            IWebElement emailBox = driver.FindElement(By.Id("Mail"));

            StringAssert.Contains(expectedUser.firstName, firstNameBox.Text);
            StringAssert.Contains(expectedUser.lastName, lastNameBox.Text);
            StringAssert.Contains(expectedUser.email, emailBox.Text);
        }

        private void CreateEditAndCheckUserAccount(IWebDriver driver, SimpleUser editedUser)
        {
            RegisterInfo randomRegisterInfo = Register.CreateRandomRegisterInfo("h0rse!Battery?XKCD");
            Register.PerformRegistration(driver, randomRegisterInfo);
            Common.PerformLogIn(driver, randomRegisterInfo.username, randomRegisterInfo.password);
            GoToUserEditView(driver);

            PerformEditUserAccount(driver, editedUser);
            CheckUserAccount(driver, editedUser);
            IWebElement editSuccessfulMessage = driver.FindElement(By.XPath("//*[@id=\"message\"]"));
            string expectedMessage = randomRegisterInfo.username + ", your profile has been updated!";
            StringAssert.Contains(expectedMessage, editSuccessfulMessage.Text);
        }

        private void DoInvalidPasswordTest(IWebDriver driver, SimpleUser editedUser)
        {
            Common.LogInRandomPerson007();
            GoToUserEditView(driver);
            PerformEditUserAccount(driver, editedUser);
            IWebElement incorrectPasswordMessage = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/div/ul/li"));
            StringAssert.Contains("Password did not change! Is it strong (6 characters, 1 number and 1 special character) enough?", incorrectPasswordMessage.Text);
        }

        private static void GoToUserEditView(IWebDriver driver)
        {
            IWebElement linkToAccountEdit = driver.FindElement(By.XPath("//*[@id=\"username\"]/a"));
            linkToAccountEdit.Click();
        }

        private void PerformEditUserAccount(IWebDriver driver, SimpleUser user)
        {
            IWebElement firstNameBox = driver.FindElement(By.Id("Name"));
            IWebElement lastNameBox = driver.FindElement(By.Id("Surname"));
            IWebElement oldPasswordBox = driver.FindElement(By.Id("OldPassword"));
            IWebElement passwordBox = driver.FindElement(By.Id("Password"));
            IWebElement emailBox = driver.FindElement(By.Id("Mail"));
            IWebElement saveButton = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[3]/input"));

            firstNameBox.Clear();
            firstNameBox.SendKeys(user.firstName);

            lastNameBox.Clear();
            lastNameBox.SendKeys(user.lastName);

            oldPasswordBox.Clear();
            oldPasswordBox.SendKeys(user.oldPassword);

            passwordBox.Clear();
            passwordBox.SendKeys(user.newPassword);

            emailBox.Clear();
            emailBox.SendKeys(user.email);
            saveButton.Click();
        }

        private void UserEditContent(IWebDriver driver)
        {
            Common.LogInRandomPerson007();
            GoToUserEditView(driver);

            IWebElement titleElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/h1"));
            IWebElement subTitleElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/legend"));

            IWebElement nameText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[1]/label"));
            IWebElement surnameText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[3]/label"));
            IWebElement oldPasswordText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[5]/label"));
            IWebElement newPasswordText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[7]/label"));
            IWebElement mailText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[9]/label"));
            IWebElement avatarText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[11]"));

            IWebElement firstNameBox = driver.FindElement(By.Id("Name"));
            IWebElement lastNameBox = driver.FindElement(By.Id("Surname"));
            IWebElement oldPasswordBox = driver.FindElement(By.Id("OldPassword"));
            IWebElement passwordBox = driver.FindElement(By.Id("Password"));
            IWebElement emailBox = driver.FindElement(By.Id("Mail"));

            IWebElement homeLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/a"));
            Common.CheckLink(driver, homeLink, Common.HOME_URL);
        }

        private void UserEditInvalidEmail(IWebDriver driver)
        {
            Common.LogInRandomPerson007();
            GoToUserEditView(driver);
            SimpleUser incorrectEmail = new SimpleUser("", "", "randomemail.net");
            PerformEditUserAccount(driver, incorrectEmail);
            IWebElement incorrectEmailMessage = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[10]/span/span"));
            StringAssert.Contains("Email address is not valid!", incorrectEmailMessage.Text);
        }

        private void UserEditInvalidNewPassword(IWebDriver driver)
        {
            SimpleUser incorrectNewPassword = new SimpleUser("Random", "Person", "random@email.net", "new_v3ry_str0ng_password!", "a");
            DoInvalidPasswordTest(driver, incorrectNewPassword);
        }

        private void UserEditInvalidOldPassword(IWebDriver driver)
        {
            SimpleUser incorrectOldPassword = new SimpleUser("Random", "Person", "random@email.net", "a", "new_v3ry_str0ng_password!");
            DoInvalidPasswordTest(driver, incorrectOldPassword);
        }

        private void UserEditValid1(IWebDriver driver)
        {
            SimpleUser editedUser = new SimpleUser("something", "random", "random@email.net", "", "");
            CreateEditAndCheckUserAccount(driver, editedUser);
        }

        private void UserEditValid2(IWebDriver driver)
        {
            SimpleUser editedUser = new SimpleUser("a completely different first name", "a completely different last name", "a_completely_different@email.net", "", "");
            CreateEditAndCheckUserAccount(driver, editedUser);
        }

        private void UserEditValidUploadAvatar(IWebDriver driver)
        {
            Common.LogInRandomPerson007();
            string pathToImage = "C:\\Maarten\\2 Ma\\Thesis\\Thesis\\Figures\\CESK_tracing_interface original.png";
            GoToUserEditView(driver);

            IWebElement avatarSelectionButton = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[1]/input"));
            IWebElement saveButton = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[2]/input"));

            avatarSelectionButton.SendKeys(pathToImage);
            saveButton.Click();

            IWebElement message = driver.FindElement(By.XPath("//*[@id=\"message\"]"));
            StringAssert.Contains("randomperson007, your profile has been updated!", message.Text);

            Common.GoToUrl(Common.HOME_URL);
            IWebElement avatar = driver.FindElement(By.XPath("//*[@id=\"UserAvatar\"]"));
            StringAssert.Contains(avatar.TagName, "img");
        }

        [TestMethod]
        public void TestUserEditContent()
        {
            UserEditContent(Common.chromeDriver);
        }

        [TestMethod]
        public void TestUserEditInvalidEmail()
        {
            UserEditInvalidEmail(Common.chromeDriver);
        }

        [TestMethod]
        public void TestUserEditInvalidNewPassword()
        {
            UserEditInvalidNewPassword(Common.chromeDriver);
        }

        [TestMethod]
        public void TestUserEditInvalidOldPassword()
        {
            UserEditInvalidOldPassword(Common.chromeDriver);
        }

        [TestMethod]
        public void TestUserEditValid1()
        {
            UserEditValid1(Common.chromeDriver);
        }

        [TestMethod]
        public void TestUserEditValid2()
        {
            UserEditValid2(Common.chromeDriver);
        }

        [TestMethod]
        public void TestUserEditValidUploadAvatar() {
            UserEditValidUploadAvatar(Common.chromeDriver);
        }

    }
}
