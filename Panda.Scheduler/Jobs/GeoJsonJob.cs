using Newtonsoft.Json;
using Panda.Library.Models.GeoJsonModels;
using Panda.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Jobs
{
    public class GeoJsonJob : IJob
    {
        private ApplicationDbContext _context;
        public GeoJsonJob()
        {
            this._context = new ApplicationDbContext();
        }
        public async Task Execute(IJobExecutionContext context)
        {

            var geoJson = new GeoJsonModel { 
                type = "FeatureCollection", 
                features = new List<Feature>() 
            };

            foreach (var ad in _context.Ads.ToList())
            {
                try
                {
                    var geometry = new Geometry
                    {
                        type = "Point",
                        coordinates = new List<double>()
                    };

                    var properties = new Properties
                    {
                        adress = ad.Adress,
                        description = ad.Description,
                        price = ad.Price,
                        url = ad.Url,
                        markerSybbol = "BallStone"
                    };
                    var feature = new Feature
                    {
                        geometry = geometry,
                        properties = properties,
                        type = "Feature"
                    };

                    if (ad.Coordinates != null)
                    {
                        var x = ad.Coordinates.Substring(1, ad.Coordinates.IndexOf(",") - 1);
                        var y = ad.Coordinates.Substring(ad.Coordinates.IndexOf(",") + 1, ad.Coordinates.Length - ad.Coordinates.IndexOf(",") - 2);
                        geometry.coordinates.Add(double.Parse(x.Replace(".", ",")));
                        geometry.coordinates.Add(double.Parse(y.Replace(".", ",")));
                        geoJson.features.Add(feature);
                    }
                }
                catch (Exception ex) { }
            }

            var json = JsonConvert.SerializeObject(geoJson);
            System.IO.File.WriteAllText("C:\\Users\\anna.stetsenko\\Documents\\Anna\\Panda\\Panda.Library\\GeoJSON\\ads.geojson", json);
            await Task.CompletedTask;
        }
    }
}
