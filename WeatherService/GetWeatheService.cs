using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public class GetWeatheService
    {
        private string apiConnectString = "https://api.openweathermap.org/data/2.5/weather";
        private string apiKey = "87755fff44a3bc0f44d2fc9f6319a18c";

        public Root GetWeather(string city)
        {
            var client = new RestClient(apiConnectString);
            var request = new RestRequest(Method.GET);
            request.AddParameter("q", city);
            request.AddParameter("apiKey", apiKey);
            var root = client.Execute<Root>(request);
            if(root.IsSuccessful)
            {
                return root.Data;
            }
            return null;
            //var res = request.Resource;
        }
    }
}
