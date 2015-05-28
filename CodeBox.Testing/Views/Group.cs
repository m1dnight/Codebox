using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class Group
    {

        [TestInitialize]
        public void LogIn()
        {
            Common.LogInRandomPerson007();
            Common.GoToUrl(Common.HOME_URL + "Group");
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        private void GroupContent(IWebDriver driver)
        {
            IWebElement titleElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/h1"));
            IWebElement nameElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr/th[1]"));
            IWebElement descriptionElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr/th[2]"));
            IWebElement adminElement = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/table/tbody/tr/th[3]"));
            IWebElement createGroupLink = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/p/a"));

            StringAssert.Contains("Group Management", titleElement.Text);
            StringAssert.Contains("Name", nameElement.Text);
            StringAssert.Contains("Description", descriptionElement.Text);
            StringAssert.Contains("Admin?", adminElement.Text);

            Common.CheckLink(driver, createGroupLink, Common.HOME_URL + "Group/Create");
        }

        [TestMethod]
        public void TestGroupContent()
        {
            GroupContent(Common.chromeDriver);
        }

    }
}
