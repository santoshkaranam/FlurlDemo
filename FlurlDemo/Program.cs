using System;
using System.Net;
using System.Net.Http;
using Flurl.Http;
using RestSharp;

namespace FlurlDemo
{
    class Program
    {
        // reading weather data from open weather map. If the below keys dont work, please register new API key and use them.
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

            
            //Supports Builder Pattern, simple and effective way to handle
            //url.WithTimeout(100); with timeout
            //url.WithBasicAuth("username", "******");
            //url.AllowHttpStatus(HttpStatusCode.NotFound); allows to add success codes in addition to 2XX, so that exception is not thrown.
            //url.WithOAuthBearerToken("myBearerToken");
            //url.PostJsonAsync() directly POST json object with application/json, same way also for PUT
            //url.GetStreamAsync() get stream and save to file or pass down the stream in web api response.

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