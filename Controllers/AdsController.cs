using Panda.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Panda.Repositories;
using Panda.Services.SitemapHeplers;
using Newtonsoft.Json;
using Panda.Library.Models.GeoJsonModels;
using System;

namespace Panda.Controllers
{
    public class AdsController : Controller
    {
        private readonly ISitemapHelper _sitemapHelper;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AdsController()
        {
            _sitemapHelper = new DefaultSitemapHelper();
        }

        // GET: Ads
        public async Task<ActionResult> Index()
        {
            return View(await db.Ads.ToListAsync());
        }


        // GET: Ads/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = await db.Ads.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // GET: Ads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SourceKey,Url,Price,Adress,AdressLink,Square,Rooms,Floor,Description,PetsAllowed,Gallery")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                if (ad.Id == null)
                {
                    ad.Id = AdsRepository.GetNewId();
                }
                db.Ads.Add(ad);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        // GET: Ads/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = await db.Ads.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SourceKey,Url,Price,Adress,AdressLink,Square,Rooms,Floor,Description,PetsAllowed,Gallery")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = await db.Ads.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Ad ad = await db.Ads.FindAsync(id);
            List<LikedAd> likes = await db.LikedAds.Where(l => l.AdId == ad.Id).ToListAsync();
            List<Photo> gallery = await db.Gallery.Where(ph => ph.AdId == ad.Id).ToListAsync();

            if (likes.Any())
            {
                foreach(var like in likes)
                {
                    db.LikedAds.Remove(like);
                }
            }
            if (gallery.Any())
            {
                foreach (var photo in gallery)
                {
                    db.Gallery.Remove(photo);
                }
            }
            db.Ads.Remove(ad);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
