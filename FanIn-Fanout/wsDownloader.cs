using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static FanIn_Fanout.Program;

namespace FanIn_Fanout
{
    public static class wsDownloader
    {
        #region internal method's
        internal static string GetName(runOptions value)
        {
            return Enum.GetName(typeof(runOptions), value);
        }
        internal static List<string> GetURLs()
        {
            var urlCollection = new List<string>();

            urlCollection.Add("https://www.bbc.co.uk");
            urlCollection.Add("https://www.yahoo.com");
            urlCollection.Add("https://www.ebay.com/");
            urlCollection.Add("https://www.google.com");
            urlCollection.Add("https://www.snapdeal.com/");
            urlCollection.Add("https://www.microsoft.com/en-au/cloud-platform/biztalk");
            urlCollection.Add("https://www.flipkart.com/");
            urlCollection.Add("https://www.amazon.com");
            urlCollection.Add("https://www.bseindia.com/");
            urlCollection.Add("https://www.facebook.com");
            urlCollection.Add("https://www.nseindia.com/");
            urlCollection.Add("https://azure.microsoft.com/en-us/");
            urlCollection.Add("https://www.planet-source-code.com/vb/scripts/BrowseCategoryOrSearchResults.asp?lngWId=10&blnAuthorSearch=TRUE&lngAuthorId=77425636&strAuthorName=Deepak%20Kumar%20Shaw%20(from%20psc%20cd)&txtMaxNumberOfEntriesPerPage=25");
            urlCollection.Add("https://www.planet-source-code.com/vb/scripts/BrowseCategoryOrSearchResults.asp?lngWId=1&blnAuthorSearch=TRUE&lngAuthorId=77425636&strAuthorName=Deepak%20Kumar%20Shaw%20(from%20psc%20cd)&txtMaxNumberOfEntriesPerPage=25");
            urlCollection.Add("https://www.stackoverflow.com");

            return urlCollection;
        }
        internal static WebsiteDataModel DownloadWebsite(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }

        #endregion
        public static List<WebsiteDataModel> RunDownloadSync()
        {
            List<string> websites = GetURLs();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            Console.WriteLine("download is in progress for site.. ");
            foreach (string site in websites)
            {
                Console.Write($"Download started for {site}");
                WebsiteDataModel results = DownloadWebsite(site);
                Console.WriteLine($"..completed.");
                output.Add(results);
            }
            return output;
        }
        //public static List<WebsiteDataModel> RunDownloadAsync()
        public static async Task<List<WebsiteDataModel>> RunDownloadAsync()
        {
            List<string> websites = GetURLs();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            Console.WriteLine("download is in progress for site.. ");
            foreach (string site in websites)
            {
                Console.Write($"Download started for {site}");
                WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                Console.WriteLine($"..completed.");
                output.Add(results);
            }
            return output;
            //return "Ok";
        }
    }
}
