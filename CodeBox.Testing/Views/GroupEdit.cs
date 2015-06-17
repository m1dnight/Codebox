using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class GroupEdit
    {

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        private void GoToLastGroupEditView(IWebDriver driver)
        {
            IWebElement link = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[4]/a[1]"));
            link.Click();
        }

        private void GoToLastGroupShowView(IWebDriver driver)
        {
            IWebElement link = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[1]/a[1]"));
            link.Click();
        }

        private void CheckGroup(IWebDriver driver, SimpleGroup group)
        {
            IWebElement nameElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[2]"));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/fieldset/div[4]"));

            StringAssert.Contains(nameElement.Text, group.Name());
            StringAssert.Contains(descriptionElement.Text, group.Description());
        }

        private void CreateEditAndCheckGroup(IWebDriver driver, SimpleGroup editedGroup)
        {
            Common.LogInRandomPerson007();
            SimpleGroup group = new SimpleGroup("description", "name");
            GroupCreate.PerformCreation(driver, group);
            GoToLastGroupEditView(driver);

            PerformEditGroup(driver, editedGroup);

            IWebElement editSuccessfulMessage = driver.FindElement(By.XPath("//*[@id=\"message\"]"));
            string expectedMessage = "Group '" + editedGroup.Name() + "' has been updated!";
            StringAssert.Contains(expectedMessage, editSuccessfulMessage.Text);

            driver.Navigate().GoToUrl(Common.HOME_URL + "Group");
            GoToLastGroupShowView(driver);
            CheckGroup(driver, editedGroup);
        }

        private void PerformAddUserToGroup(IWebDriver driver, string email)
        {
            IWebElement emailBox = driver.FindElement(By.XPath("//*[@id=\"Mail\"]"));
            IWebElement addButton = driver.FindElement(By.XPath("//*[@id=\"adduser\"]/form/fieldset/p/input"));

            emailBox.Clear();
            emailBox.SendKeys(email);
            addButton.Click();
        }

        private void PerformEditGroup(IWebDriver driver, SimpleGroup editedGroup)
        {
            IWebElement nameBox = driver.FindElement(By.XPath("//*[@id=\"Name\"]"));
            IWebElement descriptionBox = driver.FindElement(By.XPath("//*[@id=\"Description\"]"));
            IWebElement saveButton = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/pno/p[2]/input"));
            nameBox.Clear();
            descriptionBox.Clear();

            nameBox.SendKeys(editedGroup.Name());
            descriptionBox.SendKeys(editedGroup.Description());
            saveButton.Click();
        }

        private void GroupEditAddComplete(IWebDriver driver)
        {
            SimpleGroup group = new SimpleGroup("add complete test description", "add complete test name");
            SimpleSnippet snippet = new SimpleSnippet("some test name", "some test code", "some test desc", "Java", true, "add complete test name");

            Common.LogInRandomPerson007();
            GroupCreate.PerformCreation(driver, group);
            string email = "random2@person.com";
            Group.GoToGroupListView();
            GoToLastGroupEditView(driver);
            PerformAddUserToGroup(driver, email);

            SnippetNew.CreateSnippet(driver, snippet);

            Common.LogOut();
            Common.LogInRandomPerson008();

            Group.GoToLastGroupView(driver);

            IWebElement allSnippetsLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/p/a[2]"));
            allSnippetsLink.Click();

            IWebElement snippetLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[3]/a"));
            snippetLink.Click();

            SnippetView.CheckSnippet(driver, snippet);

        }

        private void GroupEditAddNonExistingUser(IWebDriver driver)
        {
            string nonExistingEmail = "doesnot@exist.com";
            Common.LogInRandomPerson007();
            Group.GoToGroupListView();
            GoToLastGroupEditView(driver);

            PerformAddUserToGroup(driver, nonExistingEmail);

            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@id=\"error\"]"));
            string expectedErrorMessageText = "A user for " + nonExistingEmail + " was not found!";
            StringAssert.Contains(errorMessage.Text, expectedErrorMessageText);
        }

        private void GroupEditAddUserValid(IWebDriver driver)
        {
            string email = "random2@person.com";
            Common.LogInRandomPerson007();
            Group.GoToGroupListView();
            GoToLastGroupEditView(driver);
            PerformAddUserToGroup(driver, email);

            IWebElement message = driver.FindElement(By.XPath("//*[@id=\"message\"]"));
            StringAssert.StartsWith(message.Text, "randomperson008 has been added to ");
        }

        private void GroupEditValid(IWebDriver driver)
        {
            SimpleGroup editedGroup = new SimpleGroup("a new description", "a new name");
            CreateEditAndCheckGroup(driver, editedGroup);
        }

        [TestMethod]
        public void TestGroupEditAddComplete()
        {
            GroupEditAddComplete(Common.chromeDriver);
        }

        [TestMethod]
        public void TestGroupEditAddNonExistingUser()
        {
            GroupEditAddNonExistingUser(Common.chromeDriver);
        }

        [TestMethod]
        public void TestGroupEditAddUserValid()
        {
            GroupEditAddUserValid(Common.chromeDriver);
        }

        [TestMethod]
        public void TestGroupEditValid()
        {
            GroupEditValid(Common.chromeDriver);
        }


    }
}
