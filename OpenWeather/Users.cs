using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather
{
    internal class Users
    {
        private string username;
        private string password;
        private string city;
        private string apiKey;

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string City {  get; private set; }
        public string ApiKey { get; set; }
        public Users(string userName, string password, string city)
        {
            UserName = userName;
            Password = password;
            City = city;
        }

        public override string ToString()
        {
            return $"City: {City}";
        }

    }
}
