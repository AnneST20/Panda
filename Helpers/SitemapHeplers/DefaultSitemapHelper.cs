using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Panda.Helpers.SitemapHeplers
{
    public class DefaultSitemapHelper : ISitemapHelper
    {

        public virtual async Task<XDocument> Read(string sitemapUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var sitemap = await httpClient.GetStringAsync(sitemapUrl);

                var doc = XDocument.Parse(sitemap);

                return doc;
            }
        }

        public virtual Task<List<string>> Parse(XDocument xSitemap, string urlFilter)
        {
            // Define the namespace used in the sitemap
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            // Select all the <url> elements in the sitemap
            var urlElements = xSitemap.Descendants(ns + "url");

            // Extract the <loc> element for each <url> element
            var urlList = urlElements
                .Select(url => url.Element(ns + "loc").Value)
                .Where(url => url.StartsWith(urlFilter)); // Filter by URLs for rented flats

            return Task.FromResult(urlList.ToList());
        }

    }
}