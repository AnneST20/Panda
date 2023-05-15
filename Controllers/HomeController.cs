using Panda.Models;
using Panda.Services.SitemapHeplers;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Panda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISitemapHelper _sitemapHelper;
        private ApplicationDbContext db = new ApplicationDbContext();

        public HomeController()
        {
            _sitemapHelper = new DefaultSitemapHelper();
        }

        public async Task<ActionResult> Index()
        {
            var ads = await db.Ads.ToListAsync();

            return View(ads);
        }
    }

}