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
    }
}
