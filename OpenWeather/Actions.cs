using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather
{
    internal class Actions
    {
        public IWebDriver driver;

        public void EnterText(By locator, string text)
        {
            IWebElement element = driver.FindElement(locator);
            element.SendKeys(text);
        }

        public void ClickButton(By locator)
        {
            IWebElement element = driver.FindElement(locator);
            element.Click();
        }

        public string GetText(By locator)
        {
            IWebElement element = driver.FindElement(locator);
            return element.Text;
        }
    }
}
