using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;

namespace SimpleCrawler
{
    class Crawler
    {
        public event Action<Crawler, string, string> PageDownloaded;

        public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
        public string HostFilter { get; set; } 
        public string FileFilter { get; set; } 
        public string StartURL { get; set; } 
        private Hashtable urls = new Hashtable();
        private int count = 0;

        public void Start()
        {
            urls.Clear();
            urls.Add(StartURL, false);
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }
                if (current == null || count > 100) break;
                try
                {
                    string html = DownLoad(current);
                    urls[current] = true;
                    PageDownloaded(this, current, "Susccess");
                    count++;
                    Parse(html, current);
                }
                catch(Exception ex)
                {
                    PageDownloaded(this, current, "Error:"+ex.Message);
                }
            }
        }

        public string DownLoad(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string html = webClient.DownloadString(url);
            return html;
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
