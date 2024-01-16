using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;

namespace OpenWeather
{
    [TestClass]
    public class TestClass
    {

        private IWebDriver driver;
        private OpenWeather openWeather;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Set implicit wait to 10 seconds
            driver.Manage().Window.Maximize();
            openWeather = new OpenWeather(driver);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestCondition11()
        {
            Users user = new Users("test01@email.com", "password01", "Toronto, CA");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            user.ApiKey = openWeather.GetAPIKey();

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);

            WaitInSeconds(2);
            openWeather.CompareTemperature(user);
        }

        [TestMethod]
        public void TestCondition12()
        {
            Users user = new Users("test02@email.com", "password02", "Vancouver, CA");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            user.ApiKey = openWeather.GetAPIKey();

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);

            WaitInSeconds(2);
            openWeather.CompareTemperature(user);
        }

        [TestMethod]
        public void TestCondition13()
        {
            Users user = new Users("test03@email.com", "password03", "Pickering, CA");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            user.ApiKey = openWeather.GetAPIKey();

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);

            WaitInSeconds(2);
            openWeather.CompareTemperature(user);
        }

        [TestMethod]
        public void TestCondition14()
        {
            Users user = new Users("test04@email.com", "password04", "Ottawa, CA");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            user.ApiKey = openWeather.GetAPIKey();

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);

            WaitInSeconds(2);
            openWeather.CompareTemperature(user);
        }

        [TestMethod]
        public void TestCondition15()
        {
            Users user = new Users("test05@email.com", "password05", "Calgary, CA");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            user.ApiKey = openWeather.GetAPIKey();

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);

            WaitInSeconds(2);
            openWeather.CompareTemperature(user);
        }

        [TestMethod]
        public void TestCondition21()
        {
            Users user = new Users("test06@email.com", "password06", "Invalid City 01");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);
        }

        [TestMethod]
        public void TestCondition22()
        {
            Users user = new Users("test07@email.com", "password07", "Invalid City 02");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);
        }

        [TestMethod]
        public void TestCondition23()
        {
            Users user = new Users("test08@email.com", "password08", "Invalid City 03");
            openWeather.NavigateToOpenWeather();

            WaitInSeconds(5);
            openWeather.LogIn(user);

            openWeather.NavigateToDashboard();

            WaitInSeconds(5);
            openWeather.SearchForACity(user.City);
        }

        public void WaitInSeconds(int time)
        {
            int seconds = time * 1000;
            Thread.Sleep(seconds);
        }

    }
}
