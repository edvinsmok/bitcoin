using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitCoin = GetRates();
            Console.WriteLine($"Current rate: {currentBitCoin.bpi.EUR.code}  {currentBitCoin.bpi.EUR.rate_float}");
            // Console.WriteLine($"Current rate: {currentBitCoin.bpi.Dollar.code}  {currentBitCoin.bpi.Dollar.rate_float}");
            //Console.WriteLine($"Current rate: {currentBitCoin.bpi.GBP.code}  {currentBitCoin.bpi.GBP.rate_float}");
            // Console.WriteLine($"Current rate: {currentBitCoin.bpi.Dollar.code}  {currentBitCoin.bpi.Dollar.rate_float}");
            Console.WriteLine("Calculate in EUR/USD/GBP");
            string userChoice = Console.ReadLine();
            Console.WriteLine("Enter the amount of bitcoins");
            float userCoins = float.Parse(Console.ReadLine());
            float currentRate = 0;

            if (userChoice == "EUR")
            {
                currentRate = currentBitCoin.bpi.EUR.rate_float;
            }

            float result = currentRate * userCoins;
            Console.WriteLine($"your bitcoins are worth {userChoice} {result}");
        }

        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest reguest = (HttpWebRequest)WebRequest.Create(url);
            reguest.Method = "GET";

            var webResponse = reguest.GetResponse();
            var WebStream = webResponse.GetResponseStream();

            BitcoinRate bitcoinData;

            using (var responseReader = new StreamReader(WebStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }
            return bitcoinData;
        }
    }
}