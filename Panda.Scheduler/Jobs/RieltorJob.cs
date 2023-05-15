using Panda.Models;
using Panda.Services.HtmlHelpers;
using Panda.Services.SitemapHeplers;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Panda.Jobs
{
    public class RieltorJob : IJob
    {

        private ApplicationDbContext _context;
        private readonly ISitemapHelper _sitemapHelper;

        public RieltorJob()
        {
            this._context = new ApplicationDbContext();
            this._sitemapHelper = new DefaultSitemapHelper();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            // Read ads url from rieltor sitemap
            var sitemapUrl = "https://rieltor.ua/sitemap/offers.xml";
            var xSitemap = await _sitemapHelper.Read(sitemapUrl);
            var adUrls = await _sitemapHelper.Parse(xSitemap, "https://rieltor.ua/flats-rent/view/");

            // Remove ads from context that are not exist anymore
            var adsToRemove = new List<Ad>();
            foreach (var ad in this._context.Ads)
            {
                if (!adUrls.Contains(ad.Url))
                {
                    adsToRemove.Add(ad);
                }
            }
            foreach (var ad in adsToRemove)
            {
                this._context.Ads.Remove(ad);
            }

            //Remove urls that already exist in context
            var adsUrlsToAdd = new List<string>();
            foreach(var url in adUrls)
            {
                if (!this._context.Ads.Any(a => a.Url == url))
                {
                    adsUrlsToAdd.Add(url);
                }
            }

            HtmlDownloadHelper downloader = new HtmlDownloadHelper(new System.Net.Http.HttpClient());
            HtmlParseHelperRieltor rieltor = new HtmlParseHelperRieltor();

            var tasks = adsUrlsToAdd.Select(async (adUrl) =>
            {
                try
                {
                    var html = await downloader.GetHtml(adUrl);
                    var ad = await rieltor.GetAd(html);
                    lock (this._context.Ads)
                    {
                        this._context.Ads.Add(ad);
                    }
                }
                catch { }
            });

            await Task.WhenAll(tasks);

            await this._context.SaveChangesAsync();

        }
    }
}
