using System;
using System.Net.Http;
using Flurl.Http;
using RestSharp;

namespace FlurlDemo
{
    class Program
    {
        private const string BaseUrl = "https://community-open-weather-map.p.rapidapi.com";
        private const string ResourcePath =
            "/forecast/daily?q=bengaluru%2CIN&lat=35&lon=139&cnt=10&units=metric%20or%20imperial";
        private const string XRapidapiHost = "X-RapidAPI-Host";
        private const string XRapidapiKey = "X-RapidAPI-Key";
        private const string XRapidapiHostValue = "community-open-weather-map.p.rapidapi.com";
        private const string XRapidapiKeyValue = "226df10385mshf11f422999ea416p107cf8jsn1495e7e2ea00";

        static void Main(string[] args)
        {
            // First we will see how we have been calling urls using Http CLient
            Console.WriteLine("-----------------------HttpClient Demo---------------------");
            DemoHttpClient();

            Console.WriteLine();
            Console.WriteLine("-----------------------RestSharp Demo---------------------");
            DemoRestSharp();


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-----------------------Flurl Demo---------------------");
            Console.WriteLine();
            var url = BaseUrl + ResourcePath;
            Console.WriteLine(url.WithHeader(XRapidapiHost, XRapidapiHostValue)
                .WithHeader(XRapidapiKey, XRapidapiKeyValue).GetStringAsync().Result);

            Console.Read();
        }

        private static void DemoRestSharp()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(ResourcePath);
            request.AddHeader(XRapidapiHost, XRapidapiHostValue);
            request.AddHeader(XRapidapiKey, XRapidapiKeyValue);
            var response = client.ExecuteGetAsync<string>(request).Result;
            Console.WriteLine();
            Console.Write(response.Content);
        }

        private static void DemoHttpClient()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri(BaseUrl + ResourcePath),
                Headers =
                {
                    {XRapidapiHost, XRapidapiHostValue},
                    {XRapidapiKey, XRapidapiKeyValue},
                },
            };
            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(body);
            }
        }
    }
}