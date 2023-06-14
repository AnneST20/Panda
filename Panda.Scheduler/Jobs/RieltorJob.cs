using Panda.Models;
using Panda.Repositories;
using Panda.Services.HtmlHelpers;
using Panda.Services.SitemapHeplers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

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
            foreach (var url in adUrls)
            {
                if (!this._context.Ads.Any(a => a.Url == url))
                {
                    adsUrlsToAdd.Add(url);
                }
            }

            HtmlDownloadHelper downloader = new HtmlDownloadHelper(new System.Net.Http.HttpClient());
            HtmlParseHelperRieltor rieltor = new HtmlParseHelperRieltor();

            var ads = new List<Ad>();

            for (int i = _context.Ads.Count() - 1; i >= 0; i--) 
            {
                _context.Ads.Remove(_context.Ads.ToList()[i]);
            }

            for (int i = _context.Gallery.Count() - 1; i >= 0; i--)
            {
                _context.Gallery.Remove(_context.Gallery.ToList()[i]);
            }
            await _context.SaveChangesAsync();
            var tasks = adsUrlsToAdd.Select(async (adUrl) =>
            {
                try
                {
                    var html = await downloader.GetHtml(adUrl);
                    var ad = await rieltor.GetAd(html);
                    if (!String.IsNullOrEmpty(ad.Id))
                    {

                        ad.Id = AdsRepository.GetNewId();
                        ads.Add(ad);
                        lock (this._context.Ads)
                        {
                            this._context.Ads.Add(ad);
                            foreach (var photo in ad.Gallery)
                            {
                                this._context.Gallery.Add(photo);
                            }
                            //try
                            //{
                            //    if (ad.Id != null)
                            //    {
                            //        this._context.SaveChangesAsync();
                            //    }
                            //}
                            //catch (DbEntityValidationException ex)
                            //{
                            //    foreach (var error in ex.EntityValidationErrors)
                            //    {
                            //        foreach (var vError in error.ValidationErrors)
                            //        {
                            //            Console.WriteLine("Property: " + vError.PropertyName + " Error: " + vError.ErrorMessage);
                            //        }
                            //    }
                            //}
                            //catch (System.Data.Entity.Infrastructure.DbUpdateException ex1)
                            //{
                            //    Console.WriteLine(ex1.Message);
                            //    Console.WriteLine(ex1.InnerException.InnerException.Message);
                            //}
                        }
                    }
                }
                catch { }
            });

            await Task.WhenAll(tasks);

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var vError in error.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + vError.PropertyName + " Error: " + vError.ErrorMessage);
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex1)
            {
                Console.WriteLine(ex1.Message);
                Console.WriteLine(ex1.InnerException.InnerException.Message);
            }


        }
    }
}
