using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;

namespace CurrencyRatesStore
{
    internal class Program
    {
        public static async Task Main(string[] args)

        {
            //Creating of an HTTP client

            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory?.CreateClient();

            //Setup of HTTP request and response

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5");
            HttpResponseMessage response = await httpClient.GetAsync("https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5");

            //Console output of content of HTTP response

            Console.WriteLine("\nContent:");
            string jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Console.WriteLine();

            //Deserialization of response Json string into a list of CurrencyRate objects 

            List<CurrencyRate> currencyRates = JsonConvert.DeserializeObject<List<CurrencyRate>>(jsonString);

            //Creating of a new DbContext instance

            using var crDb = new CrDbContext();

            //-----------------------------------------------

            if (currencyRates != null ) 
            
            {

             //Console output of deserialized Json string as properties of resulting CurrencyRate object

                foreach (var currency in currencyRates)

            {
                Console.WriteLine($"Currency: {currency.Ccy}");
                Console.WriteLine($"Base Currency: {currency.Base_Ccy}");
                Console.WriteLine($"Buy Rate: {currency.Buy}");
                Console.WriteLine($"Sale Rate: {currency.Sale}");
                Console.WriteLine();

            //Adding CurrencyRate object from the list into the database

                    crDb.Currencies.Add( currency );

            }
            crDb.SaveChanges();

            //Saving CurrencyRate objects from the database to the list

                var allRates = crDb.Currencies.ToList();

             //Console output of proprties of a CurrencyRate object from the list of CurrencyRate objects

                foreach ( var currency in allRates ) 
            {
                Console.WriteLine($"Currency Id :{currency.Id} \t Currency : {currency.Ccy} \t Base currency :" +
                    $" {currency.Base_Ccy} \t Buy Rate : {currency.Buy} \t Sale Rate :{currency.Sale}");
            }

            }
        }
    }
}