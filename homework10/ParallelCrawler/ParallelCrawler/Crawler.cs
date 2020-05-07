using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ParallelCrawler
{
    class Crawler
    {
        public event Action<Crawler,int, string, string> PageDownloaded;

        public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
        public string HostFilter { get; set; } 
        public string FileFilter { get; set; } 
        public string StartURL { get; set; } 
        private Hashtable urls = new Hashtable(); 
        private ConcurrentQueue<string> pending = new ConcurrentQueue<string>();


        public void Start()
        {
            urls.Clear();
            pending = new ConcurrentQueue<string>();
            pending.Enqueue(StartURL);
            List<Task> tasks = new List<Task>();
            int count = -1;
            PageDownloaded += (crawler, index, url, info) => { count++; };
            while (tasks.Count<100)
            {
                if(!pending.TryDequeue(out string current))
                {
                    if (count >= tasks.Count) break; else continue;
                }
                int index = tasks.Count;
                Task task = Task.Run(() => DownLoad(current, index));
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
        }

        public void DownLoad(string url,int index)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                urls[url] = true;
                PageDownloaded(this, index, url, "Success");
                Parse(html, url);
            }
            catch (Exception ex)
            {
                PageDownloaded(this, index, url, "Error:" + ex.Message);
            }
        }

        private void Parse(string html,string pageUrl)
        {
            var strRef = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;
                if (linkUrl == null || linkUrl == "") continue;
                linkUrl = TranslateURL(linkUrl, pageUrl);

                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;
                if (file == "") file = "index.html";

                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)&&!urls.ContainsKey(linkUrl))
                {
                    pending.Enqueue(linkUrl);
                    urls.Add(linkUrl, false);
                }
            }
        }

        private string TranslateURL(string url, string baseUrl)
        {
            if (url.Contains("://")) { return url; }

            if (url.StartsWith("//")) { return "http:" + url; }

            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(baseUrl, urlParseRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int idx = baseUrl.LastIndexOf('/');
                return TranslateURL(url, baseUrl.Substring(0, idx));
            }

            if (url.StartsWith("./"))
            {
                return TranslateURL(url.Substring(2), baseUrl);
            }
            return baseUrl.Substring(0, baseUrl.LastIndexOf("/")) + "/" + url;
        }
    }
}
