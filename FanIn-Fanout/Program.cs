using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FanIn_Fanout
{
    class Program
    {

        public enum runOptions { async, sync, parallel, quit, exit }

        /** Asynchronous 'Main' only available c# 7.1 and above
        Change to C# 7.1 in Advanced Build option from Project properties **/
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("▄▀▄▀█▓▒░W░E░L░C░O░M░E░▒▓█▀▄▀▄"); Console.ResetColor();
            Console.WriteLine("To FanIn Fanout Asynchronous programming demo!");

            StringBuilder sbInputMessage = new StringBuilder("Please selection the operation from the list:");
            foreach (var item in Enum.GetNames(typeof(runOptions)))
                sbInputMessage.Append(item + ", ");
            try
            {
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
                                var results = wsDownloader.RunWebSiteDownloadSync();
                                DownloadReporter.PrintResults(results);
                                break;
                            case runOptions.async:
                                Console.WriteLine("Asynchronous Testing about to begin");
                                var resultsAsync = await wsDownloader.RunWebSiteDownloadAsync();
                                DownloadReporter.PrintResults(resultsAsync);
                                break;
                            case runOptions.parallel:
                                Console.WriteLine("Parallel Testing 'FanIn_Fanout' about to begin");
                                var resultsParall = await wsDownloader.RunWebSiteDownloadFanInOutAsync();
                                DownloadReporter.PrintResults(resultsParall);
                                break;
                            case runOptions.quit:
                            case runOptions.exit:
                                return;
                        }
                        watch.Stop();
                        //var elapsedMs = watch.ElapsedMilliseconds;
                        TimeSpan ts = watch.Elapsed;
                        Console.Write("Total execution time:");
                        Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(ts.ToString(@"hh\:mm\:ss\.ff")); Console.ResetColor();
                    }
                    else
                        Console.WriteLine("Sorry, Invalid argument to perform the test");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex}");
                Console.ReadKey();
            }
        }
    }
}

