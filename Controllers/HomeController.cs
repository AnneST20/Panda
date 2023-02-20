using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Panda.Helpers.SitemapHeplers;
using Panda.Helpers.HtmlHelpers;
using System.Linq;

namespace Panda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISitemapHelper _sitemapHelper;

        public HomeController()
        {
            _sitemapHelper = new DefaultSitemapHelper();
        }

        public async Task<ActionResult> Index()
        {
            var sitemapUrls = new List<string>
        {
            "https://rieltor.ua/sitemap/offers.xml"
        };

            var adsUrlList = new List<string>();

            foreach (var sitemapUrl in sitemapUrls)
            {
                var xSitemap = await _sitemapHelper.Read(sitemapUrl);
                var adUrls = await _sitemapHelper.Parse(xSitemap, "https://rieltor.ua/flats-rent/view/");
                adsUrlList.AddRange(adUrls);
            }

            // Pass the list of ad URLs to the view
            //return View(adsUrlList);
            var htmlDownload = new HtmlDownloadHelper(new System.Net.Http.HttpClient());

            var htmlParser = new HtmlParseHelper(htmlDownload);
            var ad = await htmlParser.GetAd(adsUrlList.First());

            return View(ad);
        }
    }

}