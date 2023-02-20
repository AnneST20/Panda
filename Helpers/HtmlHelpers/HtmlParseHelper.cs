using HtmlAgilityPack;
using Panda.Helpers.HtmlHelpers;
using Panda.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Helpers.HtmlHelpers
{
    public class HtmlParseHelper
    {
        private readonly HtmlDownloadHelper _downloadHelper;

        public HtmlParseHelper(HtmlDownloadHelper downloadHelper)
        {
            _downloadHelper = downloadHelper;
        }

        public async Task<List<Ad>> GetAdsFromUrls(List<string> urls)
        {
            var bagOfAds = new ConcurrentBag<Ad>();

            await Task.WhenAll(urls.Select(async url =>
            {
                var ad = await GetAd(url);
                bagOfAds.Add(ad);
            }));

            return bagOfAds.ToList();
        }

        public async Task<Ad> GetAd(string url)
        {
            var htmlContent = await _downloadHelper.GetHtml(url);
            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            var ad = new Ad();

            ad.Url = document.DocumentNode.Descendants("link")
                .Where(x => x.GetAttributeValue("rel", "") == "canonical").FirstOrDefault()?.GetAttributeValue("href", "");

            ad.Id = document.DocumentNode.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "") == "object-id").FirstOrDefault()?.InnerText.Trim();

            ad.Price = document.DocumentNode.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "") == "offer-view-price-title").FirstOrDefault()?.InnerText.Trim();

            ad.Adress = document.DocumentNode.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "") == "offer-view-address").FirstOrDefault()?.InnerText.Trim();

            ad.Rooms = document.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("href", "").Contains("-rooms/")).FirstOrDefault()?.InnerText.Trim();

            ad.Floor = document.DocumentNode.Descendants("span")
                .Where(x => x.ParentNode.GetAttributeValue("class", "") == "offer-view-details-row")
                .Where(y => y.InnerText.Contains("поверх")).Select(z => z.InnerText).FirstOrDefault().Trim().Replace("поверх ", "");

            //ad.Description = document.DocumentNode.Descendants("div")
            //    .Where(x => x.GetAttributeValue("class", "") == "offer-view-section-text").FirstOrDefault()?.InnerText.Trim();

            //ad.Contact = document.DocumentNode.Descendants("div")
            //    .Where(x => x.GetAttributeValue("class", "") == "object-contact").FirstOrDefault()?.InnerText.Trim();

            ad.Gallery = document.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("class", "") == "offer-view-images-cell")
                .Select(y => y.GetAttributeValue("href", "")).ToList();

            return ad;
        }

    }
}
