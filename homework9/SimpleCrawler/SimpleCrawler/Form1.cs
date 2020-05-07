using System;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SimpleCrawler
{
    public partial class Form1 : Form
    {
        BindingSource urlBindingSource = new BindingSource();
        Crawler crawler = new Crawler();
        Thread thread = null;

        public Form1()
        {
            InitializeComponent();
            dgvURL.DataSource = urlBindingSource;
            crawler.PageDownloaded += Crawler_PageDownloaded;
        }

        private void Crawler_PageDownloaded(Crawler crawler, string url, string info)
        {
            var pageInfo = new { Index = urlBindingSource.Count + 1, URL = url, Status = info };
            Action action = () => { urlBindingSource.Add(pageInfo); };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            urlBindingSource.Clear();
            crawler.StartURL = txtUrl.Text;

            Match match = Regex.Match(crawler.StartURL, Crawler.urlParseRegex);
            if (match.Length == 0) return;
            string host = match.Groups["host"].Value;
            crawler.HostFilter = "^" + host + "$";
            crawler.FileFilter = ".html?$";

            if (thread != null) { thread.Abort(); }
            thread = new Thread(crawler.Start);
            thread.Start();
        }
    }
}
