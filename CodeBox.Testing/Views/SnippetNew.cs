using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class SnippetNew
    {

        [TestInitialize]
        public void LogIn()
        {
            Common.LogInRandomPerson007();
            Common.chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Snippet/Create");
        }

        [TestCleanup]
        public void LogOut()
        {
            Common.LogOut();
        }

        public static void CreateSnippet(IWebDriver driver, SimpleSnippet snippet)
        {
            Common.chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Snippet/Create");

            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            IWebElement codeBox = mainContent.FindElement(By.Id("Snippet_Code"));
            IWebElement publicCheckmark = mainContent.FindElement(By.Id("Snippet_Public"));
            IWebElement languageBox = mainContent.FindElement(By.Id("SelectedLanguageId"));
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));

            nameBox.SendKeys(snippet._name);
            descriptionBox.SendKeys(snippet._description);
            codeBox.SendKeys(snippet._code);
            if (snippet._isPublic)
            {
                publicCheckmark.Click();
            }
            SelectElement languagesSelect = new SelectElement(languageBox);
            languagesSelect.SelectByText(snippet._language);
            saveButton.Click();
        }

        private void CheckSnippet(IWebDriver driver, string code, string description, string name, string language)
        {
            string capitalizedName = name.First().ToString().ToUpper() + String.Join("", name.Skip(1));
            IWebElement descriptionText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[4]/p"));
            IWebElement languageText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[2]"));
            IWebElement nameText = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/div[1]/div[3]/a"));
            IWebElement insertionSucceededMessage = driver.FindElement(By.Id("message"));
            StringAssert.Contains(description, descriptionText.Text);
            StringAssert.Contains(language, languageText.Text);
            StringAssert.Contains(capitalizedName + " (0)", nameText.Text);
            StringAssert.Contains(name + " has been saved!", insertionSucceededMessage.Text);
        }

        private void DeleteSnippet(IWebDriver driver, int snippetNr)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            string xPath = "//div/div[" + snippetNr + "]/div[2]/a[2]";
            IWebElement selectedSnippetDeleteLink = mainContent.FindElement(By.XPath(xPath));
            string link = selectedSnippetDeleteLink.GetAttribute("href");
            driver.Navigate().GoToUrl(Common.HOME_URL + link.Substring(1));
        }

        private void SnippetNewContent(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            StringAssert.Contains("New Snippet", mainContent.FindElement(By.XPath("//h1[1]")).Text);

            StringAssert.Contains("Name", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[1]/label")).Text);
            StringAssert.Contains("Description", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[3]/label")).Text);
            StringAssert.Contains("Code", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[5]/label")).Text);
            StringAssert.Contains("Public", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[1]")).Text);
            StringAssert.Contains("Syntax:", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[3]")).Text);
            StringAssert.Contains("Share with group (optional):", driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/p[5]")).Text);
            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            IWebElement codeBox = mainContent.FindElement(By.Id("Snippet_Code"));
            IWebElement publicCheckmark = mainContent.FindElement(By.Id("Snippet_Public"));
            IWebElement languageBox = mainContent.FindElement(By.Id("SelectedLanguageId"));
            IWebElement groupBox = mainContent.FindElement(By.Id("SelectedGroupId"));
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));
            IWebElement listLink = mainContent.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/div/a"));
            Common.CheckLink(driver, listLink, Common.HOME_URL + "Snippet/List");
        }

        private void SnippetNewEmptyCodeInsert(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement nameBox = mainContent.FindElement(By.Id("Snippet_Name"));
            IWebElement descriptionBox = mainContent.FindElement(By.Id("Snippet_Description"));
            nameBox.SendKeys("a");
            descriptionBox.SendKeys("a");
            IWebElement saveButton = mainContent.FindElement(By.Id("SnippetSubmit"));
            saveButton.Click();

            IWebElement codeError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[6]/span"));
            StringAssert.Contains("No empty snippets allowed!", codeError.Text);
        }

        private void SnippetNewEmptyNameInsert(IWebDriver driver)
        {
            SimpleSnippet snippet = new SimpleSnippet("", "", "a");
            CreateSnippet(driver, snippet);

            IWebElement nameError = driver.FindElement(By.XPath("//*[@id=\"ContentMain\"]/div/form/fieldset/div[2]/span/span"));
            StringAssert.Contains("A Name is required!", nameError.Text);
        }

        private void SnippetNewValidInsert(IWebDriver driver)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));


            string code = "a";
            string description = "a";
            string name = "a";
            SimpleSnippet snippet = new SimpleSnippet(name, code, description);
            CreateSnippet(driver, snippet);

            StringAssert.Contains(Common.HOME_URL + "Snippet/List", driver.Url);
            CheckSnippet(driver, code, description, name, "None");
            DeleteSnippet(driver, 1);
        }

        [TestMethod]
        public void TestSnippetNewContent()
        {
            SnippetNewContent(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetNewEmptyCodeInsert()
        {
            SnippetNewEmptyCodeInsert(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetNewEmptyNameInsert()
        {
            SnippetNewEmptyNameInsert(Common.chromeDriver);
        }

        [TestMethod]
        public void TestSnippetNewValidInsert()
        {
            SnippetNewValidInsert(Common.chromeDriver);
        }
    }
}
