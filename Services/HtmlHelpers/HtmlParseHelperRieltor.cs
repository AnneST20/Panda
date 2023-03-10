using HtmlAgilityPack;
using Panda.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Services.HtmlHelpers
{
    public class HtmlParseHelperRieltor
    {
        private readonly string sourceKey = "rieltor";

        public async Task<List<Ad>> GetAdsFromUrls(List<string> htmlContents)
        {
            var bagOfAds = new ConcurrentBag<Ad>();

            await Task.WhenAll(htmlContents.Select(async htmlContent =>
            {
                var ad = await GetAd(htmlContent);
                bagOfAds.Add(ad);
            }));

            return bagOfAds.ToList();
        }

        public async Task<Ad> GetAd(string htmlContent)
        {
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

            ad.AdressLink = document.DocumentNode.Descendants("a")
                .Where(x => x.ParentNode.GetAttributeValue("class", "") == "offer-view-address")
                .Where(y => y.GetAttributeValue("class", "") == "").FirstOrDefault()?.InnerText.Trim();

            var square = document.DocumentNode.Descendants("span")
                .Where(x => x.ParentNode.GetAttributeValue("class", "") == "offer-view-details-row")
                .Where(y => y.InnerText.Contains("м²")).Select(z => z.InnerText).FirstOrDefault().Trim();
            ad.Square = square.Substring(0, square.IndexOf("/") - 1);

            var rooms = document.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("href", "").Contains("-rooms/")).FirstOrDefault()?.InnerText.Trim();
            ad.Rooms = rooms.Substring(0, rooms.IndexOf(" ")) + "к";

            ad.Floor = document.DocumentNode.Descendants("span")
                .Where(x => x.ParentNode.GetAttributeValue("class", "") == "offer-view-details-row")
                .Where(y => y.InnerText.Contains("поверх")).Select(z => z.InnerText).FirstOrDefault().Trim();

            ad.Description = document.DocumentNode.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "") == "offer-view-section-text").FirstOrDefault()?.InnerText.Trim(); 
            var gallery = document.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("class", "") == "offer-view-images-cell")
                .Select(y => y.GetAttributeValue("href", "")).ToList();

            ad.Gallery = new List<Photo>();
            foreach (var photo in gallery)
            {
                ad.Gallery.Add(new Photo { AdId = ad.Id, Url = photo });
            }

            ad.SourceKey = sourceKey;

            return ad;
        }

    }
}
