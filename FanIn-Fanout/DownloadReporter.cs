using System;
using System.Collections.Generic;
using System.Text;

namespace FanIn_Fanout
{
    public static class DownloadReporter
    {
        internal static void PrintResults(List<WebsiteDataModel> results)
        {
            Console.WriteLine();
            foreach (var item in results)
            {
                //resultsWindow.Text += $"{ item.WebsiteUrl } downloaded: { item.WebsiteData.Length } characters long.{ Environment.NewLine }";
                Console.WriteLine($"{ item.WebsiteUrl } downloaded: { item.WebsiteData.Length } characters long.{ Environment.NewLine }");
            }
        }
        internal static void RepostWesSiteInfo(WebsiteDataModel dataModel)
        {
            Console.WriteLine($"{ dataModel.WebsiteUrl } downloaded: { dataModel.WebsiteData.Length } characters long.{ Environment.NewLine }");
        }
    }
}
