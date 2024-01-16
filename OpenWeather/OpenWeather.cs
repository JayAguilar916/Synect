using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWeather
{
    internal class OpenWeather : Actions
    {
        string url = "https://openweathermap.org/";
        string searchXpath = "//input[contains(@placeholder,'Search city')]";
        string buttonXpath = "//button[contains(@type,'submit')]";
        string signInXpath = "//nav[contains(@id, 'nav-website')]//child::div[1]//child::li[11]//child::a";
        string emailXpath = "//input[contains(@class,'string email optional form-control')]";
        string passwordXpath = "//input[contains(@placeholder,'Password')]";
        string submitXpath = "//input[contains(@value,'Submit')]";
        string logoXpath = "//img[contains(@alt,'Logo white')]";
        string temperatureXpath = "//div[contains(@class,'current-temp')]//child::span";
        string dropDownXpath = "//ul[contains(@class,'search-dropdown-menu')]";
        string errorMessageXpath = "//div[contains(@class,'current-container mobile-padding')]/child::div[1]/child::span";
        string userContainerXpath = "//div[contains(@class,'inner-user-container')]";
        string myAPIkeysXpath = "//ul[contains(@class,'dropdown-menu dropdown-visible')]/child::li[2]/child::a";
        string apiKeyXpath = "//table[contains(@class,'material_table api-keys')]/child::tbody/child::tr[1]/child::td[1]/child::pre";

        public OpenWeather(IWebDriver driver)
        {
           this.driver = driver;
        }

        public void NavigateToOpenWeather()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void LogIn(Users user)
        {
            ClickButton(By.XPath(signInXpath));
            EnterText(By.XPath(emailXpath), user.UserName);
            EnterText(By.XPath(passwordXpath), user.Password);
            ClickButton(By.XPath(submitXpath));
        }

        public string GetAPIKey()
        {
            ClickButton(By.XPath(userContainerXpath));
            ClickButton(By.XPath(myAPIkeysXpath));
            string apiKey = GetText(By.XPath(apiKeyXpath));
            return apiKey;
        }

        public void NavigateToDashboard()
        {
            ClickButton(By.XPath(logoXpath));
        }

        public void SearchForACity(string city)
        {
            EnterText(By.XPath(searchXpath), city);
            ClickButton(By.XPath(buttonXpath));

            Thread.Sleep(3000);

            try
            {
                ClickButton(By.XPath(dropDownXpath));
            } catch (WebDriverException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}"); //exception details

                string displayedCity = GetText(By.XPath(errorMessageXpath));
                string expectedError = $"No results for {city}";

                Assert.AreEqual(expectedError, displayedCity, "Error message mismatch");
                Console.WriteLine($"Expected Error: {expectedError}");
                Console.WriteLine($"Actual Error: {displayedCity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected exception occurred: {ex.Message}"); // Log other exceptions/unexpected exception
            }

        }

        public void CompareTemperature(Users user)
        {
            string displayedTemperatureText = GetText(By.XPath(temperatureXpath));
            int displayedTemperature = int.Parse(displayedTemperatureText.Replace("°C", "").Trim());

            //Fetch the temperature from the API
            int apiTemperature = GetApiTemperature(user);

            //Check if displayed temperature matches the API data
            Assert.AreEqual(apiTemperature, displayedTemperature, "Temperature mismatch");
        }

        // Method to fetch temperature from the API
        private int GetApiTemperature(Users user)
        {
            string apiUrl = "https://api.openweathermap.org/data/2.5/weather";
            string apiKey = user.ApiKey;

            using (HttpClient client = new HttpClient())
            {
                // Construct the API URL with query parameters
                string fullApiUrl = $"{apiUrl}?q={user.City}&appid={apiKey}&units=metric";

                HttpResponseMessage response = client.GetAsync(fullApiUrl).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(responseBody);

                // Assuming the temperature is stored in a property called "main.temp" in the API response
                return (int)json["main"]["temp"];
            }
        }

        public void TemporaryOutput(Users user)
        {
            Console.WriteLine($"{user}");
            Console.WriteLine($"Temperature: {GetText(By.XPath(temperatureXpath))}");
        }

    }
}
