using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                Console.WriteLine("You can find it in your browser by navigating to: '{0}'.", baseAddress);
                Console.WriteLine("To stop the app, enter 'exit'.");
                Console.WriteLine("Enjoy.");

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
