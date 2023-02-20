using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Panda.Helpers.HtmlHelpers
{
    public class HtmlDownloadHelper
    {
        private readonly HttpClient _httpClient;

        public HtmlDownloadHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetHtml(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var htmlContent = await response.Content.ReadAsStringAsync();
            return htmlContent;
        }

        public async Task<List<string>> GetHtmlFromUrls(List<string> urls)
        {
            var htmlContents = new List<string>();
            var tasks = urls.Select(async url =>
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var html = await response.Content.ReadAsStringAsync();
                        htmlContents.Add(html);
                    }
                    else
                    {
                        // handle the error response here
                    }
                }
            });

            await Task.WhenAll(tasks);
            return htmlContents;
        }

    }
}