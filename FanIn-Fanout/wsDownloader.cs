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
        internal static WebsiteDataModel WsDownloadSycn(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }
        internal static async Task<WebsiteDataModel> WsDownloadAsycn(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);

            return output;
        }
        internal static async Task<WebsiteDataModel> WsDownloadPrallelAsycn(string websiteURL)
        {
            System.Diagnostics.Debug.WriteLine(websiteURL);
            Console.WriteLine($"[{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}] " +
                $"Download started for {websiteURL}");
                //$"Download started for {websiteURL.Substring(0,websiteURL.IndexOf('/',8))}");
            
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);
            //Console.WriteLine($"..completed[{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}].");
            return output;
        }

        #endregion
        public static List<WebsiteDataModel> RunWebSiteDownloadSync()
        {
            List<string> websites = GetURLs();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            Console.WriteLine("download is in progress for site.. ");
            foreach (string site in websites)
            {
                Console.Write($"[{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}] Download started for {site}");
                WebsiteDataModel results = WsDownloadSycn(site);
                Console.WriteLine($"..completed [{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}].");
                output.Add(results);
            }
            return output;
        }
        public static async Task<List<WebsiteDataModel>> RunWebSiteDownloadAsync()
        {
            List<string> websites = GetURLs();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            Console.WriteLine("download is in progress for site.. ");
            foreach (string site in websites)
            {
                Console.Write($"[{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}]Download started for {site}");
                // WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                WebsiteDataModel results = await WsDownloadAsycn(site);
                 Console.WriteLine($"..completed[{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}].");
                output.Add(results);
            }
            return output;
        }
        //**FanIn Fanout Asynchronous Pattern**//
        public static async Task<List<WebsiteDataModel>> RunWebSiteDownloadFanInOutAsync()
        {
            List<string> websites = GetURLs();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            List<Task<WebsiteDataModel>> dowloadTasks = new  List<Task<WebsiteDataModel>>();
            Console.WriteLine("download is in progress for site.. ");
            foreach (string site in websites)
            {
                Console.WriteLine($"[{DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}]Download initiated for {site}");
                dowloadTasks.Add(WsDownloadPrallelAsycn(site));
            }
            var result = await Task.WhenAll(dowloadTasks);
            foreach (var item in result)
            {
                output.Add(item);
            }
            return output;
        }
    }
}
