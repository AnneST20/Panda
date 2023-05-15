using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Panda.Services.SitemapHeplers
{
    public class DefaultXmlHelper : DefaultSitemapHelper
    {
        public override async Task<List<string>> Parse(XDocument xDoc, string urlFilter)
        {
            // Define the namespace used in the XML file
            XNamespace ns = "http://example.com/xmlns";

            // Select all the <ad> elements in the XML file
            var adElements = xDoc.Descendants(ns + "ad");

            // Extract the URL from each <ad> element
            var urlList = adElements
                .Select(ad => ad.Attribute("url").Value)
                .Where(url => url.Contains(urlFilter)); // Filter by URLs for rented flats

            return urlList.ToList();
        }

    }
}