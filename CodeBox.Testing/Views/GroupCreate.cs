using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class GroupCreate
    {

        [TestInitialize]
        public void LogIn()
        {
            Common.LogInRandomPerson007();
            GoToGroupCreateView();
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        private static void GoToGroupCreateView()
        {
            Common.GoToUrl(Common.HOME_URL + "Group/Create");
        }

        public static void PerformCreation(IWebDriver driver, SimpleGroup group)
        {
            GoToGroupCreateView();
            IWebElement nameBox = driver.FindElement(By.XPath("//*[@id=\"Name\"]"));
            IWebElement descriptionBox = driver.FindElement(By.XPath("//*[@id=\"Description\"]"));
            IWebElement createButton = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p/input"));
            nameBox.SendKeys(group.Name());
            descriptionBox.SendKeys(group.Description());
            createButton.Click();
        }

        private void GroupCreateContent(IWebDriver driver)
        {
            IWebElement titleElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/h2"));
            IWebElement groupElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/legend"));
            IWebElement nameElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[1]/label"));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[3]/label"));
            IWebElement listLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/a"));
            IWebElement createButton = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p/input"));
            driver.FindElement(By.XPath("//*[@id=\"Name\"]"));
            driver.FindElement(By.XPath("//*[@id=\"Description\"]"));

            StringAssert.Contains("Create", titleElement.Text);
            StringAssert.Contains("Group", groupElement.Text);
            StringAssert.Contains("Name", nameElement.Text);
            StringAssert.Contains("Description", descriptionElement.Text);
            StringAssert.Contains("Create", createButton.Text);

            Common.CheckLink(driver, listLink, Common.HOME_URL + "Group");
        }

        private void GroupCreateEmptyName(IWebDriver driver)
        {
            SimpleGroup group = new SimpleGroup("description", "");
            PerformCreation(driver, group);

            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[2]/span/span"));
            StringAssert.Contains("Name required!", errorMessage.Text);
        }

        private void CheckValidGroupCreation(IWebDriver driver, SimpleGroup group)
        {
            PerformCreation(driver, group);
            StringAssert.Contains(Common.HOME_URL + "Group", driver.Url);

            //*[@id="ContentMain"]/div/table/tbody/tr[2]/td[1]/a
            IWebElement name = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[1]/a"));
            IWebElement description = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[2]"));

            StringAssert.Contains(group.Name(), name.Text);
            StringAssert.Contains(group.Description(), description.Text);
        }

        private void GroupCreateValid1(IWebDriver driver)
        {
            SimpleGroup group = new SimpleGroup("description", "name");
            CheckValidGroupCreation(driver, group);

            IWebElement editLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[4]/a[1]"));
            IWebElement deleteLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[4]/a[2]"));
            IWebElement leaveLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr[last()]/td[4]/a[3]"));

            StringAssert.Contains(editLink.Text, "Edit");
            StringAssert.StartsWith(editLink.GetAttribute("href"), Common.HOME_URL + "Group/Edit/");
            StringAssert.Contains(deleteLink.Text, "Delete");
            StringAssert.StartsWith(deleteLink.GetAttribute("href"), Common.HOME_URL + "Group/Delete/");
            StringAssert.Contains(leaveLink.Text, "Leave");
            StringAssert.StartsWith(leaveLink.GetAttribute("href"), Common.HOME_URL + "Group/LeaveGroup/");
        }

        private void GroupCreateValid2(IWebDriver driver)
        {
            SimpleGroup group = new SimpleGroup("?A? Long_AND#C0mplex D3scription!", "LO000ng_-#'NAME!");
            CheckValidGroupCreation(driver, group);
        }

        [TestMethod]
        public void TestGroupCreateContent()
        {
            GroupCreateContent(Common.chromeDriver);
        }

        [TestMethod]
        public void TestGroupCreateEmptyName()
        {
            GroupCreateEmptyName(Common.chromeDriver);
        }

        [TestMethod]
        public void TestGroupCreateValid1()
        {
            GroupCreateValid1(Common.chromeDriver);
        }

        [TestMethod]
        public void TestGroupCreateValid2()
        {
            GroupCreateValid2(Common.chromeDriver);
        }

    }
}
