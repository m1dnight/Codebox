using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    [TestClass]
    public class Common
    {

        public const string HOME_URL = "http://localhost:64886/";
        public static ChromeDriver chromeDriver;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            chromeDriver = new ChromeDriver();
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            chromeDriver.Quit();
        }

        public static void CheckLink(IWebDriver driver, IWebElement link, string expectedUrl)
        {
            link.Click();
            StringAssert.Contains(expectedUrl, driver.Url);
            driver.Navigate().Back();
        }

        public static void PerformLogIn(IWebDriver driver, string userName, string passWord)
        {
            IWebElement mainContent = driver.FindElement(By.Id("ContentMain"));
            IWebElement userNameBox = mainContent.FindElement(By.Id("Username"));
            IWebElement passwordBox = mainContent.FindElement(By.Id("Password"));
            IWebElement submitButton = mainContent.FindElement(By.XPath("//div[1]/form[1]/fieldset[1]/p[1]/input[1]"));
            userNameBox.SendKeys(userName);
            passwordBox.SendKeys(passWord);
            submitButton.Click();
        }

        public static void LogInRandomPerson007()
        {
            chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Account/LogIn");
            string userName = "randomperson007";
            string password = "v3ry_str0ng_password!";
            PerformLogIn(chromeDriver, userName, password);
        }

        public static void LogOut()
        {
            chromeDriver.Navigate().GoToUrl(Common.HOME_URL + "Account/LogIn");
            IWebElement logOutButton = chromeDriver.FindElement(By.XPath("//*[@id=\"logout\"]/a"));
            logOutButton.Click();
        }

    }
}
