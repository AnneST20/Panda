using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Panda.Helpers.SitemapHeplers
{
    public interface ISitemapHelper
    {
        Task<XDocument> Read(string sitemapUrl);

        Task<List<string>> Parse(XDocument xSitemap, string urlFilter);
        
    }
}
