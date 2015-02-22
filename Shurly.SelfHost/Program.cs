using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Shurly.SelfHost
{
    class Program
    {
        static void Main()
        {
            const string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                Console.WriteLine("Welcome to self hosted short url repository.");
                Console.WriteLine();
                Console.WriteLine("   You can find it in your browser by navigating to:");
                Console.WriteLine(" > {0}", baseAddress);
                Console.WriteLine();
                Console.WriteLine("   If you need some assistance, help page is located at:");
                Console.WriteLine(" > {0}help", baseAddress);
                Console.WriteLine();
                Console.WriteLine("   To stop the client, enter 'exit'.");
                Console.WriteLine();
                Console.WriteLine("   Enjoy.");
                Console.WriteLine();

                string input;
                do
                {
                    input = Console.ReadLine();
                } 
                while (input != null && input.ToLower() != "exit");
            }
        }
    }
}
