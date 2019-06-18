using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FanIn_Fanout
{
    class Program
    {

        public enum runOptions { async, sync, parallel, quit }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to FanIn Fanout Asynchronous programming demo!");

            StringBuilder sbInputMessage = new StringBuilder("Please selection the opertion from the list:");
            foreach (var item in Enum.GetNames(typeof(runOptions)))
                sbInputMessage.Append(item + ", ");

            var inputMessage = sbInputMessage.ToString().TrimEnd(new char[] { ' ', ',' });

            while (true)
            {
                Console.WriteLine(inputMessage);
                var userInput = Console.ReadLine();
                runOptions syncType;
                if (Enum.TryParse(userInput, true, out syncType))
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    switch (syncType)
                    {
                        case runOptions.sync:
                            Console.WriteLine("Synchronous Testing about to begin");
                            var results = wsDownloader.RunDownloadSync();
                            DownloadReporter.PrintResults(results);
                            break;
                        case runOptions.async:
                            Console.WriteLine("Asynchronous Testing about to begin");
                            WebResultAsync();
                            break;
                        case runOptions.parallel:
                            Console.WriteLine("Prallel Testing 'FanIn_Fanout' about to begin");
                            break;
                        case runOptions.quit:
                            return;
                    }
                    watch.Stop();
                    //var elapsedMs = watch.ElapsedMilliseconds;
                    TimeSpan ts = watch.Elapsed;
                    Console.Write("Total execution time:");
                    Console.ForegroundColor =  ConsoleColor.Blue; Console.WriteLine(ts.ToString(@"hh\:mm\:ss\.ff")); Console.ResetColor();
                }
                else
                    Console.WriteLine("Sorry, Invalide arrgument to perform the test");
            }
        }

        private static async void WebResultAsync()
        {
            var results = await wsDownloader.RunDownloadAsync();
            DownloadReporter.PrintResults(results);
        }

    }
}
