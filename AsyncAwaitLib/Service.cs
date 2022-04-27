using AsyncAwaitLib.Models;
using System.Net;

namespace AsyncAwaitLib
{
    public class Service : IService
    {
        public string executeSync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            RunDownloadSync();

            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;

            return "Total execution time: " + elapsedTime;

        }

        public async Task<string> executeAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await RunDownloadParallelASync();

            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;

            return "Total Async Execution Time: " + elapsedTime.ToString();
        }

        private void RunDownloadSync()
        {
            List<string> websites = TestData();

            foreach (string site in websites)
            {
                WebsiteModel model = DownloadWebsite(site);

                ReportWebsiteModel(model);
            }
        }

        private async Task RunDownloadASync()
        {
            List<string> websites = TestData();

            foreach (string site in websites)
            {
                WebsiteModel model = await Task.Run(() => DownloadWebsite(site)); //await because we need the models before reporting on them

                ReportWebsiteModel(model);
            }
        }

        private async Task RunDownloadParallelASync()
        {
            List<string> websites = TestData();

            List<Task<WebsiteModel>> tasks = new List<Task<WebsiteModel>>();
            foreach (string site in websites)
            {
                tasks.Add(Task.Run(() => DownloadWebsite(site)));
            }

            var results = await Task.WhenAll(tasks); // when all tasks are done, pass all tasks into results var

            foreach (var item in results)
            {
                ReportWebsiteModel(item);
            }
        }

        private List<string> TestData()
        {
            List<string> data = new List<string>();

            data.Add("http://www.google.com");
            data.Add("http://www.yahoo.com");
            data.Add("http://www.microsoft.com");
            data.Add("http://www.stackoverflow.com");

            return data;
        }

        private WebsiteModel DownloadWebsite(string website)
        {
            WebsiteModel model = new WebsiteModel();
            WebClient client = new WebClient();

            model.WebsiteUrl = website;
            model.WebsiteData = client.DownloadString(website);

            return model;
        }

        private void ReportWebsiteModel(WebsiteModel model)
        {
            Console.WriteLine(model.WebsiteUrl + " downloaded: " + model.WebsiteData.Length + " characters long.");
        }



    }
}